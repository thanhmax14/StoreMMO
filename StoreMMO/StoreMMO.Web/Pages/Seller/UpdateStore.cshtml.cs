using BusinessLogic.Services.StoreMMO.Core.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Web.Models.ViewModels;

namespace StoreMMO.Web.Pages.Seller
{
    public class UpdateStoreModel : PageModel
    {
        private readonly IStoreService _storeServices;

        public UpdateStoreModel(IStoreService storeService)
        {
            _storeServices = storeService;
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
                    // Tạo tên file duy nhất để lưu vào máy chủ
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(input.InputImage.FileName);
                    var filePath = Path.Combine("wwwroot/images", fileName);

                    // Lưu file vào thư mục wwwroot/images
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        input.InputImage.CopyTo(stream);
                    }

                    // Lưu đường dẫn ảnh vào cơ sở dữ liệu
                    storeDetail.Img = "/images/" + fileName;
                }

                // Cập nhật dữ liệu khác
                storeDetail.Name = input.Name;
                storeDetail.SubDescription = input.SubDescription;
                storeDetail.DescriptionDetail = input.DescriptionDetail;
                storeDetail.CreatedDate = input.CreatedDate;

                // Gọi service để update
                var result = _storeServices.UpdateStore(storeDetail);

                if (result != null)
                {
                    success = "Update success!";
                }
                else
                {
                    fail = "Update fail!";
                }

                return RedirectToPage("/Seller/Store", new { storeDetailId = input.Id });
            }
            catch (Exception ex)
            {
                fail = $"Có lỗi xảy ra: {ex.Message}";
                return Page();
            }
        }
    }
}
