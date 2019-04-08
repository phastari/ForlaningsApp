using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class SubsidiaryDataModel : INotifyPropertyChanged, ICloneable, IDataModelBase
    {
        public SubsidiaryDataModel()
        {
            ConstructingCollection.CollectionChanged += ConstructingCollectionPropertyChanged;
            SubsidiaryCollection.CollectionChanged += SubsidiaryCollectionPropertyChanged;
            StewardsCollection.CollectionChanged += UpdateStewardsCollectionsInIncomes;
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

        private ObservableCollection<SubsidiaryModel> _subsidiaryCollection = new ObservableCollection<SubsidiaryModel>();
        public ObservableCollection<SubsidiaryModel> SubsidiaryCollection
        {
            get => _subsidiaryCollection;
            set
            {
                _subsidiaryCollection = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<SubsidiaryModel> _subsidiaryTypesCollection = new ObservableCollection<SubsidiaryModel>();
        public ObservableCollection<SubsidiaryModel> SubsidiaryTypesCollection
        {
            get => _subsidiaryTypesCollection;
            set
            {
                _subsidiaryTypesCollection = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<SubsidiaryModel> _constructingCollection = new ObservableCollection<SubsidiaryModel>();
        public ObservableCollection<SubsidiaryModel> ConstructingCollection
        {
            get => _constructingCollection;
            set
            {
                _constructingCollection = value;
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

        #region PropertyChanged Listener For ConstructingCollection

        private void ConstructingCollectionPropertyChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    item.PropertyChanged -= ConstructingCollection_PropertyChanged;
                }
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    item.PropertyChanged += ConstructingCollection_PropertyChanged;
                }
            }
        }

        private void ConstructingCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        #endregion

        #region PropertyChanged Listener For SubsidiaryCollection

        private void SubsidiaryCollectionPropertyChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    item.PropertyChanged -= SubsidiaryCollection_PropertyChanged;
                }
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    item.PropertyChanged += SubsidiaryCollection_PropertyChanged;
                }
            }
        }

        private void SubsidiaryCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        #endregion

        #region PropertyChanged Listener For StewardsCollection

        private void UpdateStewardsCollectionsInIncomes(object sender, NotifyCollectionChangedEventArgs e)
        {
            for (int x = 0; x < ConstructingCollection.Count; x++)
            {
                ConstructingCollection[x].StewardsCollection = StewardsCollection;
            }

            for (int x = 0; x < SubsidiaryCollection.Count; x++)
            {
                SubsidiaryCollection[x].StewardsCollection = StewardsCollection;
            }
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
