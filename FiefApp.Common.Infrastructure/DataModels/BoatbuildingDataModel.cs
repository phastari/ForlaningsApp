using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;

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
                Console.WriteLine($"CanBuildShipyard : {CanBuildShipyard}");
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
                Console.WriteLine($"GotShipyard : {GotShipyard}");
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
                Console.WriteLine($"BuildingShipyard : {BuildingShipyard}");
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
                Console.WriteLine($"UpgradingShipyard : {UpgradingShipyard}");
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
