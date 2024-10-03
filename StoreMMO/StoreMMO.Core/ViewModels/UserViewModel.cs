namespace StoreMMO.Core.ViewModels
{
    public class UserViewModel
    {
        public string UserID { get; set; }
        public string? FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public bool IsSeller { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string RoleName { get; set; }
    }
}
