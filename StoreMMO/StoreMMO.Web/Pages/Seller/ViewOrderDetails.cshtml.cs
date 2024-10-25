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

        public IEnumerable<GetOrderDetailsViewModel> OrderDetail { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {


            // Lấy thông tin đơn hàng từ service
            OrderDetail =  _purchaseService.getOrderDetails(id);
          
            if (OrderDetail == null)
            {
                return NotFound();
            }

            return Page();
        }
    }

}
