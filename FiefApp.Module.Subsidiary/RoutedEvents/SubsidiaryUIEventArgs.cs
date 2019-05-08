using System.Windows;
using FiefApp.Common.Infrastructure.Models;

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

        private readonly string _skill;
        public string Skill => _skill;

        private readonly SubsidiaryModel _model;
        public SubsidiaryModel Model => _model;

        public SubsidiaryUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            int stewardId,
            string steward,
            int subsidiaryId,
            string skill,
            string subsidiary = "",
            SubsidiaryModel model = null
            )
            : base(routedEvent)
        {
            _stewardId = stewardId;
            _subsidiaryId = subsidiaryId;
            _action = action;
            _steward = steward;
            _subsidiary = subsidiary;
            _skill = skill;
            _model = model;
        }
    }
}
