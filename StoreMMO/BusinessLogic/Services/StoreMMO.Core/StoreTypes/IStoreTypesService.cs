using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.StoreTypes
{
    public interface IStoreTypesService
    {
        IEnumerable<StoreType> getAllStoreTypes();
        StoreTypeViewModels getByIdStoreTypes(string id);
        StoreTypeViewModels AddStoretype(StoreTypeViewModels model);

        StoreTypeViewModels UpdateStoreType(StoreTypeViewModels model);

        IEnumerable<StoreTypeViewModels> GetStoreTypeIsActive();

        IEnumerable<StoreTypeViewModels> GetStoreTypeHidden();


    }
}
