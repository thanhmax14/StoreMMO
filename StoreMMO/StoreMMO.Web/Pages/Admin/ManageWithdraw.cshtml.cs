using BusinessLogic.Services.StoreMMO.Core.Stores;
using BusinessLogic.Services.StoreMMO.Core.Withdraws;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
    public class ManageWithdrawModel : PageModel
    {
        private readonly IWithdrawService _withdrawService;
        private readonly AppDbContext _context;

        [BindProperty]
        public int isAccept { get; set; }

        public IEnumerable<WithdrawViewModels> list = new List<WithdrawViewModels>();

        public ManageWithdrawModel(IWithdrawService withdrawService, AppDbContext context)
        {
            _withdrawService = withdrawService;
            _context = context;
        }

        public void OnGet()
        {
            list = _withdrawService.getAllWithdraw();
        }
        public async Task<IActionResult> OnPostAsync(string Id)
        {
            // L?y c?a hàng t? database theo Id
            var withdraw = await _context.Balances.FindAsync(Id);
            if (withdraw != null)
            {
                // Ki?m tra giá tr? isAccept
                if (isAccept == 1)
                {
                    // C?p nh?t tr?ng thái ch?p nh?n (accept)
                    withdraw.Status = "Approved"; // Gi? s? có thu?c tính IsAccept
                    await _context.SaveChangesAsync();
                }
            }

            // Quay l?i trang hi?n t?i
            return RedirectToPage("ManageWithdraw");
        }
        public async Task<IActionResult> OnPostAsyncReject(string Id)
        {
            // L?y c?a hàng t? database theo Id
            var withdraw = await _context.Balances.FindAsync(Id);
            if (withdraw != null)
            {
                // Ki?m tra giá tr? isAccept
                if (isAccept == 1)
                {
                    // C?p nh?t tr?ng thái ch?p nh?n (accept)
                    withdraw.Status = "Reject"; // Gi? s? có thu?c tính IsAccept
                    await _context.SaveChangesAsync();
                }
            }

            // Quay l?i trang hi?n t?i
            return RedirectToPage("ManageWithdraw");
        }
    }
}
