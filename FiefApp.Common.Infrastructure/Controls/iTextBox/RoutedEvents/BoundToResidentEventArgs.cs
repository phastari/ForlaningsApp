using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Controls.iTextBox.RoutedEvents
{
    public class BoundToResidentEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly SoldierModel _soldierModel;
        public SoldierModel SoldierModel => _soldierModel;

        public BoundToResidentEventArgs(
            RoutedEvent routedEvent,
            string action,
            SoldierModel soldierModel = null
            )
            : base(routedEvent)
        {
            _action = action;
            _soldierModel = soldierModel;
        }
    }
}
