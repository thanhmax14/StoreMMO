using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using StoreMMO.Core.Models;
using StoreMMO.Models;

namespace StoreMMO.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            this._emailSender = emailSender;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.UserName,
                    Email = model.Email,
                    CreatedDate = DateTime.UtcNow,
                    FullName ="Thanh Dep Trai",				
			};
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {                  
                    if (!await _roleManager.RoleExistsAsync("User"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                    }
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Your account is locked.");
                }else if (result.IsNotAllowed)
                {
                    ModelState.AddModelError(string.Empty, "You must Verify Email before to Login!!");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Invalid login attempt.");
				}

			}
            return View(model);
        }

		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
		{
            if (ModelState.IsValid)
            {
                var user = await this._userManager.FindByEmailAsync(model.Email);
                if(user == null)
                {
                    ModelState.AddModelError(string.Empty, "Email Doesn't exit!");
					return View();
				}
                if(!await this._userManager.IsEmailConfirmedAsync(user))
                {
					ModelState.AddModelError(string.Empty, "You must verify account before request forgot password!");
					return View();
				}
				var token = await this._userManager.GeneratePasswordResetTokenAsync(user);
                var resetLink = Url.Action("ResetPassword", "Account" , new {token, email = model.Email}, Request.Scheme);
				await _emailSender.SendEmailAsync(model.Email, "Reset Password", TemplateMail.TemplateResetPass(user.FullName??"thanh dep trai heeh",resetLink) );






				ViewData["SuccessMessage"] = "If your email is registered and confirmed, you will receive a password reset link shortly.";
				return View();
			}
			return View();
		}

		[HttpGet]
		public IActionResult ResetPassword(string token, string email)
		{
			if (token == null || email == null)
			{
				ModelState.AddModelError("", "Invalid password reset token.");
			}

			return View(new ResetPasswordViewModel { Token = token, Email = email });
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user == null)
				{
					return NotFound();
				}

				var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
				if (result.Succeeded)
				{
					ViewData["SuccessMessage"] = "Change Password susses";
                    return View();
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}

			return View(model);
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
