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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Lấy thông tin hiện tại của StoreDetail từ DB
                var storeDetail = _storeServices.getStoreDetailById(input.Id);

                if (storeDetail == null)
                {
                    fail = "Store not found";
                    return Page();
                }

                // Cập nhật dữ liệu
                storeDetail.Name = input.Name;
                storeDetail.SubDescription = input.SubDescription;
                storeDetail.DescriptionDetail = input.DescriptionDetail;
                storeDetail.CreatedDate = input.CreatedDate;
                storeDetail.Img = input.Img;

                // Gọi service để update
                var result = _storeServices.UpdateStore(storeDetail);

                if (result != null)
                {
                    success = "Update thành công!";
                }
                else
                {
                    fail = "Cập nhật thất bại!";
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
