using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MarketSolo.Data;
using MarketSolo.Models;
using MarketSolo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketSolo.Services;

public class UserService : IUserService
{
    private readonly MarketDbContext _context;

    public UserService(MarketDbContext context)
    {
        _context = context;
    }

    public async Task<ObservableCollection<User>> GetUsersAsync()
    {
        var users = await _context.Users
            .AsNoTracking()
            .Include(u => u.IdRoleNavigation)
            .ToListAsync();
        return new ObservableCollection<User?>(users);
    }
}