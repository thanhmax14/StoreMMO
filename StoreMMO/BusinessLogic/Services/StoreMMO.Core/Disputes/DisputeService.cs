using StoreMMO.Core.Repositories.Disputes;
using StoreMMO.Core.Repositories.RegisteredSeller;
using StoreMMO.Core.Repositories.Stores;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.Disputes
{
    public class DisputeService : IDisputeService
    {
        private readonly IDisputeRepository _disputeRepository;
            public DisputeService(IDisputeRepository dispute)
        {
            this._disputeRepository = dispute;
        }
        public IEnumerable<DisputeViewModels> getAllDispute()
        {
            return _disputeRepository.getAllDispute();
        }
        public IEnumerable<DisputeViewModels> Getcomstatus()
        {
            return _disputeRepository.Getcomstatus();
        }
    }
}
