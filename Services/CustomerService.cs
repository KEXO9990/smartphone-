using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MobileShopApp.Data;
using MobileShopApp.Models;

namespace MobileShopApp.Services
{
    public class CustomerService
    {
        private readonly DatabaseService _databaseService;

        public CustomerService()
        {
            _databaseService = new DatabaseService();
        }

        public List<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();

            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Customers ORDER BY Name";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customers.Add(new Customer
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString() ?? string.Empty,
                    Email = reader["Email"]?.ToString() ?? string.Empty,
                    Phone = reader["Phone"]?.ToString() ?? string.Empty,
                    Address = reader["Address"]?.ToString() ?? string.Empty,
                    DateRegistered = Convert.ToDateTime(reader["DateRegistered"]),
                    TotalPurchases = Convert.ToDecimal(reader["TotalPurchases"]),
                    TotalOrders = Convert.ToInt32(reader["TotalOrders"])
                });
            }

            return customers;
        }

        public Customer? GetCustomerById(int id)
        {
            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Customers WHERE Id = @id";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Customer
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString() ?? string.Empty,
                    Email = reader["Email"]?.ToString() ?? string.Empty,
                    Phone = reader["Phone"]?.ToString() ?? string.Empty,
                    Address = reader["Address"]?.ToString() ?? string.Empty,
                    DateRegistered = Convert.ToDateTime(reader["DateRegistered"]),
                    TotalPurchases = Convert.ToDecimal(reader["TotalPurchases"]),
                    TotalOrders = Convert.ToInt32(reader["TotalOrders"])
                };
            }

            return null;
        }

        public void AddCustomer(Customer customer)
        {
            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = @"
                INSERT INTO Customers (Name, Email, Phone, Address)
                VALUES (@name, @email, @phone, @address)";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@name", customer.Name);
            command.Parameters.AddWithValue("@email", customer.Email);
            command.Parameters.AddWithValue("@phone", customer.Phone);
            command.Parameters.AddWithValue("@address", customer.Address);

            command.ExecuteNonQuery();
        }

        public void UpdateCustomer(Customer customer)
        {
            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = @"
                UPDATE Customers SET 
                    Name = @name, 
                    Email = @email, 
                    Phone = @phone, 
                    Address = @address
                WHERE Id = @id";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", customer.Id);
            command.Parameters.AddWithValue("@name", customer.Name);
            command.Parameters.AddWithValue("@email", customer.Email);
            command.Parameters.AddWithValue("@phone", customer.Phone);
            command.Parameters.AddWithValue("@address", customer.Address);

            command.ExecuteNonQuery();
        }

        public void DeleteCustomer(int id)
        {
            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = "DELETE FROM Customers WHERE Id = @id";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }

        public List<Customer> SearchCustomers(string searchTerm)
        {
            var customers = new List<Customer>();

            using var connection = _databaseService.GetConnection();
            connection.Open();

            string query = @"
                SELECT * FROM Customers 
                WHERE Name LIKE @search OR Email LIKE @search OR Phone LIKE @search
                ORDER BY Name";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@search", $"%{searchTerm}%");
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customers.Add(new Customer
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString() ?? string.Empty,
                    Email = reader["Email"]?.ToString() ?? string.Empty,
                    Phone = reader["Phone"]?.ToString() ?? string.Empty,
                    Address = reader["Address"]?.ToString() ?? string.Empty,
                    DateRegistered = Convert.ToDateTime(reader["DateRegistered"]),
                    TotalPurchases = Convert.ToDecimal(reader["TotalPurchases"]),
                    TotalOrders = Convert.ToInt32(reader["TotalOrders"])
                });
            }

            return customers;
        }
    }
}
