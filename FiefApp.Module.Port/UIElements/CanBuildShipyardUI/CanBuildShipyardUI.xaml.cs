using FiefApp.Module.Port.RoutedEvents;
using Prism.Commands;
using System.Windows;

namespace FiefApp.Module.Port.UIElements.CanBuildShipyardUI
{
    /// <summary>
    /// Interaction logic for CanBuildShipyardUI.xaml
    /// </summary>
    public partial class CanBuildShipyardUI
    {
        public CanBuildShipyardUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            ConstructShipyardCommand = new DelegateCommand(ExecuteConstructShipyardCommand);
        }

        public DelegateCommand ConstructShipyardCommand { get; set; }
        private void ExecuteConstructShipyardCommand()
        {
            //CHECK FOR PAYMENT

            ConstructShipyardEventArgs newEventArgs =
                new ConstructShipyardEventArgs(
                    ConstructShipyardRoutedEvent
                );

            RaiseEvent(newEventArgs);
        }

        #region RoutedEvents

        public static readonly RoutedEvent ConstructShipyardRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "   ",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(CanBuildShipyardUI)
            );

        public event RoutedEventHandler ConstructShipyardEvent
        {
            add => AddHandler(ConstructShipyardRoutedEvent, value);
            remove => RemoveHandler(ConstructShipyardRoutedEvent, value);
        }

        #endregion
    }
}
