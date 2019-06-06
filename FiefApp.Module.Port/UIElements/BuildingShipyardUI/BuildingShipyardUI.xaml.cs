using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Port.RoutedEvents;

namespace FiefApp.Module.Port.UIElements.BuildingShipyardUI
{
    /// <summary>
    /// Interaction logic for BuildingShipyardUI.xaml
    /// </summary>
    public partial class BuildingShipyardUI : INotifyPropertyChanged
    {
        public BuildingShipyardUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;
        }

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
                typeof(BuildingShipyardUI),
                new PropertyMetadata(-1)
            );

        public bool CantEdit
        {
            get => (bool)GetValue(CantEditProperty);
            set => SetValue(CantEditProperty, value);
        }

        public static readonly DependencyProperty CantEditProperty =
            DependencyProperty.Register(
                "CantEdit",
                typeof(bool),
                typeof(BuildingShipyardUI),
                new PropertyMetadata(
                    true)
            );

        public int DaysWorkNeeded
        {
            get => (int)GetValue(DaysWorkNeededProperty);
            set => SetValue(DaysWorkNeededProperty, value);
        }

        public static readonly DependencyProperty DaysWorkNeededProperty =
            DependencyProperty.Register(
                "DaysWorkNeeded",
                typeof(int),
                typeof(BuildingShipyardUI),
                new PropertyMetadata(0)
            );

        public ObservableCollection<StewardModel> StewardsCollection
        {
            get => (ObservableCollection<StewardModel>)GetValue(StewardsCollectionProperty);
            set => SetValue(StewardsCollectionProperty, value);
        }

        public static readonly DependencyProperty StewardsCollectionProperty =
            DependencyProperty.Register(
                "StewardsCollection",
                typeof(ObservableCollection<StewardModel>),
                typeof(BuildingShipyardUI),
                new PropertyMetadata(new ObservableCollection<StewardModel>())
            );

        public int StewardId
        {
            get => (int)GetValue(StewardIdProperty);
            set => SetValue(StewardIdProperty, value);
        }

        public static readonly DependencyProperty StewardIdProperty =
            DependencyProperty.Register(
                "StewardId",
                typeof(int),
                typeof(BuildingShipyardUI),
                new PropertyMetadata(0)
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

        private int _daysWorkThisYear;
        public int DaysWorkThisYear
        {
            get => _daysWorkThisYear;
            set
            {
                if (value < 0)
                {
                    _daysWorkThisYear = 0;
                }
                else if (value > DaysWorkNeeded)
                {
                    _daysWorkThisYear = DaysWorkNeeded;
                }
                else
                {
                    _daysWorkThisYear = value;
                }
                RaiseDaysWorkThisYearChanged();
                NotifyPropertyChanged();
            }
        }        

        private bool _loaded = false;

        #endregion

        private void Selector_OnSelectionChanged(
            object sender, 
            SelectionChangedEventArgs e)
        {
            if (_loaded && SelectedIndex != -1)
            {
                BuildingShipyardUIEventArgs newEventArgs =
                    new BuildingShipyardUIEventArgs(
                        BuildingShipyardUIRoutedEvent,
                        "Changed",
                        StewardsCollection[SelectedIndex].Id,
                        Id
                    );

                RaiseEvent(newEventArgs);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            int index = -1;
            for (int x = 0; x < StewardsCollection.Count; x++)
            {
                if (StewardsCollection[x].Id == StewardId)
                {
                    index = x;
                }
            }

            SelectedIndex = index;
            _loaded = true;
        }

        private void RaiseDaysWorkThisYearChanged()
        {
            BuildingShipyardUIEventArgs newEventArgs =
                    new BuildingShipyardUIEventArgs(
                        BuildingShipyardUIRoutedEvent,
                        "DaysWorkChanged",
                        StewardsCollection[SelectedIndex].Id,
                        Id,
                        DaysWorkThisYear
                    );

            RaiseEvent(newEventArgs);
        }

        #region RoutedEvents

        public static readonly RoutedEvent BuildingShipyardUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "BuildingShipyardUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(GotShipyardUI.GotShipyardUI)
            );

        public event RoutedEventHandler BuildingShipyardUIEvent
        {
            add => AddHandler(BuildingShipyardUIRoutedEvent, value);
            remove => RemoveHandler(BuildingShipyardUIRoutedEvent, value);
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
