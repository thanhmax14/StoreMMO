using BusinessLogic.Services.StoreMMO.API;
using BusinessLogic.Services.StoreMMO.Core.Carts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using X.PagedList.Extensions;
using X.PagedList;

namespace StoreMMO.Web.Pages.Home
{
    public class ViewAllStoreModel : PageModel
    {
        private readonly StoreApiService _storeApi;
        private readonly ProductApiService _productApi;
        private readonly ICartService _cartService;
        private readonly WishListApiService _wishListApi;
        public ViewAllStoreModel(StoreApiService storeApiService, ProductApiService productApi,
            ICartService cartService, WishListApiService wishListApi)
        {
            this._storeApi = storeApiService;
            this._productApi = productApi;
            this._cartService = cartService;
            this._wishListApi = wishListApi;
        }
        public IPagedList<StoreViewModels> PagedStores { get; set; }

        public async Task<IActionResult> OnGetAsync(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            var stores = await this._storeApi.GetStoresAsync();

            PagedStores = stores.ToPagedList(pageNumber, pageSize);

            return Page();
        }


    }
}
