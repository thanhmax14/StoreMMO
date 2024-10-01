using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using StoreMMO.Core.Models;
using StoreMMO.Web.Models.ViewModels;

namespace StoreMMO.Web.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IEmailSender _emailSender;

		public ForgotPasswordModel(UserManager<AppUser> userManager,
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
		public ForgotPasswordViewModel Input { get; set; }

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(Input.Email);
				if (user == null)
				{
					ModelState.AddModelError(string.Empty, "Email doesn't exist!");
					return Page();
				}

				if (!await _userManager.IsEmailConfirmedAsync(user))
				{
					ModelState.AddModelError(string.Empty, "You must verify account before requesting forgot password!");
					return Page();
				}

				var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				var resetLink = Url.Page("/Account/ResetPassword", null, new { token, email = Input.Email }, Request.Scheme);
				await _emailSender.SendEmailAsync(Input.Email, "Reset Password", Models.Content.TemplateMail.TemplateResetPass(user.FullName ?? "Thanh dep trai heeh", resetLink));

				ViewData["SuccessMessage"] = "Send email success. <a href='https://www.gmail.com' target='_blank' style='font-weight: bold;'>Click here</a> to open Gmail.";
				return Page();
			}

			return Page();
		}



	}
}
