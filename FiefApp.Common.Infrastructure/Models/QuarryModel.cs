using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class QuarryModel : INotifyPropertyChanged
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

        private string _quarryType;
        public string QuarryType
        {
            get => _quarryType;
            set
            {
                _quarryType = value;
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

        private int _modifier;
        public int Modifier
        {
            get => _modifier;
            set
            {
                _modifier = value;
                NotifyPropertyChanged();
            }
        }

        private int _approximate;
        public int Approximate
        {
            get => _approximate;
            set
            {
                _approximate = value;
                NotifyPropertyChanged();
            }
        }

        private int _upkeep;
        public int Upkeep
        {
            get => _upkeep;
            set
            {
                _upkeep = value;
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

        private int _daysWorkThisYear;
        public int DaysWorkThisYear
        {
            get => _daysWorkThisYear;
            set
            {
                if (value > -1)
                {
                    if (value <= DaysWorkNeeded)
                    {
                        _daysWorkThisYear = value;
                    }
                    else
                    {
                        _daysWorkThisYear = DaysWorkNeeded;
                    }
                }
                else
                {
                    _daysWorkThisYear = 0;
                }
                
                NotifyPropertyChanged();
            }
        }

        private int _daysWorkNeeded;
        public int DaysWorkNeeded
        {
            get => _daysWorkNeeded;
            set
            {
                _daysWorkNeeded = value;
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
