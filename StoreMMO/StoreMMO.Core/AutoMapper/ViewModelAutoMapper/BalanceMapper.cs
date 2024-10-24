using StoreMMO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.AutoMapper.ViewModelAutoMapper
{
    public class BalanceMapper
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
        public string? OrderCode { get; set; }
        public DateTime? ApprovalDate { get; set; }

        public virtual UserMapper Usermapforbalance  { get; set; }
    }
}
