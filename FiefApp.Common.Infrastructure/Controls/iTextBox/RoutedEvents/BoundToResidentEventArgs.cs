using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Controls.iTextBox.RoutedEvents
{
    public class BoundToResidentEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly SoldierModel _model;
        public SoldierModel Model => _model;

        public BoundToResidentEventArgs(
            RoutedEvent routedEvent,
            string action,
            SoldierModel model
            )
            : base(routedEvent)
        {
            _action = action;
            _model = model;
        }
    }
}