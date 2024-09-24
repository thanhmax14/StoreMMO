using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class StoreDetailViewModel
    {
        public string OwnerUserName { get; set; } = "thanh";
        public string StoreName { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string CategoryName { get; set; }
        public int QuantityComment { get; set; }
        [NotMapped]
        public Dictionary<string, string> ProductStock { get; set; } = new Dictionary<string, string>();
      

    }
}
