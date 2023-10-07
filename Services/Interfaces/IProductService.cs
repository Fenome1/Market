using System.Collections.ObjectModel;
using MarketSolo.Models;

namespace MarketSolo.Services.Interfaces;

public interface IProductService
{
    ObservableCollection<Product> GetProductsAsync();
    int DeleteProducts(ObservableCollection<int> productsId);
}