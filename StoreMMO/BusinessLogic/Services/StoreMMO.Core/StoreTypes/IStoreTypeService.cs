using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.StoreTypes
{
    public interface IStoreTypeService
    {
        IEnumerable<StoreType> getAllStoreType();
        StoreTypeViewModels AddStoreType(StoreTypeViewModels storeViewModels);
        StoreTypeViewModels UpdateStoreType(StoreTypeViewModels storeViewModels);
        StoreTypeViewModels getByIdStoreType(string id);
        void deleteByIdStoreType(string id);
        double GetCommitssionByStoreID(string id);
        StoreTypeViewModels UpdateStoreType1(StoreTypeViewModels storeViewModels);
    }
}
