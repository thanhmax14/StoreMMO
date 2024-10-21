using StoreMMO.Core.Repositories.Balances;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.Balances
{
    public class BalanceService : IBalanceService
    {
        private readonly IBalanceRepository _balance;
        public BalanceService(IBalanceRepository balance)
        {
            this._balance = balance;
        }
        public bool add(BalanceViewModels balanceViewModels)
        {
          return this._balance.add(balanceViewModels);
        }

        public bool Delete(BalanceViewModels balanceViewModels)
        {
         return this._balance.Delete(balanceViewModels);
        }



        public IEnumerable<BalanceViewModels> getBalaceByUserID(string urserID)
        {
           return this._balance.getBalaceByUserID(urserID);
        }

        public BalanceViewModels GetBalanceByID(string id)
        {
            return this._balance.GetBalanceByID(id);
        }

        public BalanceViewModels GetBalanceByOrderCode(long orderCode)
        {
            return this._balance.GetBalanceByOrderCode(orderCode);
        }

        public bool Update(BalanceViewModels balanceViewModels)
        {
            return this._balance.Update(balanceViewModels);
        }
    }
}
