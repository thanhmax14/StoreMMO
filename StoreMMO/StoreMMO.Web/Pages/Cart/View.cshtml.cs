using BusinessLogic.Services.StoreMMO.Core.Carts;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.Purchases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using StoreMMO.Core.ViewModels;
namespace StoreMMO.Web.Pages.Cart
{
    public class ViewModel : PageModel
    {
        //private readonly CartApiService _cartApiService;
        private readonly ICartService _cartService;
        private readonly IPurchaseService _purchase;

        public ViewModel(ICartService cartService, IPurchaseService service)
        {
            //_cartApiService = cartApiService;
            this._cartService = cartService;
            this._purchase = service;
        }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public void OnGetAsync()
        {
            CartItems = this._cartService.GetCartFromSession();
        }


        public IActionResult OnPost()
        {
            this._purchase.SaveProductToSession(null);
            var PurchaseItem = this._purchase.GetProductFromSession() ?? new List<PurchaseItem>(); // Khởi tạo nếu null

            var cartItem = this._cartService.GetCartFromSession();
            if (cartItem != null)
            {
                foreach (var itemCart in cartItem)
                {
                    var getitem = this._cartService.getProductAddByID(itemCart.productID);
                    if (getitem != null) // Sử dụng getitem.Count > 0 thay vì IsNullOrEmpty()
                    {
                        foreach (var item in getitem)
                        {
                            // Kiểm tra nếu sản phẩm đã tồn tại trong PurchaseItem
                            var existingItem = PurchaseItem.FirstOrDefault(p => p.ProductID == item.productID);
                            if (existingItem != null)
                            {
                                existingItem.quantity += itemCart.quantity;
                            }
                            else
                            {
                                // Nếu không tồn tại, tạo một mục mới
                                var newItem = new PurchaseItem
                                {
                                    ProductID = item.productID,
                                    ProductName = item.proName,
                                    quantity = itemCart.quantity,
                                    storeName = "thanh",
                                    total = itemCart.subtotal,
                                };
                                PurchaseItem.Add(newItem);
                            }
                        }
                    }
                }

                // Lưu lại danh sách vào session
                this._purchase.SaveProductToSession(PurchaseItem);

                return RedirectToPage("/Purchase/checkout");
            }
            else
            {
                return NotFound();
            }
        }


        public IActionResult OnPostAddToCart(string saveProID, string quan)
        {
            if (string.IsNullOrEmpty(saveProID) || string.IsNullOrEmpty(quan))
            {
                return new JsonResult(new { message = "Sản phẩm đã được thêm vào giỏ hàng!" });
            }
            else
            {
               /* var cart = this._cartService.GetCartFromSession();
                var getitem = this._cartService.getProductAddByID(request.saveProID);
                if (getitem != null || !getitem.IsNullOrEmpty())

                {
                    foreach (var item in getitem)
                    {
                        double quantity = double.Parse(quan);
                        var temp = new CartItem
                        {
                            img = item.img,
                            productID = item.productID,
                            quantity = quan,
                            price = item.price,
                            proName = item.proName,
                            subtotal = "" + item.price * quantity

                        };
                        var existingItem = cart.FirstOrDefault(u => u.productID == item.productID);
                        if (existingItem != null)
                        {
                            existingItem.quantity = (double.Parse(existingItem.quantity) + quantity).ToString();
                            existingItem.subtotal = (item.price * (double.Parse(existingItem.quantity))).ToString();
                        }
                        else
                        {

                            cart.Add(temp);
                        }
                        this._cartService.SaveCartToSession(cart);
                    }*/

                }
                return new JsonResult(new { message = "Sản phẩm đã được thêm vào giỏ hàng!" });
            }


        }

    
}
