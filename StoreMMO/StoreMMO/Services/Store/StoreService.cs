using StoreMMO.Core.Repositories.Store;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Services.Store
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
    }
}
