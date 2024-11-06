using BusinessLogic.Services.StoreMMO.Core.StoreTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StoreMMO.Web.Pages.Admin
{
	[Authorize(Roles = "Admin")]
	public class StoreTypeHiddenModel : PageModel
    {
        private readonly IStoreTypesService _store;

        public StoreTypeHiddenModel(IStoreTypesService store)
        {
            _store = store;
        }
        [BindProperty]
        public string id { get; set; }

        [TempData]
        public string success { get; set; }

        [TempData]
        public string fail { get; set; }

        public IEnumerable<StoreTypeViewModels> listcate = new List<StoreTypeViewModels>();
        public void OnGet()
        {
            listcate = this._store.GetStoreTypeHidden();
        }
        public IActionResult OnPost(string id)
        {

            var storeType = _store.getByIdStoreTypes(id);

            if (storeType != null)
            {
                storeType.IsActive = true; // Ẩn danh mục
                var result = _store.UpdateStoreType(storeType); // Cập nhật danh mục

                success = "Active StoreType successfully";
            }
            else
            {
                fail = "Active failed StoreType";
            }

            // Chuyển hướng lại trang danh sách danh mục ẩn sau khi xử lý
            return RedirectToPage("StoreTypeHidden");
          //ggggggggggggggggggggggggggg
           
            


            // Nếu thành công, chuyển hướng lại danh sách categories
          

        }

    }
}

