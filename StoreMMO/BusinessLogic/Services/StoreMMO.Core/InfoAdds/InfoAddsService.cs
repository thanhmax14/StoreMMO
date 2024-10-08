using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.InfoAdds;
using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.InfoAdds
{
    public class InfoAddsService : IInfoAddsService
    {
        private readonly InfoAddRepository _infoAddRepository;

        public InfoAddsService(InfoAddRepository infoAddRepository)
        {
            _infoAddRepository = infoAddRepository;
        }

        public InfoAddViewModels AddInforAdd(InfoAddViewModels inforAddViewModels)
        {
            return _infoAddRepository.AddInforAdd(inforAddViewModels);
        }

        public void DeleteInforAdd(string id)
        {
            _infoAddRepository.DeleteInforAdd(id);
        }

        public IEnumerable<InfoAdd> getAllInforAdd()
        {
           return _infoAddRepository.getAllInforAdd();
        }

        public InfoAddViewModels getByIdInforAdd(string id)
        {
           return _infoAddRepository.getByIdInforAdd(id);
        }

        public InfoAddViewModels UpdateInforAdd(InfoAddViewModels inforAddViewModels)
        {
           return _infoAddRepository.UpdateInforAdd(inforAddViewModels);
        }
    }
}
