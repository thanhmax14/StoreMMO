using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class StoreSellerViewModels
    {
        public string Id { get; set; } // Id từ StoreDetails
        public string StoreId { get; set; }
        public string Name { get; set; }
        public string SubDescription { get; set; }
        public string DescriptionDetail { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string IsAccept { get; set; }
    }
}
