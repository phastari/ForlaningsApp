using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;

using Prism.Events;
using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using Prism.Commands;

namespace FiefApp.Module.EndOfYear
{
    public class EndOfYearViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IEndOfYearService _endOfYearService;
        private readonly IEventAggregator _eventAggregator;

        public EndOfYearViewModel(
            IBaseService baseService,
            IEndOfYearService endOfYearService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            _baseService = baseService;
            _endOfYearService = endOfYearService;
            _eventAggregator = eventAggregator;

            TabName = "Bokslut";

            EndOfYearCancelCommand = new DelegateCommand(ExecuteEndOfYearCancelCommand);
        }

        #region DelegateCommand : EndOfYearCancelCommand

        public DelegateCommand EndOfYearCancelCommand { get; set; }
        private void ExecuteEndOfYearCancelCommand()
        {
            _eventAggregator.GetEvent<EndOfYearEvent>().Publish();
        }

        #endregion

        #region Data Model

        private EndOfYearDataModel _dataModel = new EndOfYearDataModel();
        public EndOfYearDataModel DataModel
        {
            get => _dataModel;
            set => _dataModel = value;
        }

        #endregion

        #region Methods

        protected override void SaveData(int index = -1)
        {

        }

        protected override void LoadData()
        {
            DataModel.IncomeListFief = new ObservableCollection<EndOfYearIncomeFiefModel>(_endOfYearService.Initialize());
        }

        #endregion
    }
}
