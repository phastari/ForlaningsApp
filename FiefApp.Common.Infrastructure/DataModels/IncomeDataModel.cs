using FiefApp.Common.Infrastructure.Models;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class IncomeDataModel : INotifyPropertyChanged, IDataModelBase, ICloneable
    {
        public IncomeDataModel()
        {
            IncomesCollection.CollectionChanged += IncomeCollectionPropertyChanged;
            StewardsCollection.CollectionChanged += UpdateStewardsCollectionsInIncomes;
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

        private ObservableCollection<IncomeModel> _incomesCollection = new ObservableCollection<IncomeModel>();
        public ObservableCollection<IncomeModel> IncomesCollection
        {
            get => _incomesCollection;
            set
            {
                _incomesCollection = value;
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

        private int _totalIron;
        public int TotalIron
        {
            get => _totalIron;
            set
            {
                _totalIron = value;
                NotifyPropertyChanged();
            }
        }

        private int _totalStone;
        public int TotalStone
        {
            get => _totalStone;
            set
            {
                _totalStone = value;
                NotifyPropertyChanged();
            }
        }

        private int _totalWood;
        public int TotalWood
        {
            get => _totalWood;
            set
            {
                _totalWood = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isAll = false;
        public bool IsAll
        {
            get => _isAll;
            set
            {
                _isAll = value;
                NotifyPropertyChanged();
            }
        }

        #region PropertyChanged Listener For IncomeCollection

        private void IncomeCollectionPropertyChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    item.PropertyChanged -= item_PropertyChanged;
                }
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    item.PropertyChanged += item_PropertyChanged;
                }
            }
        }

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Silver")
            {
                UpdateTotals();
            }
            else if (e.PropertyName == "Base")
            {
                UpdateTotals();
            }
            else if (e.PropertyName == "Luxury")
            {
                UpdateTotals();
            }
            else if (e.PropertyName == "Iron")
            {
                UpdateTotals();
            }
            else if (e.PropertyName == "Wood")
            {
                UpdateTotals();
            }
            else if (e.PropertyName == "Stone")
            {
                UpdateTotals();
            }
        }

        #endregion

        #region PropertyChanged Listener For StewardsCollection

        private void UpdateStewardsCollectionsInIncomes(object sender, NotifyCollectionChangedEventArgs e)
        {
            for (int x = 0; x < IncomesCollection.Count; x++)
            {
                IncomesCollection[x].StewardsCollection = StewardsCollection;
            }
        }

        #endregion
        #region TotalCosts Methods

        public void UpdateTotals()
        {
            int silver = 0;
            int ibase = 0;
            int luxury = 0;
            int wood = 0;
            int iron = 0;
            int stone = 0;

            for (int x = 0; x < IncomesCollection.Count; x++)
            {
                if (IncomesCollection[x].Silver != -1)
                {
                    silver += IncomesCollection[x].Silver;
                }
                if (IncomesCollection[x].Base != -1)
                {
                    ibase += IncomesCollection[x].Base;
                }
                if (IncomesCollection[x].Luxury != -1)
                {
                    luxury += IncomesCollection[x].Luxury;
                }
                if (IncomesCollection[x].Iron != -1)
                {
                    iron += IncomesCollection[x].Iron;
                }
                if (IncomesCollection[x].Stone != -1)
                {
                    stone += IncomesCollection[x].Stone;
                }
                if (IncomesCollection[x].Wood != -1)
                {
                    wood += IncomesCollection[x].Wood;
                }
            }

            TotalSilver = silver;
            TotalBase = ibase;
            TotalLuxury = luxury;
            TotalIron = iron;
            TotalStone = stone;
            TotalWood = wood;
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
