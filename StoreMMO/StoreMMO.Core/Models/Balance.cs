using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Models
{
    public class Balance
    {
        public string ID { get; set; }
        public virtual ICollection<AppUser> ProductConnects { get; set; } = new List<AppUser>();
    }
}
