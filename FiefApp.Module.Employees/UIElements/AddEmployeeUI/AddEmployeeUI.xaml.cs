using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Employees.RoutedEvents;
using Prism.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace FiefApp.Module.Employees.UIElements.AddEmployeeUI
{
    /// <summary>
    /// Interaction logic for AddEmployeeUI.xaml
    /// </summary>
    public partial class AddEmployeeUI : INotifyPropertyChanged
    {
        public AddEmployeeUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            SaveCommand = new DelegateCommand(ExecuteSaveCommand);
            CancelCommand = new DelegateCommand(ExecuteCancelCommand);
            AddButtonClickCommand = new DelegateCommand(ExecuteAddButtonClickCommand);
        }

        #region DelegateCommands

        public DelegateCommand AddButtonClickCommand { get; set; }
        private void ExecuteAddButtonClickCommand()
        {
            ShowingButtonVisibility = Visibility.Collapsed;
        }

        public DelegateCommand SaveCommand { get; set; }
        private void ExecuteSaveCommand()
        {
            AddEmployeeUIEventArgs newEventArgs =
                new AddEmployeeUIEventArgs(
                    AddEmployeeUIRoutedEvent,
                    -1,
                    "Save",
                    new EmployeeModel()
                    {
                        Name = Employee,
                        BaseCost = Base,
                        LuxuryCost = Luxury
                    }
                );

            RaiseEvent(newEventArgs);

            Employee = "";
            Base = 0;
            Luxury = 0;

            ShowingButtonVisibility = Visibility.Visible;
        }

        public DelegateCommand CancelCommand { get; set; }
        private void ExecuteCancelCommand()
        {
            Employee = "";
            Base = 0;
            Luxury = 0;

            ShowingButtonVisibility = Visibility.Visible;
        }

        #endregion

        #region UI Properties

        private string _employee;
        public string Employee
        {
            get => _employee;
            set
            {
                _employee = value;
                NotifyPropertyChanged();
            }
        }

        private int _base;
        public int Base
        {
            get => _base;
            set
            {
                _base = value;
                NotifyPropertyChanged();
            }
        }

        private int _luxury;
        public int Luxury
        {
            get => _luxury;
            set
            {
                _luxury = value;
                NotifyPropertyChanged();
            }
        }

        private Visibility _showingButtonVisibility = Visibility.Visible;
        public Visibility ShowingButtonVisibility
        {
            get => _showingButtonVisibility;
            set
            {
                _showingButtonVisibility = value;
                ShowingAddVisibility = value
                    == Visibility.Collapsed
                    ? Visibility.Visible
                    : Visibility.Collapsed;
                NotifyPropertyChanged();
            }
        }

        private Visibility _showingAddVisibility = Visibility.Collapsed;
        public Visibility ShowingAddVisibility
        {
            get => _showingAddVisibility;
            set
            {
                _showingAddVisibility = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent AddEmployeeUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "AddEmployeeUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(AddEmployeeUI)
            );

        public event RoutedEventHandler AddEmployeeUIEvent
        {
            add => AddHandler(AddEmployeeUIRoutedEvent, value);
            remove => RemoveHandler(AddEmployeeUIRoutedEvent, value);
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
