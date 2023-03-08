using System;
using System.Collections.Generic;

namespace OrderApiApp.Model.Entity;

public partial class Order
{
    public long Id { get; set; }

    public long ClientId { get; set; }

    public string Description { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<OrderInfo> OrderInfos { get; } = new List<OrderInfo>();
}
