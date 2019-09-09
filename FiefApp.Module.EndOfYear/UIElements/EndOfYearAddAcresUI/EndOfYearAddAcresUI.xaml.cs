using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FiefApp.Module.EndOfYear.RoutedEvents;

namespace FiefApp.Module.EndOfYear.UIElements.EndOfYearAddAcresUI
{
    /// <summary>
    /// Interaction logic for EndOfYearAddAcresUI.xaml
    /// </summary>
    public partial class EndOfYearAddAcresUI : INotifyPropertyChanged
    {
        public EndOfYearAddAcresUI()
        {
            InitializeComponent();

            RootGrid.DataContext = this;
        }

        #region Dependency Properties

        public int FiefId
        {
            get => (int)GetValue(FiefIdProperty);
            set => SetValue(FiefIdProperty, value);
        }

        public static readonly DependencyProperty FiefIdProperty =
            DependencyProperty.Register(
                "FiefId",
                typeof(int),
                typeof(EndOfYearAddAcresUI),
                new PropertyMetadata(0)
            );

        public int Acres
        {
            get => (int)GetValue(AcresProperty);
            set => SetValue(AcresProperty, value);
        }

        public static readonly DependencyProperty AcresProperty =
            DependencyProperty.Register(
                "Acres",
                typeof(int),
                typeof(EndOfYearAddAcresUI),
                new PropertyMetadata(0)
            );

        #endregion

        #region UI Properties

        private int _pasture;
        public int Pasture
        {
            get => _pasture;
            set
            {
                if (value + Agricultural <= Acres)
                {
                    _pasture = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _agricultural;
        public int Agricultural
        {
            get => _agricultural;
            set
            {
                if (value + Pasture <= Acres)
                {
                    _agricultural = value;
                    if (value + Pasture == Acres)
                    {
                        SendDone();
                    }
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Methods

        private void SendDone()
        {
            EndOfYearAddAcresEventArgs newEventArgsArgs =
                new EndOfYearAddAcresEventArgs(
                    EndOfYearAddAcresRoutedEvent,
                    FiefId,
                    Pasture,
                    Agricultural
                );

            RaiseEvent(newEventArgsArgs);
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent EndOfYearAddAcresRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "EndOfYearAddAcresEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(EndOfYearAddAcresUI)
            );

        public event RoutedEventHandler EndOfYearAddAcresEvent
        {
            add => AddHandler(EndOfYearAddAcresRoutedEvent, value);
            remove => RemoveHandler(EndOfYearAddAcresRoutedEvent, value);
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
