using System;
using System.Data.SQLite;
using System.IO;

namespace MobileShopApp.Data
{
    public class DatabaseService
    {
        private readonly string _connectionString;
        private readonly string _databasePath;

        public DatabaseService()
        {
            _databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MobileShop.db");
            _connectionString = $"Data Source={_databasePath};Version=3;";
            InitializeDatabase();
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        public void InitializeDatabase()
        {
            if (!File.Exists(_databasePath))
            {
                SQLiteConnection.CreateFile(_databasePath);
            }

            CreateTables();
        }

        private void CreateTables()
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string createProductsTable = @"
                CREATE TABLE IF NOT EXISTS Products (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Brand TEXT NOT NULL,
                    Model TEXT NOT NULL,
                    Price DECIMAL(10,2) NOT NULL,
                    CostPrice DECIMAL(10,2) NOT NULL DEFAULT 0,
                    Quantity INTEGER NOT NULL,
                    LowStockThreshold INTEGER DEFAULT 5,
                    Description TEXT,
                    Category TEXT,
                    DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
                    SerialNumber TEXT,
                    Warranty TEXT,
                    IsAvailable BOOLEAN DEFAULT 1,
                    ImagePath TEXT
                )";

            string createSuppliersTable = @"
                CREATE TABLE IF NOT EXISTS Suppliers (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    ContactPerson TEXT,
                    Email TEXT,
                    Phone TEXT,
                    Address TEXT,
                    DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
                    Notes TEXT
                )";

            string createSalesTable = @"
                CREATE TABLE IF NOT EXISTS Sales (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    CustomerName TEXT,
                    CustomerPhone TEXT,
                    SaleDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    TotalAmount DECIMAL(10,2) NOT NULL,
                    DiscountAmount DECIMAL(10,2) DEFAULT 0,
                    FinalAmount DECIMAL(10,2) NOT NULL,
                    PaymentMethod TEXT,
                    Notes TEXT,
                    InvoiceNumber TEXT UNIQUE
                )";

            string createSaleItemsTable = @"
                CREATE TABLE IF NOT EXISTS SaleItems (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    SaleId INTEGER,
                    ProductId INTEGER,
                    Quantity INTEGER NOT NULL,
                    UnitPrice DECIMAL(10,2) NOT NULL,
                    TotalPrice DECIMAL(10,2) NOT NULL,
                    FOREIGN KEY (SaleId) REFERENCES Sales(Id),
                    FOREIGN KEY (ProductId) REFERENCES Products(Id)
                )";

            string createOrdersTable = @"
                CREATE TABLE IF NOT EXISTS Orders (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    CustomerId INTEGER,
                    OrderDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    TotalAmount DECIMAL(10,2) NOT NULL,
                    Status TEXT DEFAULT 'Pending',
                    PaymentMethod TEXT,
                    Notes TEXT,
                    FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
                )";

            string createOrderItemsTable = @"
                CREATE TABLE IF NOT EXISTS OrderItems (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    OrderId INTEGER,
                    ProductId INTEGER,
                    Quantity INTEGER NOT NULL,
                    UnitPrice DECIMAL(10,2) NOT NULL,
                    TotalPrice DECIMAL(10,2) NOT NULL,
                    FOREIGN KEY (OrderId) REFERENCES Orders(Id),
                    FOREIGN KEY (ProductId) REFERENCES Products(Id)
                )";

            using var command = new SQLiteCommand(createProductsTable, connection);
            command.ExecuteNonQuery();

            command.CommandText = createSuppliersTable;
            command.ExecuteNonQuery();

            command.CommandText = createSalesTable;
            command.ExecuteNonQuery();

            command.CommandText = createSaleItemsTable;
            command.ExecuteNonQuery();

            command.CommandText = createOrdersTable;
            command.ExecuteNonQuery();

            command.CommandText = createOrderItemsTable;
            command.ExecuteNonQuery();
        }

        public void BackupDatabase(string backupPath)
        {
            try
            {
                File.Copy(_databasePath, backupPath, true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Backup failed: {ex.Message}");
            }
        }

        public void RestoreDatabase(string backupPath)
        {
            try
            {
                if (File.Exists(backupPath))
                {
                    File.Copy(backupPath, _databasePath, true);
                }
                else
                {
                    throw new FileNotFoundException("Backup file not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Restore failed: {ex.Message}");
            }
        }
    }
}
