using System;
using System.Collections.Generic;

namespace StoreMMO.Core.Models;

public partial class FeedBack
{
    public string Id { get; set; }

    public string UserId { get; set; }

    public string StoreDetailId { get; set; }

    public string Comments { get; set; }

    public DateTimeOffset? CreatedDate { get; set; }

    public double? Stars { get; set; }

    public string Relay { get; set; }

    public DateTimeOffset? DateRelay { get; set; }

    public bool? IsActive { get; set; }

    public virtual StoreDetail StoreDetail { get; set; }
    public virtual AppUser User { get; set; }
}
