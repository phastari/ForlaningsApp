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
        private readonly IFiefService _fiefService;

        public EndOfYearViewModel(
            IBaseService baseService,
            IEndOfYearService endOfYearService,
            IEventAggregator eventAggregator,
            IFiefService fiefService
            ) : base(baseService)
        {
            _baseService = baseService;
            _endOfYearService = endOfYearService;
            _eventAggregator = eventAggregator;
            _fiefService = fiefService;

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
            if (DataModel.CheckIfAllRollsHaveBeenMade())
            {
                DataModel.EnableButton = false;

                for (int x = 1; x < _fiefService.InformationList.Count; x++)
                {
                    #region BoatbuildingModule

                    for (int y = _fiefService.BoatbuildingList[x].BoatsBuildingCollection.Count - 1; y > -1; y--)
                    {
                        if (_fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].BuildTimeInDaysAll < 365)
                        {
                            for (int z = 1; z <= _fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].Amount; z++)
                            {
                                _fiefService.PortsList[x].BoatsCollection.Add(_fiefService.BoatbuildingList[x].BoatsBuildingCollection[y]);
                            }

                            _fiefService.BoatbuildingList[x].BoatsBuildingCollection.RemoveAt(y);
                        }
                        else
                        {
                            int i = Convert.ToInt32(Math.Floor(365M / _fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].BuildTimeInDays));

                            if (i > _fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].Amount)
                            {
                                for (int z = 1; z <= _fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].Amount; z++)
                                {
                                    _fiefService.PortsList[x].BoatsCollection.Add(_fiefService.BoatbuildingList[x].BoatsBuildingCollection[y]);
                                }

                                _fiefService.BoatbuildingList[x].BoatsBuildingCollection.RemoveAt(y);
                            }
                            else
                            {
                                _fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].Amount -= i;
                                while (i > 0)
                                {
                                    _fiefService.PortsList[x].BoatsCollection.Add(_fiefService.BoatbuildingList[x].BoatsBuildingCollection[y]);
                                    i--;
                                }

                                _fiefService.BoatbuildingList[x].BoatsBuildingCollection.RemoveAt(y);
                            }
                        }
                    }

                    #endregion
                    #region BuildingsModule

                    for (int y = 0; y < _fiefService.BuildingsList[x].BuildsCollection.Count; y++)
                    {
                        if (_fiefService.BuildingsList[x].BuildsCollection[y].BuilderId > 0)
                        {
                            if (_fiefService.BuildingsList[x].BuildsCollection[y].StoneThisYear == _fiefService.BuildingsList[x].BuildsCollection[y].StoneNeededThisYear)
                            {
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftStonework -= _fiefService.BuildingsList[x].BuildsCollection[y].StoneworkThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftStone -= _fiefService.BuildingsList[x].BuildsCollection[y].StoneThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].StoneworkThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[y].StoneThisYear = 0;
                            }
                            else
                            {
                                decimal factor = _fiefService.BuildingsList[x].BuildsCollection[y].StoneThisYear / _fiefService.BuildingsList[x].BuildsCollection[y].StoneNeededThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftStonework -= Convert.ToInt32(Math.Floor(factor * _fiefService.BuildingsList[x].BuildsCollection[y].StoneworkThisYear));
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftStone -= _fiefService.BuildingsList[x].BuildsCollection[y].StoneThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].StoneworkThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[y].StoneThisYear = 0;
                            }

                            if (_fiefService.BuildingsList[x].BuildsCollection[y].WoodThisYear == _fiefService.BuildingsList[x].BuildsCollection[y].WoodNeededThisYear)
                            {
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftWoodwork -= _fiefService.BuildingsList[x].BuildsCollection[y].WoodworkThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftWood -= _fiefService.BuildingsList[x].BuildsCollection[y].WoodThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].WoodworkThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[y].WoodThisYear = 0;
                            }
                            else
                            {
                                decimal factor = _fiefService.BuildingsList[x].BuildsCollection[y].WoodThisYear / _fiefService.BuildingsList[x].BuildsCollection[y].WoodNeededThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftWoodwork -= Convert.ToInt32(Math.Floor(factor * _fiefService.BuildingsList[x].BuildsCollection[y].WoodworkThisYear));
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftWood -= _fiefService.BuildingsList[x].BuildsCollection[y].WoodThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].WoodworkThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[y].WoodThisYear = 0;
                            }

                            if (_fiefService.BuildingsList[x].BuildsCollection[y].IronThisYear == _fiefService.BuildingsList[x].BuildsCollection[y].IronNeededThisYear)
                            {
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftSmithswork -= _fiefService.BuildingsList[x].BuildsCollection[y].SmithsworkThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftIron -= _fiefService.BuildingsList[x].BuildsCollection[y].IronThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].SmithsworkThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[y].IronThisYear = 0;
                            }
                            else
                            {
                                decimal factor = _fiefService.BuildingsList[x].BuildsCollection[y].IronThisYear / _fiefService.BuildingsList[x].BuildsCollection[y].IronNeededThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftSmithswork -= Convert.ToInt32(Math.Floor(factor * _fiefService.BuildingsList[x].BuildsCollection[y].SmithsworkThisYear));
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftIron -= _fiefService.BuildingsList[x].BuildsCollection[y].IronThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].SmithsworkThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[y].IronThisYear = 0;
                            }
                        }
                    }

                    

                    #endregion
                }
            }
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
