using System.ComponentModel.DataAnnotations;

namespace StoreMMO.Web.Models.ViewModels
{
    public class UserUpdateViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot be longer than 100 characters.")]
        public string FullName { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }


        [Phone(ErrorMessage = "Invalid Phone Number.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Only numbers are allowed.")]
        [StringLength(10, ErrorMessage = "The phone number must be up to 10 digits.")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }


        [StringLength(250, ErrorMessage = "Address cannot be longer than 250 characters.")]
        public string Address { get; set; }





    }
}
