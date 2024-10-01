using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.FeedBacks;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Services.StoreMMO.Core
{
    public class FeedBackService : IFeedBackService
    {
        private readonly IFeedBackRepository _feedBackRepository;
        public FeedBackService(IFeedBackRepository feedBackRepository)
        {
            _feedBackRepository = feedBackRepository;
        }
        public FeedBackViewModels AddFeedBacK(FeedBackViewModels feedBack)
        {
            return _feedBackRepository.AddFeedBacK(feedBack);
        }

        public void DeleteFeedBack(string id)
        {
            _feedBackRepository.DeleteFeedBack(id);
        }

        public IEnumerable<FeedBack> getAllFeedBack()
        {
            return _feedBackRepository.getAll();
        }

        public FeedBackViewModels getByIdFeedBack(string id)
        {
            return _feedBackRepository.getById(id);
        }

        public FeedBackViewModels UpdatefeedBack(FeedBackViewModels feedBack)
        {
          return _feedBackRepository.UpdatefeedBack(feedBack);
        }
    }
}
