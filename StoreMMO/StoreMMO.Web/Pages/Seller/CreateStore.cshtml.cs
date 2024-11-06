using BusinessLogic.Services.StoreMMO.Core.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels;

namespace StoreMMO.Web.Pages.Seller
{
    public class CreateStoreModel : PageModel
    {
        private readonly IStoreService _storeServices;
        private readonly AppDbContext _context;
        public CreateStoreModel(IStoreService storeService, AppDbContext context)
        {
            _storeServices = storeService;
            _context = context;
        }
        public IEnumerable<StoreUpdateViewModels> listS = new List<StoreUpdateViewModels>();

        [TempData]
        public string success { get; set; }

        [TempData]
        public string fail { get; set; }

        [BindProperty]
        public StoreUpdateViewModels input { get; set; }
        public void OnGet()
        {
            var categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            var storeTypes = _context.StoreTypes // Lấy StoreTypes từ database
                .Select(st => new SelectListItem
                {
                    Value = st.Id.ToString(),
                    Text = st.Name
                }).ToList();

            input = new StoreUpdateViewModels
            {
                CategoryOptions = categories,
                StoreTypeOptions = storeTypes // Gán danh sách StoreTypes vào StoreTypeOptions
            };
        }

        public IActionResult OnPost()
        {
            // Bước 1: Lấy UserId từ session
            var userId = HttpContext.Session.GetString("UserID");
            //var userId = "1f0dbbe2-2a81-43e9-8272-117507ac9c45";
            if (string.IsNullOrEmpty(userId))
            {
                // Xử lý trường hợp người dùng chưa đăng nhập
                ModelState.AddModelError(string.Empty, "Vui lòng đăng nhập trước khi tạo cửa hàng.");
                return Page();
            }

            // Bước 2: Tạo mới bản ghi trong bảng Stores
            var store = new Store
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,            // ID người dùng từ session, kiểu string
                CreatedDate = DateTime.Now, // Ngày tạo là ngày hiện tại
                IsAccept = "PENDING",       // Trạng thái mặc định là PENDING
                ModifiedDate = null         // Chưa có giá trị ModifiedDate
            };


            _context.Stores.Add(store);
            _context.SaveChanges();

            // Bước 3: Tạo mới bản ghi trong bảng StoreDetails
            var storeDetail = new StoreDetail
            {
                Id = Guid.NewGuid().ToString(),
                StoreId = store.Id,  // StoreId từ cửa hàng vừa tạo
                CategoryId = input.CategoryId,  // Lấy từ input của người dùng
                StoreTypeId = input.StoreTypeId,  // Lấy từ input của người dùng
                Name = input.Name.Trim(),
                SubDescription = input.SubDescription.Trim(),
                DescriptionDetail = input.DescriptionDetail.Trim(),
                CreatedDate = DateTime.Now,
                ModifiedDate = null

            };

            // Bước 4: Xử lý upload ảnh
            if (input.InputImage != null)
            {
                var allowedExtensions = new[] { ".jpg", ".png" };
                var fileExtension = Path.GetExtension(input.InputImage.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError(string.Empty, "Chỉ chấp nhận các tệp .jpg và .png.");
                    return Page();
                }

                var fileName = Guid.NewGuid().ToString() + fileExtension;
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var filePath = Path.Combine(uploadsFolderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    input.InputImage.CopyTo(stream);
                }

                storeDetail.Img = "/images/" + fileName;
            }

            _context.StoreDetails.Add(storeDetail);
            _context.SaveChanges();

            success = "Create Successfully!";
            return RedirectToPage("/seller/createstore");
        }
    }
}
