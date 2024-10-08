using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.InfoAdds
{
    public interface IInfoAddsService
    {
        IEnumerable<InfoAdd> getAllInforAdd();
        InfoAddViewModels getByIdInforAdd(string id);
        InfoAddViewModels AddInforAdd(InfoAddViewModels inforAddViewModels);
        InfoAddViewModels UpdateInforAdd(InfoAddViewModels inforAddViewModels);
        void DeleteInforAdd(string id);
    }
}
