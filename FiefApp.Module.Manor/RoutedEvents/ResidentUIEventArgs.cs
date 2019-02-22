using FiefApp.Common.Infrastructure.Models;
using System.Windows;

namespace FiefApp.Module.Manor.RoutedEvents
{
    public class ResidentUIEventArgs : RoutedEventArgs
    {
        private readonly int _id;
        public int Id => _id;

        private readonly string _action;
        public string Action => _action;

        private readonly IPeopleModel _model;
        public IPeopleModel Model => _model;

        public ResidentUIEventArgs(
            RoutedEvent routedEvent,
            int id,
            string action,
            IPeopleModel model = null
            )
            : base(routedEvent)
        {
            _id = id;
            _action = action;
            _model = model;
        }
    }
}
