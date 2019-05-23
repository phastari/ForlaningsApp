using FiefApp.Common.Infrastructure.Models;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class StewardsDataModel : INotifyPropertyChanged, ICloneable, IDataModelBase
    {
        public StewardsDataModel()
        {
            //IndustriesCollection.CollectionChanged += IndustriesCollectionChanged;
            StewardsCollection.CollectionChanged += StewardsCollectionChanged;
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

        #region PropertyChanged Listener For StewardsCollection

        private void StewardsCollectionChanged(
            object sender,
            NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    item.PropertyChanged -= StewardsCollection_PropertyChanged;
                }
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    item.PropertyChanged += StewardsCollection_PropertyChanged;
                }
            }
        }

        private void StewardsCollection_PropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {

        }

        #endregion

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
