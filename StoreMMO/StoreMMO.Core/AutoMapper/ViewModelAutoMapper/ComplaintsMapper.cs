using StoreMMO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.AutoMapper.ViewModelAutoMapper
{
    public class ComplaintsMapper
    {
        public string ID { get; set; }
        public string OrderDetailID { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Reply { get; set; }
        public string? Status { get; set; }
        public virtual OrderDetailsMapper OrderDetailmap { get; set; }
    }
}