using AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.ProductConnects;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using BusinessLogic.Services.StoreMMO.Core.StoreDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.ProductsConnect;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Seller
{
    public class CreateProductTypeModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IProductTypeService _productTypeService;
        private readonly IProductConnectService _productConnectService;
        private readonly IStoreDetailsService _storeDetailsService;
        private readonly IMapper _mapper;

        public CreateProductTypeModel(IProductService productService, IProductTypeService productTypeService, IProductConnectService productConnectService, IStoreDetailsService storeDetailsService, IMapper mapper)
        {
            _productService = productService;
            _productTypeService = productTypeService;
            _productConnectService = productConnectService;
            _storeDetailsService = storeDetailsService;
            _mapper = mapper;
        }

        [BindProperty]
        public ProductTypesViewModels InputProductType { get; set; }

        [BindProperty]
        public ProductConnectViewModels ProductConnectViewModels { get; set; }
        [BindProperty]
        public string? StoreDetailId { get; set; }

        public void OnGet(string id)
        {
            var obj = _storeDetailsService.GetByIdStoDetails(id);
            if(obj != null)
            {
                InputProductType = new ProductTypesViewModels
                {
                    Id = Guid.NewGuid().ToString(),
                    StoreDetailId = obj.Id,
                };
                ProductConnectViewModels = new ProductConnectViewModels
                {
                    Id = Guid.NewGuid().ToString(),
                };

            }
        }
        public IActionResult OnPost()
        {
            InputProductType.Id = Guid.NewGuid().ToString(); // Tạo ID mới
            InputProductType.IsActive = true;
            _productTypeService.AddProduct(InputProductType);
            var productConnect = new ProductConnectViewModels
            {
                Id = Guid.NewGuid().ToString(),
                // Gán các thuộc tính cần thiết từ ProductConnectViewModels
                ProductId = InputProductType.Id,
                StoreDetailId = InputProductType.StoreDetailId,
                // Gán thêm các thuộc tính khác nếu cần
            };

            // Thêm kết nối sản phẩm
            _productConnectService.AddProductConnect(productConnect);
            return RedirectToPage("/Seller/Index");
        }
    }
}
