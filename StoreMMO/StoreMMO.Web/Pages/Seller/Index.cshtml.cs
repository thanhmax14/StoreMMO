using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;

namespace StoreMMO.Web.Pages.Seller
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _product;

        public IndexModel(IProductService products)
        {   
            this._product = products;
        }
        public IEnumerable<ProductType> products = new List<ProductType>();
        public void OnGet()
        {

        

        }
        public void OnPost() {
            
        }
    }
}
