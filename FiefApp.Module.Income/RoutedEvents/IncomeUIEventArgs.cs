using FiefApp.Common.Infrastructure.Models;
using System.Windows;

namespace FiefApp.Module.Income.RoutedEvents
{
    public class IncomeUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly StewardModel _stewardModel;
        public StewardModel StewardModel => _stewardModel;

        private readonly string _income;
        public string Income => _income;

        private readonly int _fiefId;
        public int FiefId => _fiefId;

        public IncomeUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            StewardModel stewardModel = null,
            string income = "",
            int fiefId = -1
            )
            : base(routedEvent)
        {
            _action = action;
            _stewardModel = stewardModel;
            _income = income;
            _fiefId = fiefId;
        }
    }
}