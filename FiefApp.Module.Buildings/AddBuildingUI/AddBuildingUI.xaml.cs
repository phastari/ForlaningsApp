using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Buildings.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Buildings.AddBuildingUI
{
    /// <summary>
    /// Interaction logic for AddBuildingUI.xaml
    /// </summary>
    public partial class AddBuildingUI : INotifyPropertyChanged
    {
        public AddBuildingUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            CancelCommand = new DelegateCommand(ExecuteCancelCommand);
            SaveCommand = new DelegateCommand(ExecuteSaveCommand);
        }

        public DelegateCommand CancelCommand { get; set; }
        private void ExecuteCancelCommand()
        {
            AddBuildingUIEventArgs newEventArgs =
                new AddBuildingUIEventArgs(
                    AddBuildingUIRoutedEvent,
                    "Cancel"
                );

            RaiseEvent(newEventArgs);

            SelectedIndex = -1;
        }

        public DelegateCommand SaveCommand { get; set; }
        private void ExecuteSaveCommand()
        {
            BuildingModel model = new BuildingModel()
            {
                Building = BuildingsCollection[SelectedIndex].Building,
                Amount = Amount,
                Woodwork = BuildingsCollection[SelectedIndex].Woodwork,
                LeftWoodwork = BuildingsCollection[SelectedIndex].Woodwork * Amount,
                Wood = BuildingsCollection[SelectedIndex].Wood,
                LeftWood = BuildingsCollection[SelectedIndex].Wood * Amount,
                Stonework = BuildingsCollection[SelectedIndex].Stonework,
                LeftStonework = BuildingsCollection[SelectedIndex].Stonework * Amount,
                Stone = BuildingsCollection[SelectedIndex].Stone,
                LeftStone = BuildingsCollection[SelectedIndex].Stone * Amount,
                Smithswork = BuildingsCollection[SelectedIndex].Smithswork,
                LeftSmithswork = BuildingsCollection[SelectedIndex].Smithswork * Amount,
                Iron = BuildingsCollection[SelectedIndex].Iron,
                LeftIron = BuildingsCollection[SelectedIndex].Iron * Amount,
                UpkeepTotal = BuildingsCollection[SelectedIndex].Upkeep
            };

            AddBuildingUIEventArgs newEventArgs =
                new AddBuildingUIEventArgs(
                    AddBuildingUIRoutedEvent,
                    "Save",
                    model
                );

            RaiseEvent(newEventArgs);

            SelectedIndex = -1;
        }

        public ObservableCollection<BuildingModel> BuildingsCollection
        {
            get => (ObservableCollection<BuildingModel>)GetValue(BuildingsCollectionProperty);
            set => SetValue(BuildingsCollectionProperty, value);
        }

        public static readonly DependencyProperty BuildingsCollectionProperty =
            DependencyProperty.Register(
                "BuildingsCollection",
                typeof(ObservableCollection<BuildingModel>),
                typeof(AddBuildingUI),
                new PropertyMetadata(new ObservableCollection<BuildingModel>())
            );

        private int _amount = 1;
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                SetInformation();
                NotifyPropertyChanged();
            }
        }

        private int _woodwork;
        public int Woodwork
        {
            get => _woodwork;
            set
            {
                _woodwork = value;
                NotifyPropertyChanged();
            }
        }

        private int _stonework;
        public int Stonework
        {
            get => _stonework;
            set
            {
                _stonework = value;
                NotifyPropertyChanged();
            }
        }

        private int _smithswork;
        public int Smithswork
        {
            get => _smithswork;
            set
            {
                _smithswork = value;
                NotifyPropertyChanged();
            }
        }

        private int _iron;
        public int Iron
        {
            get => _iron;
            set
            {
                _iron = value;
                NotifyPropertyChanged();
            }
        }

        private int _stone;
        public int Stone
        {
            get => _stone;
            set
            {
                _stone = value;
                NotifyPropertyChanged();
            }
        }

        private int _wood;
        public int Wood
        {
            get => _wood;
            set
            {
                _wood = value;
                NotifyPropertyChanged();
            }
        }

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

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent AddBuildingUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "AddBuildingUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(AddBuildingUI)
            );

        public event RoutedEventHandler AddBuildingUIEvent
        {
            add => AddHandler(AddBuildingUIRoutedEvent, value);
            remove => RemoveHandler(AddBuildingUIRoutedEvent, value);
        }

        #endregion

        private void BuildingsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetInformation();
        }

        private void SetInformation()
        {
            if (SelectedIndex > -1)
            {
                Woodwork = BuildingsCollection[SelectedIndex].Woodwork * Amount;
                Wood = BuildingsCollection[SelectedIndex].Wood * Amount;
                Stonework = BuildingsCollection[SelectedIndex].Stonework * Amount;
                Stone = BuildingsCollection[SelectedIndex].Stone * Amount;
                Smithswork = BuildingsCollection[SelectedIndex].Smithswork * Amount;
                Iron = BuildingsCollection[SelectedIndex].Iron * Amount;
            }
            else
            {
                Woodwork = 0;
                Wood = 0;
                Stonework = 0;
                Stone = 0;
                Smithswork = 0;
                Iron = 0;
            }
        }
    }
}
