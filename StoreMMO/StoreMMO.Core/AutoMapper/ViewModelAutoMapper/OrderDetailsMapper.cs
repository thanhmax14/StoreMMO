using StoreMMO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.AutoMapper.ViewModelAutoMapper
{
    public class OrderDetailsMapper
    {
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
        public virtual OrderBuysMapper orderBuymap { get; set; }
        public virtual ProductMapper productMapper { get; set; }

    }
}