using System.Collections.Generic;

namespace MarketSolo.Models;

public sealed class Role
{
    public int IdRole { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<User> Users { get; set; } = new List<User>();
}