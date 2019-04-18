using FiefApp.Common.Infrastructure.Models;
using System.Windows;

namespace FiefApp.Module.Port.RoutedEvents
{
    public class BoatUIEventArgs : RoutedEventArgs
    {
        private readonly int _id;
        public int Id => _id;

        private readonly string _action;
        public string Action => _action;

        private readonly BoatModel _model;
        public BoatModel Model => _model;

        private readonly int _captainId;
        public int CaptainId => _captainId;

        private readonly string _captainName;
        public string CaptainName => _captainName;

        public BoatUIEventArgs(
            RoutedEvent routedEvent,
            int id,
            string action,
            BoatModel model = null,
            int captainId = -1,
            string captainName = ""
            )
            : base(routedEvent)
        {
            _id = id;
            _action = action;
            _model = model;
            _captainName = captainName;
            _captainId = captainId;
        }
    }
}