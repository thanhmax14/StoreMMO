using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class TopStoreViewModels
    {
        public string StoreID { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public int TotalProductsSold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
