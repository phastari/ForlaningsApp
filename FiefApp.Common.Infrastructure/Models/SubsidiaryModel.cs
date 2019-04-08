using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class SubsidiaryModel : INotifyPropertyChanged
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

        private string _subsidiary;
        public string Subsidiary
        {
            get => _subsidiary;
            set
            {
                _subsidiary = value;
                NotifyPropertyChanged();
            }
        }

        private int _quality;
        public int Quality
        {
            get => _quality;
            set
            {
                _quality = value; 
                NotifyPropertyChanged();
            }
        }

        private int _developmentLevel;
        public int DevelopmentLevel
        {
            get => _developmentLevel;
            set
            {
                _developmentLevel = value; 
                NotifyPropertyChanged();
            }
        }

        private int _silver;
        public int Silver
        {
            get => _silver;
            set
            {
                _silver = value; 
                NotifyPropertyChanged();
            }
        }

        private int _base;
        public int Base
        {
            get => _base;
            set
            {
                _base = value; 
                NotifyPropertyChanged();
            }
        }

        private int _luxury;
        public int Luxury
        {
            get => _luxury;
            set
            {
                _luxury = value; 
                NotifyPropertyChanged();
            }
        }

        private decimal _incomeFactor;
        public decimal IncomeFactor
        {
            get => _incomeFactor;
            set
            {
                _incomeFactor = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _incomeSilver;
        public decimal IncomeSilver
        {
            get => _incomeSilver;
            set
            {
                _incomeSilver = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _incomeBase;
        public decimal IncomeBase
        {
            get => _incomeBase;
            set
            {
                _incomeBase = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _incomeLuxury;
        public decimal IncomeLuxury
        {
            get => _incomeLuxury;
            set
            {
                _incomeLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _daysWorkUpkeep;
        public int DaysWorkUpkeep
        {
            get => _daysWorkUpkeep;
            set
            {
                _daysWorkUpkeep = value; 
                NotifyPropertyChanged();
            }
        }

        private int _daysWorkBuild;
        public int DaysWorkBuild
        {
            get => _daysWorkBuild;
            set
            {
                _daysWorkBuild = value;
                NotifyPropertyChanged();
            }
        }

        private int _daysWorkThisYear;
        public int DaysWorkThisYear
        {
            get => _daysWorkThisYear;
            set
            {
                _daysWorkThisYear = value;
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

        private int _stewardId;
        public int StewardId
        {
            get => _stewardId;
            set
            {
                _stewardId = value; 
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

        private string _skill;
        public string Skill
        {
            get => _skill;
            set
            {
                _skill = value; 
                NotifyPropertyChanged();
            }
        }

        private decimal _spring;
        public decimal Spring
        {
            get => _spring;
            set
            {
                _spring = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _summer;
        public decimal Summer
        {
            get => _summer;
            set
            {
                _summer = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _fall;
        public decimal Fall
        {
            get => _fall;
            set
            {
                _fall = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _winter;
        public decimal Winter
        {
            get => _winter;
            set
            {
                _winter = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<StewardModel> _stewardsCollection;
        public ObservableCollection<StewardModel> StewardsCollection
        {
            get => _stewardsCollection;
            set
            {
                _stewardsCollection = value;
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
