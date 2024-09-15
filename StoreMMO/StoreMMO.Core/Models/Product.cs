using System;
using System.Collections.Generic;

namespace StoreMMO.Core.Models;

public partial class Product
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Stock { get; set; }

    public double? Price { get; set; }

    public DateTimeOffset? CreatedDate { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<InfoAdd> InfoAdds { get; set; } = new List<InfoAdd>();

    public virtual ICollection<ProductConnect> ProductConnects { get; set; } = new List<ProductConnect>();

    public virtual ICollection<WishList> WishLists { get; set; } = new List<WishList>();
}
