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
        private readonly IStoreTypeService _storeTypeService;
        public StoreTypeListModel(IStoreTypeService storeTypeService)
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
        public void OnGet()
        {
            list = this._storeTypeService.getAllStoreType();
        }

       
    }
}
