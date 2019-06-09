using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FiefApp.Module.Employees.RoutedEvents;
using Prism.Commands;
using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Employees.UIElements.EmployeeUI
{
    /// <summary>
    /// Interaction logic for EmployeeUI.xaml
    /// </summary>
    public partial class EmployeeUI : INotifyPropertyChanged
    {
        public EmployeeUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            DeleteEmployee = new DelegateCommand(ExecuteDeleteEmployee);
            EditEmployee = new DelegateCommand(ExecuteEditEmployee);
            CancelCommand = new DelegateCommand(ExecuteCancelCommand);
            SaveCommand = new DelegateCommand(ExecuteSaveCommand);
        }

        #region DelegateCommands

        public DelegateCommand DeleteEmployee { get; set; }
        private void ExecuteDeleteEmployee()
        {
            EmployeeUIEventArgs newEventArgs =
                new EmployeeUIEventArgs(
                    EmployeeUIRoutedEvent,
                    Id,
                    "Delete"
                );
            RaiseEvent(newEventArgs);
        }

        public DelegateCommand EditEmployee { get; set; }
        private void ExecuteEditEmployee()
        {
            _oldValues.PersonName = Employee;
            _oldValues.BaseCost = BaseCost;
            _oldValues.Base = Base;
            _oldValues.LuxuryCost = LuxuryCost;
            _oldValues.Luxury = Luxury;

            ShowEmployeeVisibility = Visibility.Collapsed;
        }

        public DelegateCommand CancelCommand { get; set; }
        private void ExecuteCancelCommand()
        {
            Employee = _oldValues.PersonName;
            BaseCost = _oldValues.BaseCost;
            Base = _oldValues.Base;
            LuxuryCost = _oldValues.LuxuryCost;
            Luxury = _oldValues.Luxury;

            ShowEmployeeVisibility = Visibility.Visible;
        }

        public DelegateCommand SaveCommand { get; set; }
        private void ExecuteSaveCommand()
        {
            EmployeeUIEventArgs newEventArgs =
                new EmployeeUIEventArgs(
                    EmployeeUIRoutedEvent,
                    Id,
                    "Save",
                    new EmployeeModel()
                    {
                        Id = Id,
                        PersonName = Employee,
                        BaseCost = BaseCost,
                        Base = BaseCost * Number,
                        LuxuryCost = LuxuryCost,
                        Luxury = LuxuryCost * Number,
                        Number = Number
                    }
                );
            RaiseEvent(newEventArgs);

            Base = BaseCost * Number;
            Luxury = LuxuryCost * Number;

            ShowEmployeeVisibility = Visibility.Visible;
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
                typeof(EmployeeUI),
                new PropertyMetadata(-1)
            );

        public bool IsAll
        {
            get => (bool)GetValue(IsAllProperty);
            set => SetValue(IsAllProperty, value);
        }

        public static readonly DependencyProperty IsAllProperty =
            DependencyProperty.Register(
                "IsAll",
                typeof(bool),
                typeof(EmployeeUI),
                new PropertyMetadata(false)
            );

        public string Employee
        {
            get => (string)GetValue(EmployeeProperty);
            set => SetValue(EmployeeProperty, value);
        }

        public static readonly DependencyProperty EmployeeProperty =
            DependencyProperty.Register(
                "Employee",
                typeof(string),
                typeof(EmployeeUI),
                new PropertyMetadata("")
            );

        public int Number
        {
            get => (int)GetValue(NumberProperty);
            set => SetValue(NumberProperty, value);
        }

        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register(
                "Number",
                typeof(int),
                typeof(EmployeeUI),
                new PropertyMetadata(0, RaiseUpdate)
            );

        private static void RaiseUpdate(
            DependencyObject d, 
            DependencyPropertyChangedEventArgs e
            )
        {
            if (d is EmployeeUI c)
                c.RaiseUpdateEvent();
        }

        private void RaiseUpdateEvent()
        {
            Base = BaseCost * Number;
            Luxury = LuxuryCost * Number;

            EmployeeUIEventArgs newEventArgs =
                new EmployeeUIEventArgs(
                    EmployeeUIRoutedEvent,
                    Id,
                    "Update",
                    new EmployeeModel()
                    {
                        Number = Number,
                        Base = Base,
                        Luxury = Luxury
                    }
                );
            RaiseEvent(newEventArgs);
        }

        public int Base
        {
            get => (int)GetValue(BaseProperty);
            set => SetValue(BaseProperty, value);
        }

        public static readonly DependencyProperty BaseProperty =
            DependencyProperty.Register(
                "Base",
                typeof(int),
                typeof(EmployeeUI),
                new PropertyMetadata(0)
            );

        public int BaseCost
        {
            get => (int)GetValue(BaseCostProperty);
            set => SetValue(BaseCostProperty, value);
        }

        public static readonly DependencyProperty BaseCostProperty =
            DependencyProperty.Register(
                "BaseCost",
                typeof(int),
                typeof(EmployeeUI),
                new PropertyMetadata(0)
            );

        public int Luxury
        {
            get => (int)GetValue(LuxuryProperty);
            set => SetValue(LuxuryProperty, value);
        }

        public static readonly DependencyProperty LuxuryProperty =
            DependencyProperty.Register(
                "Luxury",
                typeof(int),
                typeof(EmployeeUI),
                new PropertyMetadata(0)
            );

        public int LuxuryCost
        {
            get => (int)GetValue(LuxuryCostProperty);
            set => SetValue(LuxuryCostProperty, value);
        }

        public static readonly DependencyProperty LuxuryCostProperty =
            DependencyProperty.Register(
                "LuxuryCost",
                typeof(int),
                typeof(EmployeeUI),
                new PropertyMetadata(0)
            );

        #endregion

        #region UI Properties

        private EmployeeModel _oldValues = new EmployeeModel();

        private Visibility _showingEditVisibility = Visibility.Collapsed;
        public Visibility ShowingEditVisibility
        {
            get => _showingEditVisibility;
            set
            {
                _showingEditVisibility = value;
                NotifyPropertyChanged();
            }
        }

        private Visibility _showEmployeeVisibility = Visibility.Visible;
        public Visibility ShowEmployeeVisibility
        {
            get => _showEmployeeVisibility;
            set
            {
                _showEmployeeVisibility = value;
                ShowingEditVisibility = value 
                    == Visibility.Visible 
                        ? Visibility.Collapsed 
                        : Visibility.Visible;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent EmployeeUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "EmployeeUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(EmployeeUI)
            );

        public event RoutedEventHandler EmployeeUIEvent
        {
            add => AddHandler(EmployeeUIRoutedEvent, value);
            remove => RemoveHandler(EmployeeUIRoutedEvent, value);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void EmployeeUI_OnLoaded(object sender, RoutedEventArgs e)
        {
            RaiseUpdateEvent();

            if (IsAll)
            {
                AmountTextBox.TextBoxReadOnly = true;
                DeleteButton.Style = null;
                EditButton.Style = null;
                DeleteButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
