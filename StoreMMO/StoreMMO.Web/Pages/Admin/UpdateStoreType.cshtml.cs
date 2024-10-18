using BusinessLogic.Services.StoreMMO.Core.StoreTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels.Admin;

namespace StoreMMO.Web.Pages.Admin
{
    public class UpdateStoreTypeModel : PageModel
    {
        private readonly IStoreTypesService _categoryServices;

        public UpdateStoreTypeModel(IStoreTypesService categoryServices)
        {
            _categoryServices = categoryServices;
        }
        [TempData]
        public string success { get; set; }
        [TempData]
        public string fail { get; set; }

        [BindProperty]
        public StoreTypeViewModelWeb input { get; set; }
        public StoreTypeViewModels category { get; set; }

        public IActionResult OnGet(string storetypeId)
        {
            category = this._categoryServices.getByIdStoreTypes(storetypeId); // Directly assign the returned object
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {

            var check = this._categoryServices.getByIdStoreTypes(input.Id);
            // check = new Category {
            check.Commission = input.Commission;
            check.Name = input.Name;
            check.IsActive = true;
            //     };

            var update = this._categoryServices.UpdateStoreType(check);
            if (update != null)
            {
                success = "Update thông tin thành công";
            }
            else
            {
                fail = "Update thông tin thất bại";
            }

            // Điều hướng về cùng trang để hiển thị thông báo
            return RedirectToPage("UpdateStoreType", new { input.Id });

        }


    }
}
