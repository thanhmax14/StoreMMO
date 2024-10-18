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
        public string NameStore { get; set; }
        public string CateName { get; set; }
        public double Price { get; set; }
        public double Commission { get; set; }
        public string Stock { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public bool? IsAccept { get; set; }



    }
}
