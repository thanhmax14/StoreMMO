using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.Stores
{

    public interface IStoreService
    {
        IEnumerable<StoreViewModels> getAll(string sicbo);
        StoreAddViewModels AddStore(StoreAddViewModels store);
        StoreAddViewModels Update(StoreAddViewModels store);
        void Delete(string id);
        StoreAddViewModels getByIdCategory(string id);

        IEnumerable<getProducInStoreViewModels> getAllProductInStore(string id);
        IEnumerable<StoreDetailViewModel> getStorDetailFullInfo(string id);
        IEnumerable<StoreManageViewModels> getAllStore();
        IEnumerable<StoreSellerViewModels> getAllStoreSeller(string currentUserId);
        public StoreDetailViewModels UpdateStore(StoreDetailViewModels store);
        public StoreDetailViewModels getStoreDetailById(string id);
        IEnumerable<CheckExitStore> checkExit(string userid);
		public IEnumerable<getPriceStore> getPriceStorr(string storeID);
	}
}

