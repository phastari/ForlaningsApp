using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Buildings.RoutedEvents
{
    public class BuildingBuildingUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly int _id;
        public int Id => _id;

        private readonly BuildingModel _model;
        public BuildingModel Model => _model;

        public BuildingBuildingUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            int id,
            BuildingModel model = null
            )
            : base(routedEvent)
        {
            _action = action;
            _id = id;
            _model = model;
        }
    }
}