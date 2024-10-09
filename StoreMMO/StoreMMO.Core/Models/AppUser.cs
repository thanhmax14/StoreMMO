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
		
		public string? FullName { get; set; }

       
        public DateTime? DateOfBirth { get; set; }
		

		public string? Address { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? DateDelete { get; set; }
        public  DateTime? DateRestore { get; set; }
        public DateTime? CreatedDate { get; set; }   
        public DateTime? ModifiedDateUpdateProfile { get; set; }
        public bool IsSeller { get; set; } = false;
        public DateTime? RequestSellerDate { get; set; }
        public DateTime? SellerApprovalDate { get; set; }
    }
}
