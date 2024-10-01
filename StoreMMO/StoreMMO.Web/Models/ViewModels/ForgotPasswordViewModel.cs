using System.ComponentModel.DataAnnotations;

namespace StoreMMO.Web.Models.ViewModels
{
	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
        public string Email { get; set; }
    }
}
