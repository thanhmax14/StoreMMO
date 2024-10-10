using BusinessLogic.Services.StoreMMO.Core.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;

namespace StoreMMO.Web.Pages.Seller
{
    public class IndexModel : PageModel
    {
        private readonly IProductsService _product;

        public IndexModel(IProductsService products)
        {
            this._product = products;
        }
        public IEnumerable<Product> products = new List<Product>();
        public void OnGet()
        {

            products = _product.GetAllProduct();

        }
        public void OnPost() {
            
        }
    }
}
