using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreMMO.Core.ViewModels;
using StoreMMO.Models;
using StoreMMO.Services.StoreMMO.API;
using System.Diagnostics;

namespace StoreMMO.Controllers
{
    public class HomeController : Controller
    { 
        private readonly StoreApiService _storeApi;
        private readonly ProductApiService _productApi;

        public HomeController(StoreApiService storeApi, ProductApiService productApiService)
        {
             this._storeApi = storeApi;
            this._productApi= productApiService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.list = await this._storeApi.GetStoresAsync();
            return View();
        }
        public async Task<IActionResult> StoreDetail(string id)
        {
            try
            {
                var list = await this._storeApi.GetStoreDetail(id);
                ViewBag.ListDetail = list;
                if (list == null || list.Count == 0)
                {
                    return NotFound();
                }
                foreach(var item in list)
                {
                    if (item.ProductStock.Count >= 1)
                    {
                        var defauPrce = await this._productApi.GetProductById(item.ProductStock.FirstOrDefault().Value);
                        TempData["defauPrice"] = "$"+ defauPrce.Price;
                        TempData["defauStock"] = defauPrce.Stock;
                        TempData["defauProid"] = defauPrce.Id;
                        return View();
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
