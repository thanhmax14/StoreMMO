using BusinessLogic.Services.StoreMMO.Core.FeedBacks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Seller
{
    public class FeedbackListModel : PageModel
    {
        private readonly IFeedBackService _feedBackService;
        public FeedbackListModel(IFeedBackService feedBackService)
        {
            _feedBackService = feedBackService;
        }

        public IEnumerable<FeedBackViewModels> list = new List<FeedBackViewModels>();
        public void OnGet()
        {
            /*var checkUserID = context.Session.GetString("UserID");*/
            list = this._feedBackService.getAllFeedBack("1f0dbbe2-2a81-43e9-8272-117507ac9c45");
        }
    }
}
