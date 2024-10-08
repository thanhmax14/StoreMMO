using BusinessLogic.Services.StoreMMO.API;
using BusinessLogic.Services.StoreMMO.Core.Carts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        // Index.cshtml.cs
        public IActionResult OnPost(string saveProID, string quantity)
        {
            var a = saveProID;


            // Thêm logic xử lý để thêm sản phẩm vào giỏ hàng
          
            return new JsonResult(new { success = true });
        }


    }
}
