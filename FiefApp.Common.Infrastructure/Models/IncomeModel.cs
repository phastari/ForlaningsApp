using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class IncomeModel : INotifyPropertyChanged
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

        private int _manorId;
        public int ManorId
        {
            get => _manorId;
            set
            {
                _manorId = value; 
                NotifyPropertyChanged();
            }
        }

        private string _manor;
        public string Manor
        {
            get => _manor;
            set
            {
                _manor = value; 
                NotifyPropertyChanged();
            }
        }

        private string _income;
        public string Income
        {
            get => _income;
            set
            {
                _income = value; 
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

        private bool _isStewardNeeded;
        public bool IsStewardNeeded
        {
            get => _isStewardNeeded;
            set
            {
                _isStewardNeeded = value; 
                NotifyPropertyChanged();
            }
        }

        private bool _showInIncomes = true;
        public bool ShowInIncomes
        {
            get => _showInIncomes;
            set
            {
                _showInIncomes = value;
                NotifyPropertyChanged();
            }
        }

        private bool _canBeDeveloped = true;
        public bool CanBeDeveloped
        {
            get => _canBeDeveloped;
            set
            {
                _canBeDeveloped = value;
                NotifyPropertyChanged();
            }
        }

        private bool _beingDeveloped = false;
        public bool BeingDeveloped
        {
            get => _beingDeveloped;
            set
            {
                _beingDeveloped = value;
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

        private int _wood;
        public int Wood
        {
            get => _wood;
            set
            {
                _wood = value;
                NotifyPropertyChanged();
            }
        }

        private int _stone;
        public int Stone
        {
            get => _stone;
            set
            {
                _stone = value; 
                NotifyPropertyChanged();
            }
        }

        private int _iron;
        public int Iron
        {
            get => _iron;
            set
            {
                _iron = value; 
                NotifyPropertyChanged();
            }
        }

        private string _silverFormula;
        public string SilverFormula
        {
            get => _silverFormula;
            set
            {
                _silverFormula = value; 
                NotifyPropertyChanged();
            }
        }

        private string _baseFormula;
        public string BaseFormula
        {
            get => _baseFormula;
            set
            {
                _baseFormula = value; 
                NotifyPropertyChanged();
            }
        }

        private string _luxuryFormula;
        public string LuxuryFormula
        {
            get => _luxuryFormula;
            set
            {
                _luxuryFormula = value; 
                NotifyPropertyChanged();
            }
        }

        private string _woodFormula;
        public string WoodFormula
        {
            get => _woodFormula;
            set
            {
                _woodFormula = value; 
                NotifyPropertyChanged();
            }
        }

        private string _stoneFormula;
        public string StoneFormula
        {
            get => _stoneFormula;
            set
            {
                _stoneFormula = value; 
                NotifyPropertyChanged();
            }
        }

        private string _ironFormula;
        public string IronFormula
        {
            get => _ironFormula;
            set
            {
                _ironFormula = value; 
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

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
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
