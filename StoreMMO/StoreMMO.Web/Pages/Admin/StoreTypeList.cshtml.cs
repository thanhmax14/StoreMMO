
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
        [TempData]
        public string success { get; set;}
        [TempData]
        public string fail { get; set; }

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


            var cate = _store.getByIdStoreTypes(id);
            cate.IsActive = false;
            var result = _store.UpdateStoreType(cate);
            if (result!= null)
            {
                success = "Hidden success";
                return RedirectToPage("StoreTypeList");
            }
            else
            {
                fail = "Hidden fail";
            }

            // Nếu thành công, chuyển hướng lại danh sách categories
            

        }

    }                           
}