using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MobileShopApp.Models;
using MobileShopApp.Services;

namespace MobileShopApp.Views
{
    public partial class DashboardWindow : Window
    {
        private readonly ProductService _productService;
        private readonly SaleService _saleService;

        public DashboardWindow()
        {
            InitializeComponent();
            _productService = new ProductService();
            _saleService = new SaleService();
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            try
            {
                // Load product statistics
                var allProducts = _productService.GetAllProducts();
                var lowStockProducts = _productService.GetLowStockProducts();

                TotalProductsText.Text = allProducts.Count.ToString();
                LowStockCountText.Text = lowStockProducts.Count.ToString();

                // Load today's sales
                var todayStart = DateTime.Today;
                var todayEnd = DateTime.Today.AddDays(1).AddTicks(-1);
                var todaySales = _saleService.GetTotalSalesForPeriod(todayStart, todayEnd);
                TodaySalesText.Text = todaySales.ToString("C");

                // Load low stock products
                LowStockDataGrid.ItemsSource = lowStockProducts;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
