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
   
        bool add(BalanceViewModels balanceViewModels);
        bool Delete(BalanceViewModels balanceViewModels);
        bool Update(BalanceViewModels balanceViewModels);
        BalanceViewModels GetBalanceByOrderCode(long orderCode);
        BalanceViewModels GetBalanceByID(string id);
        IEnumerable<BalanceViewModels> getBalaceByUserID(string urserID);


    }
    

    
}
