using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.Purchases
{
    public interface IPurchaseService
    {
        List<PurchaseItem> GetProductFromSession();
        void SaveProductToSession(List<PurchaseItem> product);
        bool add(OrderBuyViewModels orderBuyViewModels);
        bool Delete(OrderBuyViewModels orderBuyViewModels);
        bool Edit(OrderBuyViewModels orderBuyViewModels);
        OrderBuyViewModels GetByID(string id);
		IEnumerable<OrderBuyViewModels> GetAll();
		IEnumerable<OrderBuyViewModels> GetByUserID(string userID);
		IEnumerable<GetOrderByUserViewModel> GetAllByUserID(string userID);
		IEnumerable<GetOrderDetailsViewModel> getOrderDetails(string orderID);
        Task<List<TransactionSummary>> GetDailyTransactionSummary();
        Task<List<TransactionSummary>> GetMonth();
        Task<List<TransactionSummary>> GetMonthInYear();
        Task<List<TransactionSummary>> GetAllYear();

        Task<List<TopStoreViewModels>> TopStore();
    }
}
