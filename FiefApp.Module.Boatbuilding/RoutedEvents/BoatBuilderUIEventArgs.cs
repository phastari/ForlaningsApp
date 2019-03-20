using FiefApp.Common.Infrastructure.Models;
using System.Windows;

namespace FiefApp.Module.Boatbuilding.RoutedEvents
{
    public class BoatBuilderUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly BoatbuilderModel _boatbuilderModel;
        public BoatbuilderModel BoatbuilderModel => _boatbuilderModel;

        public BoatBuilderUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            BoatbuilderModel boatbuilderModel = null
            )
            : base(routedEvent)
        {
            _action = action;
            _boatbuilderModel = boatbuilderModel;
        }
    }
}