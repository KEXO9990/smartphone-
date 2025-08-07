using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MobileShopApp.Models;
using MobileShopApp.Services;

namespace MobileShopApp.Views
{
    public partial class SalesWindow : Window
    {
        private readonly SaleService _saleService;
        private List<Sale> _allSales;

        public SalesWindow()
        {
            InitializeComponent();
            _saleService = new SaleService();
            _allSales = new List<Sale>();
            LoadSales();
        }

        private void LoadSales()
        {
            try
            {
                _allSales = _saleService.GetAllSales();
                SalesDataGrid.ItemsSource = _allSales;
                UpdateCountText();
                StatusText.Text = "Sales loaded successfully";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sales: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusText.Text = "Error loading sales";
            }
        }

        private void UpdateCountText()
        {
            CountText.Text = $"Total Sales: {_allSales.Count}";
        }

        private void NewSale_Click(object sender, RoutedEventArgs e)
        {
            var newSaleWindow = new NewSaleWindow();
            if (newSaleWindow.ShowDialog() == true)
            {
                LoadSales();
            }
        }

        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            if (SalesDataGrid.SelectedItem is Sale sale)
            {
                var detailsWindow = new SaleDetailsWindow(sale);
                detailsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a sale to view details.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void PrintInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (SalesDataGrid.SelectedItem is Sale sale)
            {
                try
                {
                    // TODO: Implement invoice printing
                    MessageBox.Show($"Printing invoice {sale.InvoiceNumber}...", "Print", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error printing invoice: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a sale to print.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadSales();
        }
    }
}
