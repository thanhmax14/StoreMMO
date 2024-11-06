using AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Home
{
    public class ViewAllStoreSellerModel : PageModel
    {
        private readonly IProductService _product;
        private readonly IMapper _mapper;
        private readonly IProductTypeService _productTypeService;


        public IEnumerable<ManageStoreViewModels> products = new List<ManageStoreViewModels>();
        public IEnumerable<ProductType> ProductTypes { get; set; }


        public ViewAllStoreSellerModel(IProductService product, IMapper mapper, IProductTypeService productTypeService)
        {
            _product = product;
            _mapper = mapper;
            _productTypeService = productTypeService;
        }

        public void OnGet()
        {
            var userId = HttpContext.Session.GetString("UserID");
            //products = _product.ManageStore();
            //products = _product.ManageStore(userId);

        }
        public void OnPost()
        {

        }
    }
}
