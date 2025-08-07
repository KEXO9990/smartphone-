using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MobileShopApp.Models;
using MobileShopApp.Services;

namespace MobileShopApp.Views
{
    public partial class ProductsWindow : Window
    {
        private readonly ProductService _productService;
        private List<Product> _allProducts;

        public ProductsWindow()
        {
            InitializeComponent();
            _productService = new ProductService();
            _allProducts = new List<Product>();
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                _allProducts = _productService.GetAllProducts();
                ProductsDataGrid.ItemsSource = _allProducts;
                UpdateCountText();
                StatusText.Text = "Products loaded successfully";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusText.Text = "Error loading products";
            }
        }

        private void UpdateCountText()
        {
            CountText.Text = $"Total Products: {_allProducts.Count}";
        }

        private void SearchProducts()
        {
            try
            {
                string searchTerm = SearchTextBox.Text.Trim();
                
                if (string.IsNullOrEmpty(searchTerm))
                {
                    ProductsDataGrid.ItemsSource = _allProducts;
                }
                else
                {
                    var filteredProducts = _allProducts.Where(p =>
                        p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        p.Brand.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        p.Model.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                    
                    ProductsDataGrid.ItemsSource = filteredProducts;
                    StatusText.Text = $"Found {filteredProducts.Count} products";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching products: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var addEditWindow = new AddEditProductWindow();
            if (addEditWindow.ShowDialog() == true)
            {
                LoadProducts();
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Product product)
            {
                var addEditWindow = new AddEditProductWindow(product);
                if (addEditWindow.ShowDialog() == true)
                {
                    LoadProducts();
                }
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Product product)
            {
                var result = MessageBox.Show($"Are you sure you want to delete '{product.Name}'?", 
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _productService.DeleteProduct(product.Id);
                        LoadProducts();
                        StatusText.Text = "Product deleted successfully";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;
            LoadProducts();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchProducts();
        }
    }
}
