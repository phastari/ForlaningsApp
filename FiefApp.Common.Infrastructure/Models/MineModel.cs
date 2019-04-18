using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class MineModel : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }

        private string _mineType;
        public string MineType
        {
            get => _mineType;
            set
            {
                _mineType = value;
                NotifyPropertyChanged();
            }
        }

        private int _crime;
        public int Crime
        {
            get => _crime;
            set
            {
                _crime = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _population;
        public decimal Population
        {
            get => _population;
            set
            {
                _population = value;
                NotifyPropertyChanged();
            }
        }

        private int _income;
        public int Income
        {
            get => _income;
            set
            {
                _income = value;
                NotifyPropertyChanged();
            }
        }

        private int _wealth;
        public int Wealth
        {
            get => _wealth;
            set
            {
                _wealth = value;
                NotifyPropertyChanged();
            }
        }

        private int _yearsLeft;
        public int YearsLeft
        {
            get => _yearsLeft;
            set
            {
                _yearsLeft = value;
                NotifyPropertyChanged();
            }
        }

        private string _steward;
        public string Steward
        {
            get => _steward;
            set
            {
                _steward = value;
                NotifyPropertyChanged();
            }
        }

        private int _stewardId = -1;
        public int StewardId
        {
            get => _stewardId;
            set
            {
                _stewardId = value;
                NotifyPropertyChanged();
            }
        }

        private int _guards;
        public int Guards
        {
            get => _guards;
            set
            {
                _guards = value;
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
    }
}
