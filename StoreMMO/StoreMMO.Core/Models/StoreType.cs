using System;
using System.Collections.Generic;

namespace StoreMMO.Core.Models;

public partial class StoreType
{
    public string Id { get; set; }

    public string Name { get; set; }

    public DateTimeOffset? CreatedDate { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }

    public virtual ICollection<StoreDetail> StoreDetails { get; set; } = new List<StoreDetail>();
}
