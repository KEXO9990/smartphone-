using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MobileShopApp.Models;
using MobileShopApp.Services;

namespace MobileShopApp.Views
{
    public partial class SaleDetailsWindow : Window
    {
        private Sale _sale;
        private SaleService _saleService;

        public SaleDetailsWindow(Sale sale)
        {
            InitializeComponent();
            _sale = sale;
            _saleService = new SaleService();
            LoadSaleDetails();
        }

        private void LoadSaleDetails()
        {
            // Set sale header info
            SaleIdText.Text = _sale.Id.ToString();
            SaleDateText.Text = _sale.SaleDate.ToString("MMM dd, yyyy hh:mm tt");
            CustomerNameText.Text = string.IsNullOrEmpty(_sale.CustomerName) ? "Walk-in Customer" : _sale.CustomerName;
            TotalAmountText.Text = _sale.TotalAmount.ToString("C");

            // Load sale items
            var saleItems = _saleService.GetSaleItems(_sale.Id);
            ItemsDataGrid.ItemsSource = saleItems;
        }

        private void PrintReceipt_Click(object sender, RoutedEventArgs e)
        {
            // Simple receipt preview for now
            var receiptText = GenerateReceiptText();
            var receiptWindow = new ReceiptPreviewWindow(receiptText);
            receiptWindow.ShowDialog();
        }

        private string GenerateReceiptText()
        {
            var receipt = "MOBILE SHOP RECEIPT\n";
            receipt += "==================\n\n";
            receipt += $"Invoice #: {_sale.InvoiceNumber}\n";
            receipt += $"Sale ID: {_sale.Id}\n";
            receipt += $"Date: {_sale.SaleDate:MMM dd, yyyy hh:mm tt}\n";
            receipt += $"Customer: {(string.IsNullOrEmpty(_sale.CustomerName) ? "Walk-in Customer" : _sale.CustomerName)}\n";
            if (!string.IsNullOrEmpty(_sale.CustomerPhone))
            {
                receipt += $"Phone: {_sale.CustomerPhone}\n";
            }
            receipt += "\nITEMS:\n";
            receipt += "------\n";

            var saleItems = _saleService.GetSaleItems(_sale.Id);
            foreach (var item in saleItems)
            {
                receipt += $"{item.ProductName} x{item.Quantity} @ {item.UnitPrice:C} = {item.Subtotal:C}\n";
            }

            receipt += "\n==================\n";
            if (_sale.DiscountAmount > 0)
            {
                receipt += $"Subtotal: {_sale.TotalAmount:C}\n";
                receipt += $"Discount: -{_sale.DiscountAmount:C}\n";
                receipt += $"TOTAL: {_sale.FinalAmount:C}\n";
            }
            else
            {
                receipt += $"TOTAL: {_sale.TotalAmount:C}\n";
            }
            receipt += $"Payment: {_sale.PaymentMethod}\n";
            receipt += "==================\n\n";
            receipt += "Thank you for your business!";

            return receipt;
        }
    }
}
