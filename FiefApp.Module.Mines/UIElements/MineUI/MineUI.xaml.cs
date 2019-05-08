using FiefApp.Common.Infrastructure.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiefApp.Module.Mines.RoutedEvents;

namespace FiefApp.Module.Mines.UIElements.MineUI
{
    /// <summary>
    /// Interaction logic for MineUI.xaml
    /// </summary>
    public partial class MineUI : INotifyPropertyChanged
    {
        public MineUI()
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
                typeof(MineUI),
                new PropertyMetadata(-1)
            );

        public string MineType
        {
            get => (string)GetValue(MineTypeProperty);
            set => SetValue(MineTypeProperty, value);
        }

        public static readonly DependencyProperty MineTypeProperty =
            DependencyProperty.Register(
                "MineType",
                typeof(string),
                typeof(MineUI),
                new PropertyMetadata("")
            );

        public int Income
        {
            get => (int)GetValue(IncomeProperty);
            set => SetValue(IncomeProperty, value);
        }

        public static readonly DependencyProperty IncomeProperty =
            DependencyProperty.Register(
                "Income",
                typeof(int),
                typeof(MineUI),
                new PropertyMetadata(-1)
            );

        public int Guards
        {
            get => (int)GetValue(GuardsProperty);
            set => SetValue(GuardsProperty, value);
        }

        public static readonly DependencyProperty GuardsProperty =
            DependencyProperty.Register(
                "Guards",
                typeof(int),
                typeof(MineUI),
                new PropertyMetadata(-1)
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
                typeof(MineUI),
                new PropertyMetadata(6)
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
                typeof(MineUI),
                new PropertyMetadata(new ObservableCollection<StewardModel>())
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
                typeof(MineUI),
                new PropertyMetadata(-1)
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

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Event Handlers

        private void StewardsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MineUIEventArgs newEventArgs =
                new MineUIEventArgs(
                    MineUIRoutedEvent,
                    "Changed",
                    Id,
                    StewardsCollection[SelectedIndex].Id,
                    StewardsCollection[SelectedIndex].PersonName,
                    StewardsCollection[SelectedIndex].Skill
                );

            RaiseEvent(newEventArgs);
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent MineUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "MineUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(MineUI)
            );

        public event RoutedEventHandler MineUIEvent
        {
            add => AddHandler(MineUIRoutedEvent, value);
            remove => RemoveHandler(MineUIRoutedEvent, value);
        }

        #endregion

        private void MineUI_OnLoaded(object sender, RoutedEventArgs e)
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
        }
    }
}
