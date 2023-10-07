using System.Threading.Tasks;
using MarketSolo.Models;

namespace MarketSolo.Services.Interfaces;

public interface IAuthorizationService
{
    Task<User?> AuthorizeAsync(string? login, string? password);
}