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
        public InputProductViewModel CreateProduct { get; set; }  // Sử dụng đối tượng đơn thay vì IEnumerable
        [BindProperty]
        public IEnumerable<ProductType> ProductTypes { get; set; }
        public void OnGet(string id)
        {
            // Lấy danh sách ProductType
            ProductTypes = _productTypeService.GetAllProduct();
            var obj = _productTypeService.getByIDProduct(id);
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            // Thực hiện việc tạo product
            var productViewModels = _mapper.Map<ProductViewModels>(CreateProduct); // Map từ InputProductViewModel sang Product

            // Lưu product mới
            _productService.AddProduct(productViewModels);

            // Redirect về trang Index sau khi tạo thành công
            return RedirectToPage("/Seller/Index");
        }
    }
}
