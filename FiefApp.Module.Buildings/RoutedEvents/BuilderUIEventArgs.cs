using FiefApp.Common.Infrastructure.Models;
using System.Windows;

namespace FiefApp.Module.Buildings.RoutedEvents
{
    public class BuilderUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly BuilderModel _model;
        public BuilderModel Model => _model;

        public BuilderUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            BuilderModel model = null
            )
            : base(routedEvent)
        {
            _action = action;
            _model = model;
        }
    }
}