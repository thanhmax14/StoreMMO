using BusinessLogic.Services.StoreMMO.Core.OrderDetails;
using BusinessLogic.Services.StoreMMO.Core.Purchases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Seller
{
    public class ViewOrderDetailsModel : PageModel
    {
        private readonly IOderDetailsService _orderDetail;
        private readonly IPurchaseService _purchaseService;
        public ViewOrderDetailsModel(IOderDetailsService orderDetail, IPurchaseService purchaseService)
        {
            _orderDetail = orderDetail;
            _purchaseService = purchaseService;
        }
        public OrderDetailsViewModels orderDetail { get; set; }
        public SaleHistoryViewModels list { get; set; }
        [BindProperty]
        public string id { get; set; }
        [TempData]
        public string success { get; set; }
        [TempData]
        public string fail { get; set; }
        [TempData]
        public string OrderCode { get; set; }
        public IEnumerable<GetOrderDetailsViewModel> OrderDetail { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
         
            var orcode = id.Split('$');
           OrderCode ="Order Detail of: "+orcode[1];
            OrderDetail = _purchaseService.getOrderDetails(orcode[0]);
           
           
           
            if (OrderDetail == null)
            {
                return NotFound();
            }

            return Page();
        }
    }

}
