using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Trade.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Trade.UIElements.MerchantUI
{
    /// <summary>
    /// Interaction logic for MerchantUI.xaml
    /// </summary>
    public partial class MerchantUI : INotifyPropertyChanged
    {
        public MerchantUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            DeleteMerchant = new DelegateCommand(ExecuteDeleteMerchant);
            EditMerchant = new DelegateCommand(ExecuteEditMerchant);
            EditMerchantCancelCommand = new DelegateCommand(ExecuteEditMerchantCancelCommand);
            EditMerchantSaveCommand = new DelegateCommand(ExecuteEditMerchantSaveCommand);
            SendMerchantCommand = new DelegateCommand(ExecuteSendMerchantCommand);
        }

        #region DelegateCommand : DeleteMerchant

        public DelegateCommand DeleteMerchant { get; set; }
        private void ExecuteDeleteMerchant()
        {
            MerchantUIEventArgs newEventArgs =
                new MerchantUIEventArgs(
                    MerchantUIRoutedEvent,
                    Id,
                    "Delete"
                );

            RaiseEvent(newEventArgs);
        }

        #endregion
        #region DelegateCommand : EditMerchant

        public DelegateCommand EditMerchant { get; set; }
        private void ExecuteEditMerchant()
        {
            MouseArea.Visibility = Visibility.Collapsed;
            OldModel = new MerchantModel()
            {
                PersonName = PersonName,
                Age = Age,
                Skill = Skill,
                Resources = MerchantsResources,
                Loyalty = Loyalty
            };
        }

        #endregion
        #region DelegateCommand : EditMerchantCancelCommand

        public DelegateCommand EditMerchantCancelCommand { get; set; }
        private void ExecuteEditMerchantCancelCommand()
        {
            PersonName = OldModel.PersonName;
            Age = OldModel.Age;
            Skill = OldModel.Skill;
            MerchantsResources = OldModel.Resources;
            Loyalty = OldModel.Loyalty;
            MouseArea.Visibility = Visibility.Visible;
        }

        #endregion
        #region DelegateCommand : EditMerchantSaveCommand

        public DelegateCommand EditMerchantSaveCommand { get; set; }
        private void ExecuteEditMerchantSaveCommand()
        {
            MerchantUIEventArgs newEventArgs =
                new MerchantUIEventArgs(
                    MerchantUIRoutedEvent,
                    Id,
                    "Save",
                    new MerchantModel()
                    {
                        Id = Id,
                        PersonName = PersonName,
                        Age = Age,
                        Skill = Skill,
                        Resources = MerchantsResources,
                        Loyalty = Loyalty
                    }
                );

            RaiseEvent(newEventArgs);
            MouseArea.Visibility = Visibility.Visible;
        }

        #endregion
        #region DelegateCommand : SendMerchantCommand

        public DelegateCommand SendMerchantCommand { get; set; }
        private void ExecuteSendMerchantCommand()
        {
            MerchantUIEventArgs newEventArgs =
                new MerchantUIEventArgs(
                    MerchantUIRoutedEvent,
                    Id,
                    "Send"
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
                typeof(MerchantUI),
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
                typeof(MerchantUI),
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
                typeof(MerchantUI),
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
                typeof(MerchantUI),
                new PropertyMetadata("0")
            );

        public string MerchantsResources
        {
            get => (string)GetValue(MerchantsResourcesProperty);
            set => SetValue(MerchantsResourcesProperty, value);
        }

        public static readonly DependencyProperty MerchantsResourcesProperty =
            DependencyProperty.Register(
                "MerchantsResources",
                typeof(string),
                typeof(MerchantUI),
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
                typeof(MerchantUI),
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
                typeof(MerchantUI),
                new PropertyMetadata(new ObservableCollection<BoatModel>())
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
                typeof(MerchantUI),
                new PropertyMetadata("",SetSendButton)
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

        private MerchantModel _oldModel = new MerchantModel();
        public MerchantModel OldModel
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
                MerchantUIEventArgs newEventArgs =
                    new MerchantUIEventArgs(
                        MerchantUIRoutedEvent,
                        Id,
                        "Change",
                        null,
                        BoatsCollection[SelectedIndex].Id,
                        BoatsCollection[SelectedIndex].BoatType
                    );

                RaiseEvent(newEventArgs);
            }
        }

        private void MerchantUI_OnLoaded(
            object sender, 
            RoutedEventArgs e)
        {
            _loaded = true;
            RaiseSetSendButton();
        }

        private static void SetSendButton(
            DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            if (d is MerchantUI c)
                c.RaiseSetSendButton();
        }

        private void RaiseSetSendButton()
        {
            if (string.IsNullOrEmpty(Assignment))
            {
                SendButton.Visibility = Visibility.Visible;
                SendButton.IsEnabled = true;
            }
            else
            {
                SendButton.Visibility = Visibility.Collapsed;
                SendButton.IsEnabled = false;
            }
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent MerchantUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "MerchantUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(MerchantUI)
            );

        public event RoutedEventHandler MerchantUIEvent
        {
            add => AddHandler(MerchantUIRoutedEvent, value);
            remove => RemoveHandler(MerchantUIRoutedEvent, value);
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
