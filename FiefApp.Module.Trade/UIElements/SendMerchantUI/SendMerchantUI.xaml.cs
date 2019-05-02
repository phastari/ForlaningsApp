using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Trade.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Trade.UIElements.SendMerchantUI
{
    /// <summary>
    /// Interaction logic for SendMerchantUI.xaml
    /// </summary>
    public partial class SendMerchantUI : INotifyPropertyChanged
    {
        public SendMerchantUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            CancelCommand = new DelegateCommand(ExecuteCancelCommand);
            SendCommand = new DelegateCommand(ExecuteSendCommand);
        }

        #region DelegateCommand : CancelCommand

        public DelegateCommand CancelCommand { get; set; }
        private void ExecuteCancelCommand()
        {
            SendMerchantUIEventArgs newEventArgs =
                new SendMerchantUIEventArgs(
                    SendMerchantUIRoutedEvent,
                    "Cancel"
                );

            RaiseEvent(newEventArgs);
        }

        #endregion
        #region DelegateCommand : SendCommand

        public DelegateCommand SendCommand { get; set; }
        private void ExecuteSendCommand()
        {
            SendMerchantUIEventArgs newEventArgs =
                new SendMerchantUIEventArgs(
                    SendMerchantUIRoutedEvent,
                    "Send",
                    new MerchantModel()
                    {
                        Id = Id,
                        SendByCarriage = SendByCarriage,
                        SendWithCaravan = SendWithCaravan,
                        Guards = Guards,
                        SilverWith = SilverWith,
                        SilverBack = SilverBack,
                        BaseToSell = BaseToSell,
                        BuyBase = BuyBase,
                        LuxuryToSell = LuxuryToSell,
                        BuyLuxury = BuyLuxury,
                        IronToSell = IronToSell,
                        BuyIron = BuyIron,
                        WoodToSell = WoodToSell,
                        BuyWood = BuyWood,
                        StoneToSell = StoneToSell,
                        BuyStone = BuyStone
                    }
                );

            if (SelectedIndex != -1)
            {
                newEventArgs.Model.ShipId = ShipsCollection[SelectedIndex].Id;
            }
            else
            {
                newEventArgs.Model.ShipId = -1;
            }

            RaiseEvent(newEventArgs);
        }

        #endregion

        #region Dependency Properties

        public int Id
        {
            get => (int)GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register(
                "Id",
                typeof(int),
                typeof(SendMerchantUI),
                new PropertyMetadata(-1)
            );

        public ObservableCollection<BoatModel> ShipsCollection
        {
            get => (ObservableCollection<BoatModel>)GetValue(ShipsCollectionProperty);
            set => SetValue(ShipsCollectionProperty, value);
        }

        public static readonly DependencyProperty ShipsCollectionProperty =
            DependencyProperty.Register(
                "ShipsCollection",
                typeof(ObservableCollection<BoatModel>),
                typeof(SendMerchantUI),
                new PropertyMetadata(new ObservableCollection<BoatModel>())
            );

        #endregion

        #region UI Properties

        private int _selectedIndex = -1;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                NotifyPropertyChanged();
            }
        }

        private bool _sendByCarriage;
        public bool SendByCarriage
        {
            get => _sendByCarriage;
            set
            {
                _sendByCarriage = value;

                if (value == true)
                {
                    SendWithCaravan = false;
                    SelectedIndex = -1;
                    Cargo = 10;
                    SetCargoLeft();
                }

                NotifyPropertyChanged();
            }
        }

        private bool _sendWithCaravan;
        public bool SendWithCaravan
        {
            get => _sendWithCaravan;
            set
            {
                _sendWithCaravan = value;

                if (value == true)
                {
                    SendByCarriage = false;
                    SelectedIndex = -1;
                    Cargo = 50;
                    SetCargoLeft();
                }

                NotifyPropertyChanged();
            }
        }

        private int _cargo;
        public int Cargo
        {
            get => _cargo;
            set
            {
                _cargo = value;
                NotifyPropertyChanged();
            }
        }

        private int _cargoLeft;
        public int CargoLeft
        {
            get => _cargoLeft;
            set
            {
                _cargoLeft = value;
                NotifyPropertyChanged();
            }
        }

        private int _guards;
        public int Guards
        {
            get => _guards;
            set
            {
                _guards = value;
                NotifyPropertyChanged();
            }
        }

        private int _silverWith;
        public int SilverWith
        {
            get => _silverWith;
            set
            {
                _silverWith = value;
                NotifyPropertyChanged();
            }
        }

        private bool _silverBack;
        public bool SilverBack
        {
            get => _silverBack;
            set
            {
                _silverBack = value;
                NotifyPropertyChanged();
            }
        }

        private int _baseToSell;
        public int BaseToSell
        {
            get => _baseToSell;
            set
            {
                _baseToSell = value;
                NotifyPropertyChanged();
            }
        }

        private bool _buyBase;
        public bool BuyBase
        {
            get => _buyBase;
            set
            {
                _buyBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _luxuryToSell;
        public int LuxuryToSell
        {
            get => _luxuryToSell;
            set
            {
                _luxuryToSell = value;
                NotifyPropertyChanged();
            }
        }

        private bool _buyLuxury;
        public bool BuyLuxury
        {
            get => _buyLuxury;
            set
            {
                _buyLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _ironToSell;
        public int IronToSell
        {
            get => _ironToSell;
            set
            {
                _ironToSell = value;
                NotifyPropertyChanged();
            }
        }

        private bool _buyIron;
        public bool BuyIron
        {
            get => _buyIron;
            set
            {
                _buyIron = value;
                NotifyPropertyChanged();
            }
        }

        private int _stoneToSell;
        public int StoneToSell
        {
            get => _stoneToSell;
            set
            {
                _stoneToSell = value;
                NotifyPropertyChanged();
            }
        }

        private bool _buyStone;
        public bool BuyStone
        {
            get => _buyStone;
            set
            {
                _buyStone = value;
                NotifyPropertyChanged();
            }
        }

        private int _woodToSell;
        public int WoodToSell
        {
            get => _woodToSell;
            set
            {
                _woodToSell = value;
                NotifyPropertyChanged();
            }
        }

        private bool _buyWood;
        public bool BuyWood
        {
            get => _buyWood;
            set
            {
                _buyWood = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Methods

        private void Selector_OnSelectionChanged(
            object sender, 
            SelectionChangedEventArgs e)
        {
            Cargo = ShipsCollection[SelectedIndex].CargoTotal;
            SendByCarriage = false;
            SendWithCaravan = false;
            SetCargoLeft();
        }

        private void SetCargoLeft()
        {
            CargoLeft = Cargo - BaseToSell - LuxuryToSell - IronToSell - StoneToSell - WoodToSell;
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent SendMerchantUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "SendMerchantUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(SendMerchantUI)
            );

        public event RoutedEventHandler SendMerchantUIEvent
        {
            add => AddHandler(SendMerchantUIRoutedEvent, value);
            remove => RemoveHandler(SendMerchantUIRoutedEvent, value);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
