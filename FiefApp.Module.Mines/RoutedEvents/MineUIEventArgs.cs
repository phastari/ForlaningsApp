using System.Windows;

namespace FiefApp.Module.Mines.RoutedEvents
{
    public class MineUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly int _mineId;
        public int MineId => _mineId;

        private readonly int _stewardId;
        public int StewardId => _stewardId;

        private readonly string _steward;
        public string Steward => _steward;

        public MineUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            int mineId,
            int stewardId,
            string steward
            )
            : base(routedEvent)
        {
            _action = action;
            _mineId = mineId;
            _stewardId = stewardId;
            _steward = steward;
        }
    }
}