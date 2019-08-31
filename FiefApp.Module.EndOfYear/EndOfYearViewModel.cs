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
using System.IO;
using System.Linq;

namespace FiefApp.Module.EndOfYear
{
    public class EndOfYearViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IEndOfYearService _endOfYearService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IFiefService _fiefService;
        private readonly ISupplyService _supplyService;

        public EndOfYearViewModel(
            IBaseService baseService,
            IEndOfYearService endOfYearService,
            IEventAggregator eventAggregator,
            IFiefService fiefService,
            ISupplyService supplyService
            ) : base(baseService)
        {
            _baseService = baseService;
            _endOfYearService = endOfYearService;
            _eventAggregator = eventAggregator;
            _fiefService = fiefService;
            _supplyService = supplyService;

            TabName = "Bokslut";

            EndOfYearCancelCommand = new DelegateCommand(ExecuteEndOfYearCancelCommand);
            CompleteEndOfYearCommand = new DelegateCommand(ExecuteCompleteEndOfYearCommand);
            EndOfYearOkEventHandler = new CustomDelegateCommand(ExecuteEndOfYearOkEventHandler, o => true);
            EndOfYearConstructingSubsidiaryEventHandler = new CustomDelegateCommand(ExecuteEndOfYearConstructingSubsidiaryEventHandler, o => true);

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
            List<bool> tempList = new List<bool>();
            switch (e.Action)
            {
                case "Felling":
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

                        if (e.Result != "-")
                        {
                            DataModel.IncomeListFief[x].FellingModel.FellingWood = Convert.ToInt32(e.Result);
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

                case "Industry":
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

                case "Mine":
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

                case "Quarry":
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

                case "Shipyard":
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

                case "Subsidiary":
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

                case "Development":
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

                case "Population":
                    DataModel.IncomeListFief[e.Id - 1].PopulationOk = e.Ok;
                    DataModel.IncomeListFief[e.Id - 1].AmorNumeric = e.Amor;
                    DataModel.IncomeListFief[e.Id - 1].PopulationModel.AddPopulation = e.AddPopulation;

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

                case "Taxes":
                    DataModel.IncomeListFief[e.Id - 1].TaxesOk = e.Ok;
                    DataModel.IncomeListFief[e.Id - 1].TaxFactor = Convert.ToInt32(e.Result);

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

        #endregion
        #region CustomDelegateCommand : EndOfYearConstructingSubsidiaryEventHandler

        public CustomDelegateCommand EndOfYearConstructingSubsidiaryEventHandler { get; set; }
        private void ExecuteEndOfYearConstructingSubsidiaryEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is EndOfYearConstructingSubsidiaryEventArgs e))
            {
                return;
            }

            e.Handled = true;

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

                for (int y = 0; y < DataModel.IncomeListFief[x].ConstructingCollection.Count; y++)
                {
                    if (DataModel.IncomeListFief[x].ConstructingCollection[y].Id == e.Id)
                    {
                        DataModel.IncomeListFief[x].ConstructingCollection[y].Succeeded = e.Succeeded;
                        DataModel.IncomeListFief[x].ConstructingCollection[y].DaysWorkBuild += e.DaysWorkMod;
                        DataModel.IncomeListFief[x].ConstructingCollection[y].Quality = e.Quality;
                        DataModel.IncomeListFief[x].ConstructingCollection[y].DevelopmentLevel = e.DevelopmentLevel;
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
        }

        #endregion
        #region DelegateCommand : CompleteEndOfYearCommand

        public DelegateCommand CompleteEndOfYearCommand { get; set; }
        private void ExecuteCompleteEndOfYearCommand()
        {
            //if (DataModel.CheckIfAllRollsHaveBeenMade())
            //{
            DataModel.EnableButton = false;
            bool checkStewardsDeaths = true;
            List<EndOfYearReportModel> reports = new List<EndOfYearReportModel>();
            string str = $"år: {_fiefService.Year}" + Environment.NewLine + Environment.NewLine;
            int silver = 0;
            int bas = 0;
            int lyx = 0;
            int stone = 0;
            int wood = 0;
            int iron = 0;

            for (int x = 0; x < DataModel.IncomeListFief.Count; x++)
            {
                EndOfYearReportModel model = new EndOfYearReportModel();

                #region BoatbuildingModule

                for (int y = _fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection.Count - 1; y > -1; y--)
                {
                    if (_fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y].BuildTimeInDaysAll < 365)
                    {
                        for (int z = 1; z <= _fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y].Amount; z++)
                        {
                            _fiefService.PortsList[x + 1].BoatsCollection.Add(_fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y]);
                            model.FinishedBoats.Add(_fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y].BoatType);
                        }

                        model.FinishedBoatsSilverCost += _fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y].CostWhenFinishedSilver;
                        silver -= _fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y].CostWhenFinishedSilver;
                        _fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection.RemoveAt(y);
                    }
                    else
                    {
                        int i = Convert.ToInt32(Math.Floor(365M / _fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y].BuildTimeInDays));

                        if (i > _fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y].Amount)
                        {
                            for (int z = 1; z <= _fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y].Amount; z++)
                            {
                                _fiefService.PortsList[x + 1].BoatsCollection.Add(_fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y]);
                                model.FinishedBoats.Add(_fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y].BoatType);
                            }

                            model.FinishedBoatsSilverCost += _fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y].CostWhenFinishedSilver;
                            silver -= _fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y].CostWhenFinishedSilver;
                            _fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection.RemoveAt(y);
                        }
                        else
                        {
                            _fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y].Amount -= i;
                            model.FinishedBoatsSilverCost += _fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y].CostWhenFinishedSilver / i;
                            while (i > 0)
                            {
                                _fiefService.PortsList[x + 1].BoatsCollection.Add(_fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y]);
                                model.FinishedBoats.Add(_fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection[y].BoatType);
                                i--;
                            }
                            _fiefService.BoatbuildingList[x + 1].BoatsBuildingCollection.RemoveAt(y);
                        }
                    }
                }

                model.BoatbuildersSilverCost += _fiefService.BoatbuildingList[x + 1].BoatBuildersCollection.Count * 1800;
                silver -= _fiefService.BoatbuildingList[x + 1].BoatBuildersCollection.Count * 1800;

                #endregion
                #region BuildingsModule

                for (int y = 0; y < _fiefService.BuildingsList[x + 1].BuildsCollection.Count; y++)
                {
                    if (_fiefService.BuildingsList[x + 1].BuildsCollection[y].BuilderId > 0)
                    {
                        if (_fiefService.BuildingsList[x + 1].BuildsCollection[y].StoneThisYear == _fiefService.BuildingsList[x + 1].BuildsCollection[y].StoneNeededThisYear)
                        {
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftStonework -= _fiefService.BuildingsList[x + 1].BuildsCollection[y].StoneworkThisYear;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftStone -= _fiefService.BuildingsList[x + 1].BuildsCollection[y].StoneThisYear;
                            model.BuildingsStone += _fiefService.BuildingsList[x + 1].BuildsCollection[y].StoneThisYear;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].StoneworkThisYear = 0;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].StoneThisYear = 0;
                            stone -= _fiefService.BuildingsList[x + 1].BuildsCollection[y].StoneThisYear;
                        }
                        else
                        {
                            decimal factor = (decimal)_fiefService.BuildingsList[x + 1].BuildsCollection[y].StoneThisYear / _fiefService.BuildingsList[x + 1].BuildsCollection[y].StoneNeededThisYear;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftStonework -= Convert.ToInt32(Math.Floor(factor * _fiefService.BuildingsList[x + 1].BuildsCollection[y].StoneworkThisYear));
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftStone -= _fiefService.BuildingsList[x + 1].BuildsCollection[y].StoneThisYear;
                            model.BuildingsStone += _fiefService.BuildingsList[x + 1].BuildsCollection[y].StoneThisYear;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].StoneworkThisYear = 0;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].StoneThisYear = 0;
                            stone -= _fiefService.BuildingsList[x + 1].BuildsCollection[y].StoneThisYear;
                        }

                        if (_fiefService.BuildingsList[x + 1].BuildsCollection[y].WoodThisYear == _fiefService.BuildingsList[x + 1].BuildsCollection[y].WoodNeededThisYear)
                        {
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftWoodwork -= _fiefService.BuildingsList[x + 1].BuildsCollection[y].WoodworkThisYear;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftWood -= _fiefService.BuildingsList[x + 1].BuildsCollection[y].WoodThisYear;
                            model.BuildingsWood += _fiefService.BuildingsList[x + 1].BuildsCollection[y].WoodThisYear;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].WoodworkThisYear = 0;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].WoodThisYear = 0;
                            wood -= _fiefService.BuildingsList[x + 1].BuildsCollection[y].WoodThisYear;
                        }
                        else
                        {
                            decimal factor = (decimal)_fiefService.BuildingsList[x + 1].BuildsCollection[y].WoodThisYear / _fiefService.BuildingsList[x + 1].BuildsCollection[y].WoodNeededThisYear;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftWoodwork -= Convert.ToInt32(Math.Floor(factor * _fiefService.BuildingsList[x + 1].BuildsCollection[y].WoodworkThisYear));
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftWood -= _fiefService.BuildingsList[x + 1].BuildsCollection[y].WoodThisYear;
                            model.BuildingsWood += _fiefService.BuildingsList[x + 1].BuildsCollection[y].WoodThisYear;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].WoodworkThisYear = 0;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].WoodThisYear = 0;
                            wood -= _fiefService.BuildingsList[x + 1].BuildsCollection[y].WoodThisYear;
                        }

                        if (_fiefService.BuildingsList[x + 1].BuildsCollection[y].IronThisYear == _fiefService.BuildingsList[x + 1].BuildsCollection[y].IronNeededThisYear)
                        {
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftSmithswork -= _fiefService.BuildingsList[x + 1].BuildsCollection[y].SmithsworkThisYear;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftIron -= _fiefService.BuildingsList[x + 1].BuildsCollection[y].IronThisYear;
                            model.BuildingsIron += _fiefService.BuildingsList[x + 1].BuildsCollection[y].IronThisYear;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].SmithsworkThisYear = 0;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].IronThisYear = 0;
                            iron -= _fiefService.BuildingsList[x + 1].BuildsCollection[y].IronThisYear;
                        }
                        else
                        {
                            decimal factor = (decimal)_fiefService.BuildingsList[x + 1].BuildsCollection[y].IronThisYear / _fiefService.BuildingsList[x + 1].BuildsCollection[y].IronNeededThisYear;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftSmithswork -= Convert.ToInt32(Math.Floor(factor * _fiefService.BuildingsList[x + 1].BuildsCollection[y].SmithsworkThisYear));
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftIron -= _fiefService.BuildingsList[x + 1].BuildsCollection[y].IronThisYear;
                            model.BuildingsIron += _fiefService.BuildingsList[x + 1].BuildsCollection[y].IronThisYear;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].SmithsworkThisYear = 0;
                            _fiefService.BuildingsList[x + 1].BuildsCollection[y].IronThisYear = 0;
                            iron -= _fiefService.BuildingsList[x + 1].BuildsCollection[y].IronThisYear;
                        }
                    }
                }

                for (int y = 0; y < _fiefService.BuildingsList[x + 1].BuildingsCollection.Count; y++)
                {
                    model.BuildingsUpkeepBase += Convert.ToInt32(Math.Ceiling(_fiefService.BuildingsList[x + 1].BuildingsCollection[y].Upkeep * _fiefService.BuildingsList[x + 1].BuildingsCollection[y].Amount));
                }

                for (int y = _fiefService.BuildingsList[x + 1].BuildsCollection.Count - 1; y > -1; y--)
                {
                    if (_fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftSmithswork <= 0
                        && _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftStonework <= 0
                        && _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftWoodwork <= 0
                        && _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftStone <= 0
                        && _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftWood <= 0
                        && _fiefService.BuildingsList[x + 1].BuildsCollection[y].LeftIron <= 0)
                    {
                        _fiefService.BuildingsList[x + 1].BuildingsCollection.Add(_fiefService.BuildingsList[x + 1].BuildsCollection[y]);
                        model.FinishedBuildings.Add(_fiefService.BuildingsList[x + 1].BuildsCollection[y].Building);
                        _fiefService.BuildingsList[x + 1].BuildsCollection.RemoveAt(y);
                    }
                }

                model.StoneworkersBase += _fiefService.BuildingsList[x + 1].StoneworkersBase;
                model.SmithsBase += _fiefService.BuildingsList[x + 1].SmithsBase;
                model.WoodworkersBase += _fiefService.BuildingsList[x + 1].WoodworkersBase;
                bas -= _fiefService.BuildingsList[x + 1].StoneworkersBase;
                bas -= _fiefService.BuildingsList[x + 1].SmithsBase;
                bas -= _fiefService.BuildingsList[x + 1].WoodworkersBase;

                #endregion
                #region EmployeesModule

                model.EmployeesBaseCost += _fiefService.EmployeesList[x + 1].TotalBase;
                model.EmployeesLuxuryCost += _fiefService.EmployeesList[x + 1].TotalLuxury;
                bas -= _fiefService.EmployeesList[x + 1].TotalBase;
                bas -= _fiefService.EmployeesList[x + 1].TotalLuxury;

                #endregion
                #region ExpensesModule

                model.ResidentAdults = _fiefService.ExpensesList[x + 1].ResidentAdults;
                model.ResidentAdultBaseCost = _fiefService.ExpensesList[x + 1].ResidentAdultsBase;
                model.ResidentAdultLuxuryCost = _fiefService.ExpensesList[x + 1].ResidentAdultsLuxury;
                model.ResidentChildren = _fiefService.ExpensesList[x + 1].ResidentChildren;
                model.ResidentChildrenBaseCost = _fiefService.ExpensesList[x + 1].ResidentChildrenBase;
                model.ResidentChildrenLuxuryCost = _fiefService.ExpensesList[x + 1].ResidentChildrenLuxury;
                model.StableRidinghorses = _fiefService.ExpensesList[x + 1].StableRidingHorses;
                model.StableRidinghorsesBaseCost = _fiefService.ExpensesList[x + 1].StableRidingHorsesBase;
                model.StableWarhorses = _fiefService.ExpensesList[x + 1].StableWarHorses;
                model.StableWarhorsesBaseCost = _fiefService.ExpensesList[x + 1].StableWarHorsesBase;
                bas -= _fiefService.ExpensesList[x + 1].ResidentAdultsBase;
                bas -= _fiefService.ExpensesList[x + 1].ResidentChildrenBase;
                bas -= _fiefService.ExpensesList[x + 1].StableRidingHorsesBase;
                bas -= _fiefService.ExpensesList[x + 1].StableWarHorsesBase;
                lyx -= _fiefService.ExpensesList[x + 1].ResidentAdultsLuxury;
                lyx -= _fiefService.ExpensesList[x + 1].ResidentChildrenLuxury;

                if (_fiefService.ExpensesList[x + 1].FeedingPoor)
                {
                    model.FeedingThePoorBaseCost = _fiefService.ExpensesList[x + 1].FeedingPoorBase;
                    bas -= _fiefService.ExpensesList[x + 1].FeedingPoorBase;

                    int loyalty = _baseService.ConvertToNumeric(_fiefService.InformationList[x + 1].Loyalty);
                    if (loyalty < 20)
                    {
                        loyalty++;
                        _fiefService.InformationList[x + 1].Loyalty = _baseService.ConvertToT6(loyalty);
                    }
                }
                else
                {
                    int loyalty = _baseService.ConvertToNumeric(_fiefService.InformationList[x + 1].Loyalty);
                    loyalty = loyalty - 2;
                    if (loyalty < 4)
                    {
                        loyalty = 4;
                    }
                    _fiefService.InformationList[x + 1].Loyalty = _baseService.ConvertToT6(loyalty);
                }

                if (_fiefService.ExpensesList[x + 1].FeedingDayworkers)
                {
                    model.FeedingDayworkersBaseCost = _fiefService.ExpensesList[x + 1].FeedingDayworkersBase;
                    bas -= _fiefService.ExpensesList[x + 1].FeedingDayworkersBase;
                }
                else
                {
                    int loyalty = _baseService.ConvertToNumeric(_fiefService.InformationList[x + 1].Loyalty);
                    loyalty = loyalty - 4;
                    if (loyalty < 4)
                    {
                        loyalty = 4;
                    }
                    _fiefService.InformationList[x + 1].Loyalty = _baseService.ConvertToT6(loyalty);
                }

                model.UpkeepManorBaseCost = _fiefService.ExpensesList[x + 1].ManorMaintenanceBase;
                bas -= _fiefService.ExpensesList[x + 1].ManorMaintenanceBase;

                if (_fiefService.ExpensesList[x + 1].ImproveRoads)
                {
                    model.UpgradingRoadsBaseCost = _fiefService.ExpensesList[x + 1].ImproveRoadsBase;
                    model.UpgradingRoadsStoneCost = _fiefService.ExpensesList[x + 1].ImproveRoadsStone;
                    bas -= _fiefService.ExpensesList[x + 1].ImproveRoadsBase;
                    stone -= _fiefService.ExpensesList[x + 1].ImproveRoadsStone;

                    switch (_fiefService.InformationList[x + 1].Roads)
                    {
                        case "Stigar":
                            _fiefService.InformationList[x + 1].Roads = "Väg";
                            break;

                        case "Väg":
                            _fiefService.InformationList[x + 1].Roads = "Stenlagd väg";
                            break;

                        case "Stenlagd väg":
                            _fiefService.InformationList[x + 1].Roads = "Kunglig landsväg";
                            break;

                        default:
                            Console.WriteLine("ERROR IN END OF YEAR UPGRADE ROADS");
                            break;
                    }
                }

                model.FeastBaseCost += _fiefService.ExpensesList[x + 1].FeastsBase;
                model.FeastLuxuryCost += _fiefService.ExpensesList[x + 1].FeastsLuxury;
                model.PeopleFeastBaseCost += _fiefService.ExpensesList[x + 1].PeopleFeastsBase;
                model.PeopleFeastLuxuryCost += _fiefService.ExpensesList[x + 1].PeopleFeastsLuxury;
                model.ReligiousFeastsSilverCost += _fiefService.ExpensesList[x + 1].ReligiousFeastsSilver;
                model.ReligiousFeastsBaseCost += _fiefService.ExpensesList[x + 1].ReligiousFeastsBase;
                model.ReligiousFeastsLuxuryCost += _fiefService.ExpensesList[x + 1].ReligiousFeastsLuxury;
                model.TourneySilverCost += _fiefService.ExpensesList[x + 1].TournamentsSilver;
                model.TourneyBaseCost += _fiefService.ExpensesList[x + 1].TournamentsBase;
                model.TourneyLuxuryCost += _fiefService.ExpensesList[x + 1].TournamentsLuxury;
                model.OtherSilverCost += _fiefService.ExpensesList[x + 1].OthersSilver;
                model.OtherBaseCost += _fiefService.ExpensesList[x + 1].OthersBase;
                model.OtherLuxuryCost += _fiefService.ExpensesList[x + 1].OthersLuxury;
                bas -= _fiefService.ExpensesList[x + 1].FeastsBase;
                bas -= _fiefService.ExpensesList[x + 1].PeopleFeastsBase;
                bas -= _fiefService.ExpensesList[x + 1].ReligiousFeastsBase;
                bas -= _fiefService.ExpensesList[x + 1].TournamentsBase;
                bas -= _fiefService.ExpensesList[x + 1].OthersBase;
                lyx -= _fiefService.ExpensesList[x + 1].FeastsLuxury;
                lyx -= _fiefService.ExpensesList[x + 1].PeopleFeastsLuxury;
                lyx -= _fiefService.ExpensesList[x + 1].ReligiousFeastsLuxury;
                lyx -= _fiefService.ExpensesList[x + 1].TournamentsLuxury;
                lyx -= _fiefService.ExpensesList[x + 1].OthersLuxury;
                silver -= _fiefService.ExpensesList[x + 1].ReligiousFeastsSilver;
                silver -= _fiefService.ExpensesList[x + 1].TournamentsSilver;
                silver -= _fiefService.ExpensesList[x + 1].OthersSilver;

                #endregion
                #region IncomeModule !!

                string income = $"Inkomster:{Environment.NewLine}";
                for (int y = 0; y < DataModel.IncomeListFief[x].IncomeCollection.Count; y++)
                {
                    income += $"{DataModel.IncomeListFief[x].IncomeCollection[y].Income} ";
                    if (DataModel.IncomeListFief[x].IncomeCollection[y].Result != "-")
                    {
                        income += $"{DataModel.IncomeListFief[x].IncomeCollection[y].Result}bas {Environment.NewLine}";
                        bas += Convert.ToInt32(DataModel.IncomeListFief[x].IncomeCollection[y].Result);
                    }
                    else
                    {
                        income += $"{DataModel.IncomeListFief[x].IncomeCollection[y].Result}{Environment.NewLine}";
                    }
                }

                for (int y = 0; y < DataModel.IncomeListFief[x].OtherIncomesList.Count; y++)
                {
                    string tempStr = "";
                    if (DataModel.IncomeListFief[x].OtherIncomesList.Count > 0)
                    {
                        tempStr = $"{DataModel.IncomeListFief[x].OtherIncomesList[y].Income} ";
                        if (DataModel.IncomeListFief[x].OtherIncomesList[y].Silver != -1)
                        {
                            tempStr += $"{DataModel.IncomeListFief[x].OtherIncomesList[y].Silver}silver ";
                            silver += Convert.ToInt32(DataModel.IncomeListFief[x].OtherIncomesList[y].Silver);
                        }
                        if (DataModel.IncomeListFief[x].OtherIncomesList[y].Base != -1)
                        {
                            tempStr += $"{DataModel.IncomeListFief[x].OtherIncomesList[y].Base}bas ";
                            bas += Convert.ToInt32(DataModel.IncomeListFief[x].OtherIncomesList[y].Base);
                        }
                        if (DataModel.IncomeListFief[x].OtherIncomesList[y].Luxury != -1)
                        {
                            tempStr += $"{DataModel.IncomeListFief[x].OtherIncomesList[y].Luxury}lyx ";
                            lyx += Convert.ToInt32(DataModel.IncomeListFief[x].OtherIncomesList[y].Luxury);
                        }
                        if (DataModel.IncomeListFief[x].OtherIncomesList[y].Iron != -1)
                        {
                            tempStr += $"{DataModel.IncomeListFief[x].OtherIncomesList[y].Iron}järn ";
                            iron += Convert.ToInt32(DataModel.IncomeListFief[x].OtherIncomesList[y].Iron);
                        }
                        if (DataModel.IncomeListFief[x].OtherIncomesList[y].Stone != -1)
                        {
                            tempStr += $"{DataModel.IncomeListFief[x].OtherIncomesList[y].Stone}sten ";
                            stone += Convert.ToInt32(DataModel.IncomeListFief[x].OtherIncomesList[y].Stone);
                        }
                        if (DataModel.IncomeListFief[x].OtherIncomesList[y].Wood != -1)
                        {
                            tempStr += $"{DataModel.IncomeListFief[x].OtherIncomesList[y].Wood}timmer ";
                            wood += Convert.ToInt32(DataModel.IncomeListFief[x].OtherIncomesList[y].Wood);
                        }
                        tempStr = $"{Environment.NewLine}";
                    }
                    income += tempStr;
                }

                #endregion
                #region MinesModule

                if (DataModel.IncomeListFief[x].IncomeCollection != null)
                {
                    for (int y = 0; y < DataModel.IncomeListFief[x].MinesCollection.Count; y++)
                    {
                        if (DataModel.IncomeListFief[x].MinesCollection != null)
                        {
                            if (DataModel.IncomeListFief[x].MinesCollection[y].Result != "-")
                            {
                                model.MinesList.Add(new EndOfYearReportIncomeModel()
                                {
                                    Income = DataModel.IncomeListFief[x].MinesCollection[y].MineType,
                                    ResultSilver = Convert.ToInt32(DataModel.IncomeListFief[x].MinesCollection[y].Result)
                                });
                                silver += Convert.ToInt32(DataModel.IncomeListFief[x].MinesCollection[y].Result);
                            }
                            else
                            {
                                model.MinesList.Add(new EndOfYearReportIncomeModel()
                                {
                                    Income = DataModel.IncomeListFief[x].MinesCollection[y].MineType,
                                    ResultSilver = 0
                                });
                            }
                        }
                    }
                }


                if (DataModel.IncomeListFief[x].QuarriesCollection != null)
                {
                    for (int y = 0; y < DataModel.IncomeListFief[x].QuarriesCollection.Count; y++)
                    {
                        if (DataModel.IncomeListFief[x].QuarriesCollection != null)
                        {
                            if (DataModel.IncomeListFief[x].QuarriesCollection[y].Result != "-")
                            {
                                model.MinesList.Add(new EndOfYearReportIncomeModel()
                                {
                                    Income = DataModel.IncomeListFief[x].QuarriesCollection[y].QuarryType,
                                    ResultStone = Convert.ToInt32(DataModel.IncomeListFief[x].QuarriesCollection[y].Result)
                                });
                                stone += Convert.ToInt32(DataModel.IncomeListFief[x].QuarriesCollection[y].Result);
                            }
                            else
                            {
                                model.MinesList.Add(new EndOfYearReportIncomeModel()
                                {
                                    Income = DataModel.IncomeListFief[x].QuarriesCollection[y].QuarryType,
                                    ResultStone = 0
                                });
                            }
                        }
                    }
                    bas -= DataModel.IncomeListFief[x].QuarriesCollection.Sum(o => o.Upkeep);
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
                        silver += Convert.ToInt32(DataModel.IncomeListFief[x].SubsidiariesCollection[y].ResultSilver);
                    }
                    else
                    {
                        temp.ResultSilver = 0;
                    }

                    if (DataModel.IncomeListFief[x].SubsidiariesCollection[y].ResultBase != "-")
                    {
                        temp.ResultBase = Convert.ToInt32(DataModel.IncomeListFief[x].SubsidiariesCollection[y].ResultBase);
                        bas += Convert.ToInt32(DataModel.IncomeListFief[x].SubsidiariesCollection[y].ResultBase);
                    }
                    else
                    {
                        temp.ResultBase = 0;
                    }

                    if (DataModel.IncomeListFief[x].SubsidiariesCollection[y].ResultLuxury != "-")
                    {
                        temp.ResultLuxury = Convert.ToInt32(DataModel.IncomeListFief[x].SubsidiariesCollection[y].ResultLuxury);
                        lyx += Convert.ToInt32(DataModel.IncomeListFief[x].SubsidiariesCollection[y].ResultLuxury);
                    }
                    else
                    {
                        temp.ResultLuxury = 0;
                    }

                    temp.ResultStone = 0;
                    temp.ResultWood = 0;
                    temp.ResultIron = 0;
                    temp.Income = DataModel.IncomeListFief[x].SubsidiariesCollection[x + 1].Subsidiary;

                    model.SubsidiariesList.Add(temp);
                }

                for (int y = 0; y < DataModel.IncomeListFief[x].ConstructingCollection.Count; y++)
                {
                    if (DataModel.IncomeListFief[x].ConstructingCollection[y].DaysWorkBuild == DataModel.IncomeListFief[x].ConstructingCollection[y].DaysWorkThisYear
                        && DataModel.IncomeListFief[x].ConstructingCollection[y].StewardId > 0
                        && DataModel.IncomeListFief[x].ConstructingCollection[y].Succeeded)
                    {
                        for (int i = 0; i < _fiefService.SubsidiaryList[x + 1].ConstructingCollection.Count; i++)
                        {
                            if (_fiefService.SubsidiaryList[x + 1].ConstructingCollection[i].Id == DataModel.IncomeListFief[x].ConstructingCollection[y].Id)
                            {
                                DataModel.IncomeListFief[x].ConstructingCollection[y].DaysWorkThisYear = 0;
                                _fiefService.SubsidiaryList[x + 1].SubsidiaryCollection.Add(DataModel.IncomeListFief[x].ConstructingCollection[y]);
                                _fiefService.SubsidiaryList[x + 1].ConstructingCollection.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }

                #endregion
                #region TradeModule



                #endregion
                #region WeatherModule

                model.Happiness = _fiefService.WeatherList[x + 1].HappinessTotal;
                model.NumberUsedFishingBoats = _fiefService.WeatherList[x + 1].NumberUsedFishingBoats;
                model.AddedManorAcres = _fiefService.WeatherList[x + 1].LandClearing
                                        + _fiefService.WeatherList[x + 1].LandClearingOfFelling
                                        + _fiefService.WeatherList[x + 1].ClearUseless;
                model.LandClearing = _fiefService.WeatherList[x + 1].LandClearing;
                model.LandClearingOfFelling = _fiefService.WeatherList[x + 1].LandClearingOfFelling;
                model.ClearUseless = _fiefService.WeatherList[x + 1].ClearUseless;
                model.Felling = _fiefService.WeatherList[x + 1].Felling;
                model.Happiness = _fiefService.WeatherList[x + 1].HappinessTotal;

                _fiefService.ManorList[x + 1].ManorAcres += _fiefService.WeatherList[x + 1].LandClearing
                                                            + _fiefService.WeatherList[x + 1].LandClearingOfFelling
                                                            + _fiefService.WeatherList[x + 1].ClearUseless;
                _fiefService.ManorList[x + 1].ManorFelling += _fiefService.WeatherList[x + 1].Felling - _fiefService.WeatherList[x + 1].LandClearingOfFelling;
                _fiefService.ManorList[x + 1].ManorUseless -= _fiefService.WeatherList[x + 1].ClearUseless;
                _fiefService.ManorList[x + 1].ManorWoodland -= (_fiefService.WeatherList[x + 1].LandClearing
                                                                + _fiefService.WeatherList[x + 1].Felling);

                _fiefService.WeatherList[x + 1].SpringRoll = null;
                _fiefService.WeatherList[x + 1].SummerRoll = null;
                _fiefService.WeatherList[x + 1].FallRoll = null;
                _fiefService.WeatherList[x + 1].WinterRoll = null;

                _fiefService.WeatherList[x + 1].NumberUsedFishingBoats = 0;
                _fiefService.WeatherList[x + 1].LandClearing = 0;
                _fiefService.WeatherList[x + 1].LandClearingOfFelling = 0;
                _fiefService.WeatherList[x + 1].ClearUseless = 0;


                _fiefService.WeatherList[x + 1].Felling = 0;

                #endregion
                #region Population

                string population = $"Befolkning{Environment.NewLine}";
                population += $"{DataModel.IncomeListFief[x].PopulationModel.ResultPopulation} har flyttat in och fötts under året.";
                if (DataModel.IncomeListFief[x].PopulationModel.AddPopulation)
                {
                    int nrVillages = _fiefService.ManorList[x + 1].VillagesCollection.Count;

                    if (DataModel.IncomeListFief[x].PopulationModel.ResultPopulation > 59)
                    {
                        _fiefService.ManorList[x + 1].VillagesCollection.Add(new VillageModel()
                        {
                            Id = _fiefService.ManorList[x + 1].VillagesCollection.Max(o => o.Id) + 1,
                            Population = 60,
                            Serfdoms = 45,
                            Farmers = 15
                        });
                        nrVillages++;
                        population += $"En by har anlagts.{Environment.NewLine}";
                        DataModel.IncomeListFief[x].PopulationModel.ResultPopulation -= 60;
                    }
                    for (int i = 0; i < DataModel.IncomeListFief[x].PopulationModel.ResultPopulation; i++)
                    {
                        int hantverkare = 0;
                        int villageNr = _baseService.RollDie(1, nrVillages);
                        _fiefService.ManorList[x + 1].VillagesCollection[villageNr].Population++;
                        int typeVillager = _baseService.RollDie(1, 100);
                        if (typeVillager > 95)
                        {
                            _fiefService.ManorList[x + 1].VillagesCollection[villageNr].Burgess++;
                            hantverkare++;

                            int y = _baseService.RollDie(1, 100);
                            if (y < 11)
                            {
                                _fiefService.ManorList[x + 1].VillagesCollection[villageNr].Boatbuilders++;
                            }
                            else if (y < 21)
                            {
                                _fiefService.ManorList[x + 1].VillagesCollection[villageNr].Tanners++;
                            }
                            else if (y < 41)
                            {
                                _fiefService.ManorList[x + 1].VillagesCollection[villageNr].Millers++;
                            }
                            else if (y < 46)
                            {
                                _fiefService.ManorList[x + 1].VillagesCollection[villageNr].Furriers++;
                            }
                            else if (y < 51)
                            {
                                _fiefService.ManorList[x + 1].VillagesCollection[villageNr].Tailors++;
                            }
                            else if (y < 71)
                            {
                                _fiefService.ManorList[x + 1].VillagesCollection[villageNr].Carpenters++;
                            }
                            else if (y < 91)
                            {
                                _fiefService.ManorList[x + 1].VillagesCollection[villageNr].Innkeepers++;
                            }
                        }
                        else if (typeVillager > 76)
                        {
                            _fiefService.ManorList[x + 1].VillagesCollection[villageNr].Farmers++;
                        }
                        else
                        {
                            _fiefService.ManorList[x + 1].VillagesCollection[villageNr].Serfdoms++;
                        }

                        if (hantverkare > 0)
                        {
                            population += $"{hantverkare} har flyttat in i förläningen.{Environment.NewLine}";
                        }
                    }
                }

                #endregion
                #region People

                List<int> deaths = new List<int>();
                List<int> stewardDeaths = new List<int>();

                for (int y = 0; y < _fiefService.ManorList[x + 1].ResidentsCollection.Count; y++)
                {
                    int chanceOfDeath = 0;
                    decimal i = (decimal)_fiefService.ManorList[x + 1].ResidentsCollection[y].Age / 100;
                    if (i > 0)
                    {
                        decimal j = Convert.ToDecimal(Math.Pow((double)i, 2));
                        chanceOfDeath = Convert.ToInt32(Math.Floor(j * 50));

                        if (_baseService.RollDie(1, 100) <= chanceOfDeath)
                        {
                            deaths.Add(_fiefService.ManorList[x + 1].ResidentsCollection[y].Id);
                        }
                    }
                }

                if (checkStewardsDeaths)
                {
                    checkStewardsDeaths = false;
                    for (int y = 0; y < _fiefService.StewardsDataModel.StewardsCollection.Count; y++)
                    {
                        if (_fiefService.StewardsDataModel.StewardsCollection[y].Age > 34)
                        {
                            int chanceOfDeath = 0;
                            decimal i = (decimal)_fiefService.StewardsDataModel.StewardsCollection[y].Age / 100;
                            if (i > 0)
                            {
                                decimal j = Convert.ToDecimal(Math.Pow((double)i, 2));
                                chanceOfDeath = Convert.ToInt32(Math.Floor(j * 50));

                                if (_baseService.RollDie(1, 100) <= chanceOfDeath)
                                {
                                    stewardDeaths.Add(_fiefService.StewardsDataModel.StewardsCollection[y].Id);
                                }
                            }
                        }
                    }

                    if (stewardDeaths.Count > 0)
                    {
                        for (int i = stewardDeaths.Count - 1; i > 0; i--)
                        {
                            for (int j = 0; j < _fiefService.StewardsDataModel.StewardsCollection.Count; j++)
                            {
                                if (_fiefService.StewardsDataModel.StewardsCollection[j].Id == stewardDeaths[i])
                                {
                                    if (string.IsNullOrEmpty(model.Deaths))
                                    {
                                        model.Deaths = "Dödsfall" + Environment.NewLine + _fiefService.StewardsDataModel.StewardsCollection[j].PersonName + " dog vid " + _fiefService.StewardsDataModel.StewardsCollection[j].Age + "års ålder." + Environment.NewLine;
                                    }
                                    else
                                    {
                                        model.Deaths += _fiefService.StewardsDataModel.StewardsCollection[j].PersonName + " dog vid " + _fiefService.StewardsDataModel.StewardsCollection[j].Age + "års ålder." + Environment.NewLine;
                                    }
                                    _fiefService.StewardsDataModel.StewardsCollection.RemoveAt(j);
                                    break;
                                }
                            }
                        }
                    }
                }

                if (deaths.Count > 0)
                {
                    for (int i = deaths.Count - 1; i > 0; i--)
                    {
                        for (int j = 0; j < _fiefService.ManorList[x + 1].ResidentsCollection.Count; j++)
                        {
                            if (_fiefService.ManorList[x + 1].ResidentsCollection[j].Id == deaths[i])
                            {
                                if (string.IsNullOrEmpty(model.Deaths))
                                {
                                    model.Deaths = "Dödsfall" + Environment.NewLine + _fiefService.ManorList[x + 1].ResidentsCollection[j].PersonName + " dog vid " + _fiefService.ManorList[x + 1].ResidentsCollection[j].Age + "års ålder." + Environment.NewLine;
                                }
                                else
                                {
                                    model.Deaths += _fiefService.ManorList[x + 1].ResidentsCollection[j].PersonName + " dog vid " + _fiefService.ManorList[x + 1].ResidentsCollection[j].Age + "års ålder." + Environment.NewLine;
                                }
                                _fiefService.ManorList[x + 1].ResidentsCollection.RemoveAt(j);
                                break;
                            }
                        }
                    }
                }

                #endregion

                wood += DataModel.IncomeListFief[x].FellingModel.FellingWood;

                #region Write report to text file

                str += $"{_fiefService.InformationList[x + 1].FiefName}" + Environment.NewLine;
                str += $"Årets väder" + Environment.NewLine;
                str += $"   Vår: {_fiefService.WeatherList[x + 1].SpringRoll}({_fiefService.WeatherList[x + 1].SpringRollMod})" + Environment.NewLine;
                str += $"Sommar: {_fiefService.WeatherList[x + 1].SummerRoll}({_fiefService.WeatherList[x + 1].SummerRollMod})" + Environment.NewLine;
                str += $"  Höst: {_fiefService.WeatherList[x + 1].FallRoll}({_fiefService.WeatherList[x + 1].FallRollMod})" + Environment.NewLine;
                str += $"Vinter: {_fiefService.WeatherList[x + 1].WinterRoll}({_fiefService.WeatherList[x + 1].WinterRollMod})" + Environment.NewLine + Environment.NewLine;

                str += income;
                str += Environment.NewLine;

                if (model.SubsidiariesList != null)
                {
                    if (model.SubsidiariesList.Count > 1)
                    {
                        str += Environment.NewLine + $"Binäringar" + Environment.NewLine;
                    }
                    else
                    {
                        str += $"Binäring" + Environment.NewLine;
                    }

                    for (int i = 0; i < model.SubsidiariesList.Count; i++)
                    {
                        bool gotSomething = false;
                        str += $"{model.SubsidiariesList[i].Income} ";

                        if (model.SubsidiariesList[i].ResultSilver > 0)
                        {
                            str += $"{model.SubsidiariesList[i].ResultSilver}silver";
                            gotSomething = true;
                        }
                        if (model.SubsidiariesList[i].ResultBase > 0)
                        {
                            str += $"{model.SubsidiariesList[i].ResultBase}bas";
                            gotSomething = true;
                        }
                        if (model.SubsidiariesList[i].ResultLuxury > 0)
                        {
                            str += $"{model.SubsidiariesList[i].ResultLuxury}lyx";
                            gotSomething = true;
                        }

                        if (!gotSomething)
                        {
                            str += $"-";
                        }

                        str += Environment.NewLine;
                    }
                }

                if (!string.IsNullOrEmpty(model.Deaths))
                {
                    str += model.Deaths;
                }

                if (model.MinesList?.Count > 0)
                {
                    str += "Gruvor";
                    for (int i = 0; i < model.MinesList.Count; i++)
                    {
                        str += $"{model.MinesList[i].Income} {model.MinesList[i].ResultSilver}silver{Environment.NewLine}";
                    }
                }

                str += Environment.NewLine;

                if (model.QuarriesList?.Count > 0)
                {
                    str += "Stenbrott";
                    for (int i = 0; i < model.QuarriesList.Count; i++)
                    {
                        str += $"{model.QuarriesList[i].Income} {model.QuarriesList[i].ResultStone}sten{Environment.NewLine}";
                    }
                }

                str += population;

                #endregion

                #region End

                reports.Add(model);
                str += Environment.NewLine;
                str += Environment.NewLine;

                #endregion
            }

            str += $"TOTALT: {silver}Silver, {bas}Bas, {lyx}Lyx, {iron}Järn, {stone}Sten, {wood}Timmer.";


            _supplyService.ModifySupply(silver, bas, lyx, iron, stone, wood);
            _fiefService.Year++;
            _eventAggregator.GetEvent<EndOfYearEvent>().Publish();

            string filePath = $@"{AppDomain.CurrentDomain.BaseDirectory}/Reports/Bokslut_{_fiefService.Year - 1}.txt";
            System.IO.FileInfo file = new System.IO.FileInfo(filePath);
            file.Directory.Create();
            File.WriteAllText(file.FullName, str);
            System.Diagnostics.Process.Start(file.FullName);
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
