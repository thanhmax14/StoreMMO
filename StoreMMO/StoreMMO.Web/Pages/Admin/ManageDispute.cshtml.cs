using BusinessLogic.Services.StoreMMO.Core.ComplaintsN;
using BusinessLogic.Services.StoreMMO.Core.Disputes;
using BusinessLogic.Services.StoreMMO.Core.Stores;
using BusinessLogic.Services.StoreMMO.Core.Withdraws;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.AutoMapper.ViewModelAutoMapper;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
	[Authorize(Roles = "Admin")]
	public class ManageDisputeModel : PageModel
    {
        private readonly IDisputeService _disputeService;
        private readonly IComplaintsService _complaintService;
        private readonly IWithdrawService _withdrawService;

        private readonly AppDbContext _context;
        [TempData]
        public string success { get; set; }

        [TempData]
        public string fail { get; set; }
        [BindProperty]
        public int Status { get; set; }

        public IEnumerable<DisputeViewModels> list = new List<DisputeViewModels>();
        public IEnumerable<ComplaintsMapper> listC = new List<ComplaintsMapper>();
        public IEnumerable<BalanceMapper> listE = new List<BalanceMapper>();

        public ManageDisputeModel(IWithdrawService withdrawService, IComplaintsService complaintService, IDisputeService disputeService, AppDbContext context)
        {
            _withdrawService = withdrawService;
            _complaintService = complaintService;
            _disputeService = disputeService;
            _context = context;
        }

        public void OnGet()
        {
            //list = _disputeService.Getcomstatus();
            //list = _disputeService.GetAllReportAdmin();
            listC = _complaintService.GetAllReportAdmin();
            //listE = _withdrawService.getAllBalance();
        }
        public async Task<IActionResult> OnPostAsync(string Id)
        {
            // Lấy cửa hàng từ database theo Id
            var dispute = await _context.Complaints.FindAsync(Id);
            if (dispute != null)
            {
                // Kiểm tra giá trị isAccept
                if (Status == 1)
                {
                    // Cập nhật trạng thái chấp nhận (accept)
                    dispute.Status = "done"; // Giả sử có thuộc tính IsAccept
                    await _context.SaveChangesAsync();
                    success = "Accept success!";
                }
            }
            else
            {
                fail = "Accept fail!";
            }

            // Quay lại trang hiện tại
            return RedirectToPage("ManageDispute");
        }
        public async Task<IActionResult> OnPostAsyncReject(string Id)
        {
            // Lấy cửa hàng từ database theo Id
            var dispute = await _context.Complaints.FindAsync(Id);
            if (dispute != null)
            {
                // Cập nhật trạng thái thành 2 khi nhấn "Reject"
                dispute.Status = "done"; // Giả sử có thuộc tính IsAccept
                await _context.SaveChangesAsync();
                success = "Reject success!";
            }
            else
            {
                fail = "Reject fail!";
            }

            // Quay lại trang hiện tại sau khi thực hiện hành động
            return RedirectToPage("ManageDispute");
        }
        public IActionResult OnPostWarrant(string idcomplaint)
        {
            if (_complaintService.Warrant(idcomplaint))
            {
                // Xử lý thành công
                success = "Accept success!";
            }
            else
            {
                // Xử lý lỗi nếu cần
                fail = "Reject fail!";
            }
            return RedirectToPage("ManageDispute");
        }
        public IActionResult OnPostBackMoney(string idcomplaint)
        {
            if (_complaintService.BackMoney(idcomplaint))
            {
                // Xử lý thành công
                success = "Accept success!";
            }
            else
            {
                // Xử lý lỗi nếu cần
                fail = "Reject fail!";
            }
            return RedirectToPage("ManageDispute");
        }
    }

}
