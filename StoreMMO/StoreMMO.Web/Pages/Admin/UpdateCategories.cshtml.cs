using BusinessLogic.Services.StoreMMO.Core.Categorys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StoreMMO.Web.Pages.Admin
{
    public class UpdateCategoriesModel : PageModel
    {
        private readonly ICategoryService _categoryServices;

        public UpdateCategoriesModel(ICategoryService categoryServices)
        {
            _categoryServices = categoryServices;
        }
        public void OnGet()
        {
        }

    }
}
