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
        public IActionResult OnGet(string userId)
        {
            list = this._userServices.GetUserById(userId);

            return Page();
        }
    }
}
