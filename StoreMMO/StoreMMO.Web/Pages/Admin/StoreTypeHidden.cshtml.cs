using BusinessLogic.Services.StoreMMO.Core.StoreTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
    public class StoreTypeHiddenModel : PageModel
    {
        private readonly IStoreTypesService _store;

        public StoreTypeHiddenModel(IStoreTypesService store)
        {
            _store = store;
        }

        public IEnumerable<StoreTypeViewModels> listcate = new List<StoreTypeViewModels>();
        public void OnGet()
        {
            listcate = this._store.GetStoreTypeHidden();
        }
        public IActionResult OnPostActive(string id)
        {
            var cate = _store.getByIdStoreTypes(id);
            cate.IsActive = true;
            var result = _store.UpdateStoreType(cate);


            // Nếu thành công, chuyển hướng lại danh sách categories
            return RedirectToPage("/Admin/StoreList");

        }

    }
}

