# Mobile Shop Management System

A comprehensive desktop application for managing mobile shop operations built with C# and WPF.

## Features

- **Product Management**: Add, edit, delete, and search products
- **Inventory Tracking**: Track stock quantities and availability
- **Database Backup & Restore**: SQLite database with backup functionality
- **Modern UI**: Clean and intuitive WPF interface
- **Search Functionality**: Quick product search and filtering

## Technologies Used

- **Framework**: .NET 8.0 (Windows)
- **UI Framework**: WPF (Windows Presentation Foundation)
- **Database**: SQLite
- **Language**: C#

## Project Structure

```
MobileShopApp/
├── Models/                 # Data models
│   ├── Product.cs
│   ├── Customer.cs
│   ├── Order.cs
│   └── OrderItem.cs
├── Views/                  # UI Windows
│   ├── ProductsWindow.xaml
│   ├── ProductsWindow.xaml.cs
│   ├── AddEditProductWindow.xaml
│   └── AddEditProductWindow.xaml.cs
├── Services/               # Business logic
│   ├── ProductService.cs
│   └── CustomerService.cs
├── Data/                   # Database operations
│   └── DatabaseService.cs
├── MainWindow.xaml         # Main application window
└── MainWindow.xaml.cs
```

## Getting Started

### Prerequisites

- .NET 8.0 SDK or higher
- Visual Studio 2022 or Visual Studio Code
- Windows OS

### Installation

1. Clone the repository or download the project files
2. Open the solution in Visual Studio
3. Restore NuGet packages
4. Build and run the application

### First Run

On the first run, the application will automatically create a SQLite database file (`MobileShop.db`) in the application directory.

## Usage

### Managing Products

1. Click on "📦 Products" in the sidebar
2. Use the "Add New Product" button to add products
3. Edit products by clicking the edit (✏️) button
4. Delete products by clicking the delete (🗑️) button
5. Search products using the search box

### Database Backup

1. Click on "💾 Backup Database" in the sidebar
2. Choose a location to save the backup file
3. The backup will be created with a timestamp

### Database Restore

1. Click on "📂 Restore Database" in the sidebar
2. Select a backup file to restore
3. Confirm the restoration (this will replace current data)

## Database Schema

### Products Table
- Id (Primary Key)
- Name
- Brand
- Model
- Price
- Quantity
- Description
- Category
- DateAdded
- SerialNumber
- Warranty
- IsAvailable

### Customers Table
- Id (Primary Key)
- Name
- Email
- Phone
- Address
- DateRegistered
- TotalPurchases
- TotalOrders

### Orders Table
- Id (Primary Key)
- CustomerId (Foreign Key)
- OrderDate
- TotalAmount
- Status
- PaymentMethod
- Notes

### OrderItems Table
- Id (Primary Key)
- OrderId (Foreign Key)
- ProductId (Foreign Key)
- Quantity
- UnitPrice
- TotalPrice

## Future Enhancements

- Customer management interface
- Order processing system
- Sales reporting and analytics
- Barcode scanning integration
- Print receipts functionality
- Multi-user support with authentication
- Data export capabilities (Excel, PDF)

## Contributing

1. Fork the project
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For support and questions, please create an issue in the project repository.
