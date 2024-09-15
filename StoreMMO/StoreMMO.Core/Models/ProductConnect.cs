using System;
using System.Collections.Generic;

namespace StoreMMO.Core.Models;

public partial class ProductConnect
{
    public string Id { get; set; }

    public string StoreDetailId { get; set; }

    public string ProductId { get; set; }

    public virtual Product Product { get; set; }

    public virtual StoreDetail StoreDetail { get; set; }
}
