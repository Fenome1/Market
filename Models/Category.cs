using System.Collections.Generic;

namespace MarketSolo.Models;

public sealed class Category
{
    public int IdCategory { get; set; }

    public string Title { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}