using Autofac;
using MarketSolo.Configuration;
using MarketSolo.ViewModel;
using MarketSolo.ViewModel.Helpers;

namespace MarketSolo.View;

public partial class AuthWindow
{
    public AuthWindow()
    {
        ImageHelper.SendImagesToBytes();
        InitializeComponent();
        DataContext = AppConfig.Container.Resolve<AuthViewModel>();
    }

}