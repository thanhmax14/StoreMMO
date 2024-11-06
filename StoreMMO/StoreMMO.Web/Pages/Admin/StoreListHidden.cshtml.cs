using BusinessLogic.Services.StoreMMO.Core.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
	[Authorize(Roles = "Admin")]
	public class StoreListHiddenModel : PageModel
    {
        private readonly IStoreService _storeService;
        public StoreListHiddenModel(IStoreService storeService)
        {
            _storeService = storeService;
        }
        public IEnumerable<StoreViewModels> list = new List<StoreViewModels>();
        [BindProperty]
        public string id { get; set; }
        [TempData]
        public string success { get; set; }
        [TempData]
        public string fail { get; set; }
        public void OnGet()
        {
            list = this._storeService.getAll("0");
        }

        public async Task<IActionResult> OnPost(string id)
        {
            // Sử dụng category service để lấy thông tin danh mục dựa trên id
            var category = _storeService.getByIdCategory(id);

            if (category != null)
            {
                category.IsAccept = "1"; // Ẩn danh mục
                //_storeService.UpdateStore(category); // Cập nhật danh mục
                _storeService.Update(category);

                success = "Active Store successfully";
            }
            else
            {
                fail = "Active failed Store";
            }

            // Chuyển hướng lại trang danh sách danh mục ẩn sau khi xử lý
            return RedirectToPage("StoreListHidden");
        }
    }


    }

