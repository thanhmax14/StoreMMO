using BusinessLogic.Services.StoreMMO.Core.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
    public class StoreListHiddenModel : PageModel
    {
        private readonly IStoreService _storeService;
        public StoreListHiddenModel(IStoreService storeService)
        {
            _storeService = storeService;
        }
        public IEnumerable<StoreViewModels> list = new List<StoreViewModels>();
        [BindProperty]
        public string id { get; set; }
        [TempData]
        public string success { get; set; }
        [TempData]
        public string fail { get; set; }
        public void OnGet()
        {
            list = this._storeService.getAll(false);
        }
    }
}
