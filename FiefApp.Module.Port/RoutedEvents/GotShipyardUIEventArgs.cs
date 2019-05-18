using FiefApp.Common.Infrastructure.Models;
using System.Windows;

namespace FiefApp.Module.Port.RoutedEvents
{
    public class GotShipyardUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly ShipyardModel _model;
        public ShipyardModel Model => _model;

        public GotShipyardUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            ShipyardModel model = null
            )
            : base(routedEvent)
        {
            _action = action;
            _model = model;
        }
    }
}