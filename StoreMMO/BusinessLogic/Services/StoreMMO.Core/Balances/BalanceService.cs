using BusinessLogic.Services.Payments;
using Net.payOS.Types;
using StoreMMO.Core.Repositories.Balances;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks; // Nhớ import namespace này cho Task

namespace BusinessLogic.Services.StoreMMO.Core.Balances
{
    public class BalanceService : IBalanceService
    {
        private readonly IBalanceRepository _balance;

        public BalanceService(IBalanceRepository balance)
        {
            this._balance = balance;
        }

        public async Task<bool> AddAsync(BalanceViewModels balanceViewModels)
        {
            return await this._balance.AddAsync(balanceViewModels); // Sử dụng AddAsync
        }

        public async Task<bool> DeleteAsync(BalanceViewModels balanceViewModels)
        {
            return await this._balance.DeleteAsync(balanceViewModels); // Sử dụng DeleteAsync
        }

        public CreatePaymentResult Deposit(int price, int timeexpiration)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BalanceViewModels>> GetBalanceByUserIDAsync(string userId)
        {
            return await this._balance.GetBalanceByUserIDAsync(userId); // Sử dụng GetBalanceByUserIDAsync
        }

        public async Task<BalanceViewModels> GetBalanceByIDAsync(string id)
        {
            return await this._balance.GetBalanceByIDAsync(id); // Sử dụng GetBalanceByIDAsync
        }

        public async Task<BalanceViewModels> GetBalanceByOrderCodeAsync(long orderCode)
        {
            return await this._balance.GetBalanceByOrderCodeAsync(orderCode); // Sử dụng GetBalanceByOrderCodeAsync
        }

        public async Task<bool> UpdateAsync(BalanceViewModels balanceViewModels)
        {
            return await this._balance.UpdateAsync(balanceViewModels); // Sử dụng UpdateAsync
        }

		public async Task<IEnumerable<BalanceViewModels>> GetAllBalanceAsync()
		{
			return await _balance.GetAllBalanceAsync();
		}

		public async Task<bool> RejectRequestAsync(BalanceViewModels balanceViewModels, string reason)
		{
			
            return await _balance.RejectRequestAsync(balanceViewModels, reason);
		}
	}
}
