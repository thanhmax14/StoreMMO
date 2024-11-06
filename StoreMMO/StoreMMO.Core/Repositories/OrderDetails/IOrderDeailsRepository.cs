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
        IEnumerable<SaleHistoryViewModels> getAll(string sellerId);
        Task<bool> AddAsync(OrderDetailsViewModels orderDetailViewModels);
        Task<bool> DeleteAsync(OrderDetailsViewModels orderDetailViewModels);
        Task<bool> UpdateAsync(SaleHistoryViewModels orderDetailViewModels);
        Task<OrderDetailsViewModels> GetOrderDetailByOrderCodeAsync(string orderCode);
        Task<OrderDetailsViewModels> GetOrderDeailByIDAsync(string id);
        Task<IEnumerable<OrderDetailsViewModels>> GetOrderDetailsByOrderBuyIDAsync(string userID);

        IEnumerable<GetOrderDetailsViewModel> getOrderDetails(string orderID);
		Task<bool> UpdateDetailAsync(OrderDetailsViewModels orderDetailViewModels);
	}
}
