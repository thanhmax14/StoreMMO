using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.FeedBacks;
using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.FeedBacks

{
    public class FeedBackService : IFeedBackService
    {
        private readonly IFeedBackRepository _feedBackRepository;
        public FeedBackService(IFeedBackRepository feedBackRepository)
        {
            _feedBackRepository = feedBackRepository;
        }
        public async Task<FeedBackViewModels> AddFeedBacKAsync(FeedBackViewModels feedBack)
        {
            return await _feedBackRepository.AddFeedBacKAsync(feedBack);
        }

        public void DeleteFeedBack(string id)
        {
            _feedBackRepository.DeleteFeedBack(id);
        }

        public IEnumerable<FeedBackViewModels> getAllFeedBack(string StoreOwnerId)
        {
            return _feedBackRepository.getAll(StoreOwnerId);
        }

        public FeedBackViewModels getByIdFeedBack(string id)
        {
            return _feedBackRepository.getById(id);
        }



        public FeedBackViewModels UpdatefeedBack(FeedBackViewModels feedBack)
        {
            return _feedBackRepository.UpdatefeedBack(feedBack);
        }

        public IEnumerable<FeedBackViewModels> getFeedbackCustomerById(string feedbackID)
        {
            return _feedBackRepository.getFeedbackCustomerById(feedbackID);
        }

        public FeedBack replyFeedback(string id, string reply)
        {
            return _feedBackRepository.replyFeedback(id, reply);
        }
    }
}
