using System;
using System.Globalization;
using System.Windows.Data;
using MarketSolo.ViewModel.Helpers;

namespace MarketSolo.Converters;

public class ImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ImageHelper.GetImageByBytes(value);
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}