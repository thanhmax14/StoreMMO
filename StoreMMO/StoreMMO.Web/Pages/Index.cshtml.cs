using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Services.StoreMMO.API;
using StoreMMO.Web.Services.StoreMMO.Core;

namespace StoreMMO.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly StoreApiService _storeApi;
        private readonly ProductApiService _productApi;
		private readonly ICartService _cartService;



        public IndexModel(StoreApiService storeApiService, ProductApiService productApi, ICartService cartService)
        {
            this._storeApi = storeApiService;
            this._productApi = productApi;
			this._cartService = cartService;
        }

        public List<StoreViewModels> storeView = new List<StoreViewModels>();

        public async Task OnGetAsync()
        {
            storeView = await this._storeApi.GetStoresAsync();
           
        }

		public JsonResult OnPostAddToCart(string saveProID, string quantity)
		{

			var cart = this._cartService.GetCartFromSession();
			var existingItem = cart.FirstOrDefault(u => u.productID == saveProID);

			if (existingItem != null)
			{
				existingItem.quantity = (double.Parse(existingItem.quantity) + double.Parse(quantity)).ToString();
			}
			else
			{
				cart.Add(new CartItem { productID = saveProID, quantity = quantity });
			}
			this._cartService.SaveCartToSession(cart);
			return new JsonResult(new { success = true, message = "Product added to cart!" });
		}
	}
}
