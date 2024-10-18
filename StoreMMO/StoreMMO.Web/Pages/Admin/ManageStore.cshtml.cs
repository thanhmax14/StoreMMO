using BusinessLogic.Services.StoreMMO.Core.RegisteredSeller;
using BusinessLogic.Services.StoreMMO.Core.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
    public class ManageStoreModel : PageModel
    {
        private readonly IStoreService _storeService;
        private readonly AppDbContext _context;
        public ManageStoreModel(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public IEnumerable<StoreManageViewModels> list = new List<StoreManageViewModels>();
        public void OnGet()
        {
            list = this._storeService.getAllStore();
        }
    }
}
