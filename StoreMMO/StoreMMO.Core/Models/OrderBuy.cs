using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Models
{
    public class OrderBuy
    {
        [Key]
        public string ID { get; set; }
        public string UserID { get; set; }
        public string StoreID { get; set; }
        public string ProductTypeId { get; set; }
        public string?  Status { get; set; }
        public string?  OrderCode { get; set; }
        public string?  totalMoney { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual Store Store { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual ICollection<OrderDetail> StoreDetails { get; set; } = new List<OrderDetail>();




    }
}
