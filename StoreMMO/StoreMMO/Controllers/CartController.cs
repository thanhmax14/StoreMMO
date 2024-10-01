using Microsoft.AspNetCore.Mvc;
using StoreMMO.Core.ViewModels;
using StoreMMO.Services.StoreMMO.Core;

namespace StoreMMO.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cart)
        {
            this._cartService = cart;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCart(string saveProID, string quan)
        {
            var getinfoProduct = this._cartService.getProductAddByID(saveProID);
           
            float quantemp= float.Parse(quan);
            var fullInfo = new CartItem { productID =
                getinfoProduct.productID,
                img = getinfoProduct.img,
                price = getinfoProduct.price,
                proName = getinfoProduct.proName,
                quantity = quan,
                subtotal = ""+ getinfoProduct.price* quantemp

            };
            var temsd = fullInfo;

            return Json(new { success = true });
        }
    }
}
