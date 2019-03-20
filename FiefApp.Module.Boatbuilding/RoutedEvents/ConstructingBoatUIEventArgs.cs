using System.Windows;

namespace FiefApp.Module.Boatbuilding.RoutedEvents
{
    public class ConstructingBoatUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly int _constructingBoatId;
        public int ConstructingBoatId => _constructingBoatId;

        private readonly int _boatbuilderId;
        public int BoatbuilderId => _boatbuilderId;

        public ConstructingBoatUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            int constructingBoatId,
            int boatbuilderId
            )
            : base(routedEvent)
        {
            _action = action;
            _constructingBoatId = constructingBoatId;
            _boatbuilderId = boatbuilderId;
        }
    }
}