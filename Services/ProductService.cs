using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MobileShopApp.Data;
using MobileShopApp.Models;

namespace MobileShopApp.Services
{
    public class ProductService
    {
        private readonly DatabaseService _databaseService;

        public ProductService()
        {
            _databaseService = new DatabaseService();
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Products ORDER BY Name";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString() ?? string.Empty,
                    Brand = reader["Brand"].ToString() ?? string.Empty,
                    Model = reader["Model"].ToString() ?? string.Empty,
                    Price = Convert.ToDecimal(reader["Price"]),
                    CostPrice = Convert.ToDecimal(reader["CostPrice"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    LowStockThreshold = Convert.ToInt32(reader["LowStockThreshold"]),
                    Description = reader["Description"]?.ToString() ?? string.Empty,
                    Category = reader["Category"]?.ToString() ?? string.Empty,
                    DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                    SerialNumber = reader["SerialNumber"]?.ToString() ?? string.Empty,
                    Warranty = reader["Warranty"]?.ToString() ?? string.Empty,
                    IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                    ImagePath = reader["ImagePath"]?.ToString() ?? string.Empty
                });
            }

            return products;
        }

        public Product? GetProductById(int id)
        {
            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Products WHERE Id = @id";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString() ?? string.Empty,
                    Brand = reader["Brand"].ToString() ?? string.Empty,
                    Model = reader["Model"].ToString() ?? string.Empty,
                    Price = Convert.ToDecimal(reader["Price"]),
                    CostPrice = Convert.ToDecimal(reader["CostPrice"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    LowStockThreshold = Convert.ToInt32(reader["LowStockThreshold"]),
                    Description = reader["Description"]?.ToString() ?? string.Empty,
                    Category = reader["Category"]?.ToString() ?? string.Empty,
                    DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                    SerialNumber = reader["SerialNumber"]?.ToString() ?? string.Empty,
                    Warranty = reader["Warranty"]?.ToString() ?? string.Empty,
                    IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                    ImagePath = reader["ImagePath"]?.ToString() ?? string.Empty
                };
            }

            return null;
        }

        public void AddProduct(Product product)
        {
            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = @"
                INSERT INTO Products (Name, Brand, Model, Price, CostPrice, Quantity, LowStockThreshold, Description, Category, SerialNumber, Warranty, IsAvailable, ImagePath)
                VALUES (@name, @brand, @model, @price, @costPrice, @quantity, @lowStockThreshold, @description, @category, @serialNumber, @warranty, @isAvailable, @imagePath)";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@brand", product.Brand);
            command.Parameters.AddWithValue("@model", product.Model);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@costPrice", product.CostPrice);
            command.Parameters.AddWithValue("@quantity", product.Quantity);
            command.Parameters.AddWithValue("@lowStockThreshold", product.LowStockThreshold);
            command.Parameters.AddWithValue("@description", product.Description);
            command.Parameters.AddWithValue("@category", product.Category);
            command.Parameters.AddWithValue("@serialNumber", product.SerialNumber);
            command.Parameters.AddWithValue("@warranty", product.Warranty);
            command.Parameters.AddWithValue("@isAvailable", product.IsAvailable);
            command.Parameters.AddWithValue("@imagePath", product.ImagePath);

            command.ExecuteNonQuery();
        }

        public void UpdateProduct(Product product)
        {
            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = @"
                UPDATE Products SET 
                    Name = @name, 
                    Brand = @brand, 
                    Model = @model, 
                    Price = @price, 
                    CostPrice = @costPrice,
                    Quantity = @quantity, 
                    LowStockThreshold = @lowStockThreshold,
                    Description = @description, 
                    Category = @category, 
                    SerialNumber = @serialNumber, 
                    Warranty = @warranty, 
                    IsAvailable = @isAvailable,
                    ImagePath = @imagePath
                WHERE Id = @id";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", product.Id);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@brand", product.Brand);
            command.Parameters.AddWithValue("@model", product.Model);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@costPrice", product.CostPrice);
            command.Parameters.AddWithValue("@quantity", product.Quantity);
            command.Parameters.AddWithValue("@lowStockThreshold", product.LowStockThreshold);
            command.Parameters.AddWithValue("@description", product.Description);
            command.Parameters.AddWithValue("@category", product.Category);
            command.Parameters.AddWithValue("@serialNumber", product.SerialNumber);
            command.Parameters.AddWithValue("@warranty", product.Warranty);
            command.Parameters.AddWithValue("@isAvailable", product.IsAvailable);
            command.Parameters.AddWithValue("@imagePath", product.ImagePath);

            command.ExecuteNonQuery();
        }

        public void DeleteProduct(int id)
        {
            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = "DELETE FROM Products WHERE Id = @id";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            var products = new List<Product>();

            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = @"
                SELECT * FROM Products 
                WHERE Name LIKE @search OR Brand LIKE @search OR Model LIKE @search OR Category LIKE @search
                ORDER BY Name";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@search", $"%{searchTerm}%");
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString() ?? string.Empty,
                    Brand = reader["Brand"].ToString() ?? string.Empty,
                    Model = reader["Model"].ToString() ?? string.Empty,
                    Price = Convert.ToDecimal(reader["Price"]),
                    CostPrice = Convert.ToDecimal(reader["CostPrice"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    LowStockThreshold = Convert.ToInt32(reader["LowStockThreshold"]),
                    Description = reader["Description"]?.ToString() ?? string.Empty,
                    Category = reader["Category"]?.ToString() ?? string.Empty,
                    DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                    SerialNumber = reader["SerialNumber"]?.ToString() ?? string.Empty,
                    Warranty = reader["Warranty"]?.ToString() ?? string.Empty,
                    IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                    ImagePath = reader["ImagePath"]?.ToString() ?? string.Empty
                });
            }

            return products;
        }

        public List<Product> GetLowStockProducts()
        {
            var products = new List<Product>();

            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Products WHERE Quantity <= LowStockThreshold AND IsAvailable = 1 ORDER BY Quantity ASC";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString() ?? string.Empty,
                    Brand = reader["Brand"].ToString() ?? string.Empty,
                    Model = reader["Model"].ToString() ?? string.Empty,
                    Price = Convert.ToDecimal(reader["Price"]),
                    CostPrice = Convert.ToDecimal(reader["CostPrice"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    LowStockThreshold = Convert.ToInt32(reader["LowStockThreshold"]),
                    Description = reader["Description"]?.ToString() ?? string.Empty,
                    Category = reader["Category"]?.ToString() ?? string.Empty,
                    DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                    SerialNumber = reader["SerialNumber"]?.ToString() ?? string.Empty,
                    Warranty = reader["Warranty"]?.ToString() ?? string.Empty,
                    IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                    ImagePath = reader["ImagePath"]?.ToString() ?? string.Empty
                });
            }

            return products;
        }
    }
}
