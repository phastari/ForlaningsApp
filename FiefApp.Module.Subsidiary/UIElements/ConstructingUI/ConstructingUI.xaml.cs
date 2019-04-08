using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Subsidiary.RoutedEvents;

namespace FiefApp.Module.Subsidiary.UIElements.ConstructingUI
{
    /// <summary>
    /// Interaction logic for ConstructingUI.xaml
    /// </summary>
    public partial class ConstructingUI : INotifyPropertyChanged
    {
        public ConstructingUI()
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
                typeof(ConstructingUI),
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
                typeof(ConstructingUI),
                new PropertyMetadata("")
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
                typeof(ConstructingUI),
                new PropertyMetadata(-1)
            );

        public int DaysWorkThisYear
        {
            get => (int)GetValue(DaysWorkThisYearProperty);
            set
            {
                if (value > -1 && value <= DaysWorkLeft)
                {
                    SetValue(DaysWorkThisYearProperty, value);
                    RaiseUpdateEvent();
                }
                else
                {
                    if (value < 0)
                    {
                        SetValue(DaysWorkThisYearProperty, 0);
                    }
                    if (value > DaysWorkLeft)
                    {
                        SetValue(DaysWorkThisYearProperty, DaysWorkLeft);
                    }
                }
            }
        }

        public static readonly DependencyProperty DaysWorkThisYearProperty =
            DependencyProperty.Register(
                "DaysWorkThisYear",
                typeof(int),
                typeof(ConstructingUI),
                new PropertyMetadata(-1)
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
                typeof(ConstructingUI),
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
                typeof(ConstructingUI),
                new PropertyMetadata(-1)
            );

        #endregion

        #region UI Properties

        private int _stewardIndex = -1;
        public int StewardIndex
        {
            get => _stewardIndex;
            set
            {
                _stewardIndex = value;
                NotifyPropertyChanged();
            }
        }

        private bool _loaded = false;

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void RaiseUpdateEvent()
        {
            ConstructingUIEventArgs newEventArgs =
                new ConstructingUIEventArgs(
                    ConstructingUIRoutedEvent,
                    "DaysWorkThisYearChanged",
                    Id,
                    Subsidiary,
                    StewardsCollection[StewardIndex].Id,
                    StewardsCollection[StewardIndex].PersonName,
                    DaysWorkThisYear
                );

            RaiseEvent(newEventArgs);
        }

        private void ConstructingUI_OnLoaded(object sender, RoutedEventArgs e)
        {
            int index = -1;
            for (int x = 0; x < StewardsCollection.Count; x++)
            {
                if (StewardsCollection[x].Id == StewardId)
                {
                    index = x;
                }
            }

            StewardIndex = index;
            _loaded = true;
        }
        private void StewardComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_loaded && StewardIndex != -1)
            {
                ConstructingUIEventArgs newEventArgs =
                    new ConstructingUIEventArgs(
                        ConstructingUIRoutedEvent,
                        "Changed",
                        Id,
                        Subsidiary,
                        StewardsCollection[StewardIndex].Id,
                        StewardsCollection[StewardIndex].PersonName
                    );

                RaiseEvent(newEventArgs);
            }
        }

        #region RoutedEvents

        public static readonly RoutedEvent ConstructingUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "ConstructingUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(ConstructingUI)
            );

        public event RoutedEventHandler ConstructingUIEvent
        {
            add => AddHandler(ConstructingUIRoutedEvent, value);
            remove => RemoveHandler(ConstructingUIRoutedEvent, value);
        }

        #endregion
    }
}
