using System;
using System.Collections.Generic;

namespace OrderApiApp.Model.Entity;

public partial class Cart
{
    public long Id { get; set; }

    public long OrderId { get; set; }
    
    public long ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
