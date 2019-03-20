using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class BoatbuildingDataModel : INotifyPropertyChanged, IDataModelBase, ICloneable
    {
        private readonly ISettingsService _settingsService;

        public BoatbuildingDataModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

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

        private bool _canBuildShipyard;
        public bool CanBuildShipyard
        {
            get => _canBuildShipyard;
            set
            {
                _canBuildShipyard = value;
                NotifyPropertyChanged();
            }
        }

        private bool _gotShipyard;
        public bool GotShipyard
        {
            get => _gotShipyard;
            set
            {
                _gotShipyard = value;
                NotifyPropertyChanged();
            }
        }

        private bool _buildingShipyard;
        public bool BuildingShipyard
        {
            get => _buildingShipyard;
            set
            {
                _buildingShipyard = value;
                NotifyPropertyChanged();
            }
        }

        private bool _upgradingShipyard;
        public bool UpgradingShipyard
        {
            get => _upgradingShipyard;
            set
            {
                _upgradingShipyard = value;
                NotifyPropertyChanged();
            }
        }

        private ShipyardModel _shipyard = new ShipyardModel();
        public ShipyardModel Shipyard
        {
            get => _shipyard;
            set
            {
                _shipyard = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<BoatModel> _boatsBuildingCollection = new ObservableCollection<BoatModel>();
        public ObservableCollection<BoatModel> BoatsBuildingCollection
        {
            get => _boatsBuildingCollection;
            set
            {
                _boatsBuildingCollection = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<BoatModel> _boatTypeCollection = new ObservableCollection<BoatModel>();
        public ObservableCollection<BoatModel> BoatTypeCollection
        {
            get => _boatTypeCollection;
            set
            {
                _boatTypeCollection = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<BoatbuilderModel> _boatBuildersCollection = new ObservableCollection<BoatbuilderModel>();
        public ObservableCollection<BoatbuilderModel> BoatBuildersCollection
        {
            get => _boatBuildersCollection;
            set
            {
                _boatBuildersCollection = value;
                NotifyPropertyChanged();
            }
        }

        private int _availableBoatBuilders;
        public int AvailableBoatBuilders
        {
            get => _availableBoatBuilders;
            set
            {
                _availableBoatBuilders = value;
                NotifyPropertyChanged();
            }
        }

        private int _totalBoatBuilders;
        public int TotalBoatBuilders
        {
            get => _totalBoatBuilders;
            set
            {
                _totalBoatBuilders = value;
                NotifyPropertyChanged();
            }
        }

        private int _boatsBuilding;
        public int BoatsBuilding
        {
            get => _boatsBuilding;
            set
            {
                _boatsBuilding = value;
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
