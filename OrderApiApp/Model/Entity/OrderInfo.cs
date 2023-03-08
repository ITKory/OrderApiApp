using System;
using System.Collections.Generic;

namespace OrderApiApp.Model.Entity;

public partial class OrderInfo
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public long OrderId { get; set; }

    public long ProductCount { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
