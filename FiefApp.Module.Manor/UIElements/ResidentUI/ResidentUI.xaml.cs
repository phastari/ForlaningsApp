using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FiefApp.Module.Manor.RoutedEvents;
using Prism.Commands;
using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Manor.UIElements.ResidentUI
{
    /// <summary>
    /// Interaction logic for ResidentUI.xaml
    /// </summary>
    public partial class ResidentUI : INotifyPropertyChanged
    {
        public ResidentUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            DeleteResident = new DelegateCommand(ExecuteDeleteResident);
            EditResident = new DelegateCommand(ExecuteEditResident);
            SaveCommand = new DelegateCommand(ExecuteSaveCommand);
            CancelCommand = new DelegateCommand(ExecuteCancelCommand);
        }

        #region DelegateCommands

        public DelegateCommand DeleteResident { get; set; }
        private void ExecuteDeleteResident()
        {
            ResidentUIEventArgs newEventArgs =
                new ResidentUIEventArgs(
                    ResidentUIRoutedEvent,
                    Id,
                    "Delete"
                );
            RaiseEvent(newEventArgs);
        }

        public DelegateCommand EditResident { get; set; }
        private void ExecuteEditResident()
        {
            _oldResident = Resident;
            _oldAge = Age;

            ShowResidentVisibility = Visibility.Collapsed;
            ShowEditResidentVisibility = Visibility.Visible;
        }

        public DelegateCommand SaveCommand { get; set; }
        private void ExecuteSaveCommand()
        {
            ResidentUIEventArgs newEventArgs =
                new ResidentUIEventArgs(
                    ResidentUIRoutedEvent,
                    Id,
                    "Save",
                    new ResidentModel()
                    {
                        Age = Age,
                        Name = Resident
                    }
                );
            RaiseEvent(newEventArgs);
        }

        public DelegateCommand CancelCommand { get; set; }
        private void ExecuteCancelCommand()
        {
            Resident = _oldResident;
            Age = _oldAge;

            ShowResidentVisibility = Visibility.Visible;
            ShowEditResidentVisibility = Visibility.Collapsed;
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
                typeof(ResidentUI),
                new PropertyMetadata(0)
            );

        public string Resident
        {
            get => (string)GetValue(ResidentProperty);
            set => SetValue(ResidentProperty, value);
        }

        public static readonly DependencyProperty ResidentProperty =
            DependencyProperty.Register(
                "Resident",
                typeof(string),
                typeof(ResidentUI),
                new PropertyMetadata("")
            );

        public string Position
        {
            get => (string)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register(
                "Position",
                typeof(string),
                typeof(ResidentUI),
                new PropertyMetadata("")
            );

        public string ResidentType
        {
            get => (string)GetValue(ResidentTypeProperty);
            set => SetValue(ResidentTypeProperty, value);
        }

        public static readonly DependencyProperty ResidentTypeProperty =
            DependencyProperty.Register(
                "ResidentType",
                typeof(string),
                typeof(ResidentUI),
                new PropertyMetadata("", CheckIsResident)
            );

        public int Age
        {
            get => (int)GetValue(AgeProperty);
            set => SetValue(AgeProperty, value);
        }

        public static readonly DependencyProperty AgeProperty =
            DependencyProperty.Register(
                "Age",
                typeof(int),
                typeof(ResidentUI),
                new PropertyMetadata(0)
            );

        #endregion

        #region UI Properties

        private bool _isResident;
        public bool IsResident
        {
            get => _isResident;
            set
            {
                _isResident = value;
                NotifyPropertyChanged();
            }
        }

        private Visibility _showResidentVisibility = Visibility.Visible;
        public Visibility ShowResidentVisibility
        {
            get => _showResidentVisibility;
            set
            {
                _showResidentVisibility = value;
                NotifyPropertyChanged();
            }
        }

        private Visibility _showEditResidentVisibility = Visibility.Collapsed;
        public Visibility ShowEditResidentVisibility
        {
            get => _showEditResidentVisibility;
            set
            {
                _showEditResidentVisibility = value;
                NotifyPropertyChanged();
            }
        }

        private string _oldResident;
        private int _oldAge;

        #endregion

        #region Methods

        private static void CheckIsResident(
            DependencyObject d, 
            DependencyPropertyChangedEventArgs e
            )
        {
            if (d is ResidentUI c)
            {
                c.IsResident = c.ResidentType == "Resident";
            }
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent ResidentUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "ResidentUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(ResidentUI)
            );

        public event RoutedEventHandler ResidentUIEvent
        {
            add => AddHandler(ResidentUIRoutedEvent, value);
            remove => RemoveHandler(ResidentUIRoutedEvent, value);
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