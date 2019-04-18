using FiefApp.Common.Infrastructure.Models;
using System.Windows;

namespace FiefApp.Module.Trade.RoutedEvents
{
    public class MerchantUIEventArgs : RoutedEventArgs
    {
        private readonly int _id;
        public int Id => _id;

        private readonly string _action;
        public string Action => _action;

        private readonly MerchantModel _model;
        public MerchantModel Model => _model;

        private readonly int _boatId;
        public int BoatId => _boatId;

        private readonly string _boatName;
        public string BoatName => _boatName;

        public MerchantUIEventArgs(
            RoutedEvent routedEvent,
            int id,
            string action,
            MerchantModel model = null,
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