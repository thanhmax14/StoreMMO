using BusinessLogic.Services.CreateQR;
using BusinessLogic.Services.Encrypt;
using BusinessLogic.Services.Payments;
using BusinessLogic.Services.StoreMMO.Core.Balances;
using BusinessLogic.Services.StoreMMO.Core.ComplaintsN;
using BusinessLogic.Services.StoreMMO.Core.OrderDetails;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.Purchases;
using CloudinaryDotNet;
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
        private readonly IProductService _product;
        //public IEnumerable<ManageStoreViewModels> products = new List<ManageStoreViewModels>();
        [BindProperty]
		public IEnumerable<ManageStoreViewModels> storeSeller { get; set; }
		public UserProfileViewModels UserProfile { get; set; }
		private readonly PaymentLIb _pay;
		private readonly CreateQR _createQR;
		private readonly IBalanceService _balanceService;

		public ProfileModel(IProductService product, AppDbContext context, UserManager<AppUser> userManager, IBalanceService balance, IPurchaseService purchase,
			IOderDetailsService order, IComplaintsService complaints, PaymentLIb paymentLIb, CreateQR create, IBalanceService balanceService)
		{
			_product = product;
			_context = context;
			_userManager = userManager;
			this._balance = balance;
			this._pur= purchase;
			this._detail = order;
			this._complaints = complaints;
			this._pay = paymentLIb;
			this._createQR = create;
			this._balanceService = balanceService;
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
			storeSeller = _product.ManageStore();
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

		public async Task<IActionResult> OnPostSendDepo(int amount)
		{
			var checkUser = HttpContext.Session.GetString("UserID");
			amount = 20000;
			if (checkUser != null)
			{
				var host = Request.Host.ToString();
				var fullUrl = $"{Request.Scheme}://{host}/Purchase/pedding";
				var failUrl = $"{Request.Scheme}://{host}/Purchase/fail";

				// Tạo yêu cầu thanh toán
				var create = await _pay.CreatePay("Deposit", 1, amount, fullUrl, failUrl, "Payment deposit", 10);
				if (create != null)
				{
					var transaction = new BalanceViewModels
					{
						Id = Guid.NewGuid().ToString(),
						Amount = amount,
						Description = "Deposit",
						OrderCode = create.orderCode.ToString(),
						Status = create.status,
						TransactionDate = DateTime.Now,
						TransactionType = "Deposit",
						UserId = checkUser
					};

					bool add = await _balanceService.AddAsync(transaction); // Gọi phương thức AddAsync
					if (add)
					{
						string redirectUrl = Url.Page("/Purchase/pedding", null, new
						{
							Ordercode = EncryptSupport.EncodeBase64(create.orderCode.ToString()),
							descrip = EncryptSupport.EncodeBase64(create.description),
							NameBank = EncryptSupport.EncodeBase64("PHAM QUANG THANH"),
							NumberBank = EncryptSupport.EncodeBase64(create.accountNumber),
							thoigian = create.expiredAt,
							amount = create.amount,
							Price = EncryptSupport.EncodeBase64("2000"),
							img = EncryptSupport.EncodeBase64(create.qrCode)
						}, Request.Scheme);
						return new JsonResult(new
						{
							success = true,
							 url = redirectUrl
						});
					}
					else
					{
						// Hủy yêu cầu thanh toán nếu không thêm được giao dịch
						bool cancel = await _pay.cancelPay(create.orderCode.ToString());
						return new JsonResult(new { success = false, redirectUrl = failUrl });
					}
				}
				else
				{
					return new JsonResult(new { success = false, message = "Failed to create payment request." });
				}
			}
			return new JsonResult(new { success = false, message = "User not found." });

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
