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

        private readonly int _daysWorkThisYear;
        public int DaysWorkThisYear => _daysWorkThisYear;

        public BuildingShipyardUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            int stewardId,
            int shipyardId,
            int daysWorkThisYear = 0
            )
            : base(routedEvent)
        {
            _action = action;
            _stewardId = stewardId;
            _shipyardId = shipyardId;
            _daysWorkThisYear = daysWorkThisYear;
        }
    }
}