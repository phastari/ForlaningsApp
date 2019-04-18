using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Mines.RoutedEvents;

namespace FiefApp.Module.Mines.UIElements.QuarryUI
{
    /// <summary>
    /// Interaction logic for QuarryUI.xaml
    /// </summary>
    public partial class QuarryUI
    {
        public QuarryUI()
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
                typeof(QuarryUI),
                new PropertyMetadata(-1)
            );

        public string QuarryType
        {
            get => (string)GetValue(QuarryTypeProperty);
            set => SetValue(QuarryTypeProperty, value);
        }

        public static readonly DependencyProperty QuarryTypeProperty =
            DependencyProperty.Register(
                "QuarryType",
                typeof(string),
                typeof(QuarryUI),
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
                typeof(QuarryUI),
                new PropertyMetadata(-1)
            );

        public int Upkeep
        {
            get => (int)GetValue(UpkeepProperty);
            set => SetValue(UpkeepProperty, value);
        }

        public static readonly DependencyProperty UpkeepProperty =
            DependencyProperty.Register(
                "Upkeep",
                typeof(int),
                typeof(QuarryUI),
                new PropertyMetadata(-1)
            );

        public int DaysWorkNeeded
        {
            get => (int)GetValue(DaysWorkNeededProperty);
            set => SetValue(DaysWorkNeededProperty, value);
        }

        public static readonly DependencyProperty DaysWorkNeededProperty =
            DependencyProperty.Register(
                "DaysWorkNeeded",
                typeof(int),
                typeof(QuarryUI),
                new PropertyMetadata(0)
            );

        public int DaysWorkThisYear
        {
            get => (int)GetValue(DaysWorkThisYearProperty);
            set => SetValue(DaysWorkThisYearProperty, value);
        }

        public static readonly DependencyProperty DaysWorkThisYearProperty =
            DependencyProperty.Register(
                "DaysWorkThisYear",
                typeof(int),
                typeof(QuarryUI),
                new PropertyMetadata(0)
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
                typeof(QuarryUI),
                new PropertyMetadata(new ObservableCollection<StewardModel>())
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
            QuarryUIEventArgs newEventArgs =
                new QuarryUIEventArgs(
                    QuarryUIRoutedEvent,
                    "Changed",
                    Id,
                    StewardsCollection[SelectedIndex].Id,
                    StewardsCollection[SelectedIndex].PersonName
                );

            RaiseEvent(newEventArgs);
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent QuarryUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "QuarryUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(MineUI.MineUI)
            );

        public event RoutedEventHandler QuarryUIEvent
        {
            add => AddHandler(QuarryUIRoutedEvent, value);
            remove => RemoveHandler(QuarryUIRoutedEvent, value);
        }

        #endregion
    }
}
