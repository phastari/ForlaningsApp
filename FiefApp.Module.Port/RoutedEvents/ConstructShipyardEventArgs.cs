using System.Windows;

namespace FiefApp.Module.Port.RoutedEvents
{
    public class ConstructShipyardEventArgs : RoutedEventArgs
    {
        public ConstructShipyardEventArgs(
            RoutedEvent routedEvent
            )
            : base(routedEvent) { }
    }
}
