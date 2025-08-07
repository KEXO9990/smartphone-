using System;
using System.ComponentModel;

namespace MobileShopApp.Models
{
    public class Sale : INotifyPropertyChanged
    {
        private int _id;
        private DateTime _saleDate;
        private decimal _totalAmount;
        private decimal _discountAmount;
        private decimal _finalAmount;
        private string _paymentMethod = string.Empty;
        private string _notes = string.Empty;
        private string _invoiceNumber = string.Empty;
        private string _customerName = string.Empty;
        private string _customerPhone = string.Empty;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string CustomerName
        {
            get => _customerName;
            set
            {
                _customerName = value ?? string.Empty;
                OnPropertyChanged(nameof(CustomerName));
            }
        }

        public string CustomerPhone
        {
            get => _customerPhone;
            set
            {
                _customerPhone = value ?? string.Empty;
                OnPropertyChanged(nameof(CustomerPhone));
            }
        }

        public DateTime SaleDate
        {
            get => _saleDate;
            set
            {
                _saleDate = value;
                OnPropertyChanged(nameof(SaleDate));
            }
        }

        public decimal TotalAmount
        {
            get => _totalAmount;
            set
            {
                _totalAmount = value;
                OnPropertyChanged(nameof(TotalAmount));
            }
        }

        public decimal DiscountAmount
        {
            get => _discountAmount;
            set
            {
                _discountAmount = value;
                OnPropertyChanged(nameof(DiscountAmount));
            }
        }

        public decimal FinalAmount
        {
            get => _finalAmount;
            set
            {
                _finalAmount = value;
                OnPropertyChanged(nameof(FinalAmount));
            }
        }

        public string PaymentMethod
        {
            get => _paymentMethod;
            set
            {
                _paymentMethod = value;
                OnPropertyChanged(nameof(PaymentMethod));
            }
        }

        public string Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }

        public string InvoiceNumber
        {
            get => _invoiceNumber;
            set
            {
                _invoiceNumber = value;
                OnPropertyChanged(nameof(InvoiceNumber));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
