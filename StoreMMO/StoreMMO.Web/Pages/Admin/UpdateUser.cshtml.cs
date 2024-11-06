using BusinessLogic.Services.StoreMMO.Core.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
	[Authorize(Roles = "Admin")]
	public class UpdateUserModel : PageModel
    {
        private readonly IUserServices _userServices;
        private readonly UserManager<AppUser> _userManager;

        public UpdateUserModel(UserManager<AppUser> userManager, IUserServices userServices)
        {

            _userManager = userManager;
            _userServices = userServices;
        }
        [BindProperty]
        public UserUpdateViewModel input { get; set; }
        public IEnumerable<UserViewModel> list = new List<UserViewModel>();
        [TempData]
        public string role1 { get; set; }
        [TempData]
        public string role2 { get; set; }

        [TempData]
        public string success { get; set; }
        [TempData]
        public string fail { get; set; }
        public IActionResult OnGet(string userId)
        {

            list = this._userServices.GetUserById(userId);

            foreach (var item in list)
            {
                var tem = item.PhoneNumber;
                if (item.RoleName.Equals("Admin"))
                {

                    role1 = "User";
                    role2 = "Seller";
                }
                else if (item.RoleName.Equals("User"))
                {
                    role1 = "Seller";
                    role2 = "Admin";
                }
                else if (item.RoleName.Equals("Seller"))
                {
                    role1 = "User";
                    role2 = "Admin";
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("Error404");
            }
            else
            {
                var tem = input;
                var checkEmail = await this._userManager.FindByEmailAsync(input.Email);
                if (checkEmail != null)
                {
                    checkEmail.FullName = input.FullName;
                    checkEmail.DateOfBirth = input.DateOfBirth;
                    checkEmail.PhoneNumber = input.PhoneNumber;
                    checkEmail.Address = input.Address;

                    if (checkEmail.PasswordHash != input.Password)
                    {
                        var resetTokent = await _userManager.GeneratePasswordResetTokenAsync(checkEmail);
                        var resetPassResult = await _userManager.ResetPasswordAsync(checkEmail, resetTokent, input.Password);
                        if (!resetPassResult.Succeeded)
                        {
                            fail = "Update mat khau that bai";
                            return RedirectToPage("UpdateUser", new { userID = checkEmail.Id }); // Hiển thị lỗi nếu không thành công
                        }

                    }
                    var update = await this._userManager.UpdateAsync(checkEmail);
                    if (update.Succeeded)
                    {
                        success = "Update Thong Tin Thanh cong";
                    }
                    else
                    {
                        fail = "Update Thong Tin That Bai";
                    }
                    return RedirectToPage("UpdateUser", new { userID = checkEmail.Id });
                }
                return RedirectToPage("Error404");

            }

        }
    }
}
