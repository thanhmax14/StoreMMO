using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreMMO.Models;
using StoreMMO.Services.StoreMMO.API;
using System.Diagnostics;

namespace StoreMMO.Controllers
{
    public class HomeController : Controller
    { 
        private readonly StoreApiService _storeApi;

        public HomeController(StoreApiService storeApi)
        {
             this._storeApi = storeApi;
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
                return View();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }



    }
}
