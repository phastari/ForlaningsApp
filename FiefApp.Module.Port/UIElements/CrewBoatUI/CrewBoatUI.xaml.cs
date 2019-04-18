using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Port.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Port.UIElements.CrewBoatUI
{
    /// <summary>
    /// Interaction logic for CrewBoatUI.xaml
    /// </summary>
    public partial class CrewBoatUI : INotifyPropertyChanged
    {
        public CrewBoatUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            CancelCommand = new DelegateCommand(ExecuteCancelCommand);
            SaveCommand = new DelegateCommand(ExecuteSaveCommand);
        }

        #region DelegateCommands

        public DelegateCommand CancelCommand { get; set; }
        private void ExecuteCancelCommand()
        {
            CrewBoatUIEventArgs newEventArgs =
                new CrewBoatUIEventArgs(
                    CrewBoatUIRoutedEvent,
                    Id,
                    "Cancel"
                );

            RaiseEvent(newEventArgs);
        }

        public DelegateCommand SaveCommand { get; set; }
        private void ExecuteSaveCommand()
        {
            BoatModel model = new BoatModel
            {
                Id = Id,
                CrewNeeded = CrewNeeded,
                RowersNeeded = RowersNeeded,
                CrewedSailors = CrewedSailors,
                CrewedSeamens = CrewedSeamens,
                CrewedMariners = CrewedMariners,
                CrewedRowers = CrewedRowers,
                AmountOfficers = AmountOfficers,
                AmountNavigators = AmountNavigators,
                AmountGuards = AmountGuards,
                Seaworthiness = Seaworthiness,
                Defense = Defense,
                CostSilver = CostSilver
            };

            CrewBoatUIEventArgs newEventArgs =
                new CrewBoatUIEventArgs(
                    CrewBoatUIRoutedEvent,
                    Id,
                    "Save",
                    model
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
                typeof(CrewBoatUI),
                new PropertyMetadata(-1)
            );

        public int CrewNeededTotal
        {
            get => (int)GetValue(CrewNeededTotalProperty);
            set => SetValue(CrewNeededTotalProperty, value);
        }

        public static readonly DependencyProperty CrewNeededTotalProperty =
            DependencyProperty.Register(
                "CrewNeededTotal",
                typeof(int),
                typeof(CrewBoatUI),
                new PropertyMetadata(-1)
            );

        public int RowersNeededTotal
        {
            get => (int)GetValue(RowersNeededTotalProperty);
            set => SetValue(RowersNeededTotalProperty, value);
        }

        public static readonly DependencyProperty RowersNeededTotalProperty =
            DependencyProperty.Register(
                "RowersNeededTotal",
                typeof(int),
                typeof(CrewBoatUI),
                new PropertyMetadata(-1)
            );

        public int AmountSailors
        {
            get => (int)GetValue(AmountSailorsProperty);
            set => SetValue(AmountSailorsProperty, value);
        }

        public static readonly DependencyProperty AmountSailorsProperty =
            DependencyProperty.Register(
                "AmountSailors",
                typeof(int),
                typeof(CrewBoatUI),
                new PropertyMetadata(-1)
            );

        public int AmountSeamens
        {
            get => (int)GetValue(AmountSeamensProperty);
            set => SetValue(AmountSeamensProperty, value);
        }

        public static readonly DependencyProperty AmountSeamensProperty =
            DependencyProperty.Register(
                "AmountSeamens",
                typeof(int),
                typeof(CrewBoatUI),
                new PropertyMetadata(-1)
            );

        public int AmountMariners
        {
            get => (int)GetValue(AmountMarinersProperty);
            set => SetValue(AmountMarinersProperty, value);
        }

        public static readonly DependencyProperty AmountMarinersProperty =
            DependencyProperty.Register(
                "AmountMariners",
                typeof(int),
                typeof(CrewBoatUI),
                new PropertyMetadata(-1)
            );

        public int AmountRowers
        {
            get => (int)GetValue(AmountRowersProperty);
            set => SetValue(AmountRowersProperty, value);
        }

        public static readonly DependencyProperty AmountRowersProperty =
            DependencyProperty.Register(
                "AmountRowers",
                typeof(int),
                typeof(CrewBoatUI),
                new PropertyMetadata(-1)
            );

        #endregion
        #region UI Properties

        private int _crewNeeded;
        public int CrewNeeded
        {
            get => _crewNeeded;
            set
            {
                _crewNeeded = value;
                NotifyPropertyChanged();
            }
        }

        private int _rowersNeeded;
        public int RowersNeeded
        {
            get => _rowersNeeded;
            set
            {
                _rowersNeeded = value;
                NotifyPropertyChanged();
            }
        }

        private int _crewedSailors;
        public int CrewedSailors
        {
            get => _crewedSailors;
            set
            {
                _crewedSailors = value;
                NotifyPropertyChanged();
            }
        }

        private int _crewedSeamens;
        public int CrewedSeamens
        {
            get => _crewedSeamens;
            set
            {
                _crewedSeamens = value;
                NotifyPropertyChanged();
            }
        }

        private int _crewedMariners;
        public int CrewedMariners
        {
            get => _crewedMariners;
            set
            {
                _crewedMariners = value;
                NotifyPropertyChanged();
            }
        }

        private int _crewedRowers;
        public int CrewedRowers
        {
            get => _crewedRowers;
            set
            {
                _crewedRowers = value;
                NotifyPropertyChanged();
            }
        }

        private int _amountOfficers;
        public int AmountOfficers
        {
            get => _amountOfficers;
            set
            {
                _amountOfficers = value;
                NotifyPropertyChanged();
            }
        }

        private int _amountNavigators;
        public int AmountNavigators
        {
            get => _amountNavigators;
            set
            {
                _amountNavigators = value;
                NotifyPropertyChanged();
            }
        }

        private int _amountGuards;
        public int AmountGuards
        {
            get => _amountGuards;
            set
            {
                _amountGuards = value;
                NotifyPropertyChanged();
            }
        }

        private string _seaworthiness = "0";
        public string Seaworthiness
        {
            get => _seaworthiness;
            set
            {
                _seaworthiness = value;
                NotifyPropertyChanged();
            }
        }

        private string _defense = "0";
        public string Defense
        {
            get => _defense;
            set
            {
                _defense = value;
                NotifyPropertyChanged();
            }
        }

        private int _costSilver;
        public int CostSilver
        {
            get => _costSilver;
            set
            {
                _costSilver = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent CrewBoatUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "CrewBoatUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(CrewBoatUI)
            );

        public event RoutedEventHandler CrewBoatUIEvent
        {
            add => AddHandler(CrewBoatUIRoutedEvent, value);
            remove => RemoveHandler(CrewBoatUIRoutedEvent, value);
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
