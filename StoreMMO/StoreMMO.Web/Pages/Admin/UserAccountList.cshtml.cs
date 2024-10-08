using BusinessLogic.Services.StoreMMO.Core.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
    public class UserAccountListModel : PageModel
    {
        private readonly IUserServices _userServices;
        private readonly UserManager<AppUser> _userManager;

        public UserAccountListModel(UserManager<AppUser> userManager, IUserServices userServices)
        {

            _userManager = userManager;
            _userServices = userServices;
        }

        public IEnumerable<UserViewModel> list = new List<UserViewModel>();
        public void OnGet()
        {

            list = this._userServices.GetAllUser();
        }


    }
}
