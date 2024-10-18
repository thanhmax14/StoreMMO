using AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels;

namespace StoreMMO.Web.Pages.Seller
{
    public class CreateProductModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IProductTypeService _productTypeService;

        public CreateProductModel(IProductService productService, IMapper mapper, IProductTypeService productTypeService)
        {
            _productService = productService;
            _mapper = mapper;
            _productTypeService = productTypeService;
        }

        [BindProperty]
        public InputProductViewModel CreateProduct { get; set; }
        [BindProperty]
        public IEnumerable<ProductType> ProductTypes { get; set; }
        public void OnGet()
        {
            var productTypesViewModels = _productTypeService.GetAllProduct();

            // Ánh xạ danh sách ProductTypes
            ProductTypes = _mapper.Map<IEnumerable<ProductType>>(productTypesViewModels);
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var productViewModels = _mapper.Map<ProductViewModels>(CreateProduct);  // Map ProductViewModel to ProductViewModels
            _productService.AddProduct(productViewModels);  // Use the mapped ProductViewModels object
            return RedirectToPage("/Seller/Index");
        }
    }
}
