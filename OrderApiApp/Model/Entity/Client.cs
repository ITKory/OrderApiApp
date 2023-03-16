using System;
using System.Collections.Generic;

namespace OrderApiApp.Model.Entity;

public partial class Client
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; } = new List<Cart>();
}
