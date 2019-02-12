using FiefApp.Common.Infrastructure.Models;
using System.Windows;

namespace FiefApp.Module.Manor.RoutedEvents
{
    public class AddResidentUIEventArgs : RoutedEventArgs
    {
        private readonly int _id;
        public int Id => _id;

        private readonly string _action;
        public string Action => _action;

        private readonly ResidentModel _residentModel;
        public ResidentModel ResidentModel => _residentModel;

        public AddResidentUIEventArgs(
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
