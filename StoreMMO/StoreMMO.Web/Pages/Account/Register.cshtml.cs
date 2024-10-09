using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Web.Models.ViewModels;
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
					var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					var encodedToken = Uri.EscapeDataString(token); // Sử dụng Uri để mã hóa
					var confirmLink = Url.Page("/Account/Login",
						pageHandler: "ConfirmEmail",
						values: new { userId = user.Id, token = encodedToken },
						protocol: Request.Scheme);

					var emailMessage = $"Please confirm your account by clicking this link: <a href='{confirmLink}'>Confirm Email</a>";
					await _emailSender.SendEmailAsync(inputRegister.Email, "Confirm Email", emailMessage);

					if (!await _roleManager.RoleExistsAsync("User"))
					{
						await _roleManager.CreateAsync(new IdentityRole("User"));
					}
					await _userManager.AddToRoleAsync(user, "User");

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
