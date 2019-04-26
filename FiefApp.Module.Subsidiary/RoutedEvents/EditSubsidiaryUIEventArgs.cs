using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Subsidiary.RoutedEvents
{
    public class EditSubsidiaryUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly SubsidiaryModel _subsidiaryModel;
        public SubsidiaryModel SubsidiaryModel => _subsidiaryModel;

        public EditSubsidiaryUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            SubsidiaryModel subsidiaryModel = null
            )
            : base(routedEvent)
        {
            _action = action;
            _subsidiaryModel = subsidiaryModel;
        }
    }
}
