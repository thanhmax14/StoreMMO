using BusinessLogic.Services.StoreMMO.Core.SellerDashBoard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels.SellerDashboard;

namespace StoreMMO.Web.Pages.Seller.SellderDashboard
{
    public class SellerDashListModel : PageModel
    {
        private readonly ISellerDashBoardService _sellerDashBoardService;

        public SellerDashListModel(ISellerDashBoardService sellerDashBoardService)
        {
            this._sellerDashBoardService = sellerDashBoardService;
        }

        public TodayOrderSummary todayOrderSummaries { get; set; } = new TodayOrderSummary();

        //  public TodayOrderSummary todayOrderSummaries { get; set; } = new TodayOrderSummary();

        public async Task OnGetAsync()
        {
               string UserId = HttpContext.Session.GetString("UserID");
     //   string userId = "1f0dbbe2-2a81-43e9-8272-117507ac9c45";
            todayOrderSummaries = _sellerDashBoardService.GetTotalSoldOrdersAndRevenueForToday(UserId);
        }
    }
}
