using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class SubsidiaryDataModel : INotifyPropertyChanged, ICloneable, IDataModelBase
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
