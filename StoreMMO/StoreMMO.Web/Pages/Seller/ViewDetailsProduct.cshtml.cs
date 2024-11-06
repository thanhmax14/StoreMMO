using AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels;
using System.Runtime.InteropServices;

namespace StoreMMO.Web.Pages.Seller
{
	[Authorize(Roles = "Seller")]
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
        public string ProductTypeName { get; set; }
        [BindProperty]
        public string ProductTypeId { get; set; } // Để hiển thị tên loại sản phẩm

        public void OnGet(string id)
        {
            // Lấy tên loại sản phẩm
            var productType = _productTypeService.getByIDProduct(id);
            if (productType != null)
            {
                ProductTypeName = productType.Name; // Gán tên loại sản phẩm
                ProductTypeId = productType.Id;
            }

            // Lấy danh sách sản phẩm dựa trên ProductTypeID
            var productsList = _productService.getProductsByTypeID1(id); // Thay đổi ở đây

            if (productsList != null)
            {
                Products = _mapper.Map<IEnumerable<ProductViewModels>>(productsList)
                      .OrderByDescending(x => x.Status == "New")
                      .ThenByDescending(x => x.Status == "Paid");
                // Ánh xạ sang ProductViewModels
            }
        }
        public IActionResult OnPost(string id, string account, string pwd, string status)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(account) || string.IsNullOrEmpty(pwd))
            {
                return new JsonResult(new { success = false, message = "Missing required fields." });
            }

            var existingProduct = _productService.getByIdProduct(id);
            if (existingProduct == null)
            {
                return new JsonResult(new { success = false, message = "Product not found." });
            }
            var ProductTypeId = _productTypeService.getByIDProduct(existingProduct.ProductTypeId);
            //if (int.TryParse(ProductTypeId.Stock, out int stockValue))
            //{
            //    if (string.Equals(existingProduct.Status, "Paid", StringComparison.OrdinalIgnoreCase))
            //    {
            //        // Trừ Stock nếu Status là "Paid"
            //        ProductTypeId.Stock = (stockValue + 1).ToString(); // Đảm bảo Stock không âm
            //    }
            //    else if (string.Equals(existingProduct.Status, "New", StringComparison.OrdinalIgnoreCase))
            //    {
            //        // Tăng Stock nếu Status là "New"
            //        ProductTypeId.Stock = (stockValue - 1).ToString();
            //    }

            //    // Cập nhật lại giá trị Stock vào cơ sở dữ liệu
            //    _productTypeService.Update(ProductTypeId);
            //}
            //_mapper.Map(ProductTypeId, existingProduct);
            // Cập nhật thông tin sản phẩm
            existingProduct.Account = account;
            existingProduct.Pwd = pwd;
            //existingProduct.Status = status;

            // Cập nhật ProductType trong cơ sở dữ liệu
            _productService.UpdateProduct(existingProduct);
            return new JsonResult(new { success = true });
        }
        public IActionResult OnPostHidden(string Productid)
        {
            // Lấy sản phẩm theo ID
            var obj = _productService.getByIdProduct(Productid);

            // Kiểm tra sản phẩm tồn tại và lấy ProductTypeId tương ứng
            if (obj != null)
            {
                var productType = _productTypeService.getByIDProduct(obj.ProductTypeId);

                if (productType != null)
                {
                    // Nếu Stock là số, tiếp tục xử lý
                    if (int.TryParse(productType.Stock, out int stockValue))
                    {
                        // Nếu trạng thái hiện tại là "New", thì giảm Stock đi 1
                        if (string.Equals(obj.Status, "New", StringComparison.OrdinalIgnoreCase))
                        {
                            // Đảm bảo Stock không xuống dưới 0
                            productType.Stock = (stockValue > 0 ? stockValue - 1 : 0).ToString();

                            // Cập nhật trạng thái sản phẩm thành "Paid"
                            obj.Status = "Paid";

                            // Cập nhật thay đổi vào cơ sở dữ liệu
                            _productService.UpdateProduct(obj);
                            _productTypeService.Update(productType);

                            return new JsonResult(new { success = true, message = "Product updated successfully." });
                        }
                        else
                        {
                            return new JsonResult(new { success = false, message = "Only 'New' products can be hidden." });
                        }
                    }
                    else
                    {
                        return new JsonResult(new { success = false, message = "Stock value is invalid." });
                    }
                }
            }

            return new JsonResult(new { success = false, message = "Product not found." });
        }

    }
}
