using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Port.RoutedEvents
{
    public class CaptainUIEventArgs : RoutedEventArgs
    {
        private readonly int _id;
        public int Id => _id;

        private readonly string _action;
        public string Action => _action;

        private readonly CaptainModel _model;
        public CaptainModel Model => _model;

        private readonly int _boatId;
        public int BoatId => _boatId;

        private readonly string _boatName;
        public string BoatName => _boatName;

        public CaptainUIEventArgs(
            RoutedEvent routedEvent,
            int id,
            string action,
            CaptainModel model = null,
            int boatId = -1,
            string boatName = ""
            )
            : base(routedEvent)
        {
            _id = id;
            _action = action;
            _model = model;
            _boatId = boatId;
            _boatName = boatName;
        }
    }
}
