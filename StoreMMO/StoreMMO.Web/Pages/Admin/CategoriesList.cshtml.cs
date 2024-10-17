using BusinessLogic.Services.StoreMMO.Core.Categorys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;

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

      //  public IActionResult OnPostHidden(int id)
      //  {
            // Gọi service để ẩn category với ID tương ứng
          //  var result = _categoryServices.HideCategory(id);

            //if (result)
            //{
            //    // Nếu thành công, có thể chuyển hướng hoặc làm gì đó tùy ý
            //    return RedirectToPage("/Admin/CategoriesList");
            //}
            //else
            //{
            //    // Nếu thất bại, có thể hiển thị thông báo lỗi
            //    ModelState.AddModelError("", "Failed to hide the category.");
            //    return Page();
            //}
        //}


    }
}
