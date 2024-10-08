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
	}
}
