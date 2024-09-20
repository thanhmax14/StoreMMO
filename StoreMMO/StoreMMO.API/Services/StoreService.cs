using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.Stores;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.API.Services
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

        public IEnumerable<StoreViewModels> getAll()
        {
            return this._storeRepo.getAll();
        }

        public StoreAddViewModels getById(string id)
        {
            return _storeRepo.getById(id);
        }

        public StoreAddViewModels Update(StoreAddViewModels store)
        {
            return _storeRepo.Update(store);
        }
    }
}
