using StoreMMO.Core.ViewModels;

namespace StoreMMO.API.Services
{
    public interface IStoreService
    {
        IEnumerable<StoreViewModels> getAll();
        IEnumerable<getProducInStoreViewModels> getAllProductInStore(string id);
        IEnumerable<StoreDetailViewModel> getStorDetailFullInfo(string id);
    }
}

