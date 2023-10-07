using System.Windows;
using MarketSolo.ViewModel;

namespace MarketSolo.View;

public partial class AdminWindow : Window
{
    public AdminWindow(AdminViewModel adminViewModel)
    {
        InitializeComponent();
        DataContext = adminViewModel;
    }
}