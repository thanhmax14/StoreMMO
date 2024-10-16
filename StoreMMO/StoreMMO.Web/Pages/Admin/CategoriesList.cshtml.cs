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
    }
}
