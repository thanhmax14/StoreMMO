using BusinessLogic.Services.StoreMMO.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;
using X.PagedList;
using System.Linq;
using X.PagedList.Extensions;

namespace StoreMMO.Web.Pages.Home
{
    public class ViewAllStoreModel : PageModel
    {
        private readonly StoreApiService _storeApi;

        public ViewAllStoreModel(StoreApiService storeApi)
        {
            _storeApi = storeApi;
        }

        public IPagedList<StoreViewModels> storeView { get; set; }
        public string CurrentFilter { get; set; }
        public int CurrentPageSize { get; set; }

        public async Task OnGetAsync(string searchString, int? page, int? count, string cat)
        {
            CurrentFilter = searchString;
            CurrentPageSize = count ?? 12;
            int pageNumber = page ?? 1;
            var stores = await _storeApi.GetStoresAsync("1");

            if (!string.IsNullOrEmpty(searchString))
            {
                var keywords = searchString.Split(' ');

                // Lọc theo tên cửa hàng
                stores = stores.Where(s => keywords.All(k => s.nameStore.ToLower().Contains(k.ToLower()))).ToList();
            }

            // Lọc theo tên danh mục (categoryName)
            if (!string.IsNullOrEmpty(cat))
            {
                stores = stores.Where(s => s.catename.ToLower().Contains(cat.ToLower())).ToList();
            }

            storeView = stores.ToPagedList(pageNumber, CurrentPageSize);
        }

    }
}
