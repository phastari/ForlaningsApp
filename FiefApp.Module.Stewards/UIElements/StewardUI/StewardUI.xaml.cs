using System;
using FiefApp.Common.Infrastructure.Models;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
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

            EditButtonCommand = new DelegateCommand(ExecuteEditButtonCommand);
            CancelEditingButtonCommand = new DelegateCommand(ExecuteCancelEditingButtonCommand);
            SaveEditedButtonCommand = new DelegateCommand(ExecuteSaveEditedButtonCommand);
            RemoveButtonCommand = new DelegateCommand(ExecuteRemoveButtonCommand);
        }

        #region DelegateCommand : EditButtonCommand

        public DelegateCommand EditButtonCommand { get; set; }
        private void ExecuteEditButtonCommand()
        {
            _oldStewardModel = new StewardModel()
            {
                Age = Age,
                Bonus = Bonus,
                Family = Family,
                Loyalty = Loyalty,
                Name = Name,
                Resources = StewardResources,
                Skill = Skill,
                IndustryId = IndustryId,
                Speciality = Speciality
            };
        }

        #endregion
        #region DelegateCommand : CancelEditingButtonCommand

        public DelegateCommand CancelEditingButtonCommand { get; set; }
        private void ExecuteCancelEditingButtonCommand()
        {
            Age = _oldStewardModel.Age;
            Bonus = _oldStewardModel.Bonus;
            Family = _oldStewardModel.Family;
            Loyalty = _oldStewardModel.Loyalty;
            Name = _oldStewardModel.Name;
            StewardResources = _oldStewardModel.Resources;
            Skill = _oldStewardModel.Skill;
            IndustryId = _oldStewardModel.IndustryId;
            Speciality = _oldStewardModel.Speciality;
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
                        Bonus = Bonus,
                        Family = Family,
                        Loyalty = Loyalty,
                        Name = Name,
                        Resources = StewardResources,
                        Skill = Skill,
                        IndustryId = IndustryId,
                        Speciality = Speciality
                    }
                );

            RaiseEvent(newEventArgs);
        }

        #endregion
        #region DelegateCommand : RemoveButtonCommand

        public DelegateCommand RemoveButtonCommand { get; set; }
        private void ExecuteRemoveButtonCommand()
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

        public ObservableCollection<IndustryModel> IndustryCollection
        {
            get => (ObservableCollection<IndustryModel>)GetValue(IndustryCollectionProperty);
            set => SetValue(IndustryCollectionProperty, value);
        }

        public static readonly DependencyProperty IndustryCollectionProperty =
            DependencyProperty.Register(
                "IndustryCollection",
                typeof(ObservableCollection<IndustryModel>),
                typeof(StewardUI),
                new PropertyMetadata(new ObservableCollection<IndustryModel>())
            );

        public string Family
        {
            get => (string)GetValue(FamilyProperty);
            set => SetValue(FamilyProperty, value);
        }

        public static readonly DependencyProperty FamilyProperty =
            DependencyProperty.Register(
                "Family",
                typeof(string),
                typeof(StewardUI),
                new PropertyMetadata("")
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

        public string Speciality
        {
            get => (string)GetValue(SpecialityProperty);
            set => SetValue(SpecialityProperty, value);
        }

        public static readonly DependencyProperty SpecialityProperty =
            DependencyProperty.Register(
                "Speciality",
                typeof(string),
                typeof(StewardUI),
                new PropertyMetadata("0")
            );

        public int Bonus
        {
            get => (int)GetValue(BonusProperty);
            set => SetValue(BonusProperty, value);
        }

        public static readonly DependencyProperty BonusProperty =
            DependencyProperty.Register(
                "Bonus",
                typeof(int),
                typeof(StewardUI),
                new PropertyMetadata(0)
            );

        public bool TreeViewIsExpanded
        {
            get => (bool)GetValue(TreeViewIsExpandedProperty);
            set => SetValue(TreeViewIsExpandedProperty, value);
        }

        public static readonly DependencyProperty TreeViewIsExpandedProperty =
            DependencyProperty.Register(
                "TreeViewIsExpanded",
                typeof(bool),
                typeof(StewardUI),
                new PropertyMetadata(false)
            );

        #endregion

        #region UI Properties

        private StewardModel _oldStewardModel;

        private int _industryId;
        public int IndustryId
        {
            get => _industryId;
            set
            {
                _industryId = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        private void TreeViewItemIsExpandedCommand(object sender, RoutedEventArgs e)
        {
            StewardUIEventArgs newEventArgs =
                new StewardUIEventArgs(
                    StewardUIRoutedEvent,
                    Id,
                    "Expanded"
                );

            RaiseEvent(newEventArgs);
        }
        

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
