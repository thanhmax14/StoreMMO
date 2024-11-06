using AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using BusinessLogic.Services.StoreMMO.Core.StoreDetails;
using BusinessLogic.Services.StoreMMO.Core.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System.Collections;

namespace StoreMMO.Web.Pages.Seller
{
	[Authorize(Roles = "Seller")]
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
            if (id.Split('$').Length < 4) return new JsonResult(new { success = false });

            var proID = id.Split('$')[0];
            var Price = id.Split('$')[1];
            var ProductName = id.Split('$')[2];
            var isActive = id.Split('$')[3];

            var existingProduct = _productTypeService.getByIDProduct(proID);
            if (existingProduct == null)
            {
                return new JsonResult(new { success = false });
            }

            // Map dữ liệu từ EditProduct vào ProductType tương ứng
          
            if (existingProduct != null)
            {
                existingProduct.Name = ProductName;
                existingProduct.Price = Double.Parse(Price);
                existingProduct.IsActive = isActive == "true"; // Cập nhật trạng thái
                // Cập nhật ProductType trong cơ sở dữ liệu
                _productTypeService.Update(existingProduct);
                return new JsonResult(new { success = true });

            }

            return new JsonResult(new { success = false });
        }
    }
}
