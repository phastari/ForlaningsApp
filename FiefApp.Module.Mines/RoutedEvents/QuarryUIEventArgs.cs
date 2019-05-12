using System.Windows;

namespace FiefApp.Module.Mines.RoutedEvents
{
    public class QuarryUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly int _quarryId;
        public int QuarryId => _quarryId;

        private readonly int _stewardId;
        public int StewardId => _stewardId;

        private readonly string _steward;
        public string Steward => _steward;

        private readonly string _skill;
        public string Skill => _skill;

        private readonly int _dayswork;
        public int DaysWork => _dayswork;

        private readonly int _income;
        public int Income => _income;

        public QuarryUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            int quarryId,
            int stewardId,
            string steward,
            string skill = null,
            int dayswork = 0,
            int income = 0
            )
            : base(routedEvent)
        {
            _action = action;
            _quarryId = quarryId;
            _stewardId = stewardId;
            _steward = steward;
            _skill = skill;
            _dayswork = dayswork;
            _income = income;
        }
    }
}