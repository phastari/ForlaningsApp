using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class TradeDataModel : INotifyPropertyChanged, IDataModelBase, ICloneable
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

        private int _marketAvailableBase;
        public int MarketAvailableBase
        {
            get => _marketAvailableBase;
            set
            {
                _marketAvailableBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _marketAvailableLuxury;
        public int MarketAvailableLuxury
        {
            get => _marketAvailableLuxury;
            set
            {
                _marketAvailableLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _marketAvailableIron;
        public int MarketAvailableIron
        {
            get => _marketAvailableIron;
            set
            {
                _marketAvailableIron = value;
                NotifyPropertyChanged();
            }
        }

        private int _marketAvailableStone;
        public int MarketAvailableStone
        {
            get => _marketAvailableStone;
            set
            {
                _marketAvailableStone = value;
                NotifyPropertyChanged();
            }
        }

        private int _marketAvailableWood;
        public int MarketAvailableWood
        {
            get => _marketAvailableWood;
            set
            {
                _marketAvailableWood = value;
                NotifyPropertyChanged();
            }
        }

        private int _sendMerchantId = -1;
        public int SendMerchantId
        {
            get => _sendMerchantId;
            set
            {
                _sendMerchantId = value;
                NotifyPropertyChanged();
            }
        }

        private Visibility _sendMerchantVisibility = Visibility.Collapsed;
        public Visibility SendMerchantVisibility
        {
            get => _sendMerchantVisibility;
            set
            {
                _sendMerchantVisibility = value;
                NotifyPropertyChanged();
            }
        }

        private bool _rootGridIsEnabled = true;
        public bool RootGridIsEnabled
        {
            get => _rootGridIsEnabled;
            set
            {
                _rootGridIsEnabled = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<MerchantModel> _merchantsCollection = new ObservableCollection<MerchantModel>();
        public ObservableCollection<MerchantModel> MerchantsCollection
        {
            get => _merchantsCollection;
            set
            {
                _merchantsCollection = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<BoatModel> _shipsCollection = new ObservableCollection<BoatModel>();
        public ObservableCollection<BoatModel> ShipsCollection
        {
            get => _shipsCollection;
            set => _shipsCollection = value;
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
