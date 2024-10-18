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
    public class StoreTypesService : IStoreTypesService

    {
       private readonly IStoreTypeRepository _storeTypesRepository;

        public StoreTypesService(IStoreTypeRepository storeTypesRepository)
        {
            _storeTypesRepository = storeTypesRepository;
        }

        public StoreTypeViewModels AddStoretype(StoreTypeViewModels model)
        {
            return _storeTypesRepository.AddStoreType(model);
        }

        public IEnumerable<StoreType> getAllStoreTypes()
        {
            return _storeTypesRepository.getAllStoreType();
        }

        public StoreTypeViewModels getByIdStoreTypes(string id)
        {
          return _storeTypesRepository.getByIdStoreType(id);
        }

        public IEnumerable<StoreTypeViewModels> GetStoreTypeHidden()
        {
            return _storeTypesRepository.GetStoreTypeHidden();
        }

        public IEnumerable<StoreTypeViewModels> GetStoreTypeIsActive()
        {
           return _storeTypesRepository.GetStoreTypeIsActive();
        }

        public StoreTypeViewModels UpdateStoreType(StoreTypeViewModels model)
        {
           return _storeTypesRepository.UpdateStoreType(model);
        }
    }
}
