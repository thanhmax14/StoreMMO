using AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StoreMMO.Web.Pages.Seller
{
    public class HiddentProductModel : PageModel
    {
        private readonly IProductTypeService _productTypeService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public HiddentProductModel(IProductTypeService productTypeService, IProductService productService, IMapper mapper)
        {
            _productTypeService = productTypeService;
            _productService = productService;
            _mapper = mapper;
        }

        public void OnGet(string id)
        {
            var obj = _productTypeService.getByIDProduct(id);
        }
    }
}
