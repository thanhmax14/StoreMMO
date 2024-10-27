using AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels;
using System.Runtime.InteropServices;

namespace StoreMMO.Web.Pages.Seller
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _product;
        private readonly IMapper _mapper;
        private readonly IProductTypeService _productTypeService;


        public IEnumerable<ManageStoreViewModels> products = new List<ManageStoreViewModels>();
        public IEnumerable<ProductType> ProductTypes { get; set; }


        public IndexModel(IProductService product, IMapper mapper, IProductTypeService productTypeService)
        {
            _product = product;
            _mapper = mapper;
            _productTypeService = productTypeService;
        }

        public void OnGet()
        {
            //products = _product.ManageStore();
            products = _product.ManageStoreDetail();

        }
        public void OnPost() {
            
        }
        public IActionResult OnPostHideProduct(string Id)
        {
            var product = _product.getByIdProduct(Id);

            if (product != null)
            {
                product.Status = "Paid";
                _product.UpdateProduct(product);
            }

            // Trả về JSON để thông báo thành công
            return new JsonResult(new { success = true });
        }
    }
}
