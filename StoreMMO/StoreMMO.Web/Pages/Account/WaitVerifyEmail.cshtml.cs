using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StoreMMO.Web.Pages.Account
{
    public class WaitVerifyEmailModel : PageModel
    {
        public string Email { get; set; }

        public void OnGet(string email)
        {
            Email = email; // Lưu email được truyền từ trang trước
        }
    }
}
