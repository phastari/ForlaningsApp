using System.Windows;

namespace FiefApp.Module.Port.RoutedEvents
{
    public class ConstructShipyardEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        public ConstructShipyardEventArgs(
            RoutedEvent routedEvent,
            string action
            )
            : base(routedEvent)
        {
            _action = action;
        }
    }
}
