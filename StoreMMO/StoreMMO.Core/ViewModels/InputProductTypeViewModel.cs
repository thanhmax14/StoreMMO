using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class InputProductTypeViewModel
    {
        public string Id { get; set; }
        public string? StoreDetailId { get; set; }

        public string Name { get; set; }

        public string Stock { get; set; }

        public double? Price { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public bool? IsActive { get; set; }
    }
}
