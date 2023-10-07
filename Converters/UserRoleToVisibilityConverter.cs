using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MarketSolo.Converters;

public class UserRoleToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var idRole = (int)value;
        return idRole switch
        {
            3 => Visibility.Visible,
            _ => Visibility.Collapsed
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}