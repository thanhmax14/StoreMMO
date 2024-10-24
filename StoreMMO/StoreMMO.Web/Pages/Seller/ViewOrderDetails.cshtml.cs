using BusinessLogic.Services.StoreMMO.Core.OrderDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Seller
{
    public class ViewOrderDetailsModel : PageModel
    {
        private readonly IOderDetailsService _orderDetail;
        public ViewOrderDetailsModel(IOderDetailsService orderDetail)
        {
            _orderDetail = orderDetail;
        }
        public OrderDetailsViewModels OrderDetail { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {


            // Lấy thông tin đơn hàng từ service
            OrderDetail = await _orderDetail.GetOrderDeailByIDAsync(id);

            if (OrderDetail == null)
            {
                return NotFound();
            }

            return Page();
        }
    }

}
