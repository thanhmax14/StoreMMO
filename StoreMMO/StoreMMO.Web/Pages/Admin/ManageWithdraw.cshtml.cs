using BusinessLogic.Services.StoreMMO.Core.ComplaintsN;
using BusinessLogic.Services.StoreMMO.Core.Stores;
using BusinessLogic.Services.StoreMMO.Core.Withdraws;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.AutoMapper.ViewModelAutoMapper;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
    public class ManageWithdrawModel : PageModel
    {
        private readonly IWithdrawService _withdrawService;
        private readonly IComplaintsService _complaintService;
        private readonly AppDbContext _context;
        [TempData]
        public string success { get; set; }

        [TempData]
        public string fail { get; set; }
        [BindProperty]
        public int isAccept { get; set; }

        public IEnumerable<WithdrawViewModels> list = new List<WithdrawViewModels>();
        public IEnumerable<BalanceMapper> listC = new List<BalanceMapper>();

        public ManageWithdrawModel(IComplaintsService complaintService, IWithdrawService withdrawService, AppDbContext context)
        {
            _complaintService = complaintService;
            _withdrawService = withdrawService;
            _context = context;
        }

        public void OnGet()
        {
            //list = _withdrawService.getAllWithdraw();
            listC = _withdrawService.getAllBalance();
        }
        public async Task<IActionResult> OnPostAsync(string Id)
        {
            // L?y giao d?ch t? b?ng Balances theo Id
            var withdraw = await _context.Balances.FindAsync(Id);

            if (withdraw != null)
            {
                // Ki?m tra tr?ng th�i v� x? l� h?y b?
                if (isAccept == 1)  // '2' ??i di?n cho tr?ng th�i t? ch?i
                {
                    // C?p nh?t tr?ng th�i giao d?ch th�nh "CANCELLED"
                    withdraw.Status = "EXPIRED";

                    // T�m ng??i d�ng li�n quan b?ng UserId t? b?ng Balances
                    var user = await _context.Users.FindAsync(withdraw.UserId);

                    if (user != null)
                    {
                        // C?ng Amount t? giao d?ch v�o CurrentBalance c?a ng??i d�ng
                        user.CurrentBalance -= withdraw.Amount;

                        // L?u c�c thay ??i v�o c? s? d? li?u
                        await _context.SaveChangesAsync();
                        success = "Accept success! The money will refund to the customer's account.";
                    }
                    else
                    {
                        fail = "Accept fail!";
                    }
                }
            }

            // Quay l?i trang hi?n t?i
            return RedirectToPage("ManageWithdraw");
        }
        public async Task<IActionResult> OnPostAsyncReject(string Id)
        {
            // L?y c?a h�ng t? database theo Id
            var withdraw = await _context.Balances.FindAsync(Id);
            if (withdraw != null)
            {
                // Ki?m tra gi� tr? isAccept
                if (isAccept == 2)
                {
                    // C?p nh?t tr?ng th�i ch?p nh?n (accept)
                    withdraw.Status = "CANCELLED"; // Gi? s? c� thu?c t�nh IsAccept
                    await _context.SaveChangesAsync();
                    success = "Reject success!";
                }
            }
            else
            {
                fail = "Reject fail!";
            }

            // Quay l?i trang hi?n t?i
            return RedirectToPage("ManageWithdraw");
        }
    }
}
