using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Mines.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Mines.UIElements.QuarryUI
{
    /// <summary>
    /// Interaction logic for QuarryUI.xaml
    /// </summary>
    public partial class QuarryUI : INotifyPropertyChanged
    {
        public QuarryUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            DeleteQuarry = new DelegateCommand(ExecuteDeleteQuarry);
        }

        #region DelegateCommand : DeleteQuarry

        public DelegateCommand DeleteQuarry { get; set; }
        private void ExecuteDeleteQuarry()
        {
            QuarryUIEventArgs newEventArgs =
                new QuarryUIEventArgs(
                    QuarryUIRoutedEvent,
                    "Delete",
                    Id,
                    -1,
                    ""
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

        public int BaseIncome
        {
            get => (int)GetValue(BaseIncomeProperty);
            set => SetValue(BaseIncomeProperty, value);
        }

        public static readonly DependencyProperty BaseIncomeProperty =
            DependencyProperty.Register(
                "BaseIncome",
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
                new PropertyMetadata(0, RaiseDaysWorkChanged)
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

        public int StewardId
        {
            get => (int)GetValue(StewardIdProperty);
            set => SetValue(StewardIdProperty, value);
        }

        public static readonly DependencyProperty StewardIdProperty =
            DependencyProperty.Register(
                "StewardId",
                typeof(int),
                typeof(QuarryUI),
                new PropertyMetadata(-1)
            );

        public bool IsFirstYear
        {
            get => (bool)GetValue(IsFirstYearIdProperty);
            set => SetValue(IsFirstYearIdProperty, value);
        }

        public static readonly DependencyProperty IsFirstYearIdProperty =
            DependencyProperty.Register(
                "IsFirstYear",
                typeof(bool),
                typeof(QuarryUI),
                new PropertyMetadata(false)
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

        private int _income;
        public int Income
        {
            get => _income;
            set
            {
                if (_income != value)
                {
                    SendIncomeUpdated(value);
                }
                _income = value;
                NotifyPropertyChanged();
            }
        }

        private bool _sendUpdateEvent = false;

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Event Handlers

        private void QuarryUI_OnLoaded(object sender, RoutedEventArgs e)
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
            _sendUpdateEvent = true;
        }

        private static void RaiseDaysWorkChanged(
            DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            if (d is QuarryUI c)
            {
                c.UpdateIncome();
                c.SendDaysWorkUpdated();
            }
        }

        private void UpdateIncome()
        {
            if (BaseIncome != 0)
            {
                if (IsFirstYear)
                {
                    Income = Convert.ToInt32(Math.Floor(BaseIncome * (decimal)DaysWorkThisYear / DaysWorkNeeded) / 4);
                }
                else
                {
                    Income = Convert.ToInt32(Math.Floor(BaseIncome * (decimal)DaysWorkThisYear / DaysWorkNeeded));
                }
            }
        }

        private void SendDaysWorkUpdated()
        {
            QuarryUIEventArgs newEventArgs =
                new QuarryUIEventArgs(
                    QuarryUIRoutedEvent,
                    "DaysWork",
                    Id,
                    -1,
                    "",
                    "",
                    DaysWorkThisYear
                );

            RaiseEvent(newEventArgs);
        }

        private void SendIncomeUpdated(int income)
        {
            QuarryUIEventArgs newEventArgs =
                new QuarryUIEventArgs(
                    QuarryUIRoutedEvent,
                    "IncomeUpdated",
                    Id,
                    -1,
                    "",
                    "",
                    -1,
                    income
                );

            RaiseEvent(newEventArgs);
        }

        private void StewardsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_sendUpdateEvent)
            {
                QuarryUIEventArgs newEventArgs =
                    new QuarryUIEventArgs(
                        QuarryUIRoutedEvent,
                        "Changed",
                        Id,
                        StewardsCollection[SelectedIndex].Id,
                        StewardsCollection[SelectedIndex].PersonName,
                        StewardsCollection[SelectedIndex].Skill
                    );

                RaiseEvent(newEventArgs);
            }
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
