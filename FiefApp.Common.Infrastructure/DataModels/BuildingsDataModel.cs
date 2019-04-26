using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class BuildingsDataModel : INotifyPropertyChanged, IDataModelBase, ICloneable
    {
        private int _id = -1;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }

        private int _totalBuildings;
        public int TotalBuildings
        {
            get => _totalBuildings;
            set
            {
                _totalBuildings = value;
                NotifyPropertyChanged();
            }
        }

        private int _totalUpkeep;
        public int TotalUpkeep
        {
            get => _totalUpkeep;
            set
            {
                _totalUpkeep = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<BuildingsSettingsModel> _buildingsSettings;
        public ObservableCollection<BuildingsSettingsModel> BuildingsSettings
        {
            get => _buildingsSettings;
            set
            {
                _buildingsSettings = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<BuildingModel> _buildingsCollection = new ObservableCollection<BuildingModel>();
        public ObservableCollection<BuildingModel> BuildingsCollection
        {
            get => _buildingsCollection;
            set
            {
                _buildingsCollection = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<BuilderModel> _buildersCollection = new ObservableCollection<BuilderModel>();
        public ObservableCollection<BuilderModel> BuildersCollection
        {
            get => _buildersCollection;
            set
            {
                _buildersCollection = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<BuildingModel> _buildsCollection = new ObservableCollection<BuildingModel>();
        public ObservableCollection<BuildingModel> BuildsCollection
        {
            get => _buildsCollection;
            set
            {
                _buildsCollection = value;
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
