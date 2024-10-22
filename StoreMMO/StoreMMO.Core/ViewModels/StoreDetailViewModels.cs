using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class StoreDetailViewModels
    {
        public string Id { get; set; }

        public string StoreId { get; set; }

        public string CategoryId { get; set; }

        public string StoreTypeId { get; set; }

        public string Name { get; set; }

        public string SubDescription { get; set; }

        public string DescriptionDetail { get; set; }

        public string? Img { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public bool? IsActive { get; set; }
    }
}
