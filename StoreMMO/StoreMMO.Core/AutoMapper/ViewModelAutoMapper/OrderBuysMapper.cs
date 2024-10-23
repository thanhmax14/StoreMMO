using StoreMMO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.AutoMapper.ViewModelAutoMapper
{
    public class OrderBuysMapper
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string StoreID { get; set; }
        public string ProductTypeId { get; set; }
        public string? Status { get; set; }
        public string? OrderCode { get; set; }
        public string? totalMoney { get; set; }
        public virtual UserMapper UserMap { get; set; }
        public virtual StoreMapper StoreMap { get; set; }

    }
}

//  public virtual AppUser AppUser { get; set; }
//    public virtual Store Store { get; set; }
//  public virtual ProductType ProductType { get; set; }
