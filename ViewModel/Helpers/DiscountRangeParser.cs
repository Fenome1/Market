using System;

namespace MarketSolo.ViewModel.Helpers;

public static class DiscountRangeParser
{
    public static (float, float) ParseDiscountRange(string selectedItem)
    {
        return selectedItem switch
        {
            "0 - 9,99%" => ((float, float))(0, 9.99),
            "10 - 14,99%" => ((float, float))(10.0, 14.99),
            "15% и более" => ((float, float))(15.0, 100.0),
            _ => throw new ArgumentOutOfRangeException(nameof(selectedItem), selectedItem, null)
        };
    }
}