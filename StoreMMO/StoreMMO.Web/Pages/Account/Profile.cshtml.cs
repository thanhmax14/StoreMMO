using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using System.Threading.Tasks;

namespace StoreMMO.Web.Pages.Account
{
	public class ProfileModel : PageModel
	{
		private readonly UserManager<AppUser> _userManager;

		public ProfileModel(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}
		[BindProperty]
		public AppUser AppUser { get; set; }
		public async Task OnGet()
		{
			var email = HttpContext.Session.GetString("Email");
			var UserName = HttpContext.Session.GetString("UserName");
			AppUser = await this._userManager.FindByEmailAsync("ANHLDCE171348@FPT.EDU.VN");

			// Kiểm tra nếu người dùng tồn tại
			if (AppUser != null)
			{
				ViewData["IsSeller"] = AppUser.IsSeller; // Gán trạng thái IsSeller cho ViewData
			}
			else
			{
				ViewData["IsSeller"] = false; // Nếu không tìm thấy người dùng, gán false
			}
		}

		public async Task<IActionResult> OnPost()
		{
			if (!ModelState.IsValid)
			{
				// Trả về lỗi nếu dữ liệu không hợp lệ
				return new JsonResult(new { success = false, message = "Invalid data!" });
			}

			var existingUser = await _userManager.FindByEmailAsync("ANHLDCE171348@FPT.EDU.VN");

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
			// Tìm người dùng theo email
			var item = await _userManager.FindByEmailAsync("ANHLDCE171348@FPT.EDU.VN");

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

	}
}
