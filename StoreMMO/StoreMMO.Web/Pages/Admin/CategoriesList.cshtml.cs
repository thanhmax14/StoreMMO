using BusinessLogic.Services.StoreMMO.Core.Categorys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;
using static QRCoder.PayloadGenerator;

namespace StoreMMO.Web.Pages.Admin
{
    public class CategoriesListModel : PageModel
    {
        private readonly ICategoryService _categoryServices;


        public CategoriesListModel(ICategoryService categoryServices)
        {
            _categoryServices = categoryServices;
        }

        public IEnumerable<CategoryViewModels> listcate = new List<CategoryViewModels>();
        public void OnGet()
        {
            listcate = this._categoryServices.GetCategoryIsActive();
        }
        [BindProperty]
        public string id { get; set; }

        [TempData]
        public string success { get; set; }

        [TempData]
        public string fail { get; set; }
        public IActionResult OnPostHidden(string id)
        {

            var category = _categoryServices.getById(id);

            if (category != null)
            {
                category.IsActive = false; // Ẩn danh mục
                var result = _categoryServices.UpdateCategory(category); // Cập nhật danh mục

                success = "Hide category successfully";
            }
            else
            {
                fail = "Hide failed category";
            }

            // Chuyển hướng lại trang danh sách danh mục ẩn sau khi xử lý
            return RedirectToPage("CategoriesList");
           
           
            


            // Nếu thành công, chuyển hướng lại danh sách categories
           
        }
    }
}
