using BusinessLogic.Services.StoreMMO.Core.Purchases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Purchase
{
    public class checkoutModel : PageModel
    {
        private readonly IPurchaseService _purchase;
        public checkoutModel(IPurchaseService purchase)
        {
            this._purchase = purchase;
        }

        public List<PurchaseItem> purchaseItems { get; set; } = new List<PurchaseItem>();

        public IActionResult OnGetAsync()
        {
            purchaseItems = this._purchase.GetProductFromSession();
           if(purchaseItems.Count< 0 || purchaseItems.IsNullOrEmpty() )
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }
    }
}
