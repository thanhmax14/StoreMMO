using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Core.Repositories.FeedBacks
{
    public interface IFeedBackRepository
    {
        IEnumerable<FeedBackViewModels> getAll(string StoreOwnerId);
        FeedBackViewModels getById(string id);
        FeedBackViewModels AddFeedBacK(FeedBackViewModels feedBack);
        FeedBackViewModels UpdatefeedBack(FeedBackViewModels feedBack);

        void DeleteFeedBack(string id);
        IEnumerable<FeedBackViewModels> getFeedbackCustomerById(string feedbackID);

        FeedBack replyFeedback(string id, string reply);

    }
}
