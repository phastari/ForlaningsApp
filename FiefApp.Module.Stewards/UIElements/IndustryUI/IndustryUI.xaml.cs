using FiefApp.Module.Stewards.RoutedEvents;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FiefApp.Module.Stewards.UIElements.IndustryUI
{
    /// <summary>
    /// Interaction logic for IndustryUI.xaml
    /// </summary>
    public partial class IndustryUI : UserControl
    {
        public IndustryUI()
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
                typeof(IndustryUI),
                new PropertyMetadata(-1)
            );

        public string Industry
        {
            get => (string)GetValue(IndustryProperty);
            set => SetValue(IndustryProperty, value);
        }

        public static readonly DependencyProperty IndustryProperty =
            DependencyProperty.Register(
                "Industry",
                typeof(string),
                typeof(IndustryUI),
                new PropertyMetadata("")
            );

        public bool BeingDeveloped
        {
            get => (bool)GetValue(BeingDevelopedProperty);
            set => SetValue(BeingDevelopedProperty, value);
        }

        public static readonly DependencyProperty BeingDevelopedProperty =
            DependencyProperty.Register(
                "BeingDeveloped",
                typeof(bool),
                typeof(IndustryUI),
                new PropertyMetadata(false, BeingDevelopedChanged)
            );

        public bool CanBeDeveloped
        {
            get => (bool)GetValue(CanBeDevelopedProperty);
            set => SetValue(CanBeDevelopedProperty, value);
        }

        public static readonly DependencyProperty CanBeDevelopedProperty =
            DependencyProperty.Register(
                "CanBeDeveloped",
                typeof(bool),
                typeof(IndustryUI),
                new PropertyMetadata(false)
            );

        #endregion

        private void Self_Loaded(object sender, RoutedEventArgs e)
        {
            if (!CanBeDeveloped)
            {
                Self.Visibility = Visibility.Collapsed;
            }
        }

        private static void BeingDevelopedChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is IndustryUI c)
                c.BeingDevelopedPropertyChanged();
        }

        private void BeingDevelopedPropertyChanged()
        {
            IndustryUIEventArgs newEventArgs =
                new IndustryUIEventArgs(
                    IndustryUIRoutedEvent,
                    Id,
                    "Changed",
                    BeingDeveloped
                );
            RaiseEvent(newEventArgs);
        }

        #region RoutedEvents

        public static readonly RoutedEvent IndustryUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "IndustryUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(IndustryUI)
            );

        public event RoutedEventHandler IndustryUIEvent
        {
            add => AddHandler(IndustryUIRoutedEvent, value);
            remove => RemoveHandler(IndustryUIRoutedEvent, value);
        }

        #endregion
    }
}
