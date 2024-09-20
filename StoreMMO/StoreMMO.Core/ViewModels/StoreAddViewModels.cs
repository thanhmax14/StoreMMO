using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class StoreAddViewModels
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public bool? IsAccept { get; set; }

        public double? Price { get; set; }
    }
}
