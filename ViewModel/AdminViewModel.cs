using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketSolo.Models;
using MarketSolo.Services.Interfaces;
using MarketSolo.ViewModel.Helpers;

namespace MarketSolo.ViewModel;

public partial class AdminViewModel : ObservableObject
{
    private readonly IUserService _userService;

    [ObservableProperty] private User _user;
    [ObservableProperty] private ObservableCollection<User> _users;

    public AdminViewModel(IUserService userService)
    {
        _userService = userService;
        UpdateUsersAsync();
    }

    public string UserInfo => _user is null ? "Гость" : $"{_user!.FirstName} {_user!.LastName} {_user!.MiddleName}";

    private async Task UpdateUsersAsync()
    {
        Users = await _userService.GetUsersAsync();
    }

    [RelayCommand]
    private void ReturnBack()
    {
        WindowHelper.ShowAuthWindow();
        WindowHelper.CloseWindow();
    }
}