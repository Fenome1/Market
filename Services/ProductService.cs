using System;
using System.Collections.ObjectModel;
using System.Linq;
using MarketSolo.Data;
using MarketSolo.Models;
using MarketSolo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketSolo.Services;

public class ProductService : IProductService
{
    private readonly MarketDbContext _context;

    public ProductService(MarketDbContext context)
    {
        _context = context;
    }

    public ObservableCollection<Product> GetProductsAsync()
    {
        return new ObservableCollection<Product>(_context.Products
            .AsNoTracking()
            .Include(p => p.IdDiscountNavigation)
            .Include(p => p.IdManufacturerNavigation)
            .Include(p => p.IdCategoryNavigation)
            .Include(p => p.IdProviderNavigation)
            .ToList());
    }

    public int DeleteProducts(ObservableCollection<int> productsId)
    {
        if (!productsId.Any()) return 0;

        try
        {
            foreach (var productId in productsId)
            {
                var product = _context.Products.FirstOrDefault(p => p.IdProduct == productId);
                _context.Remove(product);
            }
            return ContextSave();
        }
        catch (Exception e)
        {
            return -1;
        }
    }
    private int ContextSave()
    {
        return _context.SaveChanges();
    }
}