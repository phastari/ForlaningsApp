using FiefApp.Common.Infrastructure.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiefApp.Module.Mines.RoutedEvents;
using Prism.Commands;

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

            DeleteMine = new DelegateCommand(ExecuteDeleteMine);
        }

        #region DelegateCommand : DeleteMine

        public DelegateCommand DeleteMine { get; set; }
        private void ExecuteDeleteMine()
        {
            MineUIEventArgs newEventArgs =
                new MineUIEventArgs(
                    MineUIRoutedEvent,
                    "Delete",
                    Id,
                    -1,
                    "",
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

        public int BaseIncome
        {
            get => (int)GetValue(BaseIncomeProperty);
            set => SetValue(BaseIncomeProperty, value);
        }

        public static readonly DependencyProperty BaseIncomeProperty =
            DependencyProperty.Register(
                "BaseIncome",
                typeof(int),
                typeof(MineUI),
                new PropertyMetadata(-1)
            );

        public int Crime
        {
            get => (int)GetValue(CrimeProperty);
            set => SetValue(CrimeProperty, value);
        }

        public static readonly DependencyProperty CrimeProperty =
            DependencyProperty.Register(
                "Crime",
                typeof(int),
                typeof(MineUI),
                new PropertyMetadata(0)
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

        public int AvailableGuards
        {
            get => (int)GetValue(AvailableGuardsProperty);
            set => SetValue(AvailableGuardsProperty, value);
        }

        public static readonly DependencyProperty AvailableGuardsProperty =
            DependencyProperty.Register(
                "AvailableGuards",
                typeof(int),
                typeof(MineUI),
                new PropertyMetadata(0)
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

        public bool IsFirstYear
        {
            get => (bool)GetValue(IsFirstYearProperty);
            set => SetValue(IsFirstYearProperty, value);
        }

        public static readonly DependencyProperty IsFirstYearProperty =
            DependencyProperty.Register(
                "IsFirstYear",
                typeof(bool),
                typeof(MineUI),
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

        private int _currentGuards;
        public int CurrentGuards
        {
            get => _currentGuards;
            set
            {
                _oldAmountGuards = CurrentGuards;
                if (value > -1)
                {
                    _currentGuards = value;
                }
                else
                {
                    _currentGuards = 0;
                }
                UpdateGuards();
                UpdateIncome();
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

        private int _oldAmountGuards;
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

        private void StewardsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedIndex != -1)
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
        }

        private void UpdateIncome()
        {
            if (BaseIncome != 0)
            {
                int last = Income;

                decimal factor = CurrentGuards * (((decimal)127 / (CurrentGuards + 1)) / Crime * Crime);

                if (IsFirstYear)
                {
                    Income = 0;
                }
                else
                {
                    Income = Convert.ToInt32(Math.Floor(factor / 107 * BaseIncome));
                }
            }
            else
            {
                Income = 0;
            }
        }

        private void SendIncomeUpdated(int income)
        {
            MineUIEventArgs newEventArgs =
                new MineUIEventArgs(
                    MineUIRoutedEvent,
                    "IncomeUpdated",
                    Id,
                    -1,
                    "",
                    "",
                    -1,
                    -1,
                    income
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
            CurrentGuards = Guards;
            _sendUpdateEvent = true;
        }

        private void UpdateGuards()
        {
            if (_sendUpdateEvent && CurrentGuards > -1)
            {
                MineUIEventArgs newEventArgs =
                    new MineUIEventArgs(
                        MineUIRoutedEvent,
                        "Guards",
                        Id,
                        -1,
                        "",
                        "",
                        CurrentGuards - _oldAmountGuards,
                        _oldAmountGuards
                    );

                RaiseEvent(newEventArgs);
            }
        }
    }
}
