using FiefApp.Common.Infrastructure.Models;
using System.Windows;

namespace FiefApp.Module.Subsidiary.RoutedEvents
{
    public class ConstructingUIEventArgs : RoutedEventArgs
    {
        private readonly int _subsidiaryId;
        public int SubsidiaryId => _subsidiaryId;

        private readonly string _subsidiary;
        public string Subsidiary => _subsidiary;

        private readonly int _stewardId;
        public int StewardId => _stewardId;

        private readonly string _steward;
        public string Steward => _steward;

        private readonly string _action;
        public string Action => _action;

        private readonly int _daysWorkThisYear;
        public int DaysWorkThisYear => _daysWorkThisYear;

        public ConstructingUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            int subsidiaryId,
            string subsidiary,
            int stewardId,
            string steward,
            int daysWorkThisYear = 0
            )
            : base(routedEvent)
        {
            _action = action;
            _subsidiaryId = subsidiaryId;
            _subsidiary = subsidiary;
            _stewardId = stewardId;
            _steward = steward;
            _daysWorkThisYear = daysWorkThisYear;
        }
    }
}
