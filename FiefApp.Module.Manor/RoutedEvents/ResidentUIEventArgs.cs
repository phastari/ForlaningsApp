using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Manor.RoutedEvents
{
    public class ResidentUIEventArgs : RoutedEventArgs
    {
        private readonly int _id;
        public int Id => _id;

        private readonly string _action;
        public string Action => _action;

        private readonly ResidentModel _residentModel;
        public ResidentModel ResidentModel => _residentModel;

        public ResidentUIEventArgs(
            RoutedEvent routedEvent,
            int id,
            string action,
            ResidentModel residentModel = null
            )
            : base(routedEvent)
        {
            _id = id;
            _action = action;
            _residentModel = residentModel;
        }
    }
}
