using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Web.Models.ViewModels;

namespace StoreMMO.Web.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IEmailSender _emailSender;
		public ResetPasswordModel(UserManager<AppUser> userManager,
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
        public ResetPasswordViewModel Input { get; set; }
        [TempData]
		public string SuccessMessage { get; set; }


		public IActionResult OnGet(string token, string email)
		{
			if (token == null || email == null)
			{
				ModelState.AddModelError("", "Invalid password reset token.");
			}

			Input = new ResetPasswordViewModel { Token = token, Email = email };
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(Input.Email);
				if (user == null)
				{
					return NotFound();
				}

				var result = await _userManager.ResetPasswordAsync(user, Input.Token, Input.Password);
				if (result.Succeeded)
				{
					SuccessMessage = "Password successfully changed.";
					return Page();
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}

			return Page();
		}

	}
}
