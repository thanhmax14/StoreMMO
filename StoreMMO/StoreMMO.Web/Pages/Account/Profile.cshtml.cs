using BusinessLogic.Services.StoreMMO.Core.Balances;
using BusinessLogic.Services.StoreMMO.Core.ComplaintsN;
using BusinessLogic.Services.StoreMMO.Core.OrderDetails;
using BusinessLogic.Services.StoreMMO.Core.Purchases;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels;
using StoreMMO.Web.Models.ViewModels.Admin;
using System.Threading.Tasks;

namespace StoreMMO.Web.Pages.Account
{
	public class ProfileModel : PageModel
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IBalanceService _balance;
		private readonly IPurchaseService _pur;
		private readonly IOderDetailsService _detail;
		private readonly IComplaintsService _complaints;
		private readonly AppDbContext _context;
		[BindProperty]
		public UserProfileViewModels UserProfile { get; set; }
		public ProfileModel(AppDbContext context, UserManager<AppUser> userManager, IBalanceService balance, IPurchaseService purchase,
			IOderDetailsService order, IComplaintsService complaints
			)
		{
			_context = context;
			_userManager = userManager;
			this._balance = balance;
			this._pur= purchase;
			this._detail = order;
			this._complaints = complaints;
		}
		[BindProperty]
		public AppUser AppUser { get; set; }
        public IEnumerable<BalanceViewModels> InfoBalance = new List<BalanceViewModels> ();
		public IEnumerable<GetOrderByUserViewModel> InfOrderUser = new List<GetOrderByUserViewModel>();
		public IEnumerable<GetOrderDetailsViewModel> InfoOrdeTailUser = new List<GetOrderDetailsViewModel>();
        [TempData]
        public string Balance { get; set; }

        public async Task OnGet()
		{
			var email = HttpContext.Session.GetString("Email");
			var UserID = HttpContext.Session.GetString("UserID");
			if (UserID != null)
			{
				await LoadUserDataAsync(UserID);
			}
			InfOrderUser = this._pur.GetAllByUserID(UserID);
		//	InfoOrdeTailUser = this._pur.getOrderDetails(UserID);
			InfoBalance = await this._balance.GetBalanceByUserIDAsync(UserID);

			AppUser = await this._userManager.FindByEmailAsync(email);

			if (AppUser != null)
			{
				Balance = AppUser.CurrentBalance +"";
				ViewData["IsSeller"] = AppUser.IsSeller; // Gán trạng thái IsSeller cho ViewData
			}
			else
			{
				ViewData["IsSeller"] = false; // Nếu không tìm thấy người dùng, gán false
			}
		}

		public async Task<IActionResult> OnPost()
		{
			var email = HttpContext.Session.GetString("Email");
			if (!ModelState.IsValid)
			{
				// Trả về lỗi nếu dữ liệu không hợp lệ
				return new JsonResult(new { success = false, message = "Invalid data!" });
			}

			var existingUser = await _userManager.FindByEmailAsync(email);

			if (existingUser != null)
			{
				// Cập nhật thông tin
				existingUser.UserName = AppUser.UserName;
				existingUser.FullName = AppUser.FullName;
				existingUser.DateOfBirth = AppUser.DateOfBirth;
				existingUser.PhoneNumber = AppUser.PhoneNumber;
				existingUser.Address = AppUser.Address;
				existingUser.ModifiedDateUpdateProfile = DateTime.UtcNow;
				var result = await _userManager.UpdateAsync(existingUser);

				if (result.Succeeded)
				{
					// Trả về JSON thành công
					return new JsonResult(new { success = true, message = "Profile updated successfully!" });
				}
				else
				{
					// Trả về lỗi từ Identity
					return new JsonResult(new { success = false, message = "Failed to update profile." });
				}
			}

			return new JsonResult(new { success = false, message = "User not found." });
		}
		public async Task<IActionResult> OnPostRegisterSeller()
		{
			var email = HttpContext.Session.GetString("Email");
			var item = await _userManager.FindByEmailAsync(email);

			// Nếu không tìm thấy người dùng
			if (item == null)
			{
				return new JsonResult(new { success = false, message = "No user found. Please register your account first." });
			}

			// Kiểm tra nếu các trường bắt buộc có đủ thông tin từ cơ sở dữ liệu
			if (string.IsNullOrWhiteSpace(item.FullName) ||
				!item.DateOfBirth.HasValue || // Kiểm tra xem DateOfBirth có giá trị không
				string.IsNullOrWhiteSpace(item.PhoneNumber) ||
				string.IsNullOrWhiteSpace(item.Address))
			{
				// Nếu thiếu thông tin, gửi phản hồi yêu cầu chuyển hướng đến tab chỉnh sửa
				return new JsonResult(new { success = false, requiresRedirect = true, message = "Please complete your profile in Account Details." });
			}

			// Cập nhật thông tin nếu người dùng đồng ý trở thành seller
			item.IsSeller = true; // Hoặc lấy giá trị từ form nếu cần
			item.RequestSellerDate = DateTime.UtcNow;
			var result = await _userManager.UpdateAsync(item);
			if (result.Succeeded)
			{
				// Phản hồi thành công, thông báo cần đợi admin duyệt
				return new JsonResult(new { success = true, message = "You registered as a seller successfully! Waiting for admin approval." });
			}
			else
			{
				return new JsonResult(new { success = false, message = "Failed to register seller." });
			}
		}
		public async Task<IActionResult> OnPostViewOrdetail(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return new JsonResult(new { success = false, message = "ID is null or empty." });
			}
			var orderDetailsFromDb =  this._pur.getOrderDetails(id);

