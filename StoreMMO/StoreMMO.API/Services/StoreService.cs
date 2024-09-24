using StoreMMO.Core.Repositories.Store;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.API.Services
{
    public class StoreService:IStoreService
    {
       private readonly IStoreRepository _storeRepo;
        public StoreService(IStoreRepository store)
        {
            this._storeRepo = store;
        }
        public IEnumerable<StoreViewModels> getAll()
        {
            return this._storeRepo.getAll();
        }

        public IEnumerable<getProducInStoreViewModels> getAllProductInStore(string id)
        {
            return this._storeRepo.getAllProductInStore(id);
        }
        public IEnumerable<StoreDetailViewModel> getStorDetailFullInfo(string id)
        {
            return this._storeRepo.getStorDetailFullInfo(id);    
        }
    }
}
