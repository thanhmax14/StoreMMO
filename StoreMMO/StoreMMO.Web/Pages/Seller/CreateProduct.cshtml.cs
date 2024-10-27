using AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels;
using static QRCoder.PayloadGenerator;

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
        [TempData]
        public string fail { get; set; }
        [BindProperty]
        public InputProductViewModel CreateProduct { get; set; }  // Sử dụng đối tượng đơn thay vì IEnumerable
        [BindProperty]
        public IEnumerable<ProductType> ProductTypes { get; set; }
        public void OnGet(string id)
        {
            // Lấy danh sách ProductType
            ProductTypes = _productTypeService.GetAllProduct();
            
            
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            // Thực hiện việc tạo product
            var productViewModels = _mapper.Map<ProductViewModels>(CreateProduct); // Map từ InputProductViewModel sang Product
            productViewModels.Status = "New";
            if (CreateProduct.Account == productViewModels.Account)
            {
                // Thêm thông báo lỗi vào ModelState
                fail = "Account already";
                return Page(); // Hoặc RedirectToPage() nếu bạn muốn chuyển hướng
            }
            // Lấy loại sản phẩm hiện tại từ ProductTypeId
            var existingProductType = _productTypeService.getByIDProduct(CreateProduct.ProductTypeId);

            if (existingProductType != null)
            {
                // Tăng Stock nếu sản phẩm đã tồn tại
                if (int.TryParse(existingProductType.Stock, out int stockValue))
                {
                    stockValue++; // Tăng giá trị Stock lên 1
                    existingProductType.Stock = stockValue.ToString(); // Chuyển lại thành string
                    _productTypeService.Update(existingProductType); // Cập nhật sản phẩm
                }
            }
            else
            {
                // Nếu không tìm thấy loại sản phẩm, thêm sản phẩm mới
                existingProductType.Stock = "1"; // Đặt stock ban đầu nếu là sản phẩm mới
                _productService.AddProduct(productViewModels);
            }

            // Redirect về trang Index sau khi tạo thành công
            return RedirectToPage("/Seller/Index");
        }
    }
}
