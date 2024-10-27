using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
	public class complantViewModels
	{
		public string ID { get; set; }
		public string OrderDetailID { get; set; }
		public string? Description { get; set; }
		public DateTime CreateDate { get; set; }
		public string? Reply { get; set; }
		public string? Status { get; set; }
	}
}
