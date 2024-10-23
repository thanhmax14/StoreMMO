using StoreMMO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.AutoMapper.ViewModelAutoMapper
{
    public class StoreMapper
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string? IsAccept { get; set; }

        public virtual UserMapper Usermapper { get; set; }
    }
}
