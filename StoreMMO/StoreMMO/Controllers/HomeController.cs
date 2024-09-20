using Microsoft.AspNetCore.Mvc;
using StoreMMO.Models;
using StoreMMO.Services.Store;
using System.Diagnostics;

namespace StoreMMO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreService _storeService;

        public HomeController(ILogger<HomeController> logger, IStoreService storeService)
        {
            _logger = logger;
            this._storeService = storeService;
        }

        public IActionResult Index()
        {
            ViewBag.list =  this._storeService.getAll();
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
