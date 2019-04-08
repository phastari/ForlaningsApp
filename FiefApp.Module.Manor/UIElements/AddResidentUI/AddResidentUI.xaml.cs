using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Manor.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Manor.UIElements.AddResidentUI
{
    /// <summary>
    /// Interaction logic for AddResidentUI.xaml
    /// </summary>
    public partial class AddResidentUI : INotifyPropertyChanged
    {
        public AddResidentUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            AddCommand = new DelegateCommand(ExecuteAddCommand);
            CancelCommand = new DelegateCommand(ExecuteCancelCommand);
            SaveCommand = new DelegateCommand(ExecuteSaveCommand);
        }

        #region Delegate Commands

        public DelegateCommand AddCommand { get; set; }
        private void ExecuteAddCommand()
        {
            Resident = "";
            Age = 0;

            AddButtonVisibility = Visibility.Collapsed;
            EditableVisibility = Visibility.Visible;
        }

        public DelegateCommand CancelCommand { get; set; }
        private void ExecuteCancelCommand()
        {
            Resident = "";
            Age = 0;

            AddButtonVisibility = Visibility.Visible;
            EditableVisibility = Visibility.Collapsed;
        }

        public DelegateCommand SaveCommand { get; set; }
        private void ExecuteSaveCommand()
        {
            AddResidentUIEventArgs newEventArgs =
                new AddResidentUIEventArgs(
                    AddResidentUIRoutedEvent,
                    -1,
                    "Save",
                    new ResidentModel()
                    {
                        Age = Age,
                        PersonName = Resident,
                        Position = "Boende"
                    }
                );
            RaiseEvent(newEventArgs);

            AddButtonVisibility = Visibility.Visible;
            EditableVisibility = Visibility.Collapsed;
        }

        #endregion

        #region UI Properties

        private Visibility _addButtonVisibility = Visibility.Visible;
        public Visibility AddButtonVisibility
        {
            get => _addButtonVisibility;
            set
            {
                _addButtonVisibility = value;
                NotifyPropertyChanged();
            }
        }

        private Visibility _editableVisibility = Visibility.Collapsed;
        public Visibility EditableVisibility
        {
            get => _editableVisibility;
            set
            {
                _editableVisibility = value;
                NotifyPropertyChanged();
            }
        }

        private string _resident;
        public string Resident
        {
            get => _resident;
            set
            {
                _resident = value;
                NotifyPropertyChanged();
            }
        }

        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent AddResidentUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "AddResidentUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(AddResidentUI)
            );

        public event RoutedEventHandler AddResidentUIEvent
        {
            add => AddHandler(AddResidentUIRoutedEvent, value);
            remove => RemoveHandler(AddResidentUIRoutedEvent, value);
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
