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

        [TempData]
        public string Message { get; set; }
        [TempData]
        public string Error { get; set; }
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

            var cate = _categoryServices.getByIdCategory(id);
            cate.IsActive = false;
            var result = _categoryServices.UpdateCategory(cate);
            if (result == null)
            {
                Error = "have error to hidden";
                return RedirectToPage("/Admin/CategoriesList");
            }
            else
                {
                Message = "Hidden success";
                return RedirectToPage("/Admin/CategoriesList");
            }

           
        }
    }
}
