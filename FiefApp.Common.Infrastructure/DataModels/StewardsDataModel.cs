using CommonServiceLocator;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class StewardsDataModel : INotifyPropertyChanged, ICloneable, IDataModelBase
    {
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

        private ObservableCollection<StewardIndustryModel> _industriesCollection = new ObservableCollection<StewardIndustryModel>();
        public ObservableCollection<StewardIndustryModel> IndustriesCollection
        {
            get => _industriesCollection;
            set
            {
                _industriesCollection = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<IndustryBeingDevelopedModel> _industriesBeingDevelopedCollection = new ObservableCollection<IndustryBeingDevelopedModel>();
        public ObservableCollection<IndustryBeingDevelopedModel> IndustriesBeingDevelopedCollection
        {
            get => _industriesBeingDevelopedCollection;
            set
            {
                _industriesBeingDevelopedCollection = value;
                NotifyPropertyChanged();
            }
        }

        private int _numberOfStewards;
        public int NumberOfStewards
        {
            get => _numberOfStewards;
            set
            {
                _numberOfStewards = value;
                NotifyPropertyChanged();
            }
        }

        private int _numberOfIndustires;
        public int NumberOfIndustires
        {
            get => _numberOfIndustires;
            set
            {
                _numberOfIndustires = value;
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
