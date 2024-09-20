using Microsoft.AspNetCore.Mvc;
using StoreMMO.Models;
using StoreMMO.Services.StoreMMO.API;
using System.Diagnostics;

namespace StoreMMO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StoreApiService _storeApi;

        public HomeController(ILogger<HomeController> logger,StoreApiService storeApi)
        {
            _logger = logger;
             this._storeApi = storeApi;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.list = await this._storeApi.GetStoresAsync();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
