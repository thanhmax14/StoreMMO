using System.ComponentModel.DataAnnotations;

namespace StoreMMO.Models
{
	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
        public string Email { get; set; }
    }
}
