using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using StoreMMO.Core.Models;
using StoreMMO.Web.Models.ViewModels;
using System.Text.Encodings.Web;
using System.Text;
using System.Web;

namespace StoreMMO.Web.Pages.Account
{
	public class RegisterModel : PageModel
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IEmailSender _emailSender;

		public RegisterModel(UserManager<AppUser> userManager,
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
		public RegisterViewModel inputRegister { get; set; }

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				// Tạo người dùng mới
				var user = new AppUser
				{
					UserName = inputRegister.UserName,
					Email = inputRegister.Email,
					CreatedDate = DateTime.UtcNow,
					FullName = "Thanh Dep Trai",
				};
				var result = await _userManager.CreateAsync(user, inputRegister.Password);

				if (result.Succeeded)
				{
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmailSuccess",
                        pageHandler: null,
                        values: new {userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(inputRegister.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    
                    if (!await _roleManager.RoleExistsAsync("USER"))
					{
						await _roleManager.CreateAsync(new IdentityRole("USER"));
					}
					await _userManager.AddToRoleAsync(user, "USER");
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("WaitVerifyEmail", new { email = inputRegister.Email });
                    }
                    return RedirectToPage("./Login");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return Page();
		}

		
	}
}
