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

        private int _fishingBoats;
        public int FishingBoats
        {
            get => _fishingBoats;
            set
            {
                _fishingBoats = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<CaptainModel> _captainsCollection = new ObservableCollection<CaptainModel>();
        public ObservableCollection<CaptainModel> CaptainsCollection
        {
            get => _captainsCollection;
            set
            {
                _captainsCollection = value;
                NotifyPropertyChanged();
            }
        }

        private int _sailors;
        public int Sailors
        {
            get => _sailors;
            set
            {
                _sailors = value;
                NotifyPropertyChanged();
            }
        }

        private int _sailorsSilver;
        public int SailorsSilver
        {
            get => _sailorsSilver;
            set
            {
                _sailorsSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _sailorsBase;
        public int SailorsBase
        {
            get => _sailorsBase;
            set
            {
                _sailorsBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _seaman;
        public int Seaman
        {
            get => _seaman;
            set
            {
                _seaman = value;
                NotifyPropertyChanged();
            }
        }

        private int _seamanSilver;
        public int SeamanSilver
        {
            get => _seamanSilver;
            set
            {
                _seamanSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _seamanBase;
        public int SeamanBase
        {
            get => _seamanBase;
            set
            {
                _seamanBase = value;
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

        private int _guardsSilver;
        public int GuardsSilver
        {
            get => _guardsSilver;
            set
            {
                _guardsSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _guardsBase;
        public int GuardsBase
        {
            get => _guardsBase;
            set
            {
                _guardsBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _mariner;
        public int Mariner
        {
            get => _mariner;
            set
            {
                _mariner = value;
                NotifyPropertyChanged();
            }
        }

        private int _marinerSilver;
        public int MarinerSilver
        {
            get => _marinerSilver;
            set
            {
                _marinerSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _marinerBase;
        public int MarinerBase
        {
            get => _marinerBase;
            set
            {
                _marinerBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _rowers;
        public int Rowers
        {
            get => _rowers;
            set
            {
                _rowers = value;
                NotifyPropertyChanged();
            }
        }

        private int _totalSilver;
        public int TotalSilver
        {
            get => _totalSilver;
            set
            {
                _totalSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _totalBase;
        public int TotalBase
        {
            get => _totalBase;
            set
            {
                _totalBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _totalLuxury;
        public int TotalLuxury
        {
            get => _totalLuxury;
            set
            {
                _totalLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private BoatModel _crewBoat = new BoatModel();
        public BoatModel CrewBoat
        {
            get => _crewBoat;
            set
            {
                _crewBoat = value;
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
