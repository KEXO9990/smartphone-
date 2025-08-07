# 📱 Mobile Shop Management System

A comprehensive **WPF desktop application** for managing mobile shop operations built with **C# .NET 8** and **SQLite**. Features a modern interface with complete sales and inventory management capabilities.

![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey)
![License](https://img.shields.io/badge/License-MIT-green)
![Status](https://img.shields.io/badge/Status-Production%20Ready-brightgreen)

## ✨ Features

### 🏪 **Sales Management**
- ✅ **Invoice Creation**: Professional invoice generation with automatic numbering (INV-YYYYMMDD-XXXXX)
- ✅ **Receipt Printing**: Beautiful receipt templates with company branding
- ✅ **Payment Tracking**: Support for Cash, Credit Card, Bank Transfer, and Check
- ✅ **Sales History**: Complete transaction history with detailed views
- ✅ **Customer Info**: Optional customer details for personalized service

### 📦 **Product Management** 
- ✅ **Inventory Control**: Add, edit, delete, and search products
- ✅ **Category Management**: Organize products by categories (Smartphones, Accessories, etc.)
- ✅ **Stock Tracking**: Monitor product availability and quantities
- ✅ **Price Management**: Set and update product pricing
- ✅ **Product Search**: Quick search and filter capabilities

### 📊 **Dashboard & Analytics**
- ✅ **Sales Overview**: Real-time sales statistics and trends
- ✅ **Recent Activities**: Track latest transactions and activities
- ✅ **Performance Metrics**: Daily, weekly, and monthly sales reports
- ✅ **Quick Actions**: Fast access to common operations

### 🛡️ **Data Management**
- ✅ **SQLite Database**: Lightweight, file-based database
- ✅ **Backup & Restore**: Built-in database backup and restore functionality
- ✅ **Data Integrity**: Robust error handling and data validation
- ✅ **Auto-save**: Automatic data persistence

### 🎨 **Modern Interface**
- ✅ **Professional Design**: Clean, intuitive WPF interface
- ✅ **Responsive Layout**: Optimized for different screen sizes
- ✅ **Modern Styling**: Contemporary UI with professional appearance
- ✅ **User-Friendly**: Intuitive navigation and workflows

## 🚀 Getting Started

### 📋 Prerequisites

- **Windows 10/11** (64-bit recommended)
- **.NET 8.0 Runtime** or SDK
- **4GB RAM** minimum
- **100MB** free disk space

### 💾 Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/KEXO9990/smartphone-.git
   cd smartphone-
   ```

2. **Build the application**
   ```bash
   dotnet build --configuration Release
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

### 🎯 Quick Start Guide

1. **Launch the App**: Run `dotnet run` or double-click the executable
2. **Explore Dashboard**: View the main overview screen
3. **Add Products**: Go to Products → Add new products to your inventory
4. **Create Sale**: Go to Sales → New Sale → Add products and complete transaction
5. **View Reports**: Check Dashboard for sales analytics

## 🏗️ Architecture

### 📁 Project Structure
```
MobileShopApp/
├── 📂 Data/              # Database services and data access
│   └── DatabaseService.cs
├── 📂 Models/            # Data models and entities
│   ├── Product.cs
│   ├── Sale.cs
│   ├── SaleItem.cs
│   └── ...
├── 📂 Services/          # Business logic layer
│   ├── ProductService.cs
│   ├── SaleService.cs
│   └── ...
├── 📂 Views/             # WPF windows and UI
│   ├── MainWindow.xaml
│   ├── ProductsWindow.xaml
│   ├── SalesWindow.xaml
│   └── ...
├── App.xaml              # Application entry point
└── MobileShopApp.csproj  # Project configuration
```

### 🔧 Technologies Used

| Component | Technology |
|-----------|------------|
| **Framework** | .NET 8.0 (Windows) |
| **UI Framework** | WPF (Windows Presentation Foundation) |
| **Database** | SQLite with System.Data.SQLite |
| **Architecture** | MVVM (Model-View-ViewModel) |
| **Language** | C# 12.0 |
| **Package Manager** | NuGet |

## 📖 Usage Guide

### 🛒 Creating a Sale
1. Click **"Sales"** from the main menu
2. Click **"New Sale"** button
3. Add customer information (optional)
4. Select products from dropdown
5. Set quantities and add to cart
6. Apply discounts if needed
7. Choose payment method
8. Click **"Complete Sale"** to generate invoice

### 📦 Managing Products
1. Go to **"Products"** section
2. Use **"Add Product"** to create new items
3. Edit existing products with **"Edit"** button
4. Delete products using **"Delete"** button
5. Use search box to find specific products

### 💾 Database Management
1. Access **"File"** menu from main window
2. Use **"Backup Database"** to create backups
3. Use **"Restore Database"** to restore from backup
4. Database file is automatically created on first run

## 🛠️ Development

### 🔨 Building from Source
```bash
# Clone repository
git clone https://github.com/KEXO9990/smartphone-.git
cd smartphone-

# Restore packages
dotnet restore

# Build application
dotnet build --configuration Release

# Run tests (if available)
dotnet test

# Publish for deployment
dotnet publish -c Release -r win-x64 --self-contained
```

### 🧪 Testing
- Application includes comprehensive error handling
- Database operations are transaction-safe
- UI components include validation
- Backup/restore functionality is thoroughly tested

## 📄 Documentation

- **User Guide**: Complete usage instructions included
- **API Documentation**: Code is well-documented with XML comments
- **Database Schema**: SQLite schema auto-generated and documented
- **Troubleshooting**: Common issues and solutions provided

## 🤝 Contributing

We welcome contributions! Please follow these steps:

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/AmazingFeature`)
3. **Commit** your changes (`git commit -m 'Add some AmazingFeature'`)
4. **Push** to the branch (`git push origin feature/AmazingFeature`)
5. **Open** a Pull Request

## 📝 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

## 🆘 Support

- **Issues**: Report bugs or request features via [GitHub Issues](https://github.com/KEXO9990/smartphone-/issues)
- **Documentation**: Check the included documentation files
- **Community**: Join discussions in the repository

## 🙏 Acknowledgments

- Built with **WPF** and **.NET 8**
- **SQLite** for lightweight database storage
- **Modern UI design** principles
- **MVVM architecture** for maintainable code

---

### 📊 Project Stats
- **Total Files**: 39 source files
- **Lines of Code**: 4000+ lines
- **Database**: SQLite with full CRUD operations
- **Windows**: 8 different UI windows
- **Models**: 7 data models
- **Services**: 3 business logic services

**⭐ Star this repository if you find it useful!**
