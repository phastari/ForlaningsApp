using System.Windows;

namespace FiefApp.Module.EndOfYear.RoutedEvents
{
    public class EndOfYearConstructingSubsidiaryEventArgs : RoutedEventArgs
    {
        private readonly int _id;
        public int Id => _id;

        private readonly bool _succeeded;
        public bool Succeeded => _succeeded;

        private readonly int _daysWorkMod;
        public int DaysWorkMod => _daysWorkMod;

        private readonly int _quality;
        public int Quality => _quality;

        private int _developmentLevel;
        public int DevelopmentLevel => _developmentLevel;

        private bool _ok;
        public bool Ok => _ok;

        public EndOfYearConstructingSubsidiaryEventArgs(
            RoutedEvent routedEvent,
            int id,
            bool succeeded,
            int daysWorkMod,
            int quality,
            int developmentLevel,
            bool ok)
            : base(routedEvent)
        {
            _id = id;
            _succeeded = succeeded;
            _daysWorkMod = daysWorkMod;
            _quality = quality;
            _developmentLevel = developmentLevel;
            _ok = ok;
        }
    }
}
