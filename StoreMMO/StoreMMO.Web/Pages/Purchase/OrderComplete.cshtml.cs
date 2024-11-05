using BusinessLogic.Services.StoreMMO.Core.Purchases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Purchase
{
    public class OrderCompleteModel : PageModel
    {
        private readonly IPurchaseService _purchase;

        public OrderCompleteModel(IPurchaseService purchase)
        {
            _purchase = purchase;
        }

        public List<PurchaseItem> purchaseItems { get; set; } = new List<PurchaseItem>();

        public IActionResult OnGetAsync()
        {
            purchaseItems = this._purchase.GetProductFromSession();
            return Page();
        }
    }
}
