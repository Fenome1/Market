using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketSolo.Configuration;
using MarketSolo.Models;
using MarketSolo.Services.Interfaces;
using MarketSolo.View;

namespace MarketSolo.ViewModel;

public partial class AuthViewModel : ObservableObject
{
    [ObservableProperty] private string? _userLogin;

    private readonly IAuthorizationService _authorizationService;
    private readonly IProductService _productService;
    private readonly IUserService _userService;

    public AuthViewModel(IAuthorizationService authorizationService, IProductService productService,
        IUserService userService)
    {
        _authorizationService = authorizationService;
        _productService = productService;
        _userService = userService;
    }

    [RelayCommand]
    private async Task LoginAsync(object? parameter)
    {
        var password = (parameter as PasswordBox)?.Password;

        if (string.IsNullOrEmpty(_userLogin) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Не все поля заполнены!", "Атеншн", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var user = await _authorizationService.AuthorizeAsync(_userLogin, password);

        if (user is null)
        {
            MessageBox.Show("Не верный логин или пароль!", "Атеншн", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        switch (user.IdRole)
        {
            case 1 or 3:
                NavigateToMainWindow(user);
                break;
            case 2:
                NavigateToAdminWindow(user);
                break;
        }
    }

    [RelayCommand]
    private void Guest()
    {
        NavigateToMainWindow(null);
    }

    private static void NavigateToMainWindow(User? user)
    {
        var mainWindowViewModel = AppConfig.Container.Resolve<MainWindowViewModel>();
        mainWindowViewModel.User = user;
        new MainWindow(mainWindowViewModel).Show();
        CloseWindow();
    }

    private void NavigateToAdminWindow(User user)
    {
        var adminWindowViewModel = AppConfig.Container.Resolve<AdminViewModel>();
        adminWindowViewModel.User = user;
        new AdminWindow(adminWindowViewModel).Show();
        CloseWindow();
    }

    private static void CloseWindow()
    {
        Application.Current.Windows[0]?.Close();
    }
}