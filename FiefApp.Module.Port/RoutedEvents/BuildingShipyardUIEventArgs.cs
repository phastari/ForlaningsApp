using System.Windows;

namespace FiefApp.Module.Port.RoutedEvents
{
    public class BuildingShipyardUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly int _stewardId;
        public int StewardId => _stewardId;

        private readonly int _shipyardId;
        public int ShipyardId => _shipyardId;

        public BuildingShipyardUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            int stewardId,
            int shipyardId
            )
            : base(routedEvent)
        {
            _action = action;
            _stewardId = stewardId;
            _shipyardId = shipyardId;
        }
    }
}