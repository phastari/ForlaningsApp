using FiefApp.Common.Infrastructure.Models;
using System.Windows;

namespace FiefApp.Module.Subsidiary.RoutedEvents
{
    public class AddSubsidiaryUIEventArgs : RoutedEventArgs
    {
        private readonly int _id;
        public int Id => _id;

        private readonly string _action;
        public string Action => _action;

        private readonly SubsidiaryModel _subsidiaryModel;
        public SubsidiaryModel SubsidiaryModel => _subsidiaryModel;

        public AddSubsidiaryUIEventArgs(
            RoutedEvent routedEvent,
            int id,
            string action,
            SubsidiaryModel subsidiaryModel = null
            )
            : base(routedEvent)
        {
            _id = id;
            _action = action;
            _subsidiaryModel = subsidiaryModel;
        }
    }
}
