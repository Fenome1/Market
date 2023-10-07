using System.Threading.Tasks;
using MarketSolo.Data;
using MarketSolo.Models;
using MarketSolo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketSolo.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly MarketDbContext _context;

    public AuthorizationService(MarketDbContext context)
    {
        _context = context;
    }

    public async Task<User?> AuthorizeAsync(string? login, string? password)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(u => u.IdRoleNavigation)
            .FirstOrDefaultAsync(user => user.Login == login && user.Password == password);
    }
}