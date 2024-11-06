using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StoreMMO.Web.Pages.Purchase
{
    [Authorize(Roles = "User,Seller")]
    public class SuccessModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
