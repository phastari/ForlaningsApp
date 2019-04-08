using System.Windows;

namespace FiefApp.Module.Buildings.RoutedEvents
{
    public class BuildingBuildingUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly int _id;
        public int Id => _id;

        public BuildingBuildingUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            int id
            )
            : base(routedEvent)
        {
            _action = action;
            _id = id;
        }
    }
}