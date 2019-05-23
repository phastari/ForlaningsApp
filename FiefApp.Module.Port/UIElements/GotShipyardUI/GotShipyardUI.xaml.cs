using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Port.RoutedEvents;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
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
                new PropertyMetadata(null)
            );

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

        private void ShipyardModelPropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Taxes":

                    break;
            }
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
            ShipyardModel.PropertyChanged += ShipyardModelPropertyChanged;
        }
    }
}
