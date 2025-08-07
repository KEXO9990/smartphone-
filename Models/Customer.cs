using System;
using System.ComponentModel;

namespace MobileShopApp.Models
{
    public class Customer : INotifyPropertyChanged
    {
        private int _id;
        private string _name = string.Empty;
        private string _email = string.Empty;
        private string _phone = string.Empty;
        private string _address = string.Empty;
        private DateTime _dateRegistered;
        private decimal _totalPurchases;
        private int _totalOrders;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public DateTime DateRegistered
        {
            get => _dateRegistered;
            set
            {
                _dateRegistered = value;
                OnPropertyChanged(nameof(DateRegistered));
            }
        }

        public decimal TotalPurchases
        {
            get => _totalPurchases;
            set
            {
                _totalPurchases = value;
                OnPropertyChanged(nameof(TotalPurchases));
            }
        }

        public int TotalOrders
        {
            get => _totalOrders;
            set
            {
                _totalOrders = value;
                OnPropertyChanged(nameof(TotalOrders));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
