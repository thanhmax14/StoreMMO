using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.StoreTypes;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.StoreTypes
{
    public class StoreTypeService : IStoreTypeService
    {
        private readonly IStoreTypeRepository _storeTypeRepository;

        public StoreTypeService(IStoreTypeRepository storeTypeRepository)
        {
            _storeTypeRepository = storeTypeRepository;
        }
        public StoreTypeViewModels AddStoreType(StoreTypeViewModels storeViewModels)
        {
            return _storeTypeRepository.AddStoreType(storeViewModels);
        }

        public void deleteByIdStoreType(string id)
        {
            _storeTypeRepository.deleteByIdStoreType(id);
        }

        public IEnumerable<StoreType> getAllStoreType()
        {
            return _storeTypeRepository.getAllStoreType();
        }

        public StoreTypeViewModels getByIdStoreType(string id)
        {
            return _storeTypeRepository.getByIdStoreType(id);
        }

        public StoreTypeViewModels UpdateStoreType(StoreTypeViewModels storeViewModels)
        {
            return _storeTypeRepository.UpdateStoreType(storeViewModels);
        }
    }
}
