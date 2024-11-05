using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Balances
{
    public interface IBalanceRepository
    {

        Task<bool> AddAsync(BalanceViewModels balanceViewModels);
        Task<bool> DeleteAsync(BalanceViewModels balanceViewModels);
        Task<bool> UpdateAsync(BalanceViewModels balanceViewModels);
        Task<BalanceViewModels> GetBalanceByOrderCodeAsync(long orderCode);
        Task<BalanceViewModels> GetBalanceByIDAsync(string id);
        Task<IEnumerable<BalanceViewModels>> GetBalanceByUserIDAsync(string userID);
        Task<IEnumerable<BalanceViewModels>> GetAllBalanceAsync();
        Task<bool> RejectRequestAsync(BalanceViewModels balanceViewModels, string reason);


	}
    

    
}
