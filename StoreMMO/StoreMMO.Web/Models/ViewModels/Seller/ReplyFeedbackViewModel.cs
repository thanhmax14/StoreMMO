using System.ComponentModel.DataAnnotations;

namespace StoreMMO.Web.Models.ViewModels.Seller
{
    public class ReplyFeedbackViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Relay is required.")]
        [StringLength(100, ErrorMessage = "Relay cannot be longer than 100 characters.")]

        public string Relay { get; set; }
    }
}
