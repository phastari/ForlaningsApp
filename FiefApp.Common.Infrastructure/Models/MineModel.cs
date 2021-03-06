﻿using System;
using System.Collections.ObjectModel;
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

        private int _population;
        public int Population
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

        private int _baseIncome;
        public int BaseIncome
        {
            get => _baseIncome;
            set
            {
                _baseIncome = value;
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

        private int _availableGuards;
        public int AvailableGuards
        {
            get => _availableGuards;
            set
            {
                _availableGuards = value;
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

        public string Result { get; set; }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
