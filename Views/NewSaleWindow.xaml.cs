using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MobileShopApp.Models;
using MobileShopApp.Services;

namespace MobileShopApp.Views
{
    public partial class NewSaleWindow : Window
    {
        private readonly ProductService _productService;
        private readonly SaleService _saleService;
        private List<SaleItem> _saleItems;
        private List<Product> _products;

        public NewSaleWindow()
        {
            InitializeComponent();
            _productService = new ProductService();
            _saleService = new SaleService();
            _saleItems = new List<SaleItem>();
            _products = new List<Product>();
            
            LoadData();
            SaleItemsDataGrid.ItemsSource = _saleItems;
        }

        private void LoadData()
        {
            try
            {
                _products = _productService.GetAllProducts().Where(p => p.IsAvailable && p.Quantity > 0).ToList();
                ProductComboBox.ItemsSource = _products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductComboBox.SelectedItem is Product selectedProduct)
            {
                if (!int.TryParse(QuantityTextBox.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity.", "Invalid Quantity", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (quantity > selectedProduct.Quantity)
                {
                    MessageBox.Show($"Not enough stock. Available: {selectedProduct.Quantity}", "Insufficient Stock", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Check if product already exists in sale items
                var existingItem = _saleItems.FirstOrDefault(si => si.ProductId == selectedProduct.Id);
                if (existingItem != null)
                {
                    if (existingItem.Quantity + quantity > selectedProduct.Quantity)
                    {
                        MessageBox.Show($"Not enough stock. Available: {selectedProduct.Quantity}, Already added: {existingItem.Quantity}", 
                            "Insufficient Stock", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    existingItem.Quantity += quantity;
                }
                else
                {
                    var saleItem = new SaleItem
                    {
                        ProductId = selectedProduct.Id,
                        Product = selectedProduct,
                        Quantity = quantity,
                        UnitPrice = selectedProduct.Price
                    };
                    _saleItems.Add(saleItem);
                }

                SaleItemsDataGrid.Items.Refresh();
                UpdateTotals();
                
                // Reset quantity textbox
                QuantityTextBox.Text = "1";
            }
            else
            {
                MessageBox.Show("Please select a product.", "No Product Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is SaleItem saleItem)
            {
                _saleItems.Remove(saleItem);
                SaleItemsDataGrid.Items.Refresh();
                UpdateTotals();
            }
        }

        private void DiscountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTotals();
        }

        private void UpdateTotals()
        {
            decimal subtotal = _saleItems.Sum(si => si.TotalPrice);
            
            if (!decimal.TryParse(DiscountTextBox.Text, out decimal discount))
                discount = 0;

            decimal total = subtotal - discount;

            SubtotalText.Text = subtotal.ToString("C");
            TotalText.Text = total.ToString("C");
        }

        private void CompleteSale_Click(object sender, RoutedEventArgs e)
        {
            if (!_saleItems.Any())
            {
                MessageBox.Show("Please add at least one product to the sale.", "No Products", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(DiscountTextBox.Text, out decimal discount))
                discount = 0;

            decimal subtotal = _saleItems.Sum(si => si.TotalPrice);
            decimal total = subtotal - discount;

            if (total < 0)
            {
                MessageBox.Show("Total amount cannot be negative.", "Invalid Total", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var sale = new Sale
                {
                    CustomerName = CustomerNameTextBox.Text?.Trim() ?? string.Empty,
                    CustomerPhone = CustomerPhoneTextBox.Text?.Trim() ?? string.Empty,
                    SaleDate = DateTime.Now,
                    TotalAmount = subtotal,
                    DiscountAmount = discount,
                    FinalAmount = total,
                    PaymentMethod = ((ComboBoxItem)PaymentMethodComboBox.SelectedItem)?.Content?.ToString() ?? "Cash",
                    InvoiceNumber = _saleService.GenerateInvoiceNumber()
                };

                int saleId = _saleService.CreateSale(sale, _saleItems);
                
                MessageBox.Show($"Sale completed successfully!\nInvoice Number: {sale.InvoiceNumber}", 
                    "Sale Completed", MessageBoxButton.OK, MessageBoxImage.Information);

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error completing sale: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
