using System.Configuration;
using System.Data;
using System.Windows;
using MobileShopApp.Data;

namespace MobileShopApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        // Initialize database
        try
        {
            var databaseService = new DatabaseService();
            databaseService.InitializeDatabase();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Database initialization failed: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

