using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Boatbuilding.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Boatbuilding.UIElements.BoatBuilderUI
{
    /// <summary>
    /// Interaction logic for BoatBuilderUI.xaml
    /// </summary>
    public partial class BoatBuilderUI : INotifyPropertyChanged
    {
        public BoatBuilderUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            DeleteBoatBuilder = new DelegateCommand(ExecuteDeleteBoatBuilder);
            EditBoatBuilder = new DelegateCommand(ExecuteEditBoatBuilder);
            EditBoatBuilderCancelCommand = new DelegateCommand(ExecuteEditBoatBuilderCancelCommand);
            EditBoatBuilderSaveCommand = new DelegateCommand(ExecuteEditBoatBuilderSaveCommand);
        }

        #region Event Handlers

        public DelegateCommand DeleteBoatBuilder { get; set; }
        private void ExecuteDeleteBoatBuilder()
        {
            BoatBuilderUIEventArgs newEventArgs =
                new BoatBuilderUIEventArgs(
                    BoatBuilderUIRoutedEvent,
                    "Delete",
                    new BoatbuilderModel()
                    {
                        Id = Id
                    }
                );

            RaiseEvent(newEventArgs);
        }

        public DelegateCommand EditBoatBuilder { get; set; }
        private void ExecuteEditBoatBuilder()
        {
            MouseArea.Visibility = Visibility.Collapsed;
            OldBoatbuilderModel = new BoatbuilderModel()
            {
                PersonName = BoatBuilder,
                Age = Age,
                Skill = Skill,
                Resources = BBResources,
                Loyalty = Loyalty
            };
        }

        public DelegateCommand EditBoatBuilderCancelCommand { get; set; }
        private void ExecuteEditBoatBuilderCancelCommand()
        {
            BoatBuilder = OldBoatbuilderModel.PersonName;
            Age = OldBoatbuilderModel.Age;
            Skill = OldBoatbuilderModel.Skill;
            BBResources = OldBoatbuilderModel.Resources;
            Loyalty = OldBoatbuilderModel.Loyalty;
            MouseArea.Visibility = Visibility.Visible;
        }

        public DelegateCommand EditBoatBuilderSaveCommand { get; set; }
        private void ExecuteEditBoatBuilderSaveCommand()
        {
            BoatBuilderUIEventArgs newEventArgs =
                new BoatBuilderUIEventArgs(
                    BoatBuilderUIRoutedEvent,
                    "Save",
                    new BoatbuilderModel()
                    {
                        Id = Id,
                        PersonName = BoatBuilder,
                        Age = Age,
                        Skill = Skill,
                        Resources = BBResources,
                        Loyalty = Loyalty
                    }
                );

            RaiseEvent(newEventArgs);
            MouseArea.Visibility = Visibility.Visible;
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
                typeof(BoatBuilderUI),
                new PropertyMetadata(-1)
            );

        public string BoatBuilder
        {
            get => (string)GetValue(BoatBuilderProperty);
            set => SetValue(BoatBuilderProperty, value);
        }

        public static readonly DependencyProperty BoatBuilderProperty =
            DependencyProperty.Register(
                "BoatBuilder",
                typeof(string),
                typeof(BoatBuilderUI),
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
                typeof(BoatBuilderUI),
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
                typeof(BoatBuilderUI),
                new PropertyMetadata("")
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
                typeof(BoatBuilderUI),
                new PropertyMetadata("")
            );

        public string BBResources
        {
            get => (string)GetValue(BBResourcesProperty);
            set => SetValue(BBResourcesProperty, value);
        }

        public static readonly DependencyProperty BBResourcesProperty =
            DependencyProperty.Register(
                "BBResources",
                typeof(string),
                typeof(BoatBuilderUI),
                new PropertyMetadata("")
            );

        public string Assignment
        {
            get => (string)GetValue(AssignmentProperty);
            set => SetValue(AssignmentProperty, value);
        }

        public static readonly DependencyProperty AssignmentProperty =
            DependencyProperty.Register(
                "Assignment",
                typeof(string),
                typeof(BoatBuilderUI),
                new PropertyMetadata("")
            );

        #endregion

        #region UI Properties

        private BoatbuilderModel _oldBoatbuilderModel;
        public BoatbuilderModel OldBoatbuilderModel
        {
            get => _oldBoatbuilderModel;
            set
            {
                _oldBoatbuilderModel = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent BoatBuilderUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "BoatBuilderUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(BoatBuilderUI)
            );

        public event RoutedEventHandler BoatBuilderUIEvent
        {
            add => AddHandler(BoatBuilderUIRoutedEvent, value);
            remove => RemoveHandler(BoatBuilderUIRoutedEvent, value);
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
