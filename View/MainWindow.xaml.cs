using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MarketSolo.Models;
using MarketSolo.ViewModel;

namespace MarketSolo.View;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel? mainWindowView = default)
    {
        DataContext = mainWindowView;
        InitializeComponent();
    }

    private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var textBox = sender as TextBox;
        var viewModel = DataContext as MainWindowViewModel;

        if (textBox == null || viewModel == null) return;
        viewModel.FilteredText = textBox.Text;
        viewModel.FilterProduct();
    }

    private void FilterAndSortChanged_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var viewModel = DataContext as MainWindowViewModel;
        viewModel.FilterProduct();
    }

    private void ProductsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var listView = sender as ListView;
        var viewModel = DataContext as MainWindowViewModel;
        var selectedItems = listView?.SelectedItems;

        if (selectedItems is { Count: > 0 })
        {
            viewModel.SelectedProductsId = new ObservableCollection<int>(selectedItems.Cast<Product>().Select(p => p.IdProduct));
            viewModel.ButtonState = true;
            return;
        }
        viewModel.ButtonState = false;
    }
}