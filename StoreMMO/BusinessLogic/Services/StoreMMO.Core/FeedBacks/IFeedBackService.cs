using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.FeedBacks

{
    public interface IFeedBackService
    {
        IEnumerable<FeedBackViewModels> getAllFeedBack(string StoreOwnerId);
        FeedBackViewModels getByIdFeedBack(string id);
        FeedBackViewModels AddFeedBacK(FeedBackViewModels feedBack);
        FeedBackViewModels UpdatefeedBack(FeedBackViewModels feedBack);

        void DeleteFeedBack(string id);
        IEnumerable<FeedBackViewModels> getFeedbackCustomerById(string feedbackID);
        FeedBack replyFeedback(string id, string reply);

    }
}
