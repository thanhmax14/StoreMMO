using BusinessLogic.Services.StoreMMO.Core.Categorys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
    public class CategoriesListHiddenModel : PageModel
    {
        private readonly ICategoryService _categoryServices;


        public CategoriesListHiddenModel(ICategoryService categoryServices)
        {
            _categoryServices = categoryServices;
        }

        public IEnumerable<CategoryViewModels> listcate = new List<CategoryViewModels>();
        public void OnGet()
        {
            listcate = this._categoryServices.GetCategoryIsHidden();
        }

        public IActionResult OnPostHidden(string id)
        {
            var cate = _categoryServices.getByIdCategory(id);
            cate.IsActive = true;
            var result = _categoryServices.UpdateCategory(cate);


            // Nếu thành công, chuyển hướng lại danh sách categories
            return RedirectToPage("/Admin/CategoriesList");

        }

    }



    }

