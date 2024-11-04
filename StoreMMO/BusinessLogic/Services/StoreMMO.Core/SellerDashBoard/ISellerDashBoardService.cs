using StoreMMO.Core.ViewModels;
using StoreMMO.Core.ViewModels.SellerDashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.SellerDashBoard
{
    public interface ISellerDashBoardService
    {
        Task<List<TransactionSummary>> GetMonth(string id);
        Task<List<TransactionSummary>> GetDailyTransactionSummary(string id);
                TodayOrderSummary GetTotalSoldOrdersAndRevenueForToday(string sellerUserId);
        Task<List<TransactionSummary>> GetMonthlyTransactionSummary(string userId);
        Task<List<TransactionSummary>> GetYearlyTransactionSummary(string userId);
    }
}
