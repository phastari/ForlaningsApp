using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.EndOfYear.RoutedEvents;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            CompleteEndOfYearCommand = new DelegateCommand(ExecuteCompleteEndOfYearCommand);
            EndOfYearOkEventHandler = new CustomDelegateCommand(ExecuteEndOfYearOkEventHandler, o => true);

            _eventAggregator.GetEvent<EndOfYearEvent>().Subscribe(LoadData);
        }

        #region DelegateCommand : EndOfYearCancelCommand

        public DelegateCommand EndOfYearCancelCommand { get; set; }
        private void ExecuteEndOfYearCancelCommand()
        {
            _eventAggregator.GetEvent<EndOfYearEvent>().Publish();
        }

        #endregion
        #region CustomDelegateCommand : EndOfYearOkEventHandler

        public CustomDelegateCommand EndOfYearOkEventHandler { get; set; }
        private void ExecuteEndOfYearOkEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is EndOfYearEventArgs e))
            {
                return;
            }

            e.Handled = true;
            Console.WriteLine("EndOfYearOkEvent Recived!");
            switch (e.Action)
            {
                case "Industry":
                    {
                        List<bool> tempList = new List<bool>();
                        for (int x = 0; x < DataModel.IncomeListFief.Count; x++)
                        {
                            if (DataModel.IncomeListFief[x].EndOfYearOkDictionary.ContainsKey(e.Id))
                            {
                                DataModel.IncomeListFief[x].EndOfYearOkDictionary[e.Id] = e.Ok;
                            }
                            
                            if (!DataModel.IncomeListFief[x].EndOfYearOkDictionary.ContainsValue(false))
                            {
                                tempList.Add(true);
                            }
                            else
                            {
                                tempList.Add(false);
                            }
                            tempList.Add(DataModel.IncomeListFief[x].PopulationOk);
                            tempList.Add(DataModel.IncomeListFief[x].TaxesOk);
                        }

                        if (tempList.Contains(false))
                        {
                            DataModel.EnableButton = false;
                        }
                        else
                        {
                            DataModel.EnableButton = true;
                        }
                        
                        break;
                    }

                case "Population":
                    {
                        DataModel.IncomeListFief[e.Id - 1].PopulationOk = e.Ok;

                        List<bool> tempList = new List<bool>();
                        for (int x = 0; x < DataModel.IncomeListFief.Count; x++)
                        {
                            if (!DataModel.IncomeListFief[x].EndOfYearOkDictionary.ContainsValue(false))
                            {
                                tempList.Add(true);
                            }
                            else
                            {
                                tempList.Add(false);
                            }
                            tempList.Add(DataModel.IncomeListFief[x].PopulationOk);
                            tempList.Add(DataModel.IncomeListFief[x].TaxesOk);
                        }

                        if (tempList.Contains(false))
                        {
                            DataModel.EnableButton = false;
                        }
                        else
                        {
                            DataModel.EnableButton = true;
                        }

                        break;
                    }

                case "Taxes":
                    {
                        DataModel.IncomeListFief[e.Id - 1].TaxesOk = e.Ok;

                        List<bool> tempList = new List<bool>();
                        for (int x = 0; x < DataModel.IncomeListFief.Count; x++)
                        {
                            if (!DataModel.IncomeListFief[x].EndOfYearOkDictionary.ContainsValue(false))
                            {
                                tempList.Add(true);
                            }
                            else
                            {
                                tempList.Add(false);
                            }
                            tempList.Add(DataModel.IncomeListFief[x].PopulationOk);
                            tempList.Add(DataModel.IncomeListFief[x].TaxesOk);
                        }

                        if (tempList.Contains(false))
                        {
                            DataModel.EnableButton = false;
                        }
                        else
                        {
                            DataModel.EnableButton = true;
                        }

                        break;
                    }
            }
        }

        #endregion
        #region DelegateCommand : CompleteEndOfYearCommand

        public DelegateCommand CompleteEndOfYearCommand { get; set; }
        private void ExecuteCompleteEndOfYearCommand()
        {
            DataModel.CheckIfAllRollsHaveBeenMade();
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
