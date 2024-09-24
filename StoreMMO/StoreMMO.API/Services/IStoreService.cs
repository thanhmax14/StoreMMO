using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.API.Services
{
    public interface IStoreService
    {
        IEnumerable<StoreViewModels> getAll();
        StoreAddViewModels AddStore(StoreAddViewModels store);
        StoreAddViewModels Update(StoreAddViewModels store);
        void Delete(string id);
        StoreAddViewModels getById(string id);

        IEnumerable<getProducInStoreViewModels> getAllProductInStore(string id);
        IEnumerable<StoreDetailViewModel> getStorDetailFullInfo(string id);
    }
}
