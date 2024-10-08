using BusinessLogic.Services.StoreMMO.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;


namespace StoreMMO.Web.Pages.Home
{
    public class StoredetailModel : PageModel
    {
        private readonly StoreApiService _storeApi;
        private readonly ProductApiService _productApi;


        public StoredetailModel(StoreApiService storeApiService, ProductApiService productApi)
        {
            this._storeApi = storeApiService;
            this._productApi = productApi;
        }

        [BindProperty]
        public List<StoreDetailViewModel> ListDetail { get; set; }

        [TempData]
        public string DefauPrice { get; set; }

        [TempData]
        public string DefauStock { get; set; }

       

        public async Task<IActionResult> OnGetAsync(string id)  
        {
            try
            {
                ListDetail = await _storeApi.GetStoreDetail(id);
                if (ListDetail == null || ListDetail.Count == 0)
                {
                    return NotFound();
                }

                foreach (var item in ListDetail)
                {
                    if (item.ProductStock.Count >= 1)
                    {
                        var defauPrce = await _productApi.GetProductById(item.ProductStock.FirstOrDefault().Value);
                        DefauPrice = "$" + defauPrce.Price;
                        DefauStock = defauPrce.Stock;
                        TempData["defauProid"] = defauPrce.Id;
                        return Page();
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
       




    }
}
