using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class ViewProductModels
    {
        public string StoreId { get; set; }
        public string StoreDetailId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Stock { get; set; }
        public bool? IsActive { get; set; }
    }
}
