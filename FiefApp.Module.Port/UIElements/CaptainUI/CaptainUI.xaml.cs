using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Port.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Port.UIElements.CaptainUI
{
    /// <summary>
    /// Interaction logic for CaptainUI.xaml
    /// </summary>
    public partial class CaptainUI : INotifyPropertyChanged
    {
        public CaptainUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            DeleteCaptain = new DelegateCommand(ExecuteDeleteCaptain);
            EditCaptain = new DelegateCommand(ExecuteEditCaptain);
            EditCaptainCancelCommand = new DelegateCommand(ExecuteEditCaptainCancelCommand);
            EditCaptainSaveCommand = new DelegateCommand(ExecuteEditCaptainSaveCommand);
        }

        #region DelegateCommand : DeleteCaptain

        public DelegateCommand DeleteCaptain { get; set; }
        private void ExecuteDeleteCaptain()
        {
            CaptainUIEventArgs newEventArgs =
                new CaptainUIEventArgs(
                    CaptainUIRoutedEvent,
                    Id,
                    "Delete"
                );

            RaiseEvent(newEventArgs);
        }

        #endregion
        #region DelegateCommand : EditCaptain

        public DelegateCommand EditCaptain { get; set; }
        private void ExecuteEditCaptain()
        {
            MouseArea.Visibility = Visibility.Collapsed;
            OldModel = new CaptainModel()
            {
                PersonName = PersonName,
                Age = Age,
                Skill = Skill,
                Resources = CaptainsResources,
                Loyalty = Loyalty
            };
        }

        #endregion
        #region DelegateCommand : EditCaptainCancelCommand

        public DelegateCommand EditCaptainCancelCommand { get; set; }
        private void ExecuteEditCaptainCancelCommand()
        {
            PersonName = OldModel.PersonName;
            Age = OldModel.Age;
            Skill = OldModel.Skill;
            CaptainsResources = OldModel.Resources;
            Loyalty = OldModel.Loyalty;
            MouseArea.Visibility = Visibility.Visible;
        }

        #endregion
        #region DelegateCommand : EditCaptainSaveCommand

        public DelegateCommand EditCaptainSaveCommand { get; set; }
        private void ExecuteEditCaptainSaveCommand()
        {
            CaptainUIEventArgs newEventArgs =
                new CaptainUIEventArgs(
                    CaptainUIRoutedEvent,
                    Id,
                    "Save",
                    new CaptainModel()
                    {
                        Id = Id,
                        PersonName = PersonName,
                        Age = Age,
                        Skill = Skill,
                        Resources = CaptainsResources,
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
                typeof(CaptainUI),
                new PropertyMetadata(-1)
            );

        public string PersonName
        {
            get => (string)GetValue(PersonNameProperty);
            set => SetValue(PersonNameProperty, value);
        }

        public static readonly DependencyProperty PersonNameProperty =
            DependencyProperty.Register(
                "PersonName",
                typeof(string),
                typeof(CaptainUI),
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
                typeof(CaptainUI),
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
                typeof(CaptainUI),
                new PropertyMetadata("0")
            );

        public string CaptainsResources
        {
            get => (string)GetValue(CaptainsResourcesProperty);
            set => SetValue(CaptainsResourcesProperty, value);
        }

        public static readonly DependencyProperty CaptainsResourcesProperty =
            DependencyProperty.Register(
                "CaptainsResources",
                typeof(string),
                typeof(CaptainUI),
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
                typeof(CaptainUI),
                new PropertyMetadata("0")
            );

        public ObservableCollection<BoatModel> BoatsCollection
        {
            get => (ObservableCollection<BoatModel>)GetValue(BoatsCollectionProperty);
            set => SetValue(BoatsCollectionProperty, value);
        }

        public static readonly DependencyProperty BoatsCollectionProperty =
            DependencyProperty.Register(
                "BoatsCollection",
                typeof(ObservableCollection<BoatModel>),
                typeof(CaptainUI),
                new PropertyMetadata(new ObservableCollection<BoatModel>())
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

        private CaptainModel _oldModel = new CaptainModel();
        public CaptainModel OldModel
        {
            get => _oldModel;
            set
            {
                _oldModel = value;
                NotifyPropertyChanged();
            }
        }

        private bool _loaded = false;

        #endregion

        #region Event Handlers

        private void BoatsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_loaded)
            {
                CaptainUIEventArgs newEventArgs =
                    new CaptainUIEventArgs(
                        CaptainUIRoutedEvent,
                        Id,
                        "Change",
                        null,
                        BoatsCollection[SelectedIndex].Id,
                        BoatsCollection[SelectedIndex].BoatType
                    );

                RaiseEvent(newEventArgs);
            }
        }

        private void CaptainUI_OnLoaded(object sender, RoutedEventArgs e)
        {
            _loaded = true;
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent CaptainUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "CaptainUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(CaptainUI)
            );

        public event RoutedEventHandler CaptainUIEvent
        {
            add => AddHandler(CaptainUIRoutedEvent, value);
            remove => RemoveHandler(CaptainUIRoutedEvent, value);
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
