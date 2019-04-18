using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class MinesDataModel : INotifyPropertyChanged, IDataModelBase, ICloneable
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

        private int _totalSilver = 0;
        public int TotalSilver
        {
            get => _totalSilver;
            set
            {
                _totalSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _totalBase = 0;
        public int TotalBase
        {
            get => _totalBase;
            set
            {
                _totalBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _totalStone = 0;
        public int TotalStone
        {
            get => _totalStone;
            set
            {
                _totalStone = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<MineModel> _minesCollection = new ObservableCollection<MineModel>();
        public ObservableCollection<MineModel> MinesCollection
        {
            get => _minesCollection;
            set
            {
                _minesCollection = value;
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private ObservableCollection<QuarryModel> _quarriesCollection = new ObservableCollection<QuarryModel>();
        public ObservableCollection<QuarryModel> QuarriesCollection
        {
            get => _quarriesCollection;
            set
            {
                _quarriesCollection = value;
                NotifyPropertyChanged();
                UpdateTotals();
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

        #region Methods

        public void UpdateTotals()
        {
            int silver = 0;
            int stone = 0;
            int bas = 0;

            for (int x = 0; x < MinesCollection.Count; x++)
            {
                silver += MinesCollection[x].Income;
            }

            for (int x = 0; x < QuarriesCollection.Count; x++)
            {
                stone += QuarriesCollection[x].Approximate;
                bas -= QuarriesCollection[x].Upkeep;
            }

            TotalSilver = silver;
            TotalBase = bas;
            TotalStone = stone;
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
