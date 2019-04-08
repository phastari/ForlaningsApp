using System.Windows;

namespace FiefApp.Module.Subsidiary.RoutedEvents
{
    public class SubsidiaryUIEventArgs : RoutedEventArgs
    {
        private readonly int _stewardId;
        public int StewardId => _stewardId;

        private readonly int _subsidiaryId;
        public int SubsidiaryId => _subsidiaryId;

        private readonly string _steward;
        public string Steward => _steward;

        private readonly string _action;
        public string Action => _action;

        private readonly string _subsidiary;
        public string Subsidiary => _subsidiary;

        public SubsidiaryUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            int stewardId,
            string steward,
            int subsidiaryId,
            string subsidiary = ""
            )
            : base(routedEvent)
        {
            _stewardId = stewardId;
            _subsidiaryId = subsidiaryId;
            _action = action;
            _steward = steward;
            _subsidiary = subsidiary;
        }
    }
}
