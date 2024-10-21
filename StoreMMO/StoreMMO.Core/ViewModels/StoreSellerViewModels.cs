using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class StoreSellerViewModels
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SubDescription { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string IsAccept { get; set; }
    }
}
