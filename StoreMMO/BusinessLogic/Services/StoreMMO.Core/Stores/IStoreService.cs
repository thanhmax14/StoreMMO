using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.Stores
{

    public interface IStoreService
    {
        IEnumerable<StoreViewModels> getAll(bool sicbo);
        StoreAddViewModels AddStore(StoreAddViewModels store);
        StoreAddViewModels Update(StoreAddViewModels store);
        void Delete(string id);
        StoreAddViewModels getById(string id);

        IEnumerable<getProducInStoreViewModels> getAllProductInStore(string id);
        IEnumerable<StoreDetailViewModel> getStorDetailFullInfo(string id);
    }
}
