using AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using BusinessLogic.Services.StoreMMO.Core.StoreDetails;
using BusinessLogic.Services.StoreMMO.Core.Stores;
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
        private readonly IStoreDetailsService _storeDetailsService;

        public UpdateProductModel(IProductService product, IMapper mapper, IProductTypeService productTypeService, IStoreDetailsService storeDetailsService)
        {
            _product = product;
            _mapper = mapper;
            _productTypeService = productTypeService;
            _storeDetailsService = storeDetailsService;
        }

        [BindProperty]
        public IEnumerable<ViewProductModels> EditProduct { get; set; }
        [BindProperty]
        public IEnumerable<InputProductTypeViewModel> ProductTypes { get; set; }

        public void OnGet(string id)
        {
            EditProduct = _product.GetProductsByStoreId(id);
            var obj = _storeDetailsService.GetAllStoreDetails();
            ProductTypes = _mapper.Map<IEnumerable<InputProductTypeViewModel>>(obj);
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
