using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class ManageStoreViewModels
    {

        public string Id { get; set; }
        public string StoreName { get; set; }
        public string CategoryName { get; set; }
        public string PriceRange { get; set; }
        public double Commission { get; set; }
        public int TotalStock { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string? IsAccept { get; set; }



    }
}
