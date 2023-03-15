using System;
using System.Collections.Generic;

namespace OrderApiApp.Model.Entity;

public partial class Product
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Article { get; set; } = null!;

    public string? Image { get; set; }

    public long? Cost { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<OrderInfo> OrderInfos { get; } = new List<OrderInfo>();
}
