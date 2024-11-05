using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels;

namespace StoreMMO.Web.Pages.Account
{
    public class ViewAllStoreSellerModel : PageModel
    {
        private readonly IStoreService _storeServices;
        private readonly AppDbContext _context;
        private readonly IProductService _productService;

        public IEnumerable<ManageStoreViewModels> products = new List<ManageStoreViewModels>();
        public ViewAllStoreSellerModel(IStoreService storeService, AppDbContext context, IProductService productService)
        {
            _storeServices = storeService;
            _context = context;
            _productService = productService;
        }

        [TempData]
        public string success { get; set; }

        [TempData]
        public string fail { get; set; }

        [BindProperty]
        public StoreUpdateViewModels input { get; set; }

        public IActionResult OnGet(string storeDetailId)
        {
            var storeDetail = _storeServices.getStoreDetailById(storeDetailId); // m dang dung ham cuar storedeatilrepository ma co dung hang duc anh dau 
            if (storeDetail == null)
            {
                fail = "Store not found";
                return RedirectToPage("/Error");
            }

            // Gán d? li?u vào input ?? hi?n th? s?n thông tin khi vào trang
            input = new StoreUpdateViewModels
            {
                Id = storeDetail.Id,
                Name = storeDetail.Name,
                SubDescription = storeDetail.SubDescription,
                DescriptionDetail = storeDetail.DescriptionDetail,
                CreatedDate = storeDetail.CreatedDate ?? DateTime.Now,
                CategoryId = storeDetail.CategoryId,
                StoreTypeId = storeDetail.StoreTypeId,
                //IsActive = storeDetail.IsActive,
                Img = storeDetail.Img
            };

            // Load danh sách Category và StoreType cho các dropdown
            input.CategoryOptions = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            input.StoreTypeOptions = _context.StoreTypes
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();

            return Page();
        }
    }
}
