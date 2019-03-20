using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;

namespace FiefApp.Common.Infrastructure.Models
{
    public class ShipyardModel : INotifyPropertyChanged
    {
        private string _shipyard = "";
        public string Shipyard
        {
            get => _shipyard;
            set
            {
                _shipyard = value;
                NotifyPropertyChanged();
            }
        }

        private string _size;
        public string Size
        {
            get => _size;
            set
            {
                _size = value;
                NotifyPropertyChanged();
            }
        }

        private string _un;
        public string UN
        {
            get => _un;
            set
            {
                _un = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _operationBaseCost;
        public decimal OperationBaseCost
        {
            get => _operationBaseCost;
            set
            {
                _operationBaseCost = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _operationBaseIncome;
        public decimal OperationBaseIncome
        {
            get => _operationBaseIncome;
            set
            {
                _operationBaseIncome = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isBeingUpgraded;
        public bool IsBeingUpgraded
        {
            get => _isBeingUpgraded;
            set
            {
                _isBeingUpgraded = value;
                NotifyPropertyChanged();
            }
        }

        private int _dockSmall;
        public int DockSmall
        {
            get => _dockSmall;
            set
            {
                _dockSmall = value;
                NotifyPropertyChanged();
            }
        }

        private int _dockSmallFree;
        public int DockSmallFree
        {
            get => _dockSmallFree;
            set
            {
                _dockSmallFree = value;
                NotifyPropertyChanged();
            }
        }

        private int _dockMedium;
        public int DockMedium
        {
            get => _dockMedium;
            set
            {
                _dockMedium = value;
                NotifyPropertyChanged();
            }
        }

        private int _dockMediumFree;
        public int DockMediumFree
        {
            get => _dockMediumFree;
            set
            {
                _dockMediumFree = value;
                NotifyPropertyChanged();
            }
        }

        private int _dockLarge;
        public int DockLarge
        {
            get => _dockLarge;
            set
            {
                _dockLarge = value;
                NotifyPropertyChanged();
            }
        }

        private int _dockLargeFree;
        public int DockLargeFree
        {
            get => _dockLargeFree;
            set
            {
                _dockLargeFree = value;
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

        private string _taxes;
        public string Taxes
        {
            get => _taxes;
            set
            {
                _taxes = value;
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

        private int _daysWorkThisYear;
        public int DaysWorkThisYear
        {
            get => _daysWorkThisYear;
            set
            {
                _daysWorkThisYear = value > DaysWorkNeeded ? DaysWorkNeeded : value;
                NotifyPropertyChanged();
            }
        }

        private int _bailiffs;
        public int Bailiffs
        {
            get => _bailiffs;
            set
            {
                _bailiffs = value;
                NotifyPropertyChanged();
            }
        }

        private int _captains;
        public int Captains
        {
            get => _captains;
            set
            {
                _captains = value;
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

        private decimal _crimeRate;
        public decimal CrimeRate
        {
            get => _crimeRate;
            set
            {
                _crimeRate = value;
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
