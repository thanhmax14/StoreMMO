using StoreMMO.Core.Repositories.Disputes;
using StoreMMO.Core.Repositories.Withdraw;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.Withdraws
{
    public class WithdrawService : IWithdrawService
    {
        private readonly IWithdrawRepository _withdrawRepository;
        public WithdrawService(IWithdrawRepository withdrawRepository)
        {
            this._withdrawRepository = withdrawRepository;
        }
        public IEnumerable<WithdrawViewModels> getAllWithdraw()
        {
            return _withdrawRepository.getAllWithdraw();
        }
    }
}
