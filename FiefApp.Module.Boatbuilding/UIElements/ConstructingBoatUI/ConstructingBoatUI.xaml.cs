using System;
using FiefApp.Common.Infrastructure.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiefApp.Module.Boatbuilding.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Boatbuilding.UIElements.ConstructingBoatUI
{
    /// <summary>
    /// Interaction logic for ConstructingBoatUI.xaml
    /// </summary>
    public partial class ConstructingBoatUI : INotifyPropertyChanged
    {
        public ConstructingBoatUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            DeleteBuildingBoat = new DelegateCommand(ExecuteDeleteBuildingBoat);
        }

        #region DelegateCommand

        public DelegateCommand DeleteBuildingBoat { get; set; }
        private void ExecuteDeleteBuildingBoat()
        {
            ConstructingBoatUIEventArgs newEventArgs =
                new ConstructingBoatUIEventArgs(
                    ConstructingBoatUIRoutedEvent,
                    "Delete",
                    Id,
                    0
                );

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
                typeof(ConstructingBoatUI),
                new PropertyMetadata(-1)
            );

        public string BoatType
        {
            get => (string)GetValue(BoatTypeProperty);
            set => SetValue(BoatTypeProperty, value);
        }

        public static readonly DependencyProperty BoatTypeProperty =
            DependencyProperty.Register(
                "BoatType",
                typeof(string),
                typeof(ConstructingBoatUI),
                new PropertyMetadata("")
            );

        public int Amount
        {
            get => (int)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }

        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register(
                "Amount",
                typeof(int),
                typeof(ConstructingBoatUI),
                new PropertyMetadata(-1)
            );

        public int CostWhenFinished
        {
            get => (int)GetValue(CostWhenFinishedProperty);
            set => SetValue(CostWhenFinishedProperty, value);
        }

        public static readonly DependencyProperty CostWhenFinishedProperty =
            DependencyProperty.Register(
                "CostWhenFinished",
                typeof(int),
                typeof(ConstructingBoatUI),
                new PropertyMetadata(-1)
            );

        public int NextFinishedDays
        {
            get => (int)GetValue(NextFinishedDaysProperty);
            set => SetValue(NextFinishedDaysProperty, value);
        }

        public static readonly DependencyProperty NextFinishedDaysProperty =
            DependencyProperty.Register(
                "NextFinishedDays",
                typeof(int),
                typeof(ConstructingBoatUI),
                new PropertyMetadata(-1)
            );

        public ObservableCollection<BoatbuilderModel> BoatBuildersCollection
        {
            get => (ObservableCollection<BoatbuilderModel>)GetValue(BoatBuildersCollectionProperty);
            set => SetValue(BoatBuildersCollectionProperty, value);
        }

        public static readonly DependencyProperty BoatBuildersCollectionProperty =
            DependencyProperty.Register(
                "BoatBuildersCollection",
                typeof(ObservableCollection<BoatbuilderModel>),
                typeof(ConstructingBoatUI),
                new PropertyMetadata(new ObservableCollection<BoatbuilderModel>())
            );

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register(
                "SelectedIndex",
                typeof(int),
                typeof(ConstructingBoatUI),
                new PropertyMetadata(-1)
            );

        #endregion

        #region Event Handlers

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedIndex != -1)
            {
                ConstructingBoatUIEventArgs newEventArgs =
                    new ConstructingBoatUIEventArgs(
                        ConstructingBoatUIRoutedEvent,
                        "Update",
                        Id,
                        BoatBuildersCollection[SelectedIndex].Id
                    );

                RaiseEvent(newEventArgs);
            }
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent ConstructingBoatUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "ConstructingBoatUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(ConstructingBoatUI)
            );

        public event RoutedEventHandler ConstructingBoatUIEvent
        {
            add => AddHandler(ConstructingBoatUIRoutedEvent, value);
            remove => RemoveHandler(ConstructingBoatUIRoutedEvent, value);
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
