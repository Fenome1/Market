namespace MarketSolo.Models;

public sealed class User
{
    public int IdUser { get; set; }

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string FirstName { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? IdRole { get; set; }

    public Role? IdRoleNavigation { get; set; }
}