using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MarketSolo.Models;

namespace MarketSolo.Services.Interfaces;

public interface IUserService
{
    Task<ObservableCollection<User>> GetUsersAsync();
}