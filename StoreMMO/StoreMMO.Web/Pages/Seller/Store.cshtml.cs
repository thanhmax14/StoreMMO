using BusinessLogic.Services.StoreMMO.Core.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System.Security.Claims;

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
            // L?y UserID t? session
            //var currentUserId = HttpContext.Session.GetString("UserID");
            //if (currentUserId != null)
            //{
            //    // L?y danh s�ch c?a h�ng c?a seller d?a tr�n UserID
            //    list = _storeService.getAllStoreSeller(currentUserId);
            //}
            //else
            //{
            //    // X? l� khi kh�ng c� UserID trong session (v� d?: chuy?n h??ng ??n trang ??ng nh?p)
            //    RedirectToPage("/Account/Login");
            //}
            list = _storeService.getAllStoreSeller("1f0dbbe2-2a81-43e9-8272-117507ac9c45");
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
                    store.IsAccept = "PENDING"; // Gi? s? c� thu?c t�nh IsAccept
                    await _context.SaveChangesAsync();
                }
            }

            // Quay l?i trang hi?n t?i sau khi th?c hi?n h�nh ??ng
            return RedirectToPage("Store");
        }
    }
}
