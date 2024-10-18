using System;
using System.Collections.Generic;

namespace StoreMMO.Core.Models;

public partial class Product
{
    public string Id { get; set; }

    public string ProductTypeId { get; set; }

    public string Account { get; set; }

    public string Pwd { get; set; }

    public string StatusUpload { get; set; }

    public string Status { get; set; }

    public DateTimeOffset? CreatedDate { get; set; }

    public virtual ProductType ProductType { get; set; }
    public virtual ICollection<OrderDetail> StoreDetails { get; set; } = new List<OrderDetail>();
}
