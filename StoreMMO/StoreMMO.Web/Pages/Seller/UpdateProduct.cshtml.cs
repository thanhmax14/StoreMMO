using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;

namespace StoreMMO.Web.Pages.Seller
{
    public class UpdateProductModel : PageModel
    {
        private readonly IProductTypeService _product;
        public UpdateProductModel(IProductTypeService products)
        {
            this._product = products;
        }

       public IEnumerable<ProductType> products1 = new List<ProductType>();

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
