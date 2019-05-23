using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Income.RoutedEvents;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace FiefApp.Module.Income.UIElements.IncomeUI
{
    /// <summary>
    /// Interaction logic for IncomeUI.xaml
    /// </summary>
    public partial class IncomeUI : INotifyPropertyChanged
    {
        public IncomeUI()
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
                typeof(IncomeUI),
                new PropertyMetadata(-1)
            );

        public string Income
        {
            get => (string)GetValue(IncomeProperty);
            set => SetValue(IncomeProperty, value);
        }

        public static readonly DependencyProperty IncomeProperty =
            DependencyProperty.Register(
                "Income",
                typeof(string),
                typeof(IncomeUI),
                new PropertyMetadata("")
            );

        public int ManorId
        {
            get => (int)GetValue(ManorIdProperty);
            set => SetValue(ManorIdProperty, value);
        }

        public static readonly DependencyProperty ManorIdProperty =
            DependencyProperty.Register(
                "ManorId",
                typeof(int),
                typeof(IncomeUI),
                new PropertyMetadata(-1)
            );

        public int Silver
        {
            get => (int)GetValue(SilverProperty);
            set => SetValue(SilverProperty, value);
        }

        public static readonly DependencyProperty SilverProperty =
            DependencyProperty.Register(
                "Silver",
                typeof(int),
                typeof(IncomeUI),
                new PropertyMetadata(-1)
            );

        public int Base
        {
            get => (int)GetValue(BaseProperty);
            set => SetValue(BaseProperty, value);
        }

        public static readonly DependencyProperty BaseProperty =
            DependencyProperty.Register(
                "Base",
                typeof(int),
                typeof(IncomeUI),
                new PropertyMetadata(-1)
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
                typeof(IncomeUI),
                new PropertyMetadata(-1)
            );

        public int Wood
        {
            get => (int)GetValue(WoodProperty);
            set => SetValue(WoodProperty, value);
        }

        public static readonly DependencyProperty WoodProperty =
            DependencyProperty.Register(
                "Wood",
                typeof(int),
                typeof(IncomeUI),
                new PropertyMetadata(-1)
            );

        public int Stone
        {
            get => (int)GetValue(StoneProperty);
            set => SetValue(StoneProperty, value);
        }

        public static readonly DependencyProperty StoneProperty =
            DependencyProperty.Register(
                "Stone",
                typeof(int),
                typeof(IncomeUI),
                new PropertyMetadata(-1)
            );

        public int Iron
        {
            get => (int)GetValue(IronProperty);
            set => SetValue(IronProperty, value);
        }

        public static readonly DependencyProperty IronProperty =
            DependencyProperty.Register(
                "Iron",
                typeof(int),
                typeof(IncomeUI),
                new PropertyMetadata(-1)
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
                typeof(IncomeUI),
                new PropertyMetadata(new ObservableCollection<StewardModel>())
            );

        public bool IsStewardNeeded
        {
            get => (bool)GetValue(IsStewardNeededProperty);
            set => SetValue(IsStewardNeededProperty, value);
        }

        public static readonly DependencyProperty IsStewardNeededProperty =
            DependencyProperty.Register(
                "IsStewardNeeded",
                typeof(bool),
                typeof(IncomeUI),
                new PropertyMetadata(false)
            );

        public bool ShowInIncomes
        {
            get => (bool)GetValue(ShowInIncomesProperty);
            set => SetValue(ShowInIncomesProperty, value);
        }

        public static readonly DependencyProperty ShowInIncomesProperty =
            DependencyProperty.Register(
                "ShowInIncomes",
                typeof(bool),
                typeof(IncomeUI),
                new PropertyMetadata(false)
            );

        public int Difficulty
        {
            get => (int)GetValue(DifficultyProperty);
            set => SetValue(DifficultyProperty, value);
        }

        public static readonly DependencyProperty DifficultyProperty =
            DependencyProperty.Register(
                "Difficulty",
                typeof(int),
                typeof(IncomeUI),
                new PropertyMetadata(-1)
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
                typeof(IncomeUI),
                new PropertyMetadata(-1)
            );

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

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Event Handler For Selection Changed

        private void StewardsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedIndex != -1)
            {
                IncomeUIEventArgs newEventArgs =
                    new IncomeUIEventArgs(
                        IncomeUIRoutedEvent,
                        "Changed",
                        StewardsCollection[SelectedIndex].Id,
                        Income,
                        Id
                    );

                RaiseEvent(newEventArgs);
            }
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent IncomeUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "IncomeUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(IncomeUI)
            );

        public event RoutedEventHandler IncomeUIEvent
        {
            add => AddHandler(IncomeUIRoutedEvent, value);
            remove => RemoveHandler(IncomeUIRoutedEvent, value);
        }

        #endregion

        private void IncomeUI_OnLoaded(object sender, RoutedEventArgs e)
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

            if (!ShowInIncomes)
            {
                Self.Visibility = Visibility.Collapsed;
            }
            else
            {
                Self.Visibility = Visibility.Visible;
            }
        }
    }
}
