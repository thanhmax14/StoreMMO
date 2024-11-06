using BusinessLogic.Services.StoreMMO.Core.RegisteredSeller;
using BusinessLogic.Services.StoreMMO.Core.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
	[Authorize(Roles = "Admin")]
	public class RegisteredSellerModel : PageModel
    {
        private readonly IRegisteredSellerService _registeredSellerService;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        [BindProperty]
        public UserViewModel input { get; set; }

        [BindProperty]
        public string checkacp { get; set; }
        [BindProperty]
        public string checkre { get; set; }
        [TempData]
        public string success { get; set; }
        [TempData]
        public string fail { get; set; }
        public RegisteredSellerModel(UserManager<AppUser> userManager, IRegisteredSellerService registeredSellerService)
        {

            _userManager = userManager;
            _registeredSellerService = registeredSellerService;
        }

        public IEnumerable<UserViewModel> list = new List<UserViewModel>();
        public void OnGet()
        {
            list = this._registeredSellerService.GetAllSellerUsersWithUserRole();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            // Tìm người dùng theo ID
            var find = await _userManager.FindByIdAsync(id);

            if (find != null)
            {
                    // Accept logic: Cập nhật IsSeller thành true
                    find.IsSeller = true;
                    var update = await _userManager.UpdateAsync(find);

                    if (update.Succeeded)
                    {
                        // Assign the "Seller" role
                        var roles = await _userManager.GetRolesAsync(find);
                        var removeRolesResult = await _userManager.RemoveFromRolesAsync(find, roles);

                        if (removeRolesResult.Succeeded)
                        {
                            var addRoleResult = await _userManager.AddToRoleAsync(find, "Seller");
                            if (addRoleResult.Succeeded)
                            {
                                success = "Updated to 'Seller' successfully!";
                            }
                            else
                            {
                                fail = "Updated to 'Seller' fail!";
                            }
                        }
                        else
                        {
                            fail = "Failed to remove current roles.";
                        }
                    }
                    else
                    {
                        fail = "Update failed.";
                    }
            }
            else
            {
                fail = "User not found.";
            }

            // Điều hướng trở lại trang danh sách người dùng
            return RedirectToPage("RegisteredSeller");
        }
        public async Task<IActionResult> OnPostRejectAsync(string id)
        {
            // Tìm người dùng theo ID
            var find = await _userManager.FindByIdAsync(id);

            if (find != null)
            {
                    // Accept logic: Cập nhật IsSeller thành true
                    find.IsSeller = false;

                    var update = await _userManager.UpdateAsync(find);

                    if (update.Succeeded)
                    {
                        // Assign the "Seller" role
                        var roles = await _userManager.GetRolesAsync(find);
                        var removeRolesResult = await _userManager.RemoveFromRolesAsync(find, roles);

                        if (removeRolesResult.Succeeded)
                        {
                            var addRoleResult = await _userManager.AddToRoleAsync(find, "User");
                            if (addRoleResult.Succeeded)
                            {
                                success = "Reject 'User' to 'Seller' successfully!";
                            }
                            else
                            {
                                fail = "Reject 'User' to 'Seller' fail!";
                            }
                        }
                        else
                        {
                            fail = "Failed to remove current roles.";
                        }
                    }
                    else
                    {
                        fail = "Update failed.";
                    }
            }
            else
            {
                fail = "User not found.";
            }

            // Điều hướng trở lại trang danh sách người dùng
            return RedirectToPage("RegisteredSeller");
        }
    }
}
