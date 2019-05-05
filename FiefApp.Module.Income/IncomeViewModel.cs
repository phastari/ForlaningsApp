using System;
using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Income.RoutedEvents;
using Prism.Commands;
using Prism.Events;

namespace FiefApp.Module.Income
{
    public class IncomeViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IIncomeService _incomeService;
        private readonly IEventAggregator _eventAggregator;

        public IncomeViewModel(
            IBaseService baseService, 
            IIncomeService incomeService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            _baseService = baseService;
            _incomeService = incomeService;
            _eventAggregator = eventAggregator;

            TabName = "Inkomst";

            RollDie = new DelegateCommand(ExecuteRollDie);
            IncomeUIEventUIEventHandler = new CustomDelegateCommand(ExecuteIncomeUIEventUIEventHandler, o => true);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
        }
        public DelegateCommand RollDie { get; set; }

        private void ExecuteRollDie()
        {
            _baseService.RollObDice(12);
        }

        #region CustomDelegateCommand : IncomeUIEventUIEventHandler

        public CustomDelegateCommand IncomeUIEventUIEventHandler { get; set; }
        private void ExecuteIncomeUIEventUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is IncomeUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Changed")
            {
                ChangeStewardInIncomeCollection(e.StewardModel.Id, e.StewardModel.PersonName, e.FiefId, e.Income);
                _incomeService.ChangeSteward(e.StewardModel.Id, e.FiefId, e.Income);
                DataModel.IncomesCollection.Clear();
                DataModel.IncomesCollection = _incomeService.GetAllIncomes(Index);
                SaveData();
            }
        }

        #endregion

        #region DataModel

        private IncomeDataModel _dataModel;
        public IncomeDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        protected override void LoadData()
        {
            DataModel = _baseService.GetDataModel<IncomeDataModel>(Index);
            DataModel.IncomesCollection = _incomeService.GetAllIncomes(Index);
            DataModel.UpdateTotals();
            DataModel.StewardsCollection = _incomeService.GetAllStewards(Index);

            for (int x = 0; x < DataModel.IncomesCollection.Count; x++)
            {
                DataModel.IncomesCollection[x].StewardsCollection = DataModel.StewardsCollection;
            }

            UpdateFiefCollection();
        }

        protected override void SaveData(int index = -1)
        {
            _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            LoadData();
        }

        #region Methods

        private void ChangeStewardInIncomeCollection(int stewardId, string steward, int manorId, string income)
        {
            for (int x = 0; x < DataModel.IncomesCollection.Count; x++)
            {
                if (stewardId == DataModel.IncomesCollection[x].StewardId)
                {
                    DataModel.IncomesCollection[x].StewardId = -1;
                    DataModel.IncomesCollection[x].Steward = "";
                }
            }

            for (int x = 0; x < DataModel.IncomesCollection.Count; x++)
            {
                if (DataModel.IncomesCollection[x].Income == income && DataModel.IncomesCollection[x].ManorId == manorId)
                {
                    DataModel.IncomesCollection[x].StewardId = stewardId;
                    DataModel.IncomesCollection[x].Steward = steward;
                }
            }

            SaveData();
        }

        #endregion
    }
}
