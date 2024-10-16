using System;
using System.Collections.Generic;

namespace StoreMMO.Core.Models;

public partial class WishList
{
    public string Id { get; set; }

    public string ProductTypeId { get; set; }

    public string UserId { get; set; }

    public virtual ProductType ProductType { get; set; }
    public virtual AppUser User { get; set; }
}
