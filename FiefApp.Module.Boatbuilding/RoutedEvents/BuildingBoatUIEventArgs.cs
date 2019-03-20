using FiefApp.Common.Infrastructure.Models;
using System.Windows;

namespace FiefApp.Module.Boatbuilding.RoutedEvents
{
    public class BuildingBoatUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly BoatModel _boatModel;
        public BoatModel BoatModel => _boatModel;

        public BuildingBoatUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            BoatModel boatModel = null
            )
            : base(routedEvent)
        {
            _action = action;
            _boatModel = boatModel;
        }
    }
}