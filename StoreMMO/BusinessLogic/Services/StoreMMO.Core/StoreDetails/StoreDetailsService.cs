using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.StoreDetails;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.StoreDetails
{

    public class StoreDetailsService : IStoreDetailsService
    {
        private readonly IStoreDetailRepository _storeDetailRepository;

        public StoreDetailsService(IStoreDetailRepository storeDetailRepository)
        {
            _storeDetailRepository = storeDetailRepository;
        }

        public StoreDetailViewModels AddStoDetails(StoreDetailViewModels storeDetailViewModels)
        {
           return _storeDetailRepository.AddStoDetails(storeDetailViewModels);
        }

        public void DeleteStoDetails(string id)
        {
            _storeDetailRepository.DeleteStoDetails(id);
        }

        public IEnumerable<StoreDetail> GetAllStoreDetails()
        {
                return _storeDetailRepository.GetAllStoreDetails(); 
        }

        public StoreDetailViewModels GetByIdStoDetails(string id)
        {
            return _storeDetailRepository.GetByIdStoDetails(id);
        }

        public StoreDetailViewModels GetByIdStoDetails1(string id)
        {
            return _storeDetailRepository.GetByIdStoDetails1(id);
        }

        public StoreDetailViewModels UpdateStoDetails(StoreDetailViewModels idstoreDetailViewModels)
        {
               return _storeDetailRepository.UpdateStoDetails(idstoreDetailViewModels);
        }
    }
}
