using BusinessLogic.Services.StoreMMO.Core.Categorys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels.Admin;

namespace StoreMMO.Web.Pages.Admin
{
    public class UpdateCategoriesModel : PageModel
    {
        private readonly ICategoryService _categoryServices;

        public UpdateCategoriesModel(ICategoryService categoryServices)
        {
            _categoryServices = categoryServices;
        }
        [TempData]
        public string success { get; set; }
        [TempData]
        public string fail { get; set; }

        [BindProperty]
        public CategoryUpdate input { get; set; }
     public CategoryViewModels category { get; set; }

        public IActionResult OnGet(string categoryId)
        {
            category = this._categoryServices.getByIdCategory(categoryId); // Directly assign the returned object
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {

            var check = this._categoryServices.getByIdCategory(input.Id);
            // check = new Category {

                check.Name = input.Name;
                check.IsActive = true;
       //     };

            var update = this._categoryServices.UpdateCategory(check);
            if (update != null)
            {
                success = "Update thông tin thành công";
            }
            else
            {
                fail = "Update thông tin thất bại";
            }

            // Điều hướng về cùng trang để hiển thị thông báo
            return RedirectToPage("UpdateCategories", new { input.Id });

        }

    }
}
