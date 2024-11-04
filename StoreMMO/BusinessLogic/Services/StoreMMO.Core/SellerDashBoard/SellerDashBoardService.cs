using StoreMMO.Core.Repositories.SellerDashboard;
using StoreMMO.Core.ViewModels;
using StoreMMO.Core.ViewModels.SellerDashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.SellerDashBoard
{
    public class SellerDashBoardService :ISellerDashBoardService
    {
        private readonly ISellerDashBoardRepository _sellerDashBoardRepository;

        public SellerDashBoardService(ISellerDashBoardRepository sellerDashBoardRepository)
        {
            this._sellerDashBoardRepository = sellerDashBoardRepository;
        }

        public async Task<List<TransactionSummary>> GetDailyTransactionSummary(string id)
        {
            return await this._sellerDashBoardRepository.GetDailyTransactionSummary(id);
        }

        public async Task<List<TransactionSummary>> GetMonth(string id)
        {
            return await this._sellerDashBoardRepository.GetMonth(id);
        }

        public Task<List<TransactionSummary>> GetMonthlyTransactionSummary(string userId)
        {
           return this._sellerDashBoardRepository.GetMonthlyTransactionSummary(userId);
        }

        public TodayOrderSummary GetTotalSoldOrdersAndRevenueForToday(string sellerUserId)
        {
         return  this._sellerDashBoardRepository.GetTotalSoldOrdersAndRevenueForToday(sellerUserId);
        }

        public Task<List<TransactionSummary>> GetYearlyTransactionSummary(string userId)
        {
            return this._sellerDashBoardRepository.GetYearlyTransactionSummary(userId);
        }
    }
}
