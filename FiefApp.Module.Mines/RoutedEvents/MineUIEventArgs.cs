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

        private readonly string _skill;
        public string Skill => _skill;

        private int _guards;
        public int Guards
        {
            get => _guards;
            set => _guards = value;
        }

        private int _oldAmountGuards;
        public int OldAmountGuards
        {
            get => _oldAmountGuards;
            set => _oldAmountGuards = value;
        }

        private readonly int _income;
        public int Income => _income;

        public MineUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            int mineId,
            int stewardId,
            string steward,
            string skill = null,
            int guards = 0,
            int oldAmountGuards = 0,
            int income = 0
            )
            : base(routedEvent)
        {
            _action = action;
            _mineId = mineId;
            _stewardId = stewardId;
            _steward = steward;
            _skill = skill;
            _guards = guards;
            _oldAmountGuards = oldAmountGuards;
            _income = income;
        }
    }
}