using BusinessLogic.Services.StoreMMO.Core.ComplaintsN;
using BusinessLogic.Services.StoreMMO.Core.Disputes;
using BusinessLogic.Services.StoreMMO.Core.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.AutoMapper.ViewModelAutoMapper;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Admin
{
    public class ManageDisputeModel : PageModel
    {
        private readonly IDisputeService _disputeService;
        private readonly IComplaintsService _complaintService;
        private readonly AppDbContext _context;
        [TempData]
        public string success { get; set; }

        [TempData]
        public string fail { get; set; }
        [BindProperty]
        public int Status { get; set; }

        public IEnumerable<DisputeViewModels> list = new List<DisputeViewModels>();
        public IEnumerable<ComplaintsMapper> listC = new List<ComplaintsMapper>();

        public ManageDisputeModel(IComplaintsService complaintService, IDisputeService disputeService, AppDbContext context)
        {
            _complaintService = complaintService;
            _disputeService = disputeService;
            _context = context;
        }

        public void OnGet()
        {
            //list = _disputeService.Getcomstatus();
            //list = _disputeService.GetAllReportAdmin();
            listC = _complaintService.GetAllReportAdmin();
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
                    success = "Update success!";
                }
            }
            else
            {
                fail = "Update fail!";
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
                dispute.Status = "ReportAdmin"; // Giả sử có thuộc tính IsAccept
                await _context.SaveChangesAsync();
                success = "Update success!";
            }
            else
            {
                fail = "Update fail!";
            }

            // Quay lại trang hiện tại sau khi thực hiện hành động
            return RedirectToPage("ManageDispute");
        }
    }
}
