using StoreMMO.Core.Repositories.Balances;
using StoreMMO.Core.Repositories.OrderDetails;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.OrderDetails
{
    public class OrderDetailsService : IOderDetailsService
    {
        private readonly IOrderDeailsRepository _oderDetailsRepository;

        public OrderDetailsService(IOrderDeailsRepository oderDetailsRepository)
        {
            this._oderDetailsRepository = oderDetailsRepository;
        }
        public async Task<bool> AddAsync(OrderDetailsViewModels orderDetailViewModels)
        {
            return await this._oderDetailsRepository.AddAsync(orderDetailViewModels); 
        }

        public async Task<bool> DeleteAsync(OrderDetailsViewModels orderDetailViewModels)
        {
            return await this._oderDetailsRepository.DeleteAsync(orderDetailViewModels);
        }

        public IEnumerable<SaleHistoryViewModels> getAll()
        {
            return  this._oderDetailsRepository.getAll();
        }

        public async Task<OrderDetailsViewModels> GetOrderDeailByIDAsync(string id)
        {
            return await this._oderDetailsRepository.GetOrderDeailByIDAsync(id);
        }

        public Task<OrderDetailsViewModels> GetOrderDetailByproductIDAsync(string orderCode)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDetailsViewModels> GetOrderDetailByOrderCodeAsync(string orderCode)
        {
            return await this._oderDetailsRepository.GetOrderDetailByOrderCodeAsync(orderCode);
        }

        public async Task<IEnumerable<OrderDetailsViewModels>> GetOrderDetailsByOrderBuyIDAsync(string userID)
        {
            return await this._oderDetailsRepository.GetOrderDetailsByOrderBuyIDAsync(userID);
        }

        public  async Task<bool> UpdateAsync(SaleHistoryViewModels orderDetailViewModels)
        {
            return await this._oderDetailsRepository.UpdateAsync(orderDetailViewModels);
        }
    }
}
