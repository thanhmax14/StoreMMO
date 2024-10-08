using BusinessLogic.Services.StoreMMO.API;
using BusinessLogic.Services.StoreMMO.Core.Carts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace StoreMMO.Web.Pages
{
	public class Ajax : Controller
	{
		private readonly StoreApiService _storeApi;
		private readonly ProductApiService _productApi;
		private readonly ICartService _cartService;



		public Ajax(StoreApiService storeApiService, ProductApiService productApi, ICartService cartService)
		{
			this._storeApi = storeApiService;
			this._productApi = productApi;
			this._cartService = cartService;
		}
		[HttpPost]
		public IActionResult RemoveCart(string saveProID)
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
				return new JsonResult(new { success = true, message = "ok id la " + saveProID + "Da bi xoa" });
			}
		}
	}
}
