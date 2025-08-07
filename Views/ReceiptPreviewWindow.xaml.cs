using System.Windows;

namespace MobileShopApp.Views
{
    public partial class ReceiptPreviewWindow : Window
    {
        public ReceiptPreviewWindow(string receiptText)
        {
            InitializeComponent();
            ReceiptTextBlock.Text = receiptText;
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            // For now, just show a message
            MessageBox.Show("Print functionality would be implemented here.", "Print", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
