using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Buildings.RoutedEvents
{
    public class AddBuildingUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly BuildingModel _model;
        public BuildingModel Model => _model;

        public AddBuildingUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            BuildingModel model = null
            )
            : base(routedEvent)
        {
            _action = action;
            _model = model;
        }
    }
}