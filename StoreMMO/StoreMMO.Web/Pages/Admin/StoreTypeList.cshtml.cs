<<<<<<< HEAD
﻿
using BusinessLogic.Services.StoreMMO.Core.StoreTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
=======
using BusinessLogic.Services.StoreMMO.Core.Stores;
using BusinessLogic.Services.StoreMMO.Core.StoreTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
>>>>>>> 2e998585cff9466b6c6cbd066036e56f4a3f007d
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
    public class StoreTypeListModel : PageModel
    {
<<<<<<< HEAD
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

=======
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

       
>>>>>>> 2e998585cff9466b6c6cbd066036e56f4a3f007d
    }
}
