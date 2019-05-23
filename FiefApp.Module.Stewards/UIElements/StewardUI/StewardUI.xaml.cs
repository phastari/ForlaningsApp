using System;
using FiefApp.Common.Infrastructure.Models;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiefApp.Module.Stewards.RoutedEvents;

namespace FiefApp.Module.Stewards.UIElements.StewardUI
{
    /// <summary>
    /// Interaction logic for StewardUI.xaml
    /// </summary>
    public partial class StewardUI : INotifyPropertyChanged
    {
        public StewardUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            CancelEditingButtonCommand = new DelegateCommand(ExecuteCancelEditingButtonCommand);
            SaveEditedButtonCommand = new DelegateCommand(ExecuteSaveEditedButtonCommand);
            DeleteSteward = new DelegateCommand(ExecuteDeleteSteward);
            EditSteward = new DelegateCommand(ExecuteEditSteward);

            CheckStewards();
        }

        #region DelegateCommand : CancelEditingButtonCommand

        public DelegateCommand CancelEditingButtonCommand { get; set; }
        private void ExecuteCancelEditingButtonCommand()
        {
            Age = _oldStewardModel.Age;
            Loyalty = _oldStewardModel.Loyalty;
            Steward = _oldStewardModel.PersonName;
            StewardResources = _oldStewardModel.Resources;
            Skill = _oldStewardModel.Skill;
        }

        #endregion
        #region DelegateCommand : SaveEditedButtonCommand

        public DelegateCommand SaveEditedButtonCommand { get; set; }
        private void ExecuteSaveEditedButtonCommand()
        {
            StewardUIEventArgs newEventArgs =
                new StewardUIEventArgs(
                    StewardUIRoutedEvent,
                    Id,
                    "Save",
                    new StewardModel()
                    {
                        Age = Age,
                        Loyalty = Loyalty,
                        PersonName = Steward,
                        Resources = StewardResources,
                        Skill = Skill
                    }
                );

            RaiseEvent(newEventArgs);
        }

        #endregion
        #region DelegateCommand : DeleteSteward

        public DelegateCommand DeleteSteward { get; set; }
        private void ExecuteDeleteSteward()
        {
            StewardUIEventArgs newEventArgs =
                new StewardUIEventArgs(
                    StewardUIRoutedEvent,
                    Id,
                    "Delete"
                );

            RaiseEvent(newEventArgs);
        }

        #endregion
        #region DelegateCommand : EditSteward

        public DelegateCommand EditSteward { get; set; }
        private void ExecuteEditSteward()
        {
            _oldStewardModel.Age = Age;
            _oldStewardModel.Loyalty = Loyalty;
            _oldStewardModel.PersonName = Steward;
            _oldStewardModel.Resources = StewardResources;
            _oldStewardModel.Skill = Skill;
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
                typeof(StewardUI),
                new PropertyMetadata(-1)
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
                typeof(StewardUI),
                new PropertyMetadata("")
            );

        public int Age
        {
            get => (int)GetValue(AgeProperty);
            set => SetValue(AgeProperty, value);
        }

        public static readonly DependencyProperty AgeProperty =
            DependencyProperty.Register(
                "Age",
                typeof(int),
                typeof(StewardUI),
                new PropertyMetadata(0)
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
                typeof(StewardUI),
                new PropertyMetadata("0")
            );

        public ObservableCollection<StewardIndustryModel> IndustriesCollection
        {
            get => (ObservableCollection<StewardIndustryModel>)GetValue(IndustriesCollectionProperty);
            set => SetValue(IndustriesCollectionProperty, value);
        }

        public static readonly DependencyProperty IndustriesCollectionProperty =
            DependencyProperty.Register(
                "IndustriesCollection",
                typeof(ObservableCollection<StewardIndustryModel>),
                typeof(StewardUI),
                new PropertyMetadata(new ObservableCollection<StewardIndustryModel>())
            );

        public string StewardResources
        {
            get => (string)GetValue(StewardResourcesProperty);
            set => SetValue(StewardResourcesProperty, value);
        }

        public static readonly DependencyProperty StewardResourcesProperty =
            DependencyProperty.Register(
                "StewardResources",
                typeof(string),
                typeof(StewardUI),
                new PropertyMetadata("0")
            );

        public string Loyalty
        {
            get => (string)GetValue(LoyaltyProperty);
            set => SetValue(LoyaltyProperty, value);
        }

        public static readonly DependencyProperty LoyaltyProperty =
            DependencyProperty.Register(
                "Loyalty",
                typeof(string),
                typeof(StewardUI),
                new PropertyMetadata("0")
            );
        public string IndustryType
        {
            get => (string)GetValue(IndustryTypeProperty);
            set => SetValue(IndustryTypeProperty, value);
        }

        public static readonly DependencyProperty IndustryTypeProperty =
            DependencyProperty.Register(
                "IndustryType",
                typeof(string),
                typeof(StewardUI),
                new PropertyMetadata("")
            );

        #endregion

        #region UI Properties

        private StewardModel _oldStewardModel = new StewardModel();

        private int _selectedIndustryIndex;
        public int SelectedIndustryIndex
        {
            get => _selectedIndustryIndex;
            set
            {
                _selectedIndustryIndex = value;
                NotifyPropertyChanged();
            }
        }

        private string _comboBoxText;
        public string ComboBoxText
        {
            get => _comboBoxText;
            set
            {
                _comboBoxText = value;
                NotifyPropertyChanged();
            }
        }

        private bool _editingSteward;
        public bool EditingSteward
        {
            get => _editingSteward;
            set
            {
                _editingSteward = value;
                NotifyPropertyChanged();
            }
        }

        private bool _loaded;

        #endregion

        #region Methods

        private void StewardUI_OnLoaded(
            object sender, 
            RoutedEventArgs e)
        {
            CheckStewards();
            if (Id == 0)
            {
                Self.Visibility = Visibility.Collapsed;
            }
            _loaded = true;
        }

        private void Selector_OnSelectionChanged(
            object sender, 
            SelectionChangedEventArgs e)
        {
            if (_loaded && SelectedIndustryIndex != -1 && IndustriesCollection.Count > 0)
            {
                StewardUIEventArgs newEventArgs =
                    new StewardUIEventArgs(
                        StewardUIRoutedEvent,
                        Id,
                        "Change",
                        new StewardModel()
                        {
                            Id = Id,
                            Age = Age,
                            Loyalty = Loyalty,
                            PersonName = Steward,
                            Resources = StewardResources,
                            Skill = Skill,
                            IndustryId = IndustriesCollection[SelectedIndustryIndex].IndustryId,
                            Industry = IndustriesCollection[SelectedIndustryIndex].Industry,
                            IndustryType = IndustriesCollection[SelectedIndustryIndex].IndustryType,
                            ManorId = IndustriesCollection[SelectedIndustryIndex].FiefId
                        }
                    );

                RaiseEvent(newEventArgs);
            }
        }

        private void CheckStewards()
        {
            if (IndustryType != "")
            {
                for (int x = 0; x < IndustriesCollection.Count; x++)
                {
                    if (IndustriesCollection[x].StewardId == Id)
                    {
                        SelectedIndustryIndex = x;
                        break;
                    }
                }
            }
            else
            {
                SelectedIndustryIndex = 0;
            }
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent StewardUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "StewardUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(StewardUI)
            );

        public event RoutedEventHandler StewardUIEvent
        {
            add => AddHandler(StewardUIRoutedEvent, value);
            remove => RemoveHandler(StewardUIRoutedEvent, value);
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
