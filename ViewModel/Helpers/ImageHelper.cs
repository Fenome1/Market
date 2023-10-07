using MarketSolo.Configuration;
using MarketSolo.Data;
using System;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Autofac;

namespace MarketSolo.ViewModel.Helpers;

public static class ImageHelper
{
    public static void SendImagesToBytes()
    {
        var path = Path.Combine(Environment.CurrentDirectory, "Assets", "Pictures");
        var context = AppConfig.Container.Resolve<MarketDbContext>();

        var products = context.Products.ToList();
        foreach (var product in products)
        {
            var imagePath = product.Image ?? "default.png";
            var data = File.ReadAllBytes(Path.Combine(path, imagePath));
            product.ImageBinary = data;
        }

        context.UpdateRange(products);
        context.SaveChanges();
    }

    public static BitmapImage GetImageByBytes(object value)
    {
        if (value is not byte[] bytes) return null;

        using var ms = new MemoryStream(bytes);
        var image = new BitmapImage();

        image.BeginInit();
        image.CacheOption = BitmapCacheOption.OnLoad;
        image.StreamSource = ms;
        image.EndInit();

        return image;
    }
}