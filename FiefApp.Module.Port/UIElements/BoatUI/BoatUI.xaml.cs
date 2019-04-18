using FiefApp.Module.Port.RoutedEvents;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Port.UIElements.BoatUI
{
    /// <summary>
    /// Interaction logic for BoatUI.xaml
    /// </summary>
    public partial class BoatUI : INotifyPropertyChanged
    {
        public BoatUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            CrewCommand = new DelegateCommand(ExecuteCrewCommand);
        }

        #region DelegateCommand : CrewCommand

        public DelegateCommand CrewCommand { get; set; }
        private void ExecuteCrewCommand()
        {
            BoatUIEventArgs newEventArgs =
                new BoatUIEventArgs(
                    BoatUIRoutedEvent,
                    Id,
                    "Crew"
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
                typeof(BoatUI),
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
                typeof(BoatUI),
                new PropertyMetadata("")
            );

        public ObservableCollection<CaptainModel> CaptainsCollection
        {
            get => (ObservableCollection<CaptainModel>)GetValue(CaptainsCollectionProperty);
            set => SetValue(CaptainsCollectionProperty, value);
        }

        public static readonly DependencyProperty CaptainsCollectionProperty =
            DependencyProperty.Register(
                "CaptainsCollection",
                typeof(ObservableCollection<CaptainModel>),
                typeof(BoatUI),
                new PropertyMetadata(new ObservableCollection<CaptainModel>())
            );

        public decimal Cargo
        {
            get => (decimal)GetValue(CargoProperty);
            set => SetValue(CargoProperty, value);
        }

        public static readonly DependencyProperty CargoProperty =
            DependencyProperty.Register(
                "Cargo",
                typeof(decimal),
                typeof(BoatUI),
                new PropertyMetadata(-1M)
            );

        public decimal Crew
        {
            get => (decimal)GetValue(CrewProperty);
            set => SetValue(CrewProperty, value);
        }

        public static readonly DependencyProperty CrewProperty =
            DependencyProperty.Register(
                "Crew",
                typeof(decimal),
                typeof(BoatUI),
                new PropertyMetadata(-1M)
            );

        public string Seaworthiness
        {
            get => (string)GetValue(SeaworthinessProperty);
            set => SetValue(SeaworthinessProperty, value);
        }

        public static readonly DependencyProperty SeaworthinessProperty =
            DependencyProperty.Register(
                "Seaworthiness",
                typeof(string),
                typeof(BoatUI),
                new PropertyMetadata("")
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

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent BoatUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "BoatUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(BoatUI)
            );

        public event RoutedEventHandler BoatUIEvent
        {
            add => AddHandler(BoatUIRoutedEvent, value);
            remove => RemoveHandler(BoatUIRoutedEvent, value);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void CaptainComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BoatUIEventArgs newEventArgs =
                new BoatUIEventArgs(
                    BoatUIRoutedEvent,
                    Id,
                    "Changed",
                    null,
                    CaptainsCollection[SelectedIndex].Id,
                    CaptainsCollection[SelectedIndex].PersonName
                );

            RaiseEvent(newEventArgs);
        }
    }
}
