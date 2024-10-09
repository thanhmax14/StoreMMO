using BusinessLogic.Services.StoreMMO.Core.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
    public class UpdateUserModel : PageModel
    {
        private readonly IUserServices _userServices;
        private readonly UserManager<AppUser> _userManager;

        public UpdateUserModel(UserManager<AppUser> userManager, IUserServices userServices)
        {

            _userManager = userManager;
            _userServices = userServices;
        }

        public IEnumerable<UserViewModel> list = new List<UserViewModel>();
        [TempData]
        public string role1 { get; set; }
        [TempData]
        public string role2 { get; set; }

        public IActionResult OnGet(string userId)
        {

            list = this._userServices.GetUserById(userId);
            var ten = "";
            foreach (var item in list)
            {
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
    }
}
