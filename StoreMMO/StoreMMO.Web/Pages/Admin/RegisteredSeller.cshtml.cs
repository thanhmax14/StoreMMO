using BusinessLogic.Services.StoreMMO.Core.RegisteredSeller;
using BusinessLogic.Services.StoreMMO.Core.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
    public class RegisteredSellerModel : PageModel
    {
        private readonly IRegisteredSellerService _registeredSellerService;
        private readonly UserManager<AppUser> _userManager;
        [BindProperty]
        public UserViewModel input { get; set; }

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
        public IActionResult OnPostUpdateSellerAjax(string userId)
        {
            // Tìm người dùng dựa trên userId
            var user = _userManager.Users
                        .Where(u => u.Id == userId)
                        .FirstOrDefault();

            if (user == null)
            {
                return new JsonResult(new { success = false, message = "User not found" });
            }

            // Cập nhật trạng thái IsSeller
            user.IsSeller = false;

            // Lấy vai trò "Seller"
            var sellerRole = _userManager.Roles.FirstOrDefault(r => r.Name == "Seller");

            if (sellerRole == null)
            {
                return new JsonResult(new { success = false, message = "Seller role not found" });
            }

            // Kiểm tra xem user đã có role "Seller" chưa, nếu chưa thì thêm
            var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id);

            if (userRole == null)
            {
                _context.UserRoles.Add(new IdentityUserRole<string>
                {
                    UserId = user.Id,
                    RoleId = sellerRole.Id
                });
            }
            else
            {
                userRole.RoleId = sellerRole.Id;
            }

            // Lưu thay đổi
            _context.SaveChanges();

            // Trả về kết quả thành công
            return new JsonResult(new { success = true });
        }

    }
}
