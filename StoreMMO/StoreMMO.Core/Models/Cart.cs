using System;
using System.Collections.Generic;

namespace StoreMMO.Core.Models;

public partial class Cart
{
    public string Id { get; set; }

    public string ProductId { get; set; }

    public string UserId { get; set; }

    public virtual Product Product { get; set; }
    public virtual AppUser User { get; set; }

}
