using System;
using System.Collections.Generic;

namespace StoreMMO.Core.Models;

public partial class Store
{
    public string Id { get; set; }

    public string UserId { get; set; }

    public DateTimeOffset? CreatedDate { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }

    public string? IsAccept { get; set; }

    public virtual ICollection<StoreDetail> StoreDetails { get; set; } = new List<StoreDetail>();
    public virtual ICollection<OrderBuy> OrderBuys { get; set; } = new List<OrderBuy>();
    public virtual AppUser User { get; set; }
}
