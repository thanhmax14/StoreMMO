using BusinessLogic.Services.StoreMMO.API;
using BusinessLogic.Services.StoreMMO.Core.Carts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using StoreMMO.Core.ViewModels;


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
      
        public IActionResult OnPostAddToCart(int quan, string saveProID)
        {
            if (string.IsNullOrEmpty(saveProID) || int.IsNegative(quan))
            {
                return new JsonResult(new { success = false, mess="" });
            }
            else
            {
                var cart = this._cartService.GetCartFromSession();
                var getitem = this._cartService.getProductAddByID(saveProID);
                if (getitem != null || !getitem.IsNullOrEmpty())

                {
                    foreach (var item in getitem)
                    {
                        double quantity = double.Parse(quan+"");
                        var temp = new CartItem
                        {
                            img = item.img,
                            productID = item.productID,
                            storeDetailID= item.storeDetailID,
                            quantity = quan+"",
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
                    }

                }
                return new JsonResult(new { success = true, message = "ok id la " + saveProID });
            }
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult OnPostRemoveCart(string saveProID)
		{
			if (string.IsNullOrEmpty(saveProID))
			{
				return new JsonResult(new { success = false, mess = "" });
			}
			else
			{
				var cart = this._cartService.GetCartFromSession();
				var getitem = this._cartService.getProductAddByID(saveProID);
				if (getitem != null || !getitem.IsNullOrEmpty())

				{
					foreach (var item in getitem)
					{						
						var existingItem = cart.FirstOrDefault(u => u.productID == item.productID);
						if (existingItem != null)
						{
                            cart.Remove(existingItem);
						}						
						this._cartService.SaveCartToSession(cart);
					}

				}
				return new JsonResult(new { success = true, message = "ok id la " + saveProID +"Da bi xoa" });
			}
		}
	}
}
