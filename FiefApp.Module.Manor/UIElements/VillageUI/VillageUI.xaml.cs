using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Manor.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Manor.UIElements.VillageUI
{
    /// <summary>
    /// Interaction logic for VillageUI.xaml
    /// </summary>
    public partial class VillageUI : INotifyPropertyChanged
    {
        public VillageUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;
        }

        private void TreeViewItemIsExpandedCommand(object sender, RoutedEventArgs e)
        {
            VillagesUIEventArgs newEventArgs =
                new VillagesUIEventArgs(
                    VillageUIRoutedEvent,
                    Id,
                    "Expanded"
                );
            RaiseEvent(newEventArgs);
        }

        private void TreeViewItemIsCollapsedCommand(object sender, RoutedEventArgs e)
        {
            if (CancelButton.Visibility == Visibility.Visible)
            {
                Id = _oldVillageModel.Id;
                Village = _oldVillageModel.Village;
                Population = _oldVillageModel.Population;
                Burgess = _oldVillageModel.Burgess;
                Farmers = _oldVillageModel.Farmers;
                Serfdoms = _oldVillageModel.Serfdoms;
                Boatbuilders = _oldVillageModel.Boatbuilders;
                Carpenters = _oldVillageModel.Carpenters;
                Furriers = _oldVillageModel.Furriers;
                Innkeepers = _oldVillageModel.Innkeepers;
                Millers = _oldVillageModel.Millers;
                Tanners = _oldVillageModel.Tanners;
                Tailors = _oldVillageModel.Tailors;
                Smiths = _oldVillageModel.Smiths;

                EditingVisibility = Visibility.Collapsed;
                EditButtonVisibility = Visibility.Visible;
            }
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Id = _oldVillageModel.Id;
            Village = _oldVillageModel.Village;
            Population = _oldVillageModel.Population;
            Burgess = _oldVillageModel.Burgess;
            Farmers = _oldVillageModel.Farmers;
            Serfdoms = _oldVillageModel.Serfdoms;
            Boatbuilders = _oldVillageModel.Boatbuilders;
            Carpenters = _oldVillageModel.Carpenters;
            Furriers = _oldVillageModel.Furriers;
            Innkeepers = _oldVillageModel.Innkeepers;
            Millers = _oldVillageModel.Millers;
            Tanners = _oldVillageModel.Tanners;
            Tailors = _oldVillageModel.Tailors;
            Smiths = _oldVillageModel.Smiths;

            EditingVisibility = Visibility.Collapsed;
            EditButtonVisibility = Visibility.Visible;
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            VillagesUIEventArgs newEventArgs =
                new VillagesUIEventArgs(
                    VillageUIRoutedEvent,
                    Id,
                    "Delete"
                );
            RaiseEvent(newEventArgs);

            EditingVisibility = Visibility.Collapsed;
            EditButtonVisibility = Visibility.Visible;
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            VillagesUIEventArgs newEventArgs =
                new VillagesUIEventArgs(
                    VillageUIRoutedEvent,
                    Id,
                    "Save",
                    new VillageModel()
                    {
                        Id = Id,
                        Village = Village,
                        Population = Population,
                        Burgess = Burgess,
                        Farmers = Farmers,
                        Serfdoms = Serfdoms,
                        Boatbuilders = Boatbuilders,
                        Carpenters = Carpenters,
                        Furriers = Furriers,
                        Innkeepers = Innkeepers,
                        IsExpanded = true,
                        Millers = Millers,
                        Tanners = Tanners,
                        Tailors = Tailors,
                        Smiths = Smiths
                    }
                );
            RaiseEvent(newEventArgs);

            EditingVisibility = Visibility.Collapsed;
            EditButtonVisibility = Visibility.Visible;
        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            _oldVillageModel = new VillageModel()
            {
                Id = Id,
                Village = Village,
                Population = Population,
                Burgess = Burgess,
                Farmers = Farmers,
                Serfdoms = Serfdoms,
                Boatbuilders = Boatbuilders,
                Carpenters = Carpenters,
                Furriers = Furriers,
                Innkeepers = Innkeepers,
                Millers = Millers,
                Tanners = Tanners,
                Tailors = Tailors,
                Smiths = Smiths
            };

            EditButtonVisibility = Visibility.Collapsed;
            EditingVisibility = Visibility.Visible;
        }

        #region UI Properties

        private VillageModel _oldVillageModel = new VillageModel();

        private Visibility _editButtonVisibility = Visibility.Visible;
        public Visibility EditButtonVisibility
        {
            get => _editButtonVisibility;
            set
            {
                _editButtonVisibility = value; 
                NotifyPropertyChanged();
            }
        }

        private Visibility _editingVisibility = Visibility.Collapsed;
        public Visibility EditingVisibility
        {
            get => _editingVisibility;
            set
            {
                _editingVisibility = value;
                NotifyPropertyChanged();
            }
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
                typeof(VillageUI),
                new PropertyMetadata(-1)
            );

        public string Village
        {
            get => (string)GetValue(VillageProperty);
            set => SetValue(VillageProperty, value);
        }

        public static readonly DependencyProperty VillageProperty =
            DependencyProperty.Register(
                "Village",
                typeof(string),
                typeof(VillageUI),
                new PropertyMetadata("")
            );

        public int Population
        {
            get => (int)GetValue(PopulationProperty);
            set => SetValue(PopulationProperty, value);
        }

        public static readonly DependencyProperty PopulationProperty =
            DependencyProperty.Register(
                "Population",
                typeof(int),
                typeof(VillageUI),
                new PropertyMetadata(0)
            );

        public int Burgess
        {
            get => (int)GetValue(BurgessProperty);
            set => SetValue(BurgessProperty, value);
        }

        public static readonly DependencyProperty BurgessProperty =
            DependencyProperty.Register(
                "Burgess",
                typeof(int),
                typeof(VillageUI),
                new PropertyMetadata(0)
            );

        public int Farmers
        {
            get => (int)GetValue(FarmersProperty);
            set => SetValue(FarmersProperty, value);
        }

        public static readonly DependencyProperty FarmersProperty =
            DependencyProperty.Register(
                "Farmers",
                typeof(int),
                typeof(VillageUI),
                new PropertyMetadata(0)
            );
        
        public int Serfdoms
        {
            get => (int)GetValue(SerfdomsProperty);
            set => SetValue(SerfdomsProperty, value);
        }

        public static readonly DependencyProperty SerfdomsProperty =
            DependencyProperty.Register(
                "Serfdoms",
                typeof(int),
                typeof(VillageUI),
                new PropertyMetadata(0)
            );

        public int Boatbuilders
        {
            get => (int)GetValue(BoatbuildersProperty);
            set => SetValue(BoatbuildersProperty, value);
        }

        public static readonly DependencyProperty BoatbuildersProperty =
            DependencyProperty.Register(
                "Boatbuilders",
                typeof(int),
                typeof(VillageUI),
                new PropertyMetadata(0)
            );

        public int Tanners
        {
            get => (int)GetValue(TannersProperty);
            set => SetValue(TannersProperty, value);
        }

        public static readonly DependencyProperty TannersProperty =
            DependencyProperty.Register(
                "Tanners",
                typeof(int),
                typeof(VillageUI),
                new PropertyMetadata(0)
            );

        public int Millers
        {
            get => (int)GetValue(MillersProperty);
            set => SetValue(MillersProperty, value);
        }

        public static readonly DependencyProperty MillersProperty =
            DependencyProperty.Register(
                "Millers",
                typeof(int),
                typeof(VillageUI),
                new PropertyMetadata(0)
            );

        public int Furriers
        {
            get => (int)GetValue(FurriersProperty);
            set => SetValue(FurriersProperty, value);
        }

        public static readonly DependencyProperty FurriersProperty =
            DependencyProperty.Register(
                "Furriers",
                typeof(int),
                typeof(VillageUI),
                new PropertyMetadata(0)
            );

        public int Tailors
        {
            get => (int)GetValue(TailorsProperty);
            set => SetValue(TailorsProperty, value);
        }

        public static readonly DependencyProperty TailorsProperty =
            DependencyProperty.Register(
                "Tailors",
                typeof(int),
                typeof(VillageUI),
                new PropertyMetadata(0)
            );

        public int Smiths
        {
            get => (int)GetValue(SmithsProperty);
            set => SetValue(SmithsProperty, value);
        }

        public static readonly DependencyProperty SmithsProperty =
            DependencyProperty.Register(
                "Smiths",
                typeof(int),
                typeof(VillageUI),
                new PropertyMetadata(0)
            );

        public int Carpenters
        {
            get => (int)GetValue(CarpentersProperty);
            set => SetValue(CarpentersProperty, value);
        }

        public static readonly DependencyProperty CarpentersProperty =
            DependencyProperty.Register(
                "Carpenters",
                typeof(int),
                typeof(VillageUI),
                new PropertyMetadata(0)
            );

        public int Innkeepers
        {
            get => (int)GetValue(InnkeepersProperty);
            set => SetValue(InnkeepersProperty, value);
        }

        public static readonly DependencyProperty InnkeepersProperty =
            DependencyProperty.Register(
                "Innkeepers",
                typeof(int),
                typeof(VillageUI),
                new PropertyMetadata(0)
            );

        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(
                "IsExpanded",
                typeof(bool),
                typeof(VillageUI),
                new PropertyMetadata(false)
            );

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent VillageUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "VillageUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(VillageUI)
            );

        public event RoutedEventHandler VillageUIEvent
        {
            add => AddHandler(VillageUIRoutedEvent, value);
            remove => RemoveHandler(VillageUIRoutedEvent, value);
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
