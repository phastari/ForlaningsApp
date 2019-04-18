using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Mines.RoutedEvents
{
    public class ConstructQuarryUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly QuarryModel _model;
        public QuarryModel Model => _model;

        public ConstructQuarryUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            QuarryModel model = null
            )
            : base(routedEvent)
        {
            _action = action;
            _model = model;
        }
    }
}