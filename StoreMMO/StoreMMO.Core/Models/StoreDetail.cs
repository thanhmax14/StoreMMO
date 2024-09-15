using System;
using System.Collections.Generic;

namespace StoreMMO.Core.Models;

public partial class StoreDetail
{
    public string Id { get; set; }

    public string StoreId { get; set; }

    public string CategoryId { get; set; }

    public string StoreTypeId { get; set; }

    public string Name { get; set; }

    public string SubDescription { get; set; }

    public string DescriptionDetail { get; set; }

    public string Img { get; set; }

    public DateTimeOffset? CreatedDate { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();

    public virtual ICollection<ProductConnect> ProductConnects { get; set; } = new List<ProductConnect>();

    public virtual Store Store { get; set; }

    public virtual StoreType StoreType { get; set; }
}
