using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Services.StoreMMO.API;
using StoreMMO.Web.Services.StoreMMO.Core;

namespace StoreMMO.Web.Pages.Cart
{
	public class ViewModel : PageModel
	{
		private readonly CartApiService _cartApiService;
        private readonly ICartService _cartService;

		public ViewModel(CartApiService cartApiService, ICartService cartService)
		{
			_cartApiService = cartApiService;
            this._cartService = cartService;
		}

		public List<CartItem> CartItems { get; set; } = new List<CartItem>();

		public async Task OnGetAsync()
		{
			CartItems = await _cartApiService.GetCartFomSessionApi();
		}
     
        public IActionResult OnPostAddToCart(string saveProID, string quan)
        {
            if (string.IsNullOrEmpty(saveProID) || string.IsNullOrEmpty(quan))
            {
                return new JsonResult(new { message = "Sản phẩm đã được thêm vào giỏ hàng!" });
            }
            else
            {
               /* var cart = this._cartService.GetCartFromSession();
                var getitem = this._cartService.getProductAddByID(request.saveProID);
                if (getitem != null || !getitem.IsNullOrEmpty())

                {
                    foreach (var item in getitem)
                    {
                        double quantity = double.Parse(quan);
                        var temp = new CartItem
                        {
                            img = item.img,
                            productID = item.productID,
                            quantity = quan,
                            price = item.price,
                            proName = item.proName,
                            subtotal = "" + item.price * quantity

                        };
                        var existingItem = cart.FirstOrDefault(u => u.productID == item.productID);
                        if (existingItem != null)
                        {
                            existingItem.quantity = (double.Parse(existingItem.quantity) + quantity).ToString();
                            existingItem.subtotal = (item.price * (double.Parse(existingItem.quantity))).ToString();
                        }
                        else
                        {

                            cart.Add(temp);
                        }
                        this._cartService.SaveCartToSession(cart);
                    }*/

                }
                return new JsonResult(new { message = "Sản phẩm đã được thêm vào giỏ hàng!" });
            }


        }

    
}
