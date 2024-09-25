using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class FeedBackViewModels
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string StoreDetailId { get; set; }

        public string Comments { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public double? Stars { get; set; }

        public string Relay { get; set; }

        public DateTimeOffset? DateRelay { get; set; }

        public bool? IsActive { get; set; }

    }
}
