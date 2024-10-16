using System;
using System.Collections.Generic;

namespace StoreMMO.Core.Models;

public partial class ProductConnect
{
    public string Id { get; set; }

    public string StoreDetailId { get; set; }

    public string ProductTypeId { get; set; }

    public virtual ProductType ProductType { get; set; }

    public virtual StoreDetail StoreDetail { get; set; }
}
