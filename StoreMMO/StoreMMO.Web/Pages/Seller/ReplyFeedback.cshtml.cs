using BusinessLogic.Services.StoreMMO.Core.FeedBacks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels.Seller;

namespace StoreMMO.Web.Pages.Seller
{
	[Authorize(Roles = "Seller")]
	public class ReplyFeedbackModel : PageModel
    {
        private readonly IFeedBackService _feedBackService;
        public ReplyFeedbackModel(IFeedBackService feedBackService)
        {
            _feedBackService = feedBackService;
        }

        [BindProperty]
        public ReplyFeedbackViewModel input { get; set; }

        public IEnumerable<FeedBackViewModels> list = new List<FeedBackViewModels>();
        [TempData]
        public string success { get; set; }
        [TempData]
        public string fail { get; set; }
        public IActionResult OnGet(string feedbackID)
        {
            list = this._feedBackService.getFeedbackCustomerById(feedbackID);
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            else
            {
                var tem = input;
                var update = this._feedBackService.replyFeedback(input.Id, input.Relay);
                if (update != null)
                {
                    success = "Update Thong Tin Thanh cong";
                }
                else
                {
                    fail = "Update Thong Tin That Bai";
                }
                return RedirectToPage("ReplyFeedback", new { feedbackID = update.Id });
            }
        }

    }

}

