using System.Collections.Generic;

namespace MarketSolo.Models;

public sealed class Provider
{
    public int IdProvider { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}