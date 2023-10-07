using System.Collections.Generic;

namespace MarketSolo.Models;

public sealed class Discount
{
    public int IdDiscount { get; set; }

    public double Current { get; set; }

    public double Max { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}