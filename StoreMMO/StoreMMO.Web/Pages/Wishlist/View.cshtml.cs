using BusinessLogic.Services.Encrypt;
using BusinessLogic.Services.StoreMMO.API;
using BusinessLogic.Services.StoreMMO.Core.Carts;
using BusinessLogic.Services.StoreMMO.Core.WishLists;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Wishlist
{
    public class ViewModel : PageModel
    {
        private readonly IWishListsService _wishListsService;
        private readonly WishListApiService _api;
        private readonly ICartService _cartService;
        private readonly ProductApiService _productApi;
        public ViewModel(IWishListsService service, WishListApiService wishListApi,
            ICartService cartService, ProductApiService productApiService)
            
        {
             this._wishListsService = service;
            this._api = wishListApi;
            this._cartService = cartService;
           this._productApi = productApiService;
        }
        [BindProperty]
        public List<WishListViewModels> list {  get; set; }
     
        public List<CartItem> temitem = new List<CartItem>();
       

        public async Task<IActionResult>OnGetAsync()
        {
            var useriD = HttpContext.Session.GetString("UserID");
            if(useriD == null)
            {
                return RedirectToPage("/Account/Login");
            }
           

            list = await this._api.getByUserID(useriD);         
           foreach(var item in list)
            {
                var getInfo = this._cartService.getProductAddByID(item.ProductId);
                  foreach(var item2 in getInfo)
                {
                    var check = await this._productApi.GetProductById(item2.productID);
                    if(check != null)
                    {
                        item2.quantity = check.Stock;
                        temitem.Add(item2);
                    }
                    
                }
            }

            return Page();
        }


        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostRemoveWish(string id)
        {

            var UserID = HttpContext.Session.GetString("UserID");
            if(UserID == null)
            {
                return new JsonResult(new { success = false, message = "Xóa thanh cong với ID " + id });
            }

            var get = this._wishListsService.getAllByUserID(UserID);
            foreach(var item in get)
            {
                if(item.ProductId == id)
                {
                    this._wishListsService.DeleteWishList(item.Id);
                    return new JsonResult(new { success = true, message = "Xóa thanh cong với ID " + id });
                }
            }

            return new JsonResult(new { success = false, message = "Xóa thanh cong với ID " + id });

        }
    }
}
