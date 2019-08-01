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
using System.Linq;

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

                            for (int y = 0; y < DataModel.IncomeListFief[x].IncomeCollection.Count; y++)
                            {
                                if (DataModel.IncomeListFief[x].IncomeCollection[y].Id == e.Id)
                                {
                                    DataModel.IncomeListFief[x].IncomeCollection[y].Result = e.Result;
                                }
                            }
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

                case "Mine":
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

                            for (int y = 0; y < DataModel.IncomeListFief[x].MinesCollection.Count; y++)
                            {
                                if (DataModel.IncomeListFief[x].MinesCollection[y].Id == e.Id)
                                {
                                    DataModel.IncomeListFief[x].MinesCollection[y].Result = e.Result;
                                }
                            }
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

                case "Quarry":
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

                            for (int y = 0; y < DataModel.IncomeListFief[x].QuarriesCollection.Count; y++)
                            {
                                if (DataModel.IncomeListFief[x].QuarriesCollection[y].Id == e.Id)
                                {
                                    DataModel.IncomeListFief[x].QuarriesCollection[y].Result = e.Result;
                                }
                            }
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

                case "Shipyard":
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

                            if (DataModel.IncomeListFief[x].Shipyard.Id == e.Id)
                            {
                                DataModel.IncomeListFief[x].Shipyard.Result = e.Result;
                            }
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

                case "Subsidiary":
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

                            for (int y = 0; y < DataModel.IncomeListFief[x].SubsidiariesCollection.Count; y++)
                            {
                                if (DataModel.IncomeListFief[x].SubsidiariesCollection[y].Id == e.Id)
                                {
                                    DataModel.IncomeListFief[x].SubsidiariesCollection[y].ResultSilver = e.IncomeSilver;
                                    DataModel.IncomeListFief[x].SubsidiariesCollection[y].ResultBase = e.IncomeBase;
                                    DataModel.IncomeListFief[x].SubsidiariesCollection[y].ResultLuxury = e.IncomeLuxury;
                                }
                            }
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

                case "Development":
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

                            for (int y = 0; y < DataModel.IncomeListFief[x].IncomeCollection.Count; y++)
                            {
                                if (DataModel.IncomeListFief[x].IncomeCollection[y].Id == e.Id)
                                {
                                    DataModel.IncomeListFief[x].IncomeCollection[y].Result = e.Result;
                                }
                            }
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
                        DataModel.IncomeListFief[e.Id - 1].AmorNumeric = e.Amor;

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
                        DataModel.IncomeListFief[e.Id - 1].TaxFactor = Convert.ToInt32(e.Result);

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
                List<EndOfYearReportModel> reports = new List<EndOfYearReportModel>();

                for (int x = 1; x < _fiefService.InformationList.Count; x++)
                {
                    EndOfYearReportModel model = new EndOfYearReportModel();

                    #region BoatbuildingModule

                    for (int y = _fiefService.BoatbuildingList[x].BoatsBuildingCollection.Count - 1; y > -1; y--)
                    {
                        if (_fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].BuildTimeInDaysAll < 365)
                        {
                            for (int z = 1; z <= _fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].Amount; z++)
                            {
                                _fiefService.PortsList[x].BoatsCollection.Add(_fiefService.BoatbuildingList[x].BoatsBuildingCollection[y]);
                                model.FinishedBoats.Add(_fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].BoatType);
                            }

                            model.FinishedBoatsSilverCost += _fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].CostWhenFinishedSilver;
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
                                    model.FinishedBoats.Add(_fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].BoatType);
                                }

                                model.FinishedBoatsSilverCost += _fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].CostWhenFinishedSilver;
                                _fiefService.BoatbuildingList[x].BoatsBuildingCollection.RemoveAt(y);
                            }
                            else
                            {
                                _fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].Amount -= i;
                                model.FinishedBoatsSilverCost += _fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].CostWhenFinishedSilver / i;
                                while (i > 0)
                                {
                                    _fiefService.PortsList[x].BoatsCollection.Add(_fiefService.BoatbuildingList[x].BoatsBuildingCollection[y]);
                                    model.FinishedBoats.Add(_fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].BoatType);
                                    i--;
                                }
                                _fiefService.BoatbuildingList[x].BoatsBuildingCollection.RemoveAt(y);
                            }
                        }
                    }

                    model.BoatbuildersSilverCost += _fiefService.BoatbuildingList[x].BoatBuildersCollection.Count * 1800;

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
                                model.BuildingsStone += _fiefService.BuildingsList[x].BuildsCollection[y].StoneThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].StoneworkThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[y].StoneThisYear = 0;
                            }
                            else
                            {
                                decimal factor = _fiefService.BuildingsList[x].BuildsCollection[y].StoneThisYear / _fiefService.BuildingsList[x].BuildsCollection[y].StoneNeededThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftStonework -= Convert.ToInt32(Math.Floor(factor * _fiefService.BuildingsList[x].BuildsCollection[y].StoneworkThisYear));
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftStone -= _fiefService.BuildingsList[x].BuildsCollection[y].StoneThisYear;
                                model.BuildingsStone += _fiefService.BuildingsList[x].BuildsCollection[y].StoneThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].StoneworkThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[y].StoneThisYear = 0;
                            }

                            if (_fiefService.BuildingsList[x].BuildsCollection[y].WoodThisYear == _fiefService.BuildingsList[x].BuildsCollection[y].WoodNeededThisYear)
                            {
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftWoodwork -= _fiefService.BuildingsList[x].BuildsCollection[y].WoodworkThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftWood -= _fiefService.BuildingsList[x].BuildsCollection[y].WoodThisYear;
                                model.BuildingsWood += _fiefService.BuildingsList[x].BuildsCollection[y].WoodThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].WoodworkThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[y].WoodThisYear = 0;
                            }
                            else
                            {
                                decimal factor = _fiefService.BuildingsList[x].BuildsCollection[y].WoodThisYear / _fiefService.BuildingsList[x].BuildsCollection[y].WoodNeededThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftWoodwork -= Convert.ToInt32(Math.Floor(factor * _fiefService.BuildingsList[x].BuildsCollection[y].WoodworkThisYear));
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftWood -= _fiefService.BuildingsList[x].BuildsCollection[y].WoodThisYear;
                                model.BuildingsWood += _fiefService.BuildingsList[x].BuildsCollection[y].WoodThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].WoodworkThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[y].WoodThisYear = 0;
                            }

                            if (_fiefService.BuildingsList[x].BuildsCollection[y].IronThisYear == _fiefService.BuildingsList[x].BuildsCollection[y].IronNeededThisYear)
                            {
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftSmithswork -= _fiefService.BuildingsList[x].BuildsCollection[y].SmithsworkThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftIron -= _fiefService.BuildingsList[x].BuildsCollection[y].IronThisYear;
                                model.BuildingsIron += _fiefService.BuildingsList[x].BuildsCollection[y].IronThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].SmithsworkThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[y].IronThisYear = 0;
                            }
                            else
                            {
                                decimal factor = _fiefService.BuildingsList[x].BuildsCollection[y].IronThisYear / _fiefService.BuildingsList[x].BuildsCollection[y].IronNeededThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftSmithswork -= Convert.ToInt32(Math.Floor(factor * _fiefService.BuildingsList[x].BuildsCollection[y].SmithsworkThisYear));
                                _fiefService.BuildingsList[x].BuildsCollection[y].LeftIron -= _fiefService.BuildingsList[x].BuildsCollection[y].IronThisYear;
                                model.BuildingsIron += _fiefService.BuildingsList[x].BuildsCollection[y].IronThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[y].SmithsworkThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[y].IronThisYear = 0;
                            }
                        }
                    }

                    for (int y = 0; y < _fiefService.BuildingsList[x].BuildingsCollection.Count; y++)
                    {
                        model.BuildingsUpkeepBase += Convert.ToInt32(Math.Ceiling(_fiefService.BuildingsList[x].BuildingsCollection[y].Upkeep * _fiefService.BuildingsList[x].BuildingsCollection[y].Amount));
                    }

                    for (int y = _fiefService.BuildingsList[x].BuildsCollection.Count - 1; y > -1; y--)
                    {
                        if (_fiefService.BuildingsList[x].BuildsCollection[y].LeftSmithswork <= 0
                            && _fiefService.BuildingsList[x].BuildsCollection[y].LeftStonework <= 0
                            && _fiefService.BuildingsList[x].BuildsCollection[y].LeftWoodwork <= 0
                            && _fiefService.BuildingsList[x].BuildsCollection[y].LeftStone <= 0
                            && _fiefService.BuildingsList[x].BuildsCollection[y].LeftWood <= 0
                            && _fiefService.BuildingsList[x].BuildsCollection[y].LeftIron <= 0)
                        {
                            _fiefService.BuildingsList[x].BuildingsCollection.Add(_fiefService.BuildingsList[x].BuildsCollection[y]);
                            model.FinishedBuildings.Add(_fiefService.BuildingsList[x].BuildsCollection[y].Building);
                            _fiefService.BuildingsList[x].BuildsCollection.RemoveAt(y);
                        }
                    }

                    model.StoneworkersBase += _fiefService.BuildingsList[x].StoneworkersBase;
                    model.SmithsBase += _fiefService.BuildingsList[x].SmithsBase;
                    model.WoodworkersBase += _fiefService.BuildingsList[x].WoodworkersDaysWork;

                    #endregion
                    #region EmployeesModule

                    model.EmployeesBaseCost += _fiefService.EmployeesList[x].TotalBase;
                    model.EmployeesLuxuryCost += _fiefService.EmployeesList[x].TotalLuxury;

                    #endregion
                    #region ExpensesModule

                    model.ResidentAdults = _fiefService.ExpensesList[x].ResidentAdults;
                    model.ResidentAdultBaseCost = _fiefService.ExpensesList[x].ResidentAdultsBase;
                    model.ResidentAdultLuxuryCost = _fiefService.ExpensesList[x].ResidentAdultsLuxury;
                    model.ResidentChildren = _fiefService.ExpensesList[x].ResidentChildren;
                    model.ResidentChildrenBaseCost = _fiefService.ExpensesList[x].ResidentChildrenBase;
                    model.ResidentChildrenLuxuryCost = _fiefService.ExpensesList[x].ResidentChildrenLuxury;
                    model.StableRidinghorses = _fiefService.ExpensesList[x].StableRidingHorses;
                    model.StableRidinghorsesBaseCost = _fiefService.ExpensesList[x].StableRidingHorsesBase;
                    model.StableWarhorses = _fiefService.ExpensesList[x].StableWarHorses;
                    model.StableWarhorsesBaseCost = _fiefService.ExpensesList[x].StableWarHorsesBase;

                    if (_fiefService.ExpensesList[x].FeedingPoor)
                    {
                        model.FeedingThePoorBaseCost = _fiefService.ExpensesList[x].FeedingPoorBase;

                        int loyalty = _baseService.ConvertToNumeric(_fiefService.InformationList[x].Loyalty);
                        if (loyalty < 20)
                        {
                            loyalty++;
                            _fiefService.InformationList[x].Loyalty = _baseService.ConvertToT6(loyalty);
                        }
                    }
                    else
                    {
                        int loyalty = _baseService.ConvertToNumeric(_fiefService.InformationList[x].Loyalty);
                        loyalty = loyalty - 2;
                        if (loyalty < 4)
                        {
                            loyalty = 4;
                        }
                        _fiefService.InformationList[x].Loyalty = _baseService.ConvertToT6(loyalty);
                    }

                    if (_fiefService.ExpensesList[x].FeedingDayworkers)
                    {
                        model.FeedingDayworkersBaseCost = _fiefService.ExpensesList[x].FeedingDayworkersBase;
                    }
                    else
                    {
                        int loyalty = _baseService.ConvertToNumeric(_fiefService.InformationList[x].Loyalty);
                        loyalty = loyalty - 4;
                        if (loyalty < 4)
                        {
                            loyalty = 4;
                        }
                        _fiefService.InformationList[x].Loyalty = _baseService.ConvertToT6(loyalty);
                    }

                    model.UpkeepManorBaseCost = _fiefService.ExpensesList[x].ManorMaintenanceBase;
                    
                    if (_fiefService.ExpensesList[x].ImproveRoads)
                    {
                        model.UpgradingRoadsBaseCost = _fiefService.ExpensesList[x].ImproveRoadsBase;
                        model.UpgradingRoadsStoneCost = _fiefService.ExpensesList[x].ImproveRoadsStone;

                        switch (_fiefService.InformationList[x].Roads)
                        {
                            case "Stigar":
                                _fiefService.InformationList[x].Roads = "Väg";
                                break;

                            case "Väg":
                                _fiefService.InformationList[x].Roads = "Stenlagd väg";
                                break;

                            case "Stenlagd väg":
                                _fiefService.InformationList[x].Roads = "Kunglig landsväg";
                                break;

                            default:
                                Console.WriteLine("ERROR IN END OF YEAR UPGRADE ROADS");
                                break;
                        }
                    }

                    model.FeastBaseCost += _fiefService.ExpensesList[x].FeastsBase;
                    model.FeastLuxuryCost += _fiefService.ExpensesList[x].FeastsLuxury;
                    model.PeopleFeastBaseCost += _fiefService.ExpensesList[x].PeopleFeastsBase;
                    model.PeopleFeastLuxuryCost += _fiefService.ExpensesList[x].PeopleFeastsLuxury;
                    model.ReligiousFeastsSilverCost += _fiefService.ExpensesList[x].ReligiousFeastsSilver;
                    model.ReligiousFeastsBaseCost += _fiefService.ExpensesList[x].ReligiousFeastsBase;
                    model.ReligiousFeastsLuxuryCost += _fiefService.ExpensesList[x].ReligiousFeastsLuxury;
                    model.TourneySilverCost += _fiefService.ExpensesList[x].TournamentsSilver;
                    model.TourneyBaseCost += _fiefService.ExpensesList[x].TournamentsBase;
                    model.TourneyLuxuryCost += _fiefService.ExpensesList[x].TournamentsLuxury;
                    model.OtherSilverCost += _fiefService.ExpensesList[x].OthersSilver;
                    model.OtherBaseCost += _fiefService.ExpensesList[x].OthersBase;
                    model.OtherLuxuryCost += _fiefService.ExpensesList[x].OthersLuxury;

                    #endregion
                    #region IncomeModule

                    for (int y = 0; y < DataModel.IncomeListFief[x].IncomeCollection.Count; y++)
                    {
                        EndOfYearReportIncomeModel temp = new EndOfYearReportIncomeModel();
                        
                        if (DataModel.IncomeListFief[x].IncomeCollection[y].Result != "-")
                        {
                            temp.ResultBase = Convert.ToInt32(DataModel.IncomeListFief[x].IncomeCollection[y].Result);
                        }
                        else
                        {
                            temp.ResultBase = 0;
                        }
                    }

                    #endregion
                    #region MinesModule

                    for (int y = 0; y < DataModel.IncomeListFief[x].MinesCollection.Count; y++)
                    {
                        if (DataModel.IncomeListFief[x].MinesCollection[y].Result != "-")
                        {
                            model.IncomesList.Add(new EndOfYearReportIncomeModel()
                            {
                                Income = DataModel.IncomeListFief[x].MinesCollection[y].MineType,
                                ResultSilver = Convert.ToInt32(DataModel.IncomeListFief[x].MinesCollection[y].Result)
                            });
                        }
                        else
                        {
                            model.IncomesList.Add(new EndOfYearReportIncomeModel()
                            {
                                Income = DataModel.IncomeListFief[x].MinesCollection[y].MineType,
                                ResultSilver = 0
                            });
                        }
                    }

                    #endregion
                    #region PortModule

                    if (DataModel.IncomeListFief[x].Shipyard.Shipyard != "")
                    {
                        model.Shipyard = DataModel.IncomeListFief[x].Shipyard.Shipyard;
                        if (DataModel.IncomeListFief[x].Shipyard.Result != "-")
                        {
                            model.ShipyardIncome = Convert.ToInt32(DataModel.IncomeListFief[x].Shipyard.Result);
                        }
                        else
                        {
                            model.ShipyardIncome = 0;
                        }
                    }

                    #endregion
                    #region SubsidiaryModule

                    for (int y = 0; y < DataModel.IncomeListFief[x].SubsidiariesCollection.Count; y++)
                    {
                        EndOfYearReportIncomeModel temp = new EndOfYearReportIncomeModel();
                        if (DataModel.IncomeListFief[x].SubsidiariesCollection[y].ResultSilver != "-")
                        {
                            temp.ResultSilver = Convert.ToInt32(DataModel.IncomeListFief[x].SubsidiariesCollection[y].ResultSilver);
                        }
                        else
                        {
                            temp.ResultSilver = 0;
                        }

                        if (DataModel.IncomeListFief[x].SubsidiariesCollection[y].ResultBase != "-")
                        {
                            temp.ResultBase = Convert.ToInt32(DataModel.IncomeListFief[x].SubsidiariesCollection[y].ResultBase);
                        }
                        else
                        {
                            temp.ResultBase = 0;
                        }

                        if (DataModel.IncomeListFief[x].SubsidiariesCollection[y].ResultLuxury != "-")
                        {
                            temp.ResultLuxury = Convert.ToInt32(DataModel.IncomeListFief[x].SubsidiariesCollection[y].ResultLuxury);
                        }
                        else
                        {
                            temp.ResultLuxury = 0;
                        }

                        temp.ResultStone = 0;
                        temp.ResultWood = 0;
                        temp.ResultIron = 0;

                        model.SubsidiariesList.Add(temp);
                    }

                    #endregion
                    #region TradeModule



                    #endregion
                }

                _fiefService.Year++;
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
