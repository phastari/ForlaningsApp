using System.Windows;
using FiefApp.Common.Infrastructure.Controls.iTextBox.RoutedEvents;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Controls.iTextBox
{
    /// <summary>
    /// Interaction logic for iTextBox.xaml
    /// </summary>
    public partial class iTextBox
    {
        public iTextBox()
        {
            InitializeComponent();
            RootGrid.DataContext = this;
        }

        #region Event Handlers

        private void IncreaseValue(
            object sender,
            RoutedEventArgs e
            )
        {
            Value++;
            if (BoundToResident)
            {
                BoundToResidentEventArgs newEventArgs =
                    new BoundToResidentEventArgs(
                        BoundToResidentRoutedEvent,
                        "Increase",
                        new SoldierModel()
                        {
                            Name = "",
                            Age = 0,
                            Type = "Soldier",
                            Position = ResidentType
                        }
                    );

                RaiseEvent(newEventArgs);
            }
        }

        private void DecreaseValue(
            object sender,
            RoutedEventArgs e
            )
        {
            Value--;
            if (BoundToResident)
            {
                BoundToResidentEventArgs newEventArgs =
                    new BoundToResidentEventArgs(
                        BoundToResidentRoutedEvent,
                        "Decrease",
                        new SoldierModel()
                        {
                            Position = ResidentType
                        }
                    );

                RaiseEvent(newEventArgs);
            }
        }

        #endregion

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(int),
                typeof(iTextBox),
                new PropertyMetadata(0)
            );

        public string BGColor
        {
            get => (string)GetValue(BGColorProperty);
            set => SetValue(BGColorProperty, value);
        }

        public static readonly DependencyProperty BGColorProperty =
            DependencyProperty.Register(
                "BGColor",
                typeof(string),
                typeof(iTextBox),
                new PropertyMetadata("AntiqueWhite")
            );

        public bool TextBoxReadOnly
        {
            get => (bool)GetValue(TextBoxReadOnlyProperty);
            set => SetValue(TextBoxReadOnlyProperty, value);
        }

        public static readonly DependencyProperty TextBoxReadOnlyProperty =
            DependencyProperty.Register(
                "TextBoxReadOnly",
                typeof(bool),
                typeof(iTextBox),
                new PropertyMetadata(false)
            );

        public bool BoundToResident
        {
            get => (bool)GetValue(BoundToResidentProperty);
            set => SetValue(BoundToResidentProperty, value);
        }

        public static readonly DependencyProperty BoundToResidentProperty =
            DependencyProperty.Register(
                "BoundToResident",
                typeof(bool),
                typeof(iTextBox),
                new PropertyMetadata(false)
            );

        public string ResidentType
        {
            get => (string)GetValue(ResidentTypeProperty);
            set => SetValue(ResidentTypeProperty, value);
        }

        public static readonly DependencyProperty ResidentTypeProperty =
            DependencyProperty.Register(
                "ResidentType",
                typeof(string),
                typeof(iTextBox),
                new PropertyMetadata("")
            );

        #region RoutedEvents

        public static readonly RoutedEvent BoundToResidentRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "BoundToResidentEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(iTextBox)
            );

        public event RoutedEventHandler BoundToResidentEvent
        {
            add => AddHandler(BoundToResidentRoutedEvent, value);
            remove => RemoveHandler(BoundToResidentRoutedEvent, value);
        }

        #endregion
    }
}
