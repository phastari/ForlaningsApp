using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class PortDataModel : INotifyPropertyChanged, IDataModelBase, ICloneable
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

        private ObservableCollection<BoatModel> _boatsCollection = new ObservableCollection<BoatModel>();
        public ObservableCollection<BoatModel> BoatsCollection
        {
            get => _boatsCollection;
            set
            {
                _boatsCollection = value;
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
