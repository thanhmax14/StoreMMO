using System;

namespace StoreMMO.Core.Models
{
    public class Balance
    {
        public string Id { get; set; }                      
        public string UserId { get; set; }               

        public decimal Amount { get; set; }              
        public string TransactionType { get; set; }       
        public DateTime TransactionDate { get; set; }   
        public string? Description { get; set; }           
        public string Status { get; set; }  
        public DateTime? ApprovalDate { get; set; }       

        public virtual AppUser User { get; set; }        
    }
}
