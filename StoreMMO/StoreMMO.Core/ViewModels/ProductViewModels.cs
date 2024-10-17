using StoreMMO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class ProductViewModels
    {
        public string Id { get; set; }

        public string ProductTypeId { get; set; }

        public string Account { get; set; }

        public string Pwd { get; set; }

        public string StatusUpload { get; set; }

        public string Status { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public virtual ProductType ProductType { get; set; }

    }
}
