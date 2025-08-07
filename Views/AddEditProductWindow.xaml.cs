using System;
using System.Windows;
using MobileShopApp.Models;
using MobileShopApp.Services;

namespace MobileShopApp.Views
{
    public partial class AddEditProductWindow : Window
    {
        private readonly ProductService _productService;
        private Product? _product;
        private bool _isEditMode;

        public AddEditProductWindow()
        {
            InitializeComponent();
            _productService = new ProductService();
            _isEditMode = false;
            HeaderText.Text = "📝 Add New Product";
        }

        public AddEditProductWindow(Product product) : this()
        {
            _product = product;
            _isEditMode = true;
            HeaderText.Text = "✏️ Edit Product";
            LoadProductData();
        }

        private void LoadProductData()
        {
            if (_product == null) return;

            NameTextBox.Text = _product.Name;
            BrandTextBox.Text = _product.Brand;
            ModelTextBox.Text = _product.Model;
            CategoryComboBox.Text = _product.Category;
            PriceTextBox.Text = _product.Price.ToString();
            CostPriceTextBox.Text = _product.CostPrice.ToString();
            QuantityTextBox.Text = _product.Quantity.ToString();
            LowStockTextBox.Text = _product.LowStockThreshold.ToString();
            SerialNumberTextBox.Text = _product.SerialNumber;
            WarrantyComboBox.Text = _product.Warranty;
            DescriptionTextBox.Text = _product.Description;
            IsAvailableCheckBox.IsChecked = _product.IsAvailable;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Product name is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                NameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(BrandTextBox.Text))
            {
                MessageBox.Show("Brand is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                BrandTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(ModelTextBox.Text))
            {
                MessageBox.Show("Model is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                ModelTextBox.Focus();
                return false;
            }

            if (!decimal.TryParse(PriceTextBox.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Please enter a valid selling price.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                PriceTextBox.Focus();
                return false;
            }

            if (!decimal.TryParse(CostPriceTextBox.Text, out decimal costPrice) || costPrice < 0)
            {
                MessageBox.Show("Please enter a valid cost price.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                CostPriceTextBox.Focus();
                return false;
            }

            if (!int.TryParse(QuantityTextBox.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                QuantityTextBox.Focus();
                return false;
            }

            if (!int.TryParse(LowStockTextBox.Text, out int lowStock) || lowStock < 0)
            {
                MessageBox.Show("Please enter a valid low stock threshold.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                LowStockTextBox.Focus();
                return false;
            }

            return true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                var product = _isEditMode ? _product! : new Product();

                product.Name = NameTextBox.Text.Trim();
                product.Brand = BrandTextBox.Text.Trim();
                product.Model = ModelTextBox.Text.Trim();
                product.Category = CategoryComboBox.Text.Trim();
                product.Price = decimal.Parse(PriceTextBox.Text);
                product.CostPrice = decimal.Parse(CostPriceTextBox.Text);
                product.Quantity = int.Parse(QuantityTextBox.Text);
                product.LowStockThreshold = int.Parse(LowStockTextBox.Text);
                product.SerialNumber = SerialNumberTextBox.Text.Trim();
                product.Warranty = WarrantyComboBox.Text.Trim();
                product.Description = DescriptionTextBox.Text.Trim();
                product.IsAvailable = IsAvailableCheckBox.IsChecked ?? true;

                if (!_isEditMode)
                {
                    product.DateAdded = DateTime.Now;
                }

                if (_isEditMode)
                {
                    _productService.UpdateProduct(product);
                    MessageBox.Show("Product updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _productService.AddProduct(product);
                    MessageBox.Show("Product added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
