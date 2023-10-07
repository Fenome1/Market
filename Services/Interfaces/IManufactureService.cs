using System.Collections.ObjectModel;
using MarketSolo.Models;

namespace MarketSolo.Services.Interfaces;

public interface IManufactureService
{
    ObservableCollection<Manufacturer> GetManufactures();
}