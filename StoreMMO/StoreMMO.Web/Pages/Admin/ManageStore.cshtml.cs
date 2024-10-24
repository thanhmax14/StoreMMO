using BusinessLogic.Services.StoreMMO.Core.RegisteredSeller;
using BusinessLogic.Services.StoreMMO.Core.Stores;
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
        [TempData]
        public string success { get; set; }

        [TempData]
        public string fail { get; set; }

        [BindProperty]
        public int isAccept { get; set; }

        public IEnumerable<StoreManageViewModels> list = new List<StoreManageViewModels>();

        public ManageStoreModel(IStoreService storeService, AppDbContext context)
        {
            _storeService = storeService;
            _context = context;
        }

        public void OnGet()
        {
            list = _storeService.getAllStore();
        }

        public async Task<IActionResult> OnPostAsync(string Id)
        {
            // Lấy cửa hàng từ database theo Id
            var store = await _context.Stores.FindAsync(Id);
            if (store != null)
            {
                // Kiểm tra giá trị isAccept
                if (isAccept == 1)
                {
                    // Cập nhật trạng thái chấp nhận (accept)
                    store.IsAccept = "1"; // Giả sử có thuộc tính IsAccept
                    await _context.SaveChangesAsync();
                    success = "Update success!";
                }
            }
            else
            {
                fail = "Update fail!";
            }

            // Quay lại trang hiện tại
            return RedirectToPage("ManageStore");
        }
        public async Task<IActionResult> OnPostAsyncReject(string Id)
        {
            // Lấy cửa hàng từ database theo Id
            var store = await _context.Stores.FindAsync(Id);
            if (store != null)
            {
                // Cập nhật trạng thái thành 2 khi nhấn "Reject"
                store.IsAccept = "2"; // Giả sử có thuộc tính IsAccept
                await _context.SaveChangesAsync();
                success = "Update success!";
            }
            else
            {
                fail = "Update fail!";
            }

            // Quay lại trang hiện tại sau khi thực hiện hành động
            return RedirectToPage("ManageStore");
        }
    }
}
