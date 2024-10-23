using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class OrderDetailsViewModels
    {

        [Key]
        public string ID { get; set; }
        public string OrderBuyID { get; set; }
        public string ProductID { get; set; }
        public string? AdminMoney { get; set; }
        public string? SellerMoney { get; set; }
        public string? quantity { get; set; }
        public DateTime? Dates { get; set; }
        public string? status { get; set; }
        public string? stasusPayment { get; set; }
        public string? Price { get; set; }
    }
}
