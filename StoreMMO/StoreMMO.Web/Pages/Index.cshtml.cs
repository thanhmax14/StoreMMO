using BusinessLogic.Services.Encrypt;
using BusinessLogic.Services.StoreMMO.API;
using BusinessLogic.Services.StoreMMO.Core.Carts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using StoreMMO.Core.ViewModels;
using System.Text.Json;


namespace StoreMMO.Web.Pages
{
	public class IndexModel : PageModel
	{
		private readonly StoreApiService _storeApi;
		private readonly ProductApiService _productApi;
		private readonly ICartService _cartService;
		private readonly WishListApiService _wishListApi;
		private readonly CategoryApiService _categoryApiService;


		public IndexModel(StoreApiService storeApiService, ProductApiService productApi,
			ICartService cartService, WishListApiService wishListApi, CategoryApiService categoryApiService)
		{
			this._storeApi = storeApiService;
			this._productApi = productApi;
			this._cartService = cartService;
			this._wishListApi = wishListApi;
			_categoryApiService = categoryApiService;
		}

		public List<StoreViewModels> storeView = new List<StoreViewModels>();
		public List<WishListViewModels> wishList = new List<WishListViewModels>();
		public List<WishListViewModels> wishnew = new List<WishListViewModels>();
		public async Task OnGetAsync()
		{

		   var listCate = this._categoryApiService.GetAllCategoriesAsync();
			if (listCate != null)
			{
				HttpContext.Session.SetString("ListCate",JsonSerializer.Serialize(listCate));
			}

			storeView = await this._storeApi.GetStoresAsync(true);
			var useriD = HttpContext.Session.GetString("UserID");
			if (useriD != null)
			{
				foreach (var storeViewModel in storeView)
				{
					var checktem = await this._storeApi.GetStoreDetail(storeViewModel.storeID);

					wishList = await this._wishListApi.getByUserID(useriD);
					foreach (var wish in wishList)
					{
						if (checktem.Any(u => u.ProductStock.ContainsValue(wish.ProductId)))
						{
							wish.ProductId = storeViewModel.storeID;
						}
						wishnew.Add(wish);
					}


				}
			}
		}


		public IActionResult OnPostAddToCart(int quan, string saveProID)
		{
			if (string.IsNullOrEmpty(saveProID) || int.IsNegative(quan))
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
						double quantity = double.Parse(quan + "");
						var temp = new CartItem
						{
							img = item.img,
							productID = item.productID,
							storeDetailID = item.storeDetailID,
							quantity = quan + "",
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
				return new JsonResult(new { success = true, message = "ok id la " + saveProID + "Da bi xoa" });
			}
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult OnPostAddPluts(string saveProID)
		{
			if (string.IsNullOrEmpty(saveProID))
			{
				return new JsonResult(new { success = false, mess = "" });
			}
			else
			{
				var cart = this._cartService.GetCartFromSession();
				var getitem = this._cartService.getProductAddByID(saveProID);
				var temquantit = "";
				var subprice = "";
				if (getitem != null || !getitem.IsNullOrEmpty())

				{
					foreach (var item in getitem)
					{
						var existingItem = cart.FirstOrDefault(u => u.productID == item.productID);
						if (existingItem != null)
						{
							existingItem.quantity = (double.Parse(existingItem.quantity) + 1).ToString();
							existingItem.subtotal = (item.price * (double.Parse(existingItem.quantity))).ToString();

						}
						subprice = existingItem.subtotal;
						temquantit = existingItem.quantity;
						this._cartService.SaveCartToSession(cart);
					}

				}
				return new JsonResult(new
				{
					success = true,
					message = "ok id la " + saveProID + "Da bi xoa",
					subprice1 = subprice,
					quantity = temquantit
				});
			}
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult OnPostAddMinus(string saveProID)
		{
			if (string.IsNullOrEmpty(saveProID))
			{
				return new JsonResult(new { success = false, mess = "" });
			}
			else
			{
				var cart = this._cartService.GetCartFromSession();
				var getitem = this._cartService.getProductAddByID(saveProID);
				var temquantit = "";
				var subprice = "";
				if (getitem != null || !getitem.IsNullOrEmpty())

				{
					foreach (var item in getitem)
					{
						var existingItem = cart.FirstOrDefault(u => u.productID == item.productID);
						if (existingItem != null)
						{
							existingItem.quantity = (double.Parse(existingItem.quantity) - 1).ToString();
							existingItem.subtotal = (item.price * (double.Parse(existingItem.quantity))).ToString();
						}
						subprice = existingItem.subtotal;
						temquantit = existingItem.quantity;
						if (Int32.Parse(existingItem.quantity) <= 0 || double.Parse(existingItem.subtotal) <= 0)
						{
							cart.Remove(existingItem);
						}
						this._cartService.SaveCartToSession(cart);
					}

				}
				return new JsonResult(new
				{
					success = true,
					message = "ok id la " + saveProID + "Da bi xoa",
					subprice1 = subprice,
					quantity = temquantit
				});
			}
		}




		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult OnPostAutocheck(string saveProID)
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
							if (Int32.Parse(existingItem.quantity) == 1)
							{
								return new JsonResult(new { success = false, message = "Do you want to remove??" });
							}
						}
					}

				}
				return new JsonResult(new { success = true, message = "" });
			}
		}


		public async Task<IActionResult> OnPostAddWishList(string saveProID)
		{
			// Lấy UserID từ session
			var useriD = HttpContext.Session.GetString("UserID");
			if (useriD == null)
			{
				return new JsonResult(new { success = false, message = "Bạn phải đăng nhập." ,login=false});
			}
			var wishListItem = new WishListViewModels
			{
				Id = Guid.NewGuid().ToString(),
				ProductId = saveProID,
				UserId = useriD,
			};
			var existingItems = await this._wishListApi.getByUserID(useriD);
			if (existingItems.Any(item => saveProID == item.ProductId))
			{
				return new JsonResult(new { success = false, message = $"Sản phẩm đã tồn tại trong danh sách yêu thích: {saveProID}" , login = true });
			}
			var addedItem = await this._wishListApi.Add(wishListItem);
			if (addedItem == null)
			{
				return new JsonResult(new { success = false, message = $"Thêm sản phẩm thất bại: {saveProID}", login = true });
			}
			return new JsonResult(new { success = true, message = $"Sản phẩm đã được thêm vào danh sách yêu thích: {saveProID}", login = true });
		}
	}
}
