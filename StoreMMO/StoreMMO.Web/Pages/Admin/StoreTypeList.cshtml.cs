
using BusinessLogic.Services.StoreMMO.Core.StoreTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic.Services.StoreMMO.Core.Stores;
using BusinessLogic.Services.StoreMMO.Core.StoreTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;



using StoreMMO.Core.ViewModels;
namespace StoreMMO.Web.Pages.Admin
{
    public class StoreTypeListModel : PageModel
    {

        private readonly IStoreTypesService _store;
        public string baoloi;
        public StoreTypeListModel(IStoreTypesService store)
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
            //baoloi = a;
            listcate = this._store.GetStoreTypeIsActive();
        }
        public IActionResult OnPostHidden(string id)
        {

            var storeType = _store.getByIdStoreTypes(id);

            if (storeType != null)
            {
                storeType.IsActive = false; // Ẩn danh mục
                var result = _store.UpdateStoreType(storeType); // Cập nhật danh mục

                success = "Active category successfully";
            }
            else
            {
                fail = "Active failed category";
            }

            // Chuyển hướng lại trang danh sách danh mục ẩn sau khi xử lý
            return RedirectToPage("StoreTypeList");
           
           
            // Nếu thành công, chuyển hướng lại danh sách categories
            

        }

    }                           
}