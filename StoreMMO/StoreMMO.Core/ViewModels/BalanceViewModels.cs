using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class BalanceViewModels
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
        public string? OrderCode { get; set; }
        public DateTime? approve { get; set; }

		// Thông tin ngân hàng
		public string? Bank { get; set; }
		public string? NameBank { get; set; }
		public string? NumberBank { get; set; }
		public string? Money { get; set; }
		// Hàm phân tích chuỗi giao dịch
		
	}
}
