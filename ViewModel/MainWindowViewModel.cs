using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketSolo.Models;
using MarketSolo.Services.Interfaces;
using MarketSolo.ViewModel.Helpers;

namespace MarketSolo.ViewModel;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private User _user;

    [ObservableProperty] private ObservableCollection<string> _manufacturersName = new();

    [ObservableProperty] private string? _filteredText;
    [ObservableProperty] private string? _selectedDiscountRange;
    [ObservableProperty] private string? _selectedManufacturer;
    [ObservableProperty] private string? _selectedPriceSort;

    [ObservableProperty] private ObservableCollection<Product> _products;
    [ObservableProperty] private ObservableCollection<Product> _tempProducts;

    [ObservableProperty] private ObservableCollection<int> _selectedProductsId;
    [ObservableProperty] private bool _buttonState;

    public string UserInfo => _user is null ? "Гость" : $"{_user!.FirstName} {_user!.LastName} {_user!.MiddleName}";
    public int UserRole => _user is null ? 0 : (int)_user.IdRole;
    public bool UserCanSelect => UserRole == 3;

    private readonly IManufactureService _manufactureService;
    private readonly IProductService _productService;

    public MainWindowViewModel(IProductService productService, IManufactureService manufactureService)
    {
        _productService = productService;
        _manufactureService = manufactureService;

        WriteManufacturersComboBox();
        UpdateProduct();
    }

    [RelayCommand]
    private void ReturnBack()
    {
        WindowHelper.ShowAuthWindow();
        WindowHelper.CloseWindow();
    }

    [RelayCommand]
    private void DeleteProducts()
    {
        var message = "Товары не удалены.";
        if (_selectedProductsId.Any())
        {
            var result = MessageBox.Show("Вы действительно хотите удалить выбранны(-й/-е) товар(-ы)?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var deleteResult = _productService.DeleteProducts(_selectedProductsId);
                message = deleteResult switch
                {
                    -1 => "Ошибка удаления.",
                    0 => "Нечего удалять.",
                    _ => $"Удаленно товаров: {deleteResult}."
                };
            }
        }
        else
        {
            message = "Нечего удалять.";
        }
        UpdateProduct();
        MessageBox.Show(message);
    }

    private void UpdateProduct()
    {
        Products = _productService.GetProductsAsync();
        TempProducts = _products;
    }

    private void WriteManufacturersComboBox()
    {
        ManufacturersName.Add("Все");
        var manufacturers = _manufactureService.GetManufactures();
        foreach (var manufacturer in manufacturers)
            ManufacturersName.Add(manufacturer.Name);
    }

    public void FilterProduct()
    {
        var filteredProducts = _tempProducts.AsEnumerable();

        if (!string.IsNullOrEmpty(FilteredText))
            filteredProducts = filteredProducts
                .Where(p => p.Title.Contains(FilteredText, StringComparison.OrdinalIgnoreCase)
                            || p.Description.Contains(FilteredText, StringComparison.OrdinalIgnoreCase)
                            || p.IdManufacturerNavigation.Name.Contains(FilteredText, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(SelectedManufacturer) && SelectedManufacturer.ToLower() != "все")
            filteredProducts = filteredProducts.Where(p => p.IdManufacturerNavigation.Name.Equals(SelectedManufacturer, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(SelectedDiscountRange))
        {
            var currentRangeName = NameParser.GetCurrentName(SelectedDiscountRange).ToLower();
            if (currentRangeName != "все диапазоны")
            {
                var discountRange = DiscountRangeParser.ParseDiscountRange(currentRangeName);
                filteredProducts = filteredProducts.Where(p => p.IdDiscountNavigation.Current >= discountRange.Item1 && p.IdDiscountNavigation.Current <= discountRange.Item2);
            }
        }

        if (!string.IsNullOrEmpty(SelectedPriceSort))
        {
            var currentPriceSortName = NameParser.GetCurrentName(SelectedPriceSort).ToLower();
            if (currentPriceSortName != "нет")
            {
                filteredProducts = currentPriceSortName.ToLower() == "по возрастанию цены"
                    ? filteredProducts.OrderBy(p => p.Price)
                    : filteredProducts.OrderByDescending(p => p.Price);
            }
        }

        Products = new ObservableCollection<Product>(filteredProducts);
    }
}