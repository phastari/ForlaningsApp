using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class SupplyDataModel : INotifyPropertyChanged, IDataModelBase, ICloneable
    {
        private int _supplySilver;
        public int SupplySilver
        {
            get => _supplySilver;
            set
            {
                _supplySilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _supplyBase;
        public int SupplyBase
        {
            get => _supplyBase;
            set
            {
                _supplyBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _supplyLuxury;
        public int SupplyLuxury
        {
            get => _supplyLuxury;
            set
            {
                _supplyLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _supplyIron;
        public int SupplyIron
        {
            get => _supplyIron;
            set
            {
                _supplyIron = value;
                NotifyPropertyChanged();
            }
        }

        private int _supplyStone;
        public int SupplyStone
        {
            get => _supplyStone;
            set
            {
                _supplyStone = value;
                NotifyPropertyChanged();
            }
        }

        private int _supplyWood;
        public int SupplyWood
        {
            get => _supplyWood;
            set
            {
                _supplyWood = value;
                NotifyPropertyChanged();
            }
        }

        private int _amountSilver;
        public int AmountSilver
        {
            get => _amountSilver;
            set
            {
                _amountSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _amountBase;
        public int AmountBase
        {
            get => _amountBase;
            set
            {
                _amountBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _amountLuxury;
        public int AmountLuxury
        {
            get => _amountLuxury;
            set
            {
                _amountLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _amountIron;
        public int AmountIron
        {
            get => _amountIron;
            set
            {
                _amountIron = value;
                NotifyPropertyChanged();
            }
        }

        private int _amountStone;
        public int AmountStone
        {
            get => _amountStone;
            set
            {
                _amountStone = value;
                NotifyPropertyChanged();
            }
        }

        private int _amountWood;
        public int AmountWood
        {
            get => _amountWood;
            set
            {
                _amountWood = value;
                NotifyPropertyChanged();
            }
        }

        private string _transactionSilver = "";
        public string TransactionSilver
        {
            get => _transactionSilver;
            set
            {
                _transactionSilver = value;
                NotifyPropertyChanged();
            }
        }

        private string _transactionBase = "";
        public string TransactionBase
        {
            get => _transactionBase;
            set
            {
                _transactionBase = value;
                NotifyPropertyChanged();
            }
        }

        private string _transactionLuxury = "";
        public string TransactionLuxury
        {
            get => _transactionLuxury;
            set
            {
                _transactionLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private string _transactionIron = "";
        public string TransactionIron
        {
            get => _transactionIron;
            set
            {
                _transactionIron = value;
                NotifyPropertyChanged();
            }
        }

        private string _transactionStone = "";
        public string TransactionStone
        {
            get => _transactionStone;
            set
            {
                _transactionStone = value;
                NotifyPropertyChanged();
            }
        }

        private string _transactionWood = "";
        public string TransactionWood
        {
            get => _transactionWood;
            set
            {
                _transactionWood = value;
                NotifyPropertyChanged();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
