using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Models
{
    public class AppUser: IdentityUser
    {
		[Required(ErrorMessage = "FullName cannot is blank")]
		public string? FullName { get; set; }

       
        public DateTime? DateOfBirth { get; set; }
		[Required(ErrorMessage = "Address cannot is blank")]

		public string? Address { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? DateDelete { get; set; }
        public  DateTime? DateRestore { get; set; }
        public DateTime? CreatedDate { get; set; }

     
        public DateTime? ModifiedDateUpdateProfile { get; set; }
        public bool IsSeller { get; set; } = false;  
        public DateTime? SellerApprovalDate { get; set; }
    }
}
