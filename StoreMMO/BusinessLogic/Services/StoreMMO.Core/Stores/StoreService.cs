using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.Stores;
using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.Stores
{

    public class StoreService : IStoreService
    {
       private readonly IStoreRepository _storeRepo;
        public StoreService(IStoreRepository store)
        {
            this._storeRepo = store;
        }

        public StoreAddViewModels AddStore(StoreAddViewModels store)
        {
           return _storeRepo.AddStore(store);   
        }

        public void Delete(string id)
        {
             _storeRepo.Delete(id);
        }

        public IEnumerable<StoreViewModels> getAll(string sicbo)
        {
            return this._storeRepo.getAll(sicbo);
        }

        public StoreAddViewModels getById(string id)
        {
            return _storeRepo.getById(id);
        }

        public StoreAddViewModels Update(StoreAddViewModels store)
        {
            return _storeRepo.Update(store);
        }
        public IEnumerable<getProducInStoreViewModels> getAllProductInStore(string id)
        {
            return this._storeRepo.getAllProductInStore(id);
        }
        public IEnumerable<StoreDetailViewModel> getStorDetailFullInfo(string id)
        {
            return this._storeRepo.getStorDetailFullInfo(id);
        }

        public IEnumerable<StoreManageViewModels> getAllStore()
        {
            return this._storeRepo.getAllStore();
        }
        public IEnumerable<StoreSellerViewModels> getAllStoreSeller()
        {
            return this._storeRepo.getAllStoreSeller();
        }
    }
}
