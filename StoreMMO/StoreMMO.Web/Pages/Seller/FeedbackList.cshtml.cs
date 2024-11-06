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
            var checkUserID = HttpContext.Session.GetString("UserID");
            list = this._feedBackService.getAllFeedBack(checkUserID);
        }
    }
}
