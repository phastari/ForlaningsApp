using FiefApp.Common.Infrastructure.Models;
using System.Windows;

namespace FiefApp.Module.Income.RoutedEvents
{
    public class IncomeUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly int _stewardId;
        public int StewardId => _stewardId;

        private readonly string _income;
        public string Income => _income;

        private readonly int _incomeId;
        public int IncomeId => _incomeId;

        public IncomeUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            int stewardId,
            string income = "",
            int incomeId = -1
            )
            : base(routedEvent)
        {
            _action = action;
            _stewardId = stewardId;
            _income = income;
            _incomeId = incomeId;
        }
    }
}