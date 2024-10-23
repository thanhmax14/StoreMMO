using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.payOS;

namespace StoreMMO.Web.Pages.Account
{
    public class getthogntinModel : PageModel
    {
        private readonly PayOS payOS;
        public getthogntinModel(PayOS ss)
        {
            this.payOS = ss;
        }
        public async Task OnGet(long orderCode)
        {
          

        }

    }
}
