using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Port.RoutedEvents;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace FiefApp.Module.Port.UIElements.GotShipyardUI
{
    /// <summary>
    /// Interaction logic for GotShipyardUI.xaml
    /// </summary>
    public partial class GotShipyardUI : INotifyPropertyChanged
    {
        public GotShipyardUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;
        }

        #region Dependency Properties

        public bool CantEdit
        {
            get => (bool)GetValue(CantEditProperty);
            set => SetValue(CantEditProperty, value);
        }

        public static readonly DependencyProperty CantEditProperty =
            DependencyProperty.Register(
                "CantEdit",
                typeof(bool),
                typeof(GotShipyardUI),
                new PropertyMetadata(
                    true)
            );

        public ShipyardModel ShipyardModel
        {
            get => (ShipyardModel)GetValue(ShipyardModelProperty);
            set => SetValue(ShipyardModelProperty, value);
        }

        public static readonly DependencyProperty ShipyardModelProperty =
            DependencyProperty.Register(
                "ShipyardModel",
                typeof(ShipyardModel),
                typeof(GotShipyardUI),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(RaiseUpdate))
            );

        private static void RaiseUpdate(
            DependencyObject d, 
            DependencyPropertyChangedEventArgs args)
        {
            if (d is GotShipyardUI c)
                c.ExecuteUpdate();
        }

        private void ExecuteUpdate()
        {
            if (ShipyardModel != null)
            {
                SetSize();

                if (ShipyardModel.StewardId > -1)
                {
                    SelectedIndex = ShipyardModel.StewardsCollection.IndexOf(ShipyardModel.StewardsCollection.FirstOrDefault(o => o.Id == ShipyardModel.StewardId));
                }

                ShipyardModel.PropertyChanged += ShipyardModelPropertyChanged;
            }
        }

        #endregion

        #region UI Properties

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                NotifyPropertyChanged();
            }
        }

        private string _size;
        public string Size
        {
            get => _size;
            set
            {
                _size = value;
                NotifyPropertyChanged();
            }
        }

        private bool _loaded = false;
        private int _check = 0;

        #endregion

        #region Methods

        private void StewardComboBox_OnSelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            if (SelectedIndex != -1)
            {
                StewardChanged();
            }
        }

        private void StewardChanged()
        {
            if (_loaded && SelectedIndex != -1)
            {
                ShipyardModel.StewardId = ShipyardModel.StewardsCollection[SelectedIndex].Id;
                ShipyardModel.Steward = ShipyardModel.StewardsCollection[SelectedIndex].PersonName;

                GotShipyardUIEventArgs newEventArgs =
                    new GotShipyardUIEventArgs(
                        GotShipyardUIRoutedEvent,
                        "Changed",
                        ShipyardModel
                    );

                RaiseEvent(newEventArgs);
            }
        }

        private void ShipyardModelPropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Taxes":

                    break;

                case "Income":

                    break;

                case "Size":
                    SetSize();
                    break;

                case "StewardId":
                    if (ShipyardModel.StewardId > -1)
                    {
                        SelectedIndex = ShipyardModel.StewardsCollection.IndexOf(ShipyardModel.StewardsCollection.FirstOrDefault(o => o.Id == ShipyardModel.StewardId));
                    }
                    break;

                case "IsBeingUpgraded":
                    if (_check == 0)
                    {
                        _check++;
                        if (ShipyardModel.IsBeingUpgraded
                            && _loaded
                            && !ShipyardModel.Upgrading)
                        {
                            GotShipyardUIEventArgs newEventArgs =
                                new GotShipyardUIEventArgs(
                                    GotShipyardUIRoutedEvent,
                                    "Upgrading"
                                );

                            RaiseEvent(newEventArgs);
                        }
                    }
                    else
                    {
                        _check = 0;
                    }
                    
                    break;
            }
            NotifyPropertyChanged();
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent GotShipyardUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "GotShipyardUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(GotShipyardUI)
            );

        public event RoutedEventHandler GotShipyardUIEvent
        {
            add => AddHandler(GotShipyardUIRoutedEvent, value);
            remove => RemoveHandler(GotShipyardUIRoutedEvent, value);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void GotShipyardUI_OnLoaded(
            object sender,
            RoutedEventArgs e)
        {
            SetInformation();

            if (ShipyardModel.StewardId > 0)
            {
                SelectedIndex = ShipyardModel.StewardsCollection.IndexOf(ShipyardModel.StewardsCollection.FirstOrDefault(o => o.Id == ShipyardModel.StewardId));
            }

            ShipyardModel.PropertyChanged += ShipyardModelPropertyChanged;
            _loaded = true;
        }

        private void SetInformation()
        {
            SetSize();
        }

        #region SetInformation

        private void SetSize()
        {
            if (ShipyardModel != null)
            {
                if (ShipyardModel.Size == "0")
                {
                    Size = "Byhamn";
                }
                else if (ShipyardModel.Size == "1")
                {
                    Size = "Fiskehamn";
                }
                else if (ShipyardModel.Size == "2")
                {
                    Size = "Liten hamn";
                }
                else if (ShipyardModel.Size == "3")
                {
                    Size = "Hamn";
                }
                else if (ShipyardModel.Size == "4")
                {
                    Size = "Handelshamn";
                }
                else if (ShipyardModel.Size == "5")
                {
                    Size = "Stor handelshamn";
                }
                else if (ShipyardModel.Size == "6")
                {
                    Size = "Enorm handelshamn";
                }
            }
        }

        #endregion
    }
}
