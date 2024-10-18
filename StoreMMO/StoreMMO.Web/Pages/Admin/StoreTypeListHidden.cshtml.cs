using BusinessLogic.Services.StoreMMO.Core.StoreTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
    public class StoreTypeListHiddenModel : PageModel
    {
        private readonly IStoreTypesService _store;

        public StoreTypeListHiddenModel(IStoreTypesService store)
        {
            _store = store;
        }




        public IEnumerable<StoreTypeViewModels> listcate = new List<StoreTypeViewModels>();
        public void OnGet()
        {
            listcate = this._store.GetStoreTypeHidden();
        }

        public IActionResult OnPostHidden(string id)
        {
            var cate = _store.getByIdStoreTypes(id);
            cate.IsActive = false;
            var result = _store.UpdateStoreType(cate);


            // Nếu thành công, chuyển hướng lại danh sách categories
            return RedirectToPage("/Admin/StoreTypeList");

        }


        private readonly IStoreTypeService _storeTypeService;
        public StoreTypeListHiddenModel(IStoreTypeService storeTypeService)
        {
            _storeTypeService = storeTypeService;
        }
        public IEnumerable<StoreType> list = new List<StoreType>();
        [BindProperty]
        public string id { get; set; }
        [TempData]
        public string success { get; set; }
        [TempData]
        public string fail { get; set; }

    }
}
