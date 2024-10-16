using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Seller
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _product;

        public IndexModel(IProductService products)
        {   
            this._product = products;
        }
        public IEnumerable<ProductTypesViewModels> products = new List<ProductTypesViewModels>();
        public void OnGet()
        {


        }
        public void OnPost() {
            
        }
    }
}
