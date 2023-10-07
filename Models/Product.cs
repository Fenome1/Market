using System.Windows.Media;

namespace MarketSolo.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public string VendorCode { get; set; } = null!;

    public string Title { get; set; } = null!;

    public double Price { get; set; }

    public double Count { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public byte[]? ImageBinary { get; set; }

    public int? IdManufacturer { get; set; }

    public int? IdProvider { get; set; }

    public int? IdDiscount { get; set; }

    public int? IdCategory { get; set; }

    public Category? IdCategoryNavigation { get; set; }

    public Discount? IdDiscountNavigation { get; set; }

    public Manufacturer? IdManufacturerNavigation { get; set; }

    public Provider? IdProviderNavigation { get; set; }
}

public partial class Product
{
    public SolidColorBrush CurrentBrush
    {
        get
        {
            var colorBrush = new SolidColorBrush(Colors.White);

            if (Count == 0)
                colorBrush.Color = Colors.Gray;

            if (IdDiscountNavigation.Current > 15.00)
                colorBrush.Color = (Color)ColorConverter.ConvertFromString("#7fff00");

            return colorBrush;
        }
    }

    public bool IsPriceReduced => IdDiscountNavigation.Current > 0;

    public double TotalPrice => Price - (IdDiscountNavigation!.Current / 100 * Price);
}