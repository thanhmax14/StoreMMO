using StoreMMO.Core.ViewModels;

namespace StoreMMO.Services.Store
{
    public interface IStoreService
    {
        IEnumerable<StoreViewModels> getAll();
    }
}
