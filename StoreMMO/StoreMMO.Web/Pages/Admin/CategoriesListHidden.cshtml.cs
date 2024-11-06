using BusinessLogic.Services.StoreMMO.Core.Categorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
	[Authorize(Roles = "Admin")]
	public class CategoriesListHiddenModel : PageModel
    {
        private readonly ICategoryService _categoryServices;

        public CategoriesListHiddenModel(ICategoryService categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [BindProperty]
        public string id { get; set; }

        [TempData]
        public string success { get; set; }

        [TempData]
        public string fail { get; set; }

        public IEnumerable<CategoryViewModels> listcate = new List<CategoryViewModels>();

        public void OnGet()
        {
            // Lấy danh sách các danh mục đã bị ẩn
            listcate = _categoryServices.GetCategoryIsHidden();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            // Sử dụng category service để lấy thông tin danh mục dựa trên id
            var category = _categoryServices.getByIdCategory(id);

            if (category != null)
            {
                category.IsActive = true; // Ẩn danh mục
                _categoryServices.UpdateCategory(category); // Cập nhật danh mục

                success = "Active category successfully";
            }
            else
            {
                fail = "Active failed category";
            }

            // Chuyển hướng lại trang danh sách danh mục ẩn sau khi xử lý
            return RedirectToPage("CategoriesListHidden");
        }
    }
}
