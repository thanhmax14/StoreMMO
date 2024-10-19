using AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels;
using System.Runtime.InteropServices;

namespace StoreMMO.Web.Pages.Seller
{
    public class ViewDetailsProductModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IProductTypeService _productTypeService;
        private readonly IMapper _mapper;

        public ViewDetailsProductModel(IProductService productService, IProductTypeService productTypeService, IMapper mapper)
        {
            _productService = productService;
            _productTypeService = productTypeService;
            _mapper = mapper;
        }
        [BindProperty]
        public IEnumerable<ProductViewModels> Products { get; set; } // Sử dụng danh sách thay vì đối tượng đơn
        [BindProperty]
        public string ProductTypeName { get; set; }  // Để hiển thị tên loại sản phẩm

        public void OnGet(string id)
        {
            // Lấy danh sách sản phẩm dựa trên ProductTypeID
            var productsList = _productService.getProductsByTypeID(id);

            // Lấy tên loại sản phẩm
            var productType = _productTypeService.getByIDProduct(id);
            if (productType != null)
            {
                ProductTypeName = productType.Name; // Gán tên loại sản phẩm
            }

            if (productsList != null)
            {
                Products = _mapper.Map<IEnumerable<ProductViewModels>>(productsList); // Ánh xạ sang ProductViewModels
            }
        }
    }
}
