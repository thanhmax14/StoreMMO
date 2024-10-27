using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels;
using System.Linq;

namespace StoreMMO.Web.Pages.Seller
{
    public class ViewStoreModel : PageModel
    {
        private readonly IStoreService _storeServices;
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        public ViewStoreModel(IStoreService storeService, AppDbContext context, IProductService productService)
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

        public IActionResult OnPost()
        {
            var storedetailid = input.Id;
            try
            {
                var storeDetail = _storeServices.getStoreDetailById(storedetailid);
                if (storeDetail == null)
                {
                    fail = "Storedetail not found.";
                    return Page();
                }

                // Check if the store exists
                var store = _context.Stores.FirstOrDefault(s => s.Id == storeDetail.StoreId);
                if (store == null)
                {
                    fail = "Store not found.";
                    return Page();
                }

                // Update the ModifiedDate for the store
                store.ModifiedDate = DateTime.Now;

                // Handle the image upload
                if (input.InputImage != null)
                {
                    // Validate file extension
                    var allowedExtensions = new[] { ".jpg", ".png" };
                    var fileExtension = Path.GetExtension(input.InputImage.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        fail = "Only .jpg and .png files are allowed.";
                        return Page();
                    }

                    // Create a unique file name and save the image
                    var fileName = Guid.NewGuid().ToString() + fileExtension;
                    var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                    // Ensure the uploads folder exists
                    if (!Directory.Exists(uploadsFolderPath))
                    {
                        Directory.CreateDirectory(uploadsFolderPath);
                    }

                    var filePath = Path.Combine(uploadsFolderPath, fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        input.InputImage.CopyTo(stream);
                    }

                    // Update the image path in storeDetail
                    storeDetail.Img = "/images/" + fileName;
                }

                // Update the store detail properties
                //storeDetail.Name = input.Name;
                //storeDetail.SubDescription = input.SubDescription;
                //storeDetail.DescriptionDetail = input.DescriptionDetail;
                //storeDetail.CategoryId = input.CategoryId;
                //storeDetail.StoreTypeId = input.StoreTypeId;
                //storeDetail.ModifiedDate = DateTime.Now;
                var storedetails = _context.StoreDetails.FirstOrDefault(x => x.Id == storeDetail.Id);

                //   var storedetails = new StoreDetail();
                storedetails.Name = input.Name;
                storedetails.SubDescription = input.SubDescription;
                storedetails.DescriptionDetail = input.DescriptionDetail;
                storedetails.CategoryId = input.CategoryId;
                storedetails.StoreTypeId = input.StoreTypeId;
                storedetails.ModifiedDate = DateTime.Now;
                storedetails.Img = storeDetail.Img;
                _context.StoreDetails.Update(storedetails);
                // Save changes to the database
                _context.SaveChanges();

                success = "Update successful!";
                return RedirectToPage("/Seller/index");
            }
            catch (Exception ex)
            {
                fail = $"An error occurred: {ex.Message}";
                return Page();
            }
        }

    }
}
