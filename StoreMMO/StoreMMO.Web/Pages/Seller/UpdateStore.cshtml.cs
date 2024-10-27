using BusinessLogic.Services.StoreMMO.Core.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Web.Models.ViewModels;

namespace StoreMMO.Web.Pages.Seller
{
    public class UpdateStoreModel : PageModel
    {
        private readonly IStoreService _storeServices;
        private readonly AppDbContext _context;
        public UpdateStoreModel(IStoreService storeService, AppDbContext context)
        {
            _storeServices = storeService;
            _context = context;
        }

        [TempData]
        public string success { get; set; }

        [TempData]
        public string fail { get; set; }

        [BindProperty]
        public StoreUpdateViewModels input { get; set; }

        public IActionResult OnGet(string storeDetailId)
        {
            // Lấy thông tin từ database dựa trên storeDetailId
            var storeDetail = _storeServices.getStoreDetailById(storeDetailId);
            if (storeDetail == null)
            {
                fail = "Store not found";
                return RedirectToPage("/Error");
            }

            // Gán giá trị cho `input` để tự động điền sẵn form
            input = new StoreUpdateViewModels
            {
                Id = storeDetail.Id,
                Name = storeDetail.Name,
                SubDescription = storeDetail.SubDescription,
                DescriptionDetail = storeDetail.DescriptionDetail,
                CreatedDate = storeDetail.CreatedDate ?? DateTimeOffset.Now, // Gán mặc định nếu null
                Img = storeDetail.Img
            };

            return Page();
        }
        public IActionResult OnPost()
        {
            try
            {
                // Lấy thông tin hiện tại của StoreDetail từ DB
                var storeDetail = _storeServices.getStoreDetailById(input.Id);

                if (storeDetail == null)
                {
                    fail = "Store not found";
                    return Page();
                }

                // Kiểm tra nếu có file được upload
                if (input.InputImage != null)
                {
                    // Kiểm tra định dạng file hợp lệ (chỉ cho phép .jpg, .png)
                    var allowedExtensions = new[] { ".jpg", ".png" };
                    var fileExtension = Path.GetExtension(input.InputImage.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        fail = "Only .jpg and .png files are allowed.";
                        return Page();
                    }

                    // Tạo tên file duy nhất để lưu vào máy chủ
                    var fileName = Guid.NewGuid().ToString() + fileExtension;
                    var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                    // Tạo thư mục nếu chưa tồn tại
                    if (!Directory.Exists(uploadsFolderPath))
                    {
                        Directory.CreateDirectory(uploadsFolderPath);
                    }

                    var filePath = Path.Combine(uploadsFolderPath, fileName);

                    // Lưu file vào thư mục wwwroot/images
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        input.InputImage.CopyTo(stream);
                    }

                    // Lưu đường dẫn ảnh vào cơ sở dữ liệu
                    storeDetail.Img = "/images/" + fileName;

                    // Lấy kích thước ảnh
                    using (var image = System.Drawing.Image.FromFile(filePath))
                    {
                        var width = image.Width;
                        var height = image.Height;

                        // Bạn có thể lưu width và height vào cơ sở dữ liệu hoặc sử dụng chúng theo cách bạn muốn
                        // Ví dụ: hiển thị trong log hoặc gán vào view model
                        Console.WriteLine($"Image dimensions: {width}x{height}");
                    }
                }

                // Cập nhật các trường khác
                storeDetail.Name = input.Name;
                storeDetail.SubDescription = input.SubDescription;
                storeDetail.DescriptionDetail = input.DescriptionDetail;
                storeDetail.CreatedDate = input.CreatedDate;

                // Gọi service để update
                var result = _storeServices.UpdateStore(storeDetail);

                if (result != null)
                {
                    success = "Update successful!";
                }
                else
                {
                    fail = "Failed to update store details!";
                }

                return RedirectToPage("/Seller/Store", new { storeDetailId = input.Id });
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần thiết
                fail = $"An error occurred: {ex.Message}";
                return Page();
            }
        }
    }
}
