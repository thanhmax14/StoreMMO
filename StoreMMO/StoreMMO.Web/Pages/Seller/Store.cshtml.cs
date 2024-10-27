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
            //    // L?y danh sách c?a hàng c?a seller d?a trên UserID
            //    list = _storeService.getAllStoreSeller(currentUserId);
            //}
            //else
            //{
            //    // X? lý khi không có UserID trong session (ví d?: chuy?n h??ng ??n trang ??ng nh?p)
            //    RedirectToPage("/Account/Login");
            //}
            list = _storeService.getAllStoreSeller("1f0dbbe2-2a81-43e9-8272-117507ac9c45");
        }


        //public IActionResult OnPostHidden(string id)
        //{
        //    var cate = _storeService.getStoreDetailById(id);
        //    cate.IsActive = false;
        //    var result = _storeService.UpdateCategory(cate);


        //    // N?u thành công, chuy?n h??ng l?i danh sách categories
        //    return RedirectToPage("/Admin/CategoriesList");
        //}
        public async Task<IActionResult> OnPostAsync(string Id)
        {
            // L?y c?a hàng t? database theo Id
            var store = await _context.Stores.FindAsync(Id);
            if (store != null)
            {
                // C?p nh?t tr?ng thái thành 2 khi nh?n "Reject"
                if(isAccept == 2) 
                    {
                    store.IsAccept = "PENDING"; // Gi? s? có thu?c tính IsAccept
                    await _context.SaveChangesAsync();
                }
            }

            // Quay l?i trang hi?n t?i sau khi th?c hi?n hành ??ng
            return RedirectToPage("Store");
        }
    }
}
