
using BusinessLogic.Services.StoreMMO.Core.StoreTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
    public class StoreTypeListModel : PageModel
    {
        private readonly IStoreTypesService _store;
 
       public StoreTypeListModel(IStoreTypesService store)
        {
            _store = store;
        }
  



        public IEnumerable<StoreTypeViewModels> listcate = new List<StoreTypeViewModels>();
        public void OnGet()
        {
            listcate = this._store.GetStoreTypeIsActive();
        }

        public IActionResult OnPostHidden(string id)
        {
            var cate = _store.getByIdStoreTypes(id);
            cate.IsActive = false;
            var result = _store.UpdateStoreType(cate);


            // Nếu thành công, chuyển hướng lại danh sách categories
            return RedirectToPage("/Admin/StoreTypeList");

        }

    }
}
