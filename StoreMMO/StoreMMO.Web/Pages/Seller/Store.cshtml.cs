using BusinessLogic.Services.StoreMMO.Core.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Seller
{
    public class StoreModel : PageModel
    {
        private readonly IStoreService _storeService;
        private readonly AppDbContext _context;

        [BindProperty]
        public int isAccept { get; set; }

        public IEnumerable<StoreSellerViewModels> list = new List<StoreSellerViewModels>();

        public StoreModel(IStoreService storeService, AppDbContext context)
        {
            _storeService = storeService;
            _context = context;
        }

        public void OnGet()
        {
            list = _storeService.getAllStoreSeller();
        }
    }
}
