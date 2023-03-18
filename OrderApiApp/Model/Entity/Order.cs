using System;
using System.Collections.Generic;

namespace OrderApiApp.Model.Entity;

public partial class Order
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public long ProductCount { get; set; }

    public virtual Product Product { get; set; } = null!;
}
