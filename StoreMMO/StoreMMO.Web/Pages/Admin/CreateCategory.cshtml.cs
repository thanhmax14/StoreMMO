using BusinessLogic.Services.StoreMMO.Core.Categorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels.Admin;

namespace StoreMMO.Web.Pages.Admin
{
	[Authorize(Roles = "Admin")]

	public class CreateCategoryModel : PageModel
    {
        private readonly ICategoryService _categoryServices;

        public CreateCategoryModel(ICategoryService categoryServices)
        {
            _categoryServices = categoryServices;
        }
        [TempData]
        public string success { get; set; }
        [TempData]
        public string fail { get; set; }

        [BindProperty]
        public CategoryUpdate input { get; set; }
        //public CategoryViewModels category { get; set; }

      

        public async Task<IActionResult> OnPost()
        {
            var check = new CategoryViewModels();
          //  var check = this._categoryServices.getByIdCategory(input.Id);
            // check = new Category {
            check.Id = Guid.NewGuid().ToString();
            check.Name = input.Name;
            check.IsActive = true;
            //     };
            var a = this._categoryServices.GetAll();
            var nb = a;
            if (a.Any(c =>

            c.Name == input.Name))
            {
                fail = "Category đã tồn tại ";
                return Page();
            }
            else
            {

                var update = this._categoryServices.AddCategory(check);
                if (update != null)
                {
                    success = "Addnew thông tin thành công";
                }
                else
                {
                    fail = "Addnew thông tin thất bại";
                }
            }
          //  return RedirectToPage("CreateCategory");
            // Điều hướng về cùng trang để hiển thị thông báo
            return RedirectToPage("CreateCategory");

        }
    }
}
