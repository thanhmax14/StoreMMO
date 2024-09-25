using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.API.Services
{
    public interface IFeedBackService
    {
        IEnumerable<FeedBack> getAllFeedBack();
        FeedBackViewModels getByIdFeedBack(string id);
        FeedBackViewModels AddFeedBacK(FeedBackViewModels feedBack);
        FeedBackViewModels UpdatefeedBack(FeedBackViewModels feedBack);

        void DeleteFeedBack(string id);
    }
}
