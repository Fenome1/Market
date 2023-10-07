using System.Collections.ObjectModel;
using System.Linq;
using MarketSolo.Data;
using MarketSolo.Models;
using MarketSolo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketSolo.Services;

public class ManufactureService : IManufactureService
{
    private readonly MarketDbContext _context;

    public ManufactureService(MarketDbContext context)
    {
        _context = context;
    }

    public ObservableCollection<Manufacturer> GetManufactures()
    {
        return new ObservableCollection<Manufacturer>(_context.Manufacturers
            .AsNoTracking()
            .ToList());
    }
}