			if (orderDetailsFromDb == null || !orderDetailsFromDb.Any())
			{
				return new JsonResult(new { success = false, message = "No order found with the specified ID." });
			}

			// Chuyển đổi dữ liệu từ cơ sở dữ liệu thành định dạng JSON
			var orderDetails = new
			{
				items = orderDetailsFromDb.Select(order => new
				{
					account = order.Account,           // Tên tài khoản
					password = order.Password,         // Mật khẩu (nên không trả về mật khẩu trong phản hồi thực tế)
					detailID = order.DetailID,
					price = order.Price,               // Giá sản phẩm
					totalPrice = order.Price,     // Tổng giá
					orderDate = order.Dates.ToString("yyyy-MM-dd"), // Ngày đặt hàng
					status = order.status               // Trạng thái đơn hàng
				}).ToArray()
			};

			return new JsonResult(new { success = true, items = orderDetails.items });
		}
		public async Task<IActionResult> OnPostSendReport(string detailID, string mess)
		{
			var findDetail =  await this._detail.GetOrderDeailByIDAsync(detailID);
			var a = findDetail;
			if (findDetail == null)
			{
				return new JsonResult(new { success = false, message="Fail" });
			}
			else
			{
				bool add = await this._complaints.AddAsync(new complantViewModels { 
				                      CreateDate = DateTime.Now,
									  Description = mess,
									  OrderDetailID = detailID,
									  ID = Guid.NewGuid().ToString(),
									  Status= "None"
				
				});
				 if (!add)
				{
					return new JsonResult(new { success = false, message = "Fail" });
				}
				 var getODetail = await this._detail.GetOrderDeailByIDAsync(detailID);
				     getODetail.status = "report";

				bool addDetail = await this._detail.UpdateDetailAsync(getODetail);
				if (!addDetail)
				{
					return new JsonResult(new { success = false, message = "Fail" });
				}
				return new JsonResult(new { success = true, message="thanh congs" });
			}			
		}
		public async Task LoadUserDataAsync(string userId)
		{
			UserProfile = new UserProfileViewModels();

			// Lấy thông tin người dùng
			var user = await _context.Users.FindAsync(userId);
			if (user != null)
			{
				UserProfile.Account = user.UserName;
				UserProfile.RegisteredDate = user.CreatedDate;
			}

			// Tính tổng sản phẩm đã mua dưới dạng double
			var productsPurchased = await _context.OrderDetails
				.Where(orderDetail => orderDetail.orderBuy.UserID == userId)
				.ToListAsync();

			UserProfile.ProductsPurchased = productsPurchased.Sum(od =>
			{
				double quantity = 0;
				double.TryParse(od.quantity, out quantity); // Chuyển đổi từ string sang double
				return quantity;
			});

			// Tính số lượng cửa hàng
			var stores = await _context.Stores
				.Where(store => store.UserId == userId)
				.ToListAsync();
			UserProfile.NumberOfStores = stores.Count;

			// Tính tổng sản phẩm đã bán dưới dạng double
			var productsSold = await _context.OrderDetails
				.Where(orderDetail => orderDetail.orderBuy.Store.UserId == userId)
				.ToListAsync();

			UserProfile.ProductsSold = productsSold.Sum(od =>
			{
				double quantity = 0;
				double.TryParse(od.quantity, out quantity); // Chuyển đổi từ string sang double
				return quantity;
			});
		}
	}
}
