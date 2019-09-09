using System.Windows;

namespace FiefApp.Module.EndOfYear.RoutedEvents
{
    public class EndOfYearAddAcresEventArgs : RoutedEventArgs
    {
        private readonly int _fiefId;
        public int FiefId => _fiefId;

        private readonly int _pasture;
        public int Pasture => _pasture;

        private readonly int _agricultural;
        public int Agricultural => _agricultural;

        public EndOfYearAddAcresEventArgs(
            RoutedEvent routedEvent,
            int fiefId,
            int pasture,
            int agricultural)
            : base(routedEvent)
        {
            _fiefId = fiefId;
            _pasture = pasture;
            _agricultural = agricultural;
        }
    }
}
