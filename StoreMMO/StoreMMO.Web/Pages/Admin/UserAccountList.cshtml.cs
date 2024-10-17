using BusinessLogic.Services.StoreMMO.Core.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        [BindProperty]
        public string id { get; set; }
        [TempData]
        public string success { get; set; }
        [TempData]
        public string fail { get; set; }
        public IEnumerable<UserViewModel> list = new List<UserViewModel>();
        public void OnGet()
        {

            list = this._userServices.GetAllUser(false);
        }
        public async Task<IActionResult> OnPostAsyns(string id)
        {
            var isDelete = id;
            var find = await _userManager.FindByIdAsync(id);

            if (find != null)
            {
                find.IsDelete = true;
                var update = await _userManager.UpdateAsync(find);
                success = "Hidden User Thanh Cong";
            }

            else
            {
                fail = "Hidden User That Bai";
            }

            return RedirectToPage("UserAccountList");
        }

    }
}
