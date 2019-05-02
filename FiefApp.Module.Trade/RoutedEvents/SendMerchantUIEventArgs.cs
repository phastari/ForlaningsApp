using FiefApp.Common.Infrastructure.Models;
using System.Windows;

namespace FiefApp.Module.Trade.RoutedEvents
{
    public class SendMerchantUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly MerchantModel _model;
        public MerchantModel Model => _model;

        public SendMerchantUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            MerchantModel model = null
            )
            : base(routedEvent)
        {
            _action = action;
            _model = model;
        }
    }
}