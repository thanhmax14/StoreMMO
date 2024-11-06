using AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels;
using static QRCoder.PayloadGenerator;

namespace StoreMMO.Web.Pages.Seller
{
	[Authorize(Roles = "Seller")]
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
        public string ProductTypeName { get; set; }

        [BindProperty]
        public string ProductTypeId { get; set; } // Để lưu ID loại sản phẩm
        [BindProperty]
        public InputProductViewModel CreateProduct { get; set; }  // Sử dụng đối tượng đơn thay vì IEnumerable
        [BindProperty]
        public IEnumerable<ProductType> ProductTypes { get; set; }
        public void OnGet(string id)
        {
            var ProductType = _productTypeService.getByIDProduct(id);
            if (ProductType != null)
            {
                CreateProduct = new InputProductViewModel
                {
                    ProductTypeId = ProductType.Id,
                    ProductTypeName = ProductType.Name,
                };
            }
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
            var account = productViewModels.Account;
            var productTypeId = productViewModels.ProductTypeId;
            var existingProduct = _productService.GetByAccount(account, productTypeId); 
            if (existingProduct != null)
            {
                // Thêm thông báo lỗi vào ModelState
                fail = "Account already";
                return Page(); // Hoặc RedirectToPage() nếu bạn muốn chuyển hướng
            }
            // Lấy loại sản phẩm hiện tại từ ProductTypeId
            // Lấy loại sản phẩm hiện tại từ ProductTypeId
            var existingProductType = _productTypeService.getByIDProduct(CreateProduct.ProductTypeId);

            if (existingProductType != null)
            {
                // Kiểm tra nếu Stock là null hoặc trống, đặt giá trị khởi đầu là 1
                if (string.IsNullOrEmpty(existingProductType.Stock))
                {
                    existingProductType.Stock = "1"; // Đặt stock ban đầu là 1 nếu chưa có giá trị
                }
                else if (int.TryParse(existingProductType.Stock, out int stockValue))
                {
                    stockValue++; // Tăng giá trị Stock lên 1 nếu đã có giá trị
                    existingProductType.Stock = stockValue.ToString(); // Cập nhật lại thành chuỗi
                }

                // Cập nhật loại sản phẩm với Stock mới
                _productTypeService.Update(existingProductType);
                _productService.AddProduct(productViewModels);
            }
            else
            {
                // Nếu không tìm thấy loại sản phẩm, thêm sản phẩm mới
                existingProductType.Stock = "1"; // Đặt stock ban đầu nếu là sản phẩm mới
                _productTypeService.AddProduct(existingProductType);
            }


            // Redirect về trang Index sau khi tạo thành công
            return RedirectToPage("/Seller/Index");
        }
    }
}
