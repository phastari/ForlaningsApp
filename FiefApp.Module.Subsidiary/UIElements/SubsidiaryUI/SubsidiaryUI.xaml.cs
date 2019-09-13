using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Subsidiary.RoutedEvents;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace FiefApp.Module.Subsidiary.UIElements.SubsidiaryUI
{
    /// <summary>
    /// Interaction logic for SubsidiaryUI.xaml
    /// </summary>
    public partial class SubsidiaryUI : INotifyPropertyChanged
    {
        public SubsidiaryUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            DeleteSubsidiary = new DelegateCommand(ExecuteDeleteSubsidiary);
            EditSubsidiary = new DelegateCommand(ExecuteEditSubsidiary);
        }

        #region DelegateCommands

        public DelegateCommand DeleteSubsidiary { get; set; }
        private void ExecuteDeleteSubsidiary()
        {
            SubsidiaryUIEventArgs newEventArgs =
                new SubsidiaryUIEventArgs(
                    SubsidiaryUIRoutedEvent,
                    "Delete",
                    -1,
                    "",
                    Id,
                    Skill
                );
            RaiseEvent(newEventArgs);
        }

        public DelegateCommand EditSubsidiary { get; set; }
        private void ExecuteEditSubsidiary()
        {
            _oldValues.Id = Id;
            _oldValues.Subsidiary = Subsidiary;
            _oldValues.Quality = Quality;
            _oldValues.DevelopmentLevel = DevelopmentLevel;
            _oldValues.Silver = Silver;
            _oldValues.Base = Base;
            _oldValues.Luxury = Luxury;
            _oldValues.DaysWorkUpkeep = DaysWorkLeft;
            _oldValues.DaysWorkThisYear = DaysWorkThisYear;
            _oldValues.Steward = Steward;
            _oldValues.StewardId = StewardId;
            _oldValues.Skill = Skill;

            SubsidiaryUIEventArgs newEventArgs =
                new SubsidiaryUIEventArgs(
                    SubsidiaryUIRoutedEvent,
                    "Edit",
                    -1,
                    "",
                    Id,
                    Skill
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
                typeof(SubsidiaryUI),
                new PropertyMetadata(-1)
            );

        public string Subsidiary
        {
            get => (string)GetValue(SubsidiaryProperty);
            set => SetValue(SubsidiaryProperty, value);
        }

        public static readonly DependencyProperty SubsidiaryProperty =
            DependencyProperty.Register(
                "Subsidiary",
                typeof(string),
                typeof(SubsidiaryUI),
                new PropertyMetadata("")
            );

        public int Quality
        {
            get => (int)GetValue(QualityProperty);
            set => SetValue(QualityProperty, value);
        }

        public static readonly DependencyProperty QualityProperty =
            DependencyProperty.Register(
                "Quality",
                typeof(int),
                typeof(SubsidiaryUI),
                new PropertyMetadata(0, RaiseCalculateIncomes)
            );
        public int DevelopmentLevel
        {
            get => (int)GetValue(DevelopmentLevelProperty);
            set => SetValue(DevelopmentLevelProperty, value);
        }

        public static readonly DependencyProperty DevelopmentLevelProperty =
            DependencyProperty.Register(
                "DevelopmentLevel",
                typeof(int),
                typeof(SubsidiaryUI),
                new PropertyMetadata(1, RaiseCalculateIncomes)
            );

        public decimal IncomeFactor
        {
            get => (decimal)GetValue(IncomeFactorProperty);
            set => SetValue(IncomeFactorProperty, value);
        }

        public static readonly DependencyProperty IncomeFactorProperty =
            DependencyProperty.Register(
                "IncomeFactor",
                typeof(decimal),
                typeof(SubsidiaryUI),
                new PropertyMetadata(0M)
            );

        public decimal IncomeSilver
        {
            get => (decimal)GetValue(IncomeSilverProperty);
            set => SetValue(IncomeSilverProperty, value);
        }

        public static readonly DependencyProperty IncomeSilverProperty =
            DependencyProperty.Register(
                "IncomeSilver",
                typeof(decimal),
                typeof(SubsidiaryUI),
                new PropertyMetadata(0M)
            );

        public decimal IncomeBase
        {
            get => (decimal)GetValue(IncomeBaseProperty);
            set => SetValue(IncomeBaseProperty, value);
        }

        public static readonly DependencyProperty IncomeBaseProperty =
            DependencyProperty.Register(
                "IncomeBase",
                typeof(decimal),
                typeof(SubsidiaryUI),
                new PropertyMetadata(0M)
            );

        public decimal IncomeLuxury
        {
            get => (decimal)GetValue(IncomeLuxuryProperty);
            set => SetValue(IncomeLuxuryProperty, value);
        }

        public static readonly DependencyProperty IncomeLuxuryProperty =
            DependencyProperty.Register(
                "IncomeLuxury",
                typeof(decimal),
                typeof(SubsidiaryUI),
                new PropertyMetadata(0M)
            );

        public int DaysWorkLeft
        {
            get => (int)GetValue(DaysWorkLeftProperty);
            set => SetValue(DaysWorkLeftProperty, value);
        }

        public static readonly DependencyProperty DaysWorkLeftProperty =
            DependencyProperty.Register(
                "DaysWorkLeft",
                typeof(int),
                typeof(SubsidiaryUI),
                new PropertyMetadata(-1)
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
                typeof(SubsidiaryUI),
                new PropertyMetadata(0, RaiseCalculateIncomes)
            );

        public string Steward
        {
            get => (string)GetValue(StewardProperty);
            set => SetValue(StewardProperty, value);
        }

        public static readonly DependencyProperty StewardProperty =
            DependencyProperty.Register(
                "Steward",
                typeof(string),
                typeof(SubsidiaryUI),
                new PropertyMetadata("")
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
                typeof(SubsidiaryUI),
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
                typeof(SubsidiaryUI),
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
                typeof(SubsidiaryUI),
                new PropertyMetadata(-1)
            );

        public string Skill
        {
            get => (string)GetValue(SkillProperty);
            set => SetValue(SkillProperty, value);
        }

        public static readonly DependencyProperty SkillProperty =
            DependencyProperty.Register(
                "Skill",
                typeof(string),
                typeof(SubsidiaryUI),
                new PropertyMetadata("")
            );

        #endregion

        #region UI Properties

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

        private SubsidiaryModel _oldValues = new SubsidiaryModel();

        private int _silver;
        public int Silver
        {
            get => _silver;
            set
            {
                _silver = value;
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

        #endregion

        #region Methods

        private static void RaiseCalculateIncomes(
            DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            if (d is SubsidiaryUI c)
                c.CalculateIncomes();
        }

        private void CalculateIncomes()
        {
            if (SelectedIndex > 0)
            {
                Silver = Convert.ToInt32(Math.Floor(
                    (Quality * IncomeFactor * IncomeSilver * 360
                     + Quality * IncomeFactor * IncomeSilver * 360 / 100 * (DevelopmentLevel - 1) * 5)
                    * ((decimal)DaysWorkThisYear / DaysWorkLeft)));

                Base = Convert.ToInt32(Math.Floor(
                    (Quality * IncomeFactor * IncomeBase
                     + Quality * IncomeFactor * IncomeBase / 100 * (DevelopmentLevel - 1) * 5)
                    * ((decimal)DaysWorkThisYear / DaysWorkLeft)));

                Luxury = Convert.ToInt32(Math.Floor(
                    (Quality * IncomeFactor * IncomeLuxury
                     + Quality * IncomeFactor * IncomeLuxury / 100 * (DevelopmentLevel - 1) * 5)
                    * ((decimal)DaysWorkThisYear / DaysWorkLeft)));
            }
            else
            {
                Silver = 0;
                Base = 0;
                Luxury = 0;
            }

            UpdateSubsidiary();
        }

        private void UpdateSubsidiary()
        {
            SubsidiaryUIEventArgs newEventArgs =
                new SubsidiaryUIEventArgs(
                    SubsidiaryUIRoutedEvent,
                    "Update",
                    -1,
                    "",
                    Id,
                    Skill,
                    "",
                    new SubsidiaryModel()
                    {
                        Silver = Silver,
                        Base = Base,
                        Luxury = Luxury,
                        DaysWorkThisYear = DaysWorkThisYear
                    }
                );
            RaiseEvent(newEventArgs);
        }

        #endregion

        #region Event Handler For Selection Changed

        private void StewardsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedIndex != -1)
            {
                SubsidiaryUIEventArgs newEventArgs =
                    new SubsidiaryUIEventArgs(
                        SubsidiaryUIRoutedEvent,
                        "Changed",
                        StewardsCollection[SelectedIndex].Id,
                        StewardsCollection[SelectedIndex].PersonName,
                        Id,
                        StewardsCollection[SelectedIndex].Skill,
                        Subsidiary
                    );

                RaiseEvent(newEventArgs);

                CalculateIncomes();
            }
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent SubsidiaryUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "SubsidiaryUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(SubsidiaryUI)
            );

        public event RoutedEventHandler SubsidiaryUIEvent
        {
            add => AddHandler(SubsidiaryUIRoutedEvent, value);
            remove => RemoveHandler(SubsidiaryUIRoutedEvent, value);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void SubsidiaryUI_OnLoaded(object sender, RoutedEventArgs e)
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
