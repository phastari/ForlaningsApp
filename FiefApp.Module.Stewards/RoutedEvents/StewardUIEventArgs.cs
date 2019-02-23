using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Stewards.RoutedEvents
{
    public class StewardUIEventArgs : RoutedEventArgs
    {
        private readonly int _id;
        public int Id => _id;

        private readonly string _action;
        public string Action => _action;

        private readonly StewardModel _stewardModel;
        public StewardModel StewardModel => _stewardModel;

        public StewardUIEventArgs(
            RoutedEvent routedEvent,
            int id,
            string action,
            StewardModel stewardModel = null
            )
            : base(routedEvent)
        {
            _id = id;
            _action = action;
            _stewardModel = stewardModel;
        }
    }
}