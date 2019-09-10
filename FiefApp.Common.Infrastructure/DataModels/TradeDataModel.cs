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

        private bool _marketSetThisYear;
        public bool MarketSetThisYear
        {
            get => _marketSetThisYear;
            set
            {
                _marketSetThisYear = value;
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

        private int _boughtBase;
        public int BoughtBase
        {
            get => _boughtBase;
            set
            {
                if (MarketAvailableBase != 0)
                {
                    if (value > MarketAvailableBase)
                    {
                        _boughtBase = MarketAvailableBase;
                    }
                    else if (value < 0)
                    {
                        _boughtBase = 0;
                    }
                    else
                    {
                        _boughtBase = value;
                    }
                }
                else
                {
                    _boughtBase = value;
                }
                
                NotifyPropertyChanged();
            }
        }

        private int _boughtLuxury;
        public int BoughtLuxury
        {
            get => _boughtLuxury;
            set
            {
                if (MarketAvailableLuxury != 0)
                {
                    if (value > MarketAvailableLuxury)
                    {
                        _boughtLuxury = MarketAvailableLuxury;
                    }
                    else if (value < 0)
                    {
                        _boughtLuxury = 0;
                    }
                    else
                    {
                        _boughtLuxury = value;
                    }
                }
                else
                {
                    _boughtLuxury = value;
                }

                NotifyPropertyChanged();
            }
        }

        private int _boughtIron;
        public int BoughtIron
        {
            get => _boughtIron;
            set
            {
                if (MarketAvailableIron != 0)
                {
                    if (value > MarketAvailableIron)
                    {
                        _boughtIron = MarketAvailableIron;
                    }
                    else if (value < 0)
                    {
                        _boughtIron = 0;
                    }
                    else
                    {
                        _boughtIron = value;
                    }
                }
                else
                {
                    _boughtIron = value;
                }

                NotifyPropertyChanged();
            }
        }

        private int _boughtStone;
        public int BoughtStone
        {
            get => _boughtStone;
            set
            {
                if (MarketAvailableStone != 0)
                {
                    if (value > MarketAvailableStone)
                    {
                        _boughtStone = MarketAvailableStone;
                    }
                    else if (value < 0)
                    {
                        _boughtStone = 0;
                    }
                    else
                    {
                        _boughtStone = value;
                    }
                }
                else
                {
                    _boughtStone = value;
                }

                NotifyPropertyChanged();
            }
        }

        private int _boughtWood;
        public int BoughtWood
        {
            get => _boughtWood;
            set
            {
                if (MarketAvailableWood != 0)
                {
                    if (value > MarketAvailableWood)
                    {
                        _boughtWood = MarketAvailableWood;
                    }
                    else if (value < 0)
                    {
                        _boughtWood = 0;
                    }
                    else
                    {
                        _boughtWood = value;
                    }
                }
                else
                {
                    _boughtWood = value;
                }

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

        public ObservableCollection<MerchantModel> MerchantsCollection { get; set; } = new ObservableCollection<MerchantModel>()
        {
            new MerchantModel()
            {
                Id = 0
            }
        };

        public ObservableCollection<BoatModel> ShipsCollection { get; set; } = new ObservableCollection<BoatModel>()
        {
            new BoatModel()
            {
                Id = 0
            }
        };

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
