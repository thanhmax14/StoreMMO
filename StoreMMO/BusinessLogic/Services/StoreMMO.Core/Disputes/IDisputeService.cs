using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.Disputes
{
    public interface IDisputeService
    {
        IEnumerable<DisputeViewModels> getAllDispute();
        IEnumerable<DisputeViewModels> Getcomstatus();
    }
}
