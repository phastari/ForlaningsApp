using System.Windows;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Boatbuilding.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Boatbuilding.UIElements.CanBuildShipyardUI
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
