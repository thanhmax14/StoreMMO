using System;
using System.Collections.Generic;

namespace StoreMMO.Core.Models;

public partial class InfoAdd
{
    public string Id { get; set; }

    public string ProductId { get; set; }

    public string Account { get; set; }

    public string Pwd { get; set; }

    public string StatusUpload { get; set; }

    public string Status { get; set; }

    public DateTimeOffset? CreatedDate { get; set; }

    public virtual Product Product { get; set; }
}
