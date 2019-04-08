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
