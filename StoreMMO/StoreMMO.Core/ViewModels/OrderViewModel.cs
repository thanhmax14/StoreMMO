using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class OrderViewModel
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string StoreID { get; set; }
        public string ProductTypeId { get; set; }
        public string? Status { get; set; }
        public string? OrderCode { get; set; }
        public string? totalMoney { get; set; }
    }
}
