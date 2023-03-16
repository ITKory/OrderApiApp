using System;
using System.Collections.Generic;

namespace OrderApiApp.Model.Entity;

public partial class Product
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Article { get; set; } = null!;

    public string Image { get; set; } = null!;

    public long Cost { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
