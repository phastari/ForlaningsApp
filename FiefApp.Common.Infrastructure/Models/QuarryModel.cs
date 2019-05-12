using System;
using System.Collections.ObjectModel;
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

        private int _approximateIncome;
        public int ApproximateIncome
        {
            get => _approximateIncome;
            set
            {
                _approximateIncome = value;
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

        private ObservableCollection<StewardModel> _stewardsCollection = new ObservableCollection<StewardModel>();
        public ObservableCollection<StewardModel> StewardsCollection
        {
            get => _stewardsCollection;
            set
            {
                _stewardsCollection = value;
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

        private string _skill = "0";
        public string Skill
        {
            get => _skill;
            set
            {
                _skill = value;
                NotifyPropertyChanged();
            }
        }

        private string _combinationRoll = "0";
        public string CombinationRoll
        {
            get => _combinationRoll;
            set
            {
                _combinationRoll = value;
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

        private bool _isFirstYear = true;
        public bool IsFirstYear
        {
            get => _isFirstYear;
            set
            {
                _isFirstYear = value;
                NotifyPropertyChanged();
            }
        }

        private int _endOfYearStone;
        public int EndOfYearStone
        {
            get => _endOfYearStone;
            set
            {
                _endOfYearStone = value;
                NotifyPropertyChanged();
            }
        }

        private int _difficulty;
        public int Difficulty
        {
            get => _difficulty;
            set
            {
                _difficulty = value;
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
