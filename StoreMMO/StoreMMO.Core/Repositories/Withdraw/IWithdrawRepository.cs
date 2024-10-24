using StoreMMO.Core.AutoMapper.ViewModelAutoMapper;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Withdraw
{
    public interface IWithdrawRepository
    {
        IEnumerable<WithdrawViewModels> getAllWithdraw();
        IEnumerable<BalanceMapper> getAllBalance();
    }
}
