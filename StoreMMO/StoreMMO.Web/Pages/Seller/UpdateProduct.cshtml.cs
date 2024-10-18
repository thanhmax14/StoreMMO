using AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System.Collections;

namespace StoreMMO.Web.Pages.Seller
{
    public class UpdateProductModel : PageModel
    {
        private readonly IProductService _product;
        private readonly IMapper _mapper;
        private readonly IProductTypeService _productTypeService;

        public UpdateProductModel(IProductService product, IMapper mapper, IProductTypeService productTypeService)
        {
            _product = product;
            _mapper = mapper;
            _productTypeService = productTypeService;
        }

        [BindProperty]
        public IEnumerable <InputProductTypeViewModel> EditProduct { get; set; }
        [BindProperty]
        public IEnumerable<ProductType> ProductTypes { get; set; }

        public void OnGet(string id)
        {
            // Lấy đối tượng Product từ service
            var productType = _productTypeService.getByIDProduct(id); // Giả định trả về một đối tượng đơn lẻ

            // Kiểm tra nếu product tồn tại (không null)
            if (productType != null)
            {
                // Sử dụng AutoMapper để map từ ProductType sang InputProductTypeViewModel
                EditProduct = new List<InputProductTypeViewModel> { _mapper.Map<InputProductTypeViewModel>(productType) };
            }
            /*var productTypesViewModels = _productTypeService.GetAllProduct();

            // Ánh xạ danh sách ProductTypes
            ProductTypes = _mapper.Map<IEnumerable<ProductType>>(productTypesViewModels);*/
        }

        public IActionResult OnPost(string id)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page(); // Trả về trang nếu ModelState không hợp lệ
            //}
            // Lấy đối tượng Product hiện tại để cập nhật
            var existingProduct = _product.getByIdProduct(id);
            if (existingProduct == null)
            {
                return RedirectToPage("/Seller/UpdateProduct"); // Không tìm thấy sản phẩm
            }

            // Sử dụng AutoMapper để map từ InputProductViewModel về Product
            var productToUpdate = _mapper.Map<ProductViewModels>(EditProduct);

            // Cập nhật sản phẩm
            _product.UpdateProduct(productToUpdate);

            return RedirectToPage("/Seller/Index");
        }
    }
}
