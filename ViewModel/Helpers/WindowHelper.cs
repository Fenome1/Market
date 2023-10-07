using System.Windows;
using MarketSolo.View;

namespace MarketSolo.ViewModel.Helpers;

public static class WindowHelper
{
    public static void CloseWindow()
    {
        Application.Current.Windows[0]?.Close();
    }

    public static void ShowAuthWindow()
    {
        new AuthWindow().Show();
    }
}