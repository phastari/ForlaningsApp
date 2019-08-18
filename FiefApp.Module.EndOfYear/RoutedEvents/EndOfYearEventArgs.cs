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

        private readonly string _result;
        public string Result => _result;

        private readonly int _amor;
        public int Amor => _amor;

        private readonly string _incomeSilver;
        public string IncomeSilver => _incomeSilver;

        private readonly string _incomeBase;
        public string IncomeBase => _incomeBase;

        private readonly string _incomeLuxury;
        public string IncomeLuxury => _incomeLuxury;

        public bool AddPopulation { get; set; }

        public EndOfYearEventArgs(
            RoutedEvent routedEvent,
            string action,
            int id,
            bool ok,
            string result,
            int amor = 0,
            string incomeSilver = "-",
            string incomeBase = "-",
            string incomeLuxury = "-",
            bool addPopulation = true)
            : base(routedEvent)
        {
            _action = action;
            _id = id;
            _ok = ok;
            _result = result;
            _amor = amor;
            _incomeSilver = incomeSilver;
            _incomeBase = incomeBase;
            _incomeLuxury = incomeLuxury;
            AddPopulation = addPopulation;
        }
    }
}