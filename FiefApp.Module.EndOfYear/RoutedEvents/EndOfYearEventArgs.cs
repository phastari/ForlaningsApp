using System.Windows;

namespace FiefApp.Module.EndOfYear.RoutedEvents
{
    public class EndOfYearEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly int _id;
        public int Id => _id;

        private readonly bool _ok;
        public bool Ok => _ok;

        public EndOfYearEventArgs(
            RoutedEvent routedEvent,
            string action,
            int id,
            bool ok
            )
            : base(routedEvent)
        {
            _action = action;
            _id = id;
            _ok = ok;
        }
    }
}