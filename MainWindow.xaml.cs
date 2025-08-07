using System.Windows;
using System.Windows.Controls;
using MobileShopApp.Views;
using MobileShopApp.Data;
using Microsoft.Win32;
using System;
using System.IO;

namespace MobileShopApp
{
    public partial class MainWindow : Window
    {
        private readonly DatabaseService _databaseService;

        public MainWindow()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            var dashboardWindow = new DashboardWindow();
            dashboardWindow.Show();
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            var productsWindow = new ProductsWindow();
            productsWindow.Show();
        }

        private void Sales_Click(object sender, RoutedEventArgs e)
        {
            var salesWindow = new SalesWindow();
            salesWindow.Show();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            var salesWindow = new SalesWindow();
            salesWindow.Show();
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reports coming soon!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings coming soon!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Backup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Database files (*.db)|*.db|All files (*.*)|*.*",
                    DefaultExt = "db",
                    FileName = $"MobileShop_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.db"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    _databaseService.BackupDatabase(saveFileDialog.FileName);
                    MessageBox.Show("Database backup completed successfully!", "Backup", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Backup failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Database files (*.db)|*.db|All files (*.*)|*.*"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    var result = MessageBox.Show("Are you sure you want to restore the database? Current data will be replaced.", 
                        "Confirm Restore", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    
                    if (result == MessageBoxResult.Yes)
                    {
                        _databaseService.RestoreDatabase(openFileDialog.FileName);
                        MessageBox.Show("Database restored successfully!", "Restore", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Restore failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
