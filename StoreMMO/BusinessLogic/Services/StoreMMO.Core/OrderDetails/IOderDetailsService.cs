using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.OrderDetails
{
    public interface IOderDetailsService
    {
        IEnumerable<SaleHistoryViewModels> getAll();
        Task<bool> AddAsync(OrderDetailsViewModels orderDetailViewModels);
        Task<bool> DeleteAsync(OrderDetailsViewModels orderDetailViewModels);
        Task<bool> UpdateAsync(SaleHistoryViewModels orderDetailViewModels);
        Task<OrderDetailsViewModels> GetOrderDetailByproductIDAsync(string orderCode);
        Task<OrderDetailsViewModels> GetOrderDeailByIDAsync(string id);
        Task<IEnumerable<OrderDetailsViewModels>> GetOrderDetailsByOrderBuyIDAsync(string userID);
    }
}
