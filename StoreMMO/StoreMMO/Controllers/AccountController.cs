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
                    var token = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
                    var comfirmLink = Url.Action("ConfirmEmail","Account" , new {userId = user.Id, token = token}, Request.Scheme);
 
                      await this._emailSender.SendEmailAsync(model.Email, "Comfim Email", comfirmLink);


                    if (!await _roleManager.RoleExistsAsync("User"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                    }
                    await _userManager.AddToRoleAsync(user, "User");

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
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Người dùng không tồn tại.");
                    return View(model);
                }
                var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!passwordValid)
                {
                    await _userManager.AccessFailedAsync(user);
                    var accessFailedCount = await _userManager.GetAccessFailedCountAsync(user);
                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, $"Your account is locked due to too many failed attempts.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Sai mật khẩu! Bạn còn {5 - accessFailedCount} lần thử.");
                    }
                    return View(model);
                }
                await _userManager.ResetAccessFailedCountAsync(user);
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (result.IsNotAllowed)
                {
                    return RedirectToAction("WaitVerifyEmail");
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Your account is locked.");
                    return View(model);
                }
                else if(result.Succeeded)
{
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect login information!!");
                    return View(model);
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

				ViewData["SuccessMessage"] = "Send email sucsess. <a href='https://www.gmail.com' target='_blank' style=font-weight: bold;'>Click here</a> to open gmail.";
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

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if(userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await this._userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return NotFound();

            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View("ConfirmEmailSuccess");
            }
            return View();
        }


        public IActionResult ConfirmEmailSuccess()
        {
            return View();
        }
        public IActionResult WaitVerifyEmail()
        {
            return View();
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
