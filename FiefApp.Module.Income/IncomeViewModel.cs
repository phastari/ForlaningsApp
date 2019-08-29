using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
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

            IncomeUIEventUIEventHandler = new CustomDelegateCommand(ExecuteIncomeUIEventUIEventHandler, o => true);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
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
                SaveData();
                _baseService.ChangeSteward(e.StewardId, e.IncomeId, "Income");
                List<IncomeModel> tempIncome = new List<IncomeModel>(DataModel.IncomesCollection);
                DataModel.IncomesCollection.Clear();
                DataModel.IncomesCollection = new ObservableCollection<IncomeModel>(_incomeService.SetIncomes(Index, new ObservableCollection<IncomeModel>(tempIncome)));
                UpdateStewardsCollectionInIncomes();
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
            if (Index != 0)
            {
                DataModel = _baseService.GetDataModel<IncomeDataModel>(Index);
                DataModel.IncomesCollection = new ObservableCollection<IncomeModel>(_incomeService.SetIncomes(Index, DataModel.IncomesCollection));
                DataModel.UpdateTotals();
                DataModel.StewardsCollection = _baseService.GetStewardsCollection();

                for (int x = 0; x < DataModel.IncomesCollection.Count; x++)
                {
                    DataModel.IncomesCollection[x].StewardsCollection = DataModel.StewardsCollection;
                }
            }
            else
            {
                DataModel = _incomeService.GetAllDataModel();
            }

            UpdateFiefCollection();
        }

        protected override void SaveData(int index = -1)
        {
            if (Index != 0)
            {
                _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
            }
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            LoadData();
        }

        private void UpdateStewardsCollectionInIncomes()
        {
            if (DataModel.IncomesCollection.Count > 0)
            {
                for (int x = 0; x < DataModel.IncomesCollection.Count; x++)
                {
                    DataModel.IncomesCollection[x].StewardsCollection = new ObservableCollection<StewardModel>(_baseService.GetStewardsCollection());
                }
            }
        }
    }
}
