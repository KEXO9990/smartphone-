using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using MobileShopApp.Data;
using MobileShopApp.Models;

namespace MobileShopApp.Services
{
    public class SaleService
    {
        private readonly DatabaseService _databaseService;

        public SaleService()
        {
            _databaseService = new DatabaseService();
        }

        public string GenerateInvoiceNumber()
        {
            return $"INV-{DateTime.Now:yyyyMMdd}-{DateTime.Now.Ticks.ToString().Substring(10)}";
        }

        public int CreateSale(Sale sale, List<SaleItem> saleItems)
        {
            using var connection = _databaseService.GetConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // Insert sale
                string saleQuery = @"
                    INSERT INTO Sales (CustomerName, CustomerPhone, SaleDate, TotalAmount, DiscountAmount, FinalAmount, PaymentMethod, Notes, InvoiceNumber)
                    VALUES (@customerName, @customerPhone, @saleDate, @totalAmount, @discountAmount, @finalAmount, @paymentMethod, @notes, @invoiceNumber);
                    SELECT last_insert_rowid();";

                using var saleCommand = new SQLiteCommand(saleQuery, connection, transaction);
                saleCommand.Parameters.AddWithValue("@customerName", sale.CustomerName ?? string.Empty);
                saleCommand.Parameters.AddWithValue("@customerPhone", sale.CustomerPhone ?? string.Empty);
                saleCommand.Parameters.AddWithValue("@saleDate", sale.SaleDate);
                saleCommand.Parameters.AddWithValue("@totalAmount", sale.TotalAmount);
                saleCommand.Parameters.AddWithValue("@discountAmount", sale.DiscountAmount);
                saleCommand.Parameters.AddWithValue("@finalAmount", sale.FinalAmount);
                saleCommand.Parameters.AddWithValue("@paymentMethod", sale.PaymentMethod);
                saleCommand.Parameters.AddWithValue("@notes", sale.Notes);
                saleCommand.Parameters.AddWithValue("@invoiceNumber", sale.InvoiceNumber);

                int saleId = Convert.ToInt32(saleCommand.ExecuteScalar());

                // Insert sale items and update product quantities
                foreach (var item in saleItems)
                {
                    string itemQuery = @"
                        INSERT INTO SaleItems (SaleId, ProductId, Quantity, UnitPrice, TotalPrice)
                        VALUES (@saleId, @productId, @quantity, @unitPrice, @totalPrice)";

                    using var itemCommand = new SQLiteCommand(itemQuery, connection, transaction);
                    itemCommand.Parameters.AddWithValue("@saleId", saleId);
                    itemCommand.Parameters.AddWithValue("@productId", item.ProductId);
                    itemCommand.Parameters.AddWithValue("@quantity", item.Quantity);
                    itemCommand.Parameters.AddWithValue("@unitPrice", item.UnitPrice);
                    itemCommand.Parameters.AddWithValue("@totalPrice", item.TotalPrice);
                    itemCommand.ExecuteNonQuery();

                    // Update product quantity
                    string updateQuantityQuery = "UPDATE Products SET Quantity = Quantity - @quantity WHERE Id = @productId";
                    using var updateCommand = new SQLiteCommand(updateQuantityQuery, connection, transaction);
                    updateCommand.Parameters.AddWithValue("@quantity", item.Quantity);
                    updateCommand.Parameters.AddWithValue("@productId", item.ProductId);
                    updateCommand.ExecuteNonQuery();
                }

                transaction.Commit();
                return saleId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public List<Sale> GetAllSales()
        {
            var sales = new List<Sale>();

            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = @"
                SELECT * FROM Sales 
                ORDER BY SaleDate DESC";

            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var sale = new Sale
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    CustomerName = reader["CustomerName"]?.ToString() ?? string.Empty,
                    CustomerPhone = reader["CustomerPhone"]?.ToString() ?? string.Empty,
                    SaleDate = Convert.ToDateTime(reader["SaleDate"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
                    FinalAmount = Convert.ToDecimal(reader["FinalAmount"]),
                    PaymentMethod = reader["PaymentMethod"]?.ToString() ?? string.Empty,
                    Notes = reader["Notes"]?.ToString() ?? string.Empty,
                    InvoiceNumber = reader["InvoiceNumber"]?.ToString() ?? string.Empty
                };

                sales.Add(sale);
            }

            return sales;
        }

        public Sale? GetSaleById(int id)
        {
            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = @"
                SELECT * FROM Sales 
                WHERE Id = @id";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var sale = new Sale
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    CustomerName = reader["CustomerName"]?.ToString() ?? string.Empty,
                    CustomerPhone = reader["CustomerPhone"]?.ToString() ?? string.Empty,
                    SaleDate = Convert.ToDateTime(reader["SaleDate"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
                    FinalAmount = Convert.ToDecimal(reader["FinalAmount"]),
                    PaymentMethod = reader["PaymentMethod"]?.ToString() ?? string.Empty,
                    Notes = reader["Notes"]?.ToString() ?? string.Empty,
                    InvoiceNumber = reader["InvoiceNumber"]?.ToString() ?? string.Empty
                };

                return sale;
            }

            return null;
        }

        public List<SaleItem> GetSaleItems(int saleId)
        {
            var saleItems = new List<SaleItem>();

            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = @"
                SELECT si.*, p.Name as ProductName, p.Brand, p.Model 
                FROM SaleItems si 
                INNER JOIN Products p ON si.ProductId = p.Id 
                WHERE si.SaleId = @saleId";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@saleId", saleId);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var saleItem = new SaleItem
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    SaleId = Convert.ToInt32(reader["SaleId"]),
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                    TotalPrice = Convert.ToDecimal(reader["TotalPrice"]),
                    Product = new Product
                    {
                        Id = Convert.ToInt32(reader["ProductId"]),
                        Name = reader["ProductName"].ToString() ?? string.Empty,
                        Brand = reader["Brand"].ToString() ?? string.Empty,
                        Model = reader["Model"].ToString() ?? string.Empty
                    }
                };

                saleItems.Add(saleItem);
            }

            return saleItems;
        }

        public decimal GetTotalSalesForPeriod(DateTime startDate, DateTime endDate)
        {
            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = "SELECT COALESCE(SUM(FinalAmount), 0) FROM Sales WHERE SaleDate BETWEEN @startDate AND @endDate";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@startDate", startDate);
            command.Parameters.AddWithValue("@endDate", endDate);

            return Convert.ToDecimal(command.ExecuteScalar());
        }

        public List<(string ProductName, int QuantitySold)> GetBestSellingProducts(int topCount = 10)
        {
            var bestSelling = new List<(string ProductName, int QuantitySold)>();

            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = @"
                SELECT p.Name, SUM(si.Quantity) as TotalSold
                FROM SaleItems si
                INNER JOIN Products p ON si.ProductId = p.Id
                GROUP BY si.ProductId, p.Name
                ORDER BY TotalSold DESC
                LIMIT @topCount";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@topCount", topCount);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                bestSelling.Add((
                    reader["Name"].ToString() ?? string.Empty,
                    Convert.ToInt32(reader["TotalSold"])
                ));
            }

            return bestSelling;
        }
    }
}
