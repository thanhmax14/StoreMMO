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
       public List<CategoryViewModels> listCat { get; set; }
        public async Task OnGetAsync(string searchString, int? page, int? count, string cat, string username, string orderby)
        {
            CurrentFilter = searchString;
            CurrentPageSize = count ?? 12;
            int pageNumber = page ?? 1;
            var stores = await _storeApi.GetStoresAsync("0");
            var categoryJson = HttpContext.Session.GetString("ListCate");
          
            if (!string.IsNullOrEmpty(categoryJson))
            {
                listCat = System.Text.Json.JsonSerializer.Deserialize<List<CategoryViewModels>>(categoryJson);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                var keywords = searchString.Split(' ');

                // Lọc theo tên cửa hàng
                stores = stores.Where(s => keywords.All(k => s.nameStore.ToLower().Contains(k.ToLower()))).ToList();
            }

    
            if (!string.IsNullOrEmpty(cat))
            {
                stores = stores.Where(s => s.catename.ToLower().Contains(cat.ToLower())).ToList();
            }

           
            if (!string.IsNullOrEmpty(username))
            {
                stores = stores.Where(s => s.UserName.ToLower().Contains(username.ToLower())).ToList();
            }

            stores = orderby switch
            {
                "price" => stores.OrderBy(s => ExtractMinPrice(s.price)).ToList(),
                "price-desc" => stores.OrderByDescending(s => ExtractMinPrice(s.price)).ToList(),
                _ => stores // Sắp xếp mặc định
            };



            storeView = stores.ToPagedList(pageNumber, CurrentPageSize);
        }
		private decimal ExtractMinPrice(string priceRange)
		{
			if (string.IsNullOrEmpty(priceRange)) return 0;

			var prices = priceRange.Split('-');
			return decimal.TryParse(prices[0].Trim(), out var minPrice) ? minPrice : 0;
		}
	}
}
