using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Manor.RoutedEvents
{
    public class VillagesUIEventArgs : RoutedEventArgs
    {
        private readonly int _id;
        public int Id => _id;

        private readonly string _action;
        public string Action => _action;

        private readonly VillageModel _villageModel;
        public VillageModel VillageModel => _villageModel;

        public VillagesUIEventArgs(
            RoutedEvent routedEvent,
            int id,
            string action,
            VillageModel villageModel = null
            )
            : base(routedEvent)
        {
            _id = id;
            _action = action;
            _villageModel = villageModel;
        }
    }
}