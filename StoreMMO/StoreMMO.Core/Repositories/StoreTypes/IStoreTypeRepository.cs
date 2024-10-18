using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.StoreTypes
{
    public interface IStoreTypeRepository
    {
        IEnumerable<StoreType> getAllStoreType();
        StoreTypeViewModels AddStoreType(StoreTypeViewModels storeViewModels);
        StoreTypeViewModels UpdateStoreType(StoreTypeViewModels storeViewModels);
        StoreTypeViewModels getByIdStoreType(string id);
        void deleteByIdStoreType(string id);
        IEnumerable<StoreTypeViewModels> GetStoreTypeIsActive();

        IEnumerable<StoreTypeViewModels> GetStoreTypeHidden();
    }

}
