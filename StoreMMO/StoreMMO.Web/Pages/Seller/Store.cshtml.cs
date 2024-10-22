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
        //public IActionResult OnPostHidden(string id)
        //{
        //    var cate = _storeService.getStoreDetailById(id);
        //    cate.IsActive = false;
        //    var result = _storeService.UpdateCategory(cate);


        //    // N?u th�nh c�ng, chuy?n h??ng l?i danh s�ch categories
        //    return RedirectToPage("/Admin/CategoriesList");
        //}
        public async Task<IActionResult> OnPostAsync(string Id)
        {
            // L?y c?a h�ng t? database theo Id
            var store = await _context.Stores.FindAsync(Id);
            if (store != null)
            {
                // C?p nh?t tr?ng th�i th�nh 2 khi nh?n "Reject"
                if(isAccept == 2) 
                    {
                    store.IsAccept = "Pending"; // Gi? s? c� thu?c t�nh IsAccept
                    await _context.SaveChangesAsync();
                }
            }

            // Quay l?i trang hi?n t?i sau khi th?c hi?n h�nh ??ng
            return RedirectToPage("Store");
        }
    }
}
