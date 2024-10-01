using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Services.StoreMMO.API;

namespace StoreMMO.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly StoreApiService _storeApi;
        private readonly ProductApiService _productApi;


        public IndexModel(StoreApiService storeApiService, ProductApiService productApi)
        {
            this._storeApi = storeApiService;
            this._productApi = productApi;
        }

        public List<StoreViewModels> storeView = new List<StoreViewModels>();

        public async Task OnGetAsync()
        {
            storeView = await this._storeApi.GetStoresAsync();
           
        }
    }
}
