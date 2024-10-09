using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Web.Models.ViewModels;

namespace StoreMMO.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public LoginModel(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            this._emailSender = emailSender;
        }
        [BindProperty]
        public LoginViewModel  inputLogin { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }else
            {
                var user = await this._userManager.FindByEmailAsync(inputLogin.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty,"User not exit!");
                    return Page();
                }
                var checkpwd = await this._userManager.CheckPasswordAsync(user, inputLogin.Password);
                if (!checkpwd)
                {
                    await this._userManager.AccessFailedAsync(user);
                   var accesFailCount = await this._userManager.GetAccessFailedCountAsync(user);
                    if(await this._userManager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, $"Your account is locked due to too many failed attempts.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Sai mật khẩu! Bạn còn {5 - accesFailCount} lần thử.");
                    }
                    return Page();
                }
                await this._userManager.ResetAccessFailedCountAsync(user);
                var result = await _signInManager.PasswordSignInAsync(inputLogin.Email, inputLogin.Password,
                    inputLogin.RememberMe, lockoutOnFailure: true);
                HttpContext.Session.SetString("Email", inputLogin.Email);
                HttpContext.Session.SetString("UserName", user.UserName);
				if (result.IsNotAllowed)
                {
					ModelState.AddModelError(string.Empty, "You must verify email before login");
					return Page();
				}
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Your account is locked.");
                    return Page();
                }
                else if (result.Succeeded)
                {
                   
					return RedirectToPage("/Index");
				}
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect login information!!");
                    return Page();
                }

            }

        }
		public async Task<IActionResult> ConfirmEmail(string userId, string token)
		{
			Console.WriteLine($"UserId: {userId}, Token: {token}"); // In ra UserId và Token

			if (userId == null || token == null)
			{
				return RedirectToAction("Index", "Home");
			}

			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return NotFound();
			}

			var decodedToken = Uri.UnescapeDataString(token); // Sử dụng Uri để giải mã
			var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

			if (result.Succeeded)
			{
				return RedirectToPage("/Account/Login");
			}

			// Ghi lại các lỗi nếu xác nhận không thành công
			foreach (var error in result.Errors)
			{
				Console.WriteLine($"Error: {error.Description}"); // In ra lỗi
				ModelState.AddModelError(string.Empty, error.Description);
			}
			return NotFound();
		}
	}
}
