namespace MarketSolo.ViewModel.Helpers;

public static class NameParser
{
    public static string GetCurrentName(string comboBoxString)
    {
        return comboBoxString.Split(":")[1].Trim();
    }
}