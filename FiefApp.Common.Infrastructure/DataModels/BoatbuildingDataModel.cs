using FiefApp.Common.Infrastructure.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class BoatbuildingDataModel : INotifyPropertyChanged, IDataModelBase, ICloneable
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

        private int _villageBoatBuilders;
        public int VillageBoatBuilders
        {
            get => _villageBoatBuilders;
            set
            {
                _villageBoatBuilders = value;
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

        private int _docksSmall;
        public int DocksSmall
        {
            get => _docksSmall;
            set
            {
                _docksSmall = value;
                NotifyPropertyChanged();
            }
        }

        private int _docksSmallFree;
        public int DocksSmallFree
        {
            get => _docksSmallFree;
            set
            {
                _docksSmallFree = value;
                NotifyPropertyChanged();
            }
        }

        private int _docksMedium;
        public int DocksMedium
        {
            get => _docksMedium;
            set
            {
                _docksMedium = value;
                NotifyPropertyChanged();
            }
        }

        private int _docksMediumFree;
        public int DocksMediumFree
        {
            get => _docksMediumFree;
            set
            {
                _docksMediumFree = value;
                NotifyPropertyChanged();
            }
        }

        private int _docksLarge;
        public int DocksLarge
        {
            get => _docksLarge;
            set
            {
                _docksLarge = value;
                NotifyPropertyChanged();
            }
        }

        private int _docksLargeFree;
        public int DocksLargeFree
        {
            get => _docksLargeFree;
            set
            {
                _docksLargeFree = value;
                NotifyPropertyChanged();
            }
        }

        private int _docksVillage;
        public int DocksVillage
        {
            get => _docksVillage;
            set
            {
                _docksVillage = value;
                NotifyPropertyChanged();
            }
        }

        private int _docksVillageFree;
        public int DocksVillageFree
        {
            get => _docksVillageFree;
            set
            {
                _docksVillageFree = value;
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
