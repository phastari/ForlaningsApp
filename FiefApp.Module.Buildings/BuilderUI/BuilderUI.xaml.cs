using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Buildings.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Buildings.BuilderUI
{
    /// <summary>
    /// Interaction logic for BuilderUI.xaml
    /// </summary>
    public partial class BuilderUI
    {
        public BuilderUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            DeleteBuilder = new DelegateCommand(ExecuteDeleteBuilder);
            EditBuilder = new DelegateCommand(ExecuteEditBuilder);
            EditBuilderCancelCommand = new DelegateCommand(ExecuteEditBuilderCancelCommand);
            EditBuilderSaveCommand = new DelegateCommand(ExecuteEditBuilderSaveCommand);
        }

        #region DelegateCommand : DeleteBuilder

        public DelegateCommand DeleteBuilder { get; set; }
        private void ExecuteDeleteBuilder()
        {
            BuilderUIEventArgs newEventArgs =
                new BuilderUIEventArgs(
                    BuilderUIRoutedEvent,
                    "Delete",
                    new BuilderModel()
                    {
                        Id = Id
                    }
                );
            RaiseEvent(newEventArgs);
        }

        #endregion
        #region DelegateCommand : EditBuilder

        public DelegateCommand EditBuilder { get; set; }
        private void ExecuteEditBuilder()
        {
            _oldModel.PersonName = Builder;
            _oldModel.Age = Age;
            _oldModel.Skill = Skill;
            _oldModel.Resources = BBResources;
            _oldModel.Loyalty = Loyalty;

            EditableCheckBox.IsChecked = true;
        }

        #endregion
        #region DelegateCommand : EditBuilderCancelCommand

        public DelegateCommand EditBuilderCancelCommand { get; set; }
        private void ExecuteEditBuilderCancelCommand()
        {
            Builder = _oldModel.PersonName;
            Age = _oldModel.Age;
            Skill = _oldModel.Skill;
            BBResources = _oldModel.Resources;
            Loyalty = _oldModel.Loyalty;

            EditableCheckBox.IsChecked = false;
        }

        #endregion
        #region DelegateCommand : EditBuilderSaveCommand

        public DelegateCommand EditBuilderSaveCommand { get; set; }
        private void ExecuteEditBuilderSaveCommand()
        {
            BuilderUIEventArgs newEventArgs =
                new BuilderUIEventArgs(
                    BuilderUIRoutedEvent,
                    "Save",
                    new BuilderModel()
                    {
                        Id = Id,
                        PersonName = Builder,
                        Age = Age,
                        Skill = Skill,
                        Resources = BBResources,
                        Loyalty = Loyalty
                    }
                );
            RaiseEvent(newEventArgs);

            EditableCheckBox.IsChecked = false;
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
                typeof(BuilderUI),
                new PropertyMetadata(-1)
            );

        public string Builder
        {
            get => (string)GetValue(BuilderProperty);
            set => SetValue(BuilderProperty, value);
        }

        public static readonly DependencyProperty BuilderProperty =
            DependencyProperty.Register(
                "Builder",
                typeof(string),
                typeof(BuilderUI),
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
                typeof(BuilderUI),
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
                typeof(BuilderUI),
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
                typeof(BuilderUI),
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
                typeof(BuilderUI),
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
                typeof(BuilderUI),
                new PropertyMetadata("")
            );

        #endregion

        #region UI Properties

        private bool _editable = false;
        public bool Editable
        {
            get => _editable;
            set
            {
                _editable = value;
                NotifyPropertyChanged();
            }
        }

        private BuilderModel _oldModel = new BuilderModel();

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent BuilderUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "BuilderUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(BuilderUI)
            );

        public event RoutedEventHandler BuilderUIEvent
        {
            add => AddHandler(BuilderUIRoutedEvent, value);
            remove => RemoveHandler(BuilderUIRoutedEvent, value);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Id == 0)
            {
                Self.Visibility = Visibility.Collapsed;
            }
        }
    }
}
