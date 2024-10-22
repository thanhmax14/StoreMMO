using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.OrderDetails
{
    public interface IOrderDeailsRepository
    {
        IEnumerable<SaleHistoryViewModels> getAll();
        Task<bool> AddAsync(OrderDetailsViewModels orderDetailViewModels);
        Task<bool> DeleteAsync(OrderDetailsViewModels orderDetailViewModels);
        Task<bool> UpdateAsync(SaleHistoryViewModels orderDetailViewModels);
        Task<OrderDetailsViewModels> GetOrderDetailByproductIDAsync(long orderCode);
        Task<SaleHistoryViewModels> GetOrderDeailByIDAsync(string id);
        Task<IEnumerable<OrderDetailsViewModels>> GetOrderDetailsByOrderBuyIDAsync(string userID);
    }
}
