using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Disputes
{
    public interface IDisputeRepository
    {
        IEnumerable<DisputeViewModels> getAllDispute();
        IEnumerable<DisputeViewModels> Getcomstatus();
    }
}
