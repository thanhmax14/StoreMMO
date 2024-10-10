using BusinessLogic.Services.StoreMMO.Core.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;

namespace StoreMMO.Web.Pages.Seller
{
    public class UpdateProductModel : PageModel
    {
        private readonly IProductsService _product;
        public UpdateProductModel(IProductsService products)
        {
            this._product = products;
        }

       public IEnumerable<Product> products1 = new List<Product>();

        public void OnGet(string id)
        {
            var obj = _product.getByIDProduct(id);
        }

        public void OnPost(string id) {
            var obj = _product.getByIDProduct(id);
            if (id == null)
            {
                
            }
        }
    }
}
