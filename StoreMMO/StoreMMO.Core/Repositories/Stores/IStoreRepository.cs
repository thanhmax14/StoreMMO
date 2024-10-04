using StoreMMO.Core.ViewModels;

namespace StoreMMO.Core.Repositories.Stores
{
    public interface IStoreRepository
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
