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
        private readonly ISettingsService _settingsService;

        public EndOfYearViewModel(
            IBaseService baseService,
            IEndOfYearService endOfYearService,
            IEventAggregator eventAggregator,
            IFiefService fiefService,
            ISupplyService supplyService,
            ISettingsService settingsService
            ) : base(baseService)
        {
            _baseService = baseService;
            _endOfYearService = endOfYearService;
            _eventAggregator = eventAggregator;
            _fiefService = fiefService;
            _supplyService = supplyService;
            _settingsService = settingsService;

            TabName = "Bokslut";

            EndOfYearCancelCommand = new DelegateCommand(ExecuteEndOfYearCancelCommand);
            CompleteEndOfYearCommand = new DelegateCommand(ExecuteCompleteEndOfYearCommand);
            EndOfYearOkEventHandler = new CustomDelegateCommand(ExecuteEndOfYearOkEventHandler, o => true);
            EndOfYearAddAcresEventHandler = new CustomDelegateCommand(ExecuteEndOfYearAddAcresEventHandler, o => true);
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

        #region CustomDelegateCommand : EndOfYearAddAcresEventHandler

        public CustomDelegateCommand EndOfYearAddAcresEventHandler { get; set; }

        private void ExecuteEndOfYearAddAcresEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is EndOfYearAddAcresEventArgs e))
            {
                return;
            }

            e.Handled = true;

            DataModel.IncomeListFief[e.FiefId - 1].Pasture = e.Pasture;
            DataModel.IncomeListFief[e.FiefId - 1].Agricultural = e.Agricultural;
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
                case "Trade":
                    for (int x = 0; x < DataModel.IncomeListFief.Count; x++)
                    {
                        if (DataModel.IncomeListFief[x].EndOfYearOkDictionary.ContainsKey(e.Id + 1000000))
                        {
                            DataModel.IncomeListFief[x].EndOfYearOkDictionary[e.Id] = e.Ok;
                        }

                        tempList.Add(!DataModel.IncomeListFief[x].EndOfYearOkDictionary.ContainsValue(false));
                        tempList.Add(DataModel.IncomeListFief[x].PopulationOk);
                        tempList.Add(DataModel.IncomeListFief[x].TaxesOk);

                        for (int i = 0; i < DataModel.IncomeListFief[x].TradeCollection.Count; i++)
                        {
                            if (e.Id == DataModel.IncomeListFief[x].TradeCollection[i].Id)
                            {
                                DataModel.IncomeListFief[x].TradeCollection[i].EndOfYearSilver = e.IncomeSilver != "-" ? int.Parse(e.IncomeSilver) : 0;

                                DataModel.IncomeListFief[x].TradeCollection[i].EndOfYearBase = e.IncomeBase != "-" ? int.Parse(e.IncomeBase) : 0;

                                DataModel.IncomeListFief[x].TradeCollection[i].EndOfYearLuxury = e.IncomeLuxury != "-" ? int.Parse(e.IncomeLuxury) : 0;

                                DataModel.IncomeListFief[x].TradeCollection[i].EndOfYearIron = e.IncomeIron != "-" ? int.Parse(e.IncomeIron) : 0;

                                DataModel.IncomeListFief[x].TradeCollection[i].EndOfYearStone = e.IncomeStone != "-" ? int.Parse(e.IncomeStone) : 0;

                                DataModel.IncomeListFief[x].TradeCollection[i].EndOfYearWood = e.IncomeWood != "-" ? int.Parse(e.IncomeWood) : 0;
                            }
                        }
                    }
                    break;

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
                    int population;
                    if (int.TryParse(e.Result, out population))
                    {
                        DataModel.IncomeListFief[e.Id - 1].PopulationModel.ResultPopulation = population;
                    }

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
                    DataModel.IncomeListFief[e.Id - 1].LoyaltyMod = e.Amor;

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
            // VALIDERA!!!
            // Har alla tunnland lagts ut på betesmark/åkermark!?

            DataModel.EnableButton = false;
            int silver = 0;
            int bas = 0;
            int lyx = 0;
            int iron = 0;
            int wood = 0;
            int stone = 0;

            string str = $"BOKSLUT ÅR {_fiefService.Year}{Environment.NewLine}{Environment.NewLine}";

            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {
                str += $"{_fiefService.InformationList[x].FiefName}{Environment.NewLine}";
                str += $"Väder:{Environment.NewLine}";
                str += $"   Vår {_fiefService.WeatherList[x].SpringRoll.ToString().PadLeft(3)} ({_fiefService.WeatherList[x].SpringRollMod}){Environment.NewLine}";
                str += $"Sommar {_fiefService.WeatherList[x].SummerRoll.ToString().PadLeft(3)} ({_fiefService.WeatherList[x].SummerRollMod}){Environment.NewLine}";
                str += $"  Höst {_fiefService.WeatherList[x].FallRoll.ToString().PadLeft(3)} ({_fiefService.WeatherList[x].FallRollMod}){Environment.NewLine}";
                str += $"Vinter {_fiefService.WeatherList[x].WinterRoll.ToString().PadLeft(3)} ({_fiefService.WeatherList[x].WinterRollMod}){Environment.NewLine}{Environment.NewLine}";

                #region Taxes/Happiness

                string incomeStr = $"Skatter:{Environment.NewLine}";

                int loyalty = ConvertToNumeric(_fiefService.InformationList[x].Loyalty);
                int amor = ConvertToNumeric(_fiefService.InformationList[x].Amor);

                loyalty += DataModel.IncomeListFief[x - 1].LoyaltyMod;

                if (_fiefService.ExpensesList[x].FeedingPoor)
                {
                    if (loyalty < 17)
                    {
                        loyalty++;
                    }
                }

                if (!_fiefService.ExpensesList[x].FeedingDayworkers)
                {
                    if (loyalty > 3)
                    {
                        loyalty -= 3;
                    }
                    else
                    {
                        loyalty = 0;
                    }
                }

                if (DataModel.IncomeListFief[x - 1].TaxFactor == 100)
                {
                    incomeStr += $"Dina undersåtar betalade sina skatter.{Environment.NewLine}";
                }
                else if (DataModel.IncomeListFief[x - 1].TaxFactor == 0)
                {
                    incomeStr += $"Dina undersåtar betalade inte sina skatter. UPPROR!{Environment.NewLine}";
                }
                else
                {
                    incomeStr += $"Dina undersåtar betalade endel av sina skatter.{Environment.NewLine}";
                }

                decimal taxFactor = (decimal)DataModel.IncomeListFief[x - 1].TaxFactor / 100;

                #endregion

                #region IncomeModule

                str += $"Inkomster:{Environment.NewLine}";
                for (int i = 0; i < DataModel.IncomeListFief[x - 1].OtherIncomesList.Count; i++)
                {
                    if (DataModel.IncomeListFief[x - 1].OtherIncomesList[i].Silver > -1)
                    {
                        int income = Convert.ToInt32(Math.Round(DataModel.IncomeListFief[x - 1].OtherIncomesList[i].Silver * taxFactor));
                        silver += income;
                        str += $"{DataModel.IncomeListFief[x - 1].OtherIncomesList[i].Income.PadRight(15)}{Convert.ToString(income).PadLeft(8)} Silver{Environment.NewLine}";
                    }
                }

                for (int i = 0; i < DataModel.IncomeListFief[x - 1].IncomeCollection.Count; i++)
                {
                    int temp;
                    if (int.TryParse(DataModel.IncomeListFief[x - 1].IncomeCollection[i].Result, out temp))
                    {
                        bas += temp;
                        str += $"{DataModel.IncomeListFief[x - 1].IncomeCollection[i].Income.PadRight(15)} {Convert.ToString(DataModel.IncomeListFief[x - 1].IncomeCollection[i].Result.PadLeft(7))} Bas{Environment.NewLine}";
                    }
                }

                str += Environment.NewLine;

                #endregion

                #region SubsidiaryModule

                str += $"Binäringar:{Environment.NewLine}";
                for (int i = 0; i < DataModel.IncomeListFief[x - 1].SubsidiariesCollection.Count; i++)
                {
                    int tmpSilver;
                    int tmpBas;
                    int tmpLyx;

                    str += $"{DataModel.IncomeListFief[x - 1].SubsidiariesCollection[i].Subsidiary.PadRight(15)} ";
                    if (int.TryParse(DataModel.IncomeListFief[x - 1].SubsidiariesCollection[i].ResultSilver, out tmpSilver))
                    {
                        silver += tmpSilver;
                        str += $"{Convert.ToString(tmpSilver).PadLeft(7)} Silver";
                    }
                    if (int.TryParse(DataModel.IncomeListFief[x - 1].SubsidiariesCollection[i].ResultBase, out tmpBas))
                    {
                        bas += tmpBas;
                        str += $"{Convert.ToString(tmpBas).PadLeft(5)} Bas";
                    }
                    if (int.TryParse(DataModel.IncomeListFief[x - 1].SubsidiariesCollection[i].ResultLuxury, out tmpLyx))
                    {
                        lyx += tmpLyx;
                        str += $"{Convert.ToString(tmpLyx).PadLeft(5)} Lyx";
                    }
                }

                str += Environment.NewLine;

                #endregion

                #region ConstructingSubsidiaries

                if (DataModel.IncomeListFief[x - 1].ConstructingCollection.Count > 0
                    && DataModel.IncomeListFief[x - 1].ConstructingCollection != null)
                {
                    str += $"Anlagda binäringar:{Environment.NewLine}";
                    for (int i = DataModel.IncomeListFief[x - 1].ConstructingCollection.Count - 1; i >= 0; i--)
                    {
                        str += $"{DataModel.IncomeListFief[x - 1].ConstructingCollection[i].Subsidiary.PadLeft(15)}";
                        if (DataModel.IncomeListFief[x - 1].ConstructingCollection[i].Succeeded)
                        {
                            if (DataModel.IncomeListFief[x - 1].ConstructingCollection[i].DaysWorkBuild
                                == DataModel.IncomeListFief[x - 1].ConstructingCollection[i].DaysWorkThisYear)
                            {
                                bool found = false;
                                str += $" anlades under året!{Environment.NewLine}";
                                _fiefService.SubsidiaryList[x].SubsidiaryCollection.Add(DataModel.IncomeListFief[x - 1].ConstructingCollection[i]);
                                for (int p = 1; p < _fiefService.SubsidiaryList.Count; p++)
                                {
                                    for (int o = 0; o < _fiefService.SubsidiaryList[p].ConstructingCollection.Count; o++)
                                    {
                                        if (_fiefService.SubsidiaryList[p].ConstructingCollection[o].Id == DataModel.IncomeListFief[x - 1].ConstructingCollection[i].Id)
                                        {
                                            _fiefService.SubsidiaryList[p].ConstructingCollection.RemoveAt(o);
                                            found = true;
                                            break;
                                        }
                                    }

                                    if (found)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                str +=
                                    $" arbetades på under året, {DataModel.IncomeListFief[x - 1].ConstructingCollection[i].DaysWorkBuild - DataModel.IncomeListFief[x - 1].ConstructingCollection[i].DaysWorkThisYear} dagsverk återstår.{Environment.NewLine}";
                                DataModel.IncomeListFief[x - 1].ConstructingCollection[i].DaysWorkBuild =
                                    DataModel.IncomeListFief[x - 1].ConstructingCollection[i].DaysWorkBuild
                                    - DataModel.IncomeListFief[x - 1].ConstructingCollection[i].DaysWorkThisYear;
                            }
                        }
                        else
                        {
                            str += $" anläggningen misslyckades och kommer att kräva mycket extra arbete för att anläggas.{Environment.NewLine}";
                        }
                    }

                    str += Environment.NewLine;
                }

                #endregion

                #region MinesModule

                str += $"Gruvor:{Environment.NewLine}";
                for (int i = 0; i < DataModel.IncomeListFief[x - 1].MinesCollection.Count; i++)
                {
                    int tmpSilver;

                    if (int.TryParse(DataModel.IncomeListFief[x - 1].MinesCollection[i].Result, out tmpSilver))
                    {
                        silver += tmpSilver;
                        str += $"{DataModel.IncomeListFief[x - 1].MinesCollection[i].MineType} {Convert.ToString(tmpSilver).PadLeft(7)} Silver{Environment.NewLine}";
                    }
                }

                for (int i = 1; i < _fiefService.MinesList.Count; i++)
                {
                    for (int y = 0; y < _fiefService.MinesList[i].MinesCollection.Count; y++)
                    {
                        if (_fiefService.MinesList[i].MinesCollection[y].IsFirstYear)
                        {
                            _fiefService.MinesList[i].MinesCollection[y].IsFirstYear = false;
                        }
                    }
                }

                str += Environment.NewLine;

                str += $"Stenbrott:{Environment.NewLine}";
                for (int i = 0; i < DataModel.IncomeListFief[x - 1].QuarriesCollection.Count; i++)
                {
                    int tmpStone;

                    if (int.TryParse(DataModel.IncomeListFief[x - 1].QuarriesCollection[i].Result, out tmpStone))
                    {
                        stone += tmpStone;
                        str += $"{DataModel.IncomeListFief[x - 1].QuarriesCollection[i].QuarryType} {Convert.ToString(tmpStone).PadLeft(7)} Sten{Environment.NewLine}";
                    }
                }

                for (int i = 1; i < _fiefService.MinesList.Count; i++)
                {
                    for (int y = 0; y < _fiefService.MinesList[i].QuarriesCollection.Count; y++)
                    {
                        if (_fiefService.MinesList[i].QuarriesCollection[y].IsFirstYear)
                        {
                            _fiefService.MinesList[i].QuarriesCollection[y].IsFirstYear = false;
                        }
                    }
                }

                str += Environment.NewLine;

                #endregion

                #region ExpensesModule

                silver -= DataModel.IncomeListFief[x - 1].ExpensesSilver;
                bas -= DataModel.IncomeListFief[x - 1].ExpensesBase;
                lyx -= DataModel.IncomeListFief[x - 1].ExpensesLuxury;
                iron -= DataModel.IncomeListFief[x - 1].ExpensesIron;
                stone -= DataModel.IncomeListFief[x - 1].ExpensesStone;
                wood -= DataModel.IncomeListFief[x - 1].ExpensesWood;

                #endregion

                #region PortModule

                if (DataModel.IncomeListFief[x - 1].Shipyard != null)
                {
                    if (_fiefService.PortsList[x].BuildingShipyard
                        || _fiefService.PortsList[x].GotShipyard
                        || _fiefService.PortsList[x].UpgradingShipyard)
                    {
                        str += $"Hamn:{Environment.NewLine}";
                        if (_fiefService.PortsList[x].BuildingShipyard)
                        {
                            if (DataModel.IncomeListFief[x - 1].Shipyard.DaysWorkThisYear == DataModel.IncomeListFief[x - 1].Shipyard.DaysWorkNeeded)
                            {
                                str += $"Hamnen är anlagd!{Environment.NewLine}";
                                _fiefService.PortsList[x].BuildingShipyard = false;
                                _fiefService.PortsList[x].GotShipyard = true;
                                _fiefService.PortsList[x].UpgradingShipyard = false;
                                _fiefService.PortsList[x].Shipyard = new ShipyardModel()
                                {
                                    Id = _baseService.GetNewIndustryId(),
                                    Size = _settingsService.ShipyardTypeSettingsList[0].DockSize.ToString(),
                                    OperationBaseCost = _settingsService.ShipyardTypeSettingsList[0].OperationBaseCostModifier,
                                    OperationBaseIncome = _settingsService.ShipyardTypeSettingsList[0].OperationBaseIncomeModifier,
                                    DockSmall = _settingsService.ShipyardTypeSettingsList[0].DockSmall,
                                    DockMedium = _settingsService.ShipyardTypeSettingsList[0].DockMedium,
                                    DockLarge = _settingsService.ShipyardTypeSettingsList[0].DockLarge,
                                    CrimeRate = _settingsService.ShipyardTypeSettingsList[0].CrimeRate,
                                    DaysWorkNeeded = _settingsService.ShipyardTypeSettingsList[0].DaysWork,
                                    DaysWorkThisYear = 0,
                                    Guards = 0
                                };
                            }
                            else
                            {
                                str += $"{DataModel.IncomeListFief[x - 1].Shipyard.DaysWorkThisYear.ToString().PadLeft(5)} har lagds på att anlägga en byhamn.{Environment.NewLine}";
                                str +=
                                    $"{(DataModel.IncomeListFief[x - 1].Shipyard.DaysWorkNeeded - DataModel.IncomeListFief[x - 1].Shipyard.DaysWorkThisYear).ToString().PadLeft(5)} dagsverk behövs för att färdigställa byhamnen.{Environment.NewLine}";
                                _fiefService.PortsList[x].Shipyard.DaysWorkNeeded -= _fiefService.PortsList[x].Shipyard.DaysWorkThisYear;
                            }
                        }
                        else if (_fiefService.PortsList[x].GotShipyard)
                        {
                            int temp;

                            if (int.TryParse(DataModel.IncomeListFief[x - 1].Shipyard.Result, out temp))
                            {
                                if (temp > 0)
                                {
                                    bas += temp;
                                }
                                else
                                {
                                    bas -= temp;
                                }
                            }
                        }
                        else if (_fiefService.PortsList[x].UpgradingShipyard)
                        {
                            if (DataModel.IncomeListFief[x - 1].Shipyard.DaysWorkThisYear == DataModel.IncomeListFief[x - 1].Shipyard.DaysWorkNeeded)
                            {
                                str += $"Hamnen har upgraderats!{Environment.NewLine}";
                                int size = Convert.ToInt32(DataModel.IncomeListFief[x - 1].Shipyard.Size) + 1;
                                _fiefService.PortsList[x].BuildingShipyard = false;
                                _fiefService.PortsList[x].GotShipyard = true;
                                _fiefService.PortsList[x].UpgradingShipyard = false;
                                _fiefService.PortsList[x].Shipyard = new ShipyardModel()
                                {
                                    Id = _baseService.GetNewIndustryId(),
                                    Size = _settingsService.ShipyardTypeSettingsList[size].DockSize.ToString(),
                                    OperationBaseCost = _settingsService.ShipyardTypeSettingsList[size].OperationBaseCostModifier,
                                    OperationBaseIncome = _settingsService.ShipyardTypeSettingsList[size].OperationBaseIncomeModifier,
                                    DockSmall = _settingsService.ShipyardTypeSettingsList[size].DockSmall,
                                    DockMedium = _settingsService.ShipyardTypeSettingsList[size].DockMedium,
                                    DockLarge = _settingsService.ShipyardTypeSettingsList[size].DockLarge,
                                    CrimeRate = _settingsService.ShipyardTypeSettingsList[size].CrimeRate,
                                    DaysWorkNeeded = _settingsService.ShipyardTypeSettingsList[size].DaysWork,
                                    DaysWorkThisYear = 0,
                                    Guards = DataModel.IncomeListFief[x - 1].Shipyard.Guards,
                                    AvailableGuards = DataModel.IncomeListFief[x - 1].Shipyard.AvailableGuards
                                };
                            }
                            else
                            {
                                str += $"{DataModel.IncomeListFief[x - 1].Shipyard.DaysWorkThisYear.ToString().PadLeft(5)} har lagds på att upgradera hamnen.{Environment.NewLine}";
                                str +=
                                    $"{(DataModel.IncomeListFief[x - 1].Shipyard.DaysWorkNeeded - DataModel.IncomeListFief[x - 1].Shipyard.DaysWorkThisYear).ToString().PadLeft(5)} dagsverk behövs för att färdigställa upgraderingen.{Environment.NewLine}";
                                _fiefService.PortsList[x].Shipyard.DaysWorkNeeded -= _fiefService.PortsList[x].Shipyard.DaysWorkThisYear;
                            }
                        }
                        str += Environment.NewLine;
                    }
                }

                #endregion

                #region BoatbuildingModule

                if (_fiefService.BoatbuildingList[x].BoatsBuildingCollection.Count > 0
                    && _fiefService.BoatbuildingList[x].BoatsBuildingCollection != null)
                {
                    str += $"Båtbyggen:{Environment.NewLine}";
                    for (int i = _fiefService.BoatbuildingList[x].BoatsBuildingCollection.Count - 1; i >= 0; i--)
                    {
                        if (_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BoatbuilderId > 0)
                        {
                            if (_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BoatType == "Fiskebåt")
                            {
                                if (_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BuildTimeInDaysAll <= 365)
                                {
                                    str += $"{_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Amount.ToString().PadLeft(3)} fiskebåtar byggdes under året.{Environment.NewLine}";
                                    _fiefService.PortsList[x].FishingBoats += _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Amount;
                                    silver -= _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].CostWhenFinishedSilver;
                                    _fiefService.BoatbuildingList[x].BoatsBuildingCollection.RemoveAt(i);
                                }
                                else
                                {
                                    int nr = Convert.ToInt32(Math.Floor(365M / _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BuildTimeInDays));
                                    str += $"{nr.ToString().PadLeft(3)} fiskebåtar byggdes under året.{Environment.NewLine}";
                                    int left = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Amount -= nr;
                                    _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BuildTimeInDaysAll =
                                        _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Amount
                                        * _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BuildTimeInDays;
                                    _fiefService.PortsList[x].FishingBoats += nr;

                                    silver -= _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].CostWhenFinishedSilver / _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Amount * nr;
                                    _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BuildTimeInDaysAll = left * _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BuildTimeInDays;
                                    _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Amount = left;
                                }
                            }
                            else
                            {
                                if (_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BuildTimeInDaysAll <= 365)
                                {
                                    List<int> max = new List<int>();
                                    for (int p = 1; p < _fiefService.BoatbuildingList.Count; p++)
                                    {
                                        max.Add(_fiefService.PortsList[p].BoatsCollection.Max(o => o.Id));
                                    }
                                    int id = max.Max() + 1;

                                    if (_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Amount > 1)
                                    {
                                        str +=
                                            $"En {_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BoatType} byggdes under året, {_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].CostWhenFinishedSilver} Silver kostade den.{Environment.NewLine}";
                                    }
                                    else
                                    {
                                        str +=
                                            $"{_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Amount.ToString().PadLeft(3)} {_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BoatType} byggdes under året, {_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].CostWhenFinishedSilver} Silver kostade de.{Environment.NewLine}";
                                    }

                                    for (int y = 0; y < _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Amount - 1; y++)
                                    {
                                        _fiefService.PortsList[x].BoatsCollection.Add(new BoatModel()
                                        {
                                            Id = id,
                                            BoatType = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BoatType,
                                            Masts = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Masts,
                                            Length = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Length,
                                            Width = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Width,
                                            Depth = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Depth,
                                            BL = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BL,
                                            DB = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].DB,
                                            CrewNeeded = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].CrewNeeded,
                                            CrewNeededTotal = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].CrewNeededTotal,
                                            Rower = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Rower,
                                            RowersNeeded = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].RowersNeeded,
                                            RowerMulti = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].RowerMulti,
                                            RowersNeededTotal = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].RowersNeededTotal,
                                            Seaworthiness = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Seaworthiness
                                        });
                                        id++;
                                    }

                                    silver -= _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].CostWhenFinishedSilver;
                                    _fiefService.BoatbuildingList[x].BoatsBuildingCollection.RemoveAt(i);
                                }
                                else
                                {
                                    if (_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Amount > 1)
                                    {
                                        List<int> max = new List<int>();
                                        for (int p = 1; p < _fiefService.BoatbuildingList.Count; p++)
                                        {
                                            max.Add(_fiefService.PortsList[p].BoatsCollection.Max(o => o.Id));
                                        }
                                        int id = max.Max() + 1;

                                        int nr = Convert.ToInt32(Math.Floor(365M / _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BuildTimeInDays));
                                        int left = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Amount -= nr;

                                        if (_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Amount > 1)
                                        {
                                            str +=
                                                $"En {_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BoatType} byggdes under året, {_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].CostWhenFinishedSilver} Silver kostade den.{Environment.NewLine}";
                                        }
                                        else
                                        {
                                            str +=
                                                $"{_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Amount.ToString().PadLeft(3)} {_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BoatType} byggdes under året, {_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].CostWhenFinishedSilver} Silver kostade de.{Environment.NewLine}";
                                        }

                                        for (int y = 0; y < nr - 1; y++)
                                        {
                                            _fiefService.PortsList[x].BoatsCollection.Add(new BoatModel()
                                            {
                                                Id = id,
                                                BoatType = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BoatType,
                                                Masts = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Masts,
                                                Length = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Length,
                                                Width = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Width,
                                                Depth = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Depth,
                                                BL = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BL,
                                                DB = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].DB,
                                                CrewNeeded = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].CrewNeeded,
                                                CrewNeededTotal = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].CrewNeededTotal,
                                                Rower = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Rower,
                                                RowersNeeded = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].RowersNeeded,
                                                RowerMulti = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].RowerMulti,
                                                RowersNeededTotal = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].RowersNeededTotal,
                                                Seaworthiness = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Seaworthiness
                                            });
                                            id++;
                                        }

                                        silver -= _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].CostWhenFinishedSilver / _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Amount * nr;
                                        _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BuildTimeInDaysAll = left * _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BuildTimeInDays;
                                        _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].Amount = left;
                                    }
                                    else
                                    {
                                        str +=
                                            $"{_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BoatType} byggs fortfarande och bör vara klar om {_fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BuildTimeInDays -= 365} dagar.{Environment.NewLine}";
                                        _fiefService.BoatbuildingList[x].BoatsBuildingCollection[i].BuildTimeInDays -= 365;
                                    }
                                }
                            }
                        }
                    }

                    str += Environment.NewLine;
                }

                #endregion

                #region BuildingsModule

                List<int> maxBuildingId = new List<int>();
                if (_fiefService.BuildingsList[x].BuildsCollection.Count > 0)
                {
                    for (int p = 1; p < _fiefService.BuildingsList.Count; p++)
                    {
                        maxBuildingId.Add(_fiefService.BuildingsList[x].BuildsCollection.Max(o => o.Id));
                    }
                }
                else
                {
                    maxBuildingId.Add(0);
                }
                int buildingId = maxBuildingId.Max() + 1;
                str += $"Byggnadsverk:{Environment.NewLine}";

                for (int i = _fiefService.BuildingsList[x].BuildsCollection.Count - 1; i >= 0; i--)
                {
                    if (_fiefService.BuildingsList[x].BuildsCollection[i].BuilderId > 0)
                    {
                        int time;

                        if (int.TryParse(_fiefService.BuildingsList[x].BuildsCollection[i].BuildingTime, out time))
                        {
                            if (time <= 365)
                            {
                                bool foundInList = false;
                                for (int p = 0; p < _fiefService.BuildingsList[x].BuildingsCollection.Count; p++)
                                {
                                    if (_fiefService.BuildingsList[x].BuildingsCollection[p].Building.Equals(_fiefService.BuildingsList[x].BuildsCollection[i].Building))
                                    {
                                        _fiefService.BuildingsList[x].BuildingsCollection[p].Amount += _fiefService.BuildingsList[x].BuildsCollection[i].Amount;
                                        _fiefService.BuildingsList[x].BuildingsCollection[p].UpkeepTotal =
                                            _fiefService.BuildingsList[x].BuildingsCollection[p].Amount
                                            * _fiefService.BuildingsList[x].BuildingsCollection[p].Upkeep;
                                        foundInList = true;
                                        str += $"Ytterligare en {_fiefService.BuildingsList[x].BuildsCollection[i].Building} har konstruerats.{Environment.NewLine}";
                                        _fiefService.BuildingsList[x].BuildsCollection.RemoveAt(i);
                                        break;
                                    }
                                }

                                if (!foundInList)
                                {
                                    _fiefService.BuildingsList[x].BuildingsCollection.Add(new BuildingModel()
                                    {
                                        Id = buildingId,
                                        Building = _fiefService.BuildingsList[x].BuildsCollection[i].Building,
                                        Amount = _fiefService.BuildingsList[x].BuildsCollection[i].Amount,
                                        Upkeep = _fiefService.BuildingsList[x].BuildsCollection[i].Upkeep,
                                        UpkeepTotal = _fiefService.BuildingsList[x].BuildsCollection[i].Upkeep * _fiefService.BuildingsList[x].BuildsCollection[i].Amount
                                    });
                                    buildingId++;
                                    str += $"{_fiefService.BuildingsList[x].BuildsCollection[i].Building} har byggts klart.{Environment.NewLine}";
                                }
                            }
                            else
                            {
                                _fiefService.BuildingsList[x].BuildsCollection[i].LeftIron -= _fiefService.BuildingsList[x].BuildingsCollection[i].IronThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[i].IronThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[i].LeftStone -= _fiefService.BuildingsList[x].BuildsCollection[i].StoneThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[i].StoneThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[i].LeftWood -= _fiefService.BuildingsList[x].BuildsCollection[i].WoodThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[i].WoodThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[i].LeftSmithswork -= _fiefService.BuildingsList[x].BuildsCollection[i].SmithsworkThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[i].SmithsworkThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[i].LeftWoodwork -= _fiefService.BuildingsList[x].BuildsCollection[i].WoodworkThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[i].WoodworkThisYear = 0;
                                _fiefService.BuildingsList[x].BuildsCollection[i].LeftStonework -= _fiefService.BuildingsList[x].BuildsCollection[i].StoneworkThisYear;
                                _fiefService.BuildingsList[x].BuildsCollection[i].StoneworkThisYear = 0;
                                str += $"{_fiefService.BuildingsList[x].BuildsCollection[i].Building} byggs fortfarande.{Environment.NewLine}";
                            }
                        }
                    }
                }
                str += Environment.NewLine;

                #endregion

                #region Felling

                if (DataModel.IncomeListFief[x - 1].FellingModel != null
                    && DataModel.IncomeListFief[x - 1].FellingModel.StewardId > 0)
                {
                    str += $"Skogsavverkning:{Environment.NewLine}";
                    int growth = 0;
                    if (_baseService.RollDie(1, 7) >= 6)
                    {
                        growth = Convert.ToInt32(Math.Floor((double)_fiefService.ManorList[x].ManorFelling / 50));
                    }
                    else
                    {
                        growth = Convert.ToInt32(Math.Floor((double)_fiefService.ManorList[x].ManorFelling / 100));
                    }
                    _fiefService.ManorList[x].ManorWoodland += growth;
                    _fiefService.ManorList[x].ManorFelling -= growth;
                    str += $"{growth} tunnland av den avverkade skogsmarken växte tillbaka.{Environment.NewLine}";

                    wood += DataModel.IncomeListFief[x - 1].FellingModel.FellingWood;
                    str += $"{DataModel.IncomeListFief[x - 1].FellingModel.FellingWood.ToString().PadLeft(5)} lass timmer.{Environment.NewLine}";

                    _fiefService.ManorList[x].ManorFelling += _fiefService.WeatherList[x].Felling;
                    _fiefService.ManorList[x].ManorFelling -= _fiefService.WeatherList[x].LandClearingOfFelling;
                    _fiefService.ManorList[x].ManorUseless -= _fiefService.WeatherList[x].ClearUseless;
                    _fiefService.ManorList[x].ManorWoodland -= _fiefService.WeatherList[x].Felling;

                    if (_fiefService.WeatherList[x].LandClearingOfFelling > 0
                        || _fiefService.WeatherList[x].LandClearing > 0
                        || _fiefService.WeatherList[x].ClearUseless > 0)
                    {
                        int acres =
                            _fiefService.WeatherList[x].LandClearing
                            + _fiefService.WeatherList[x].LandClearingOfFelling
                            + _fiefService.WeatherList[x].ClearUseless;

                        _fiefService.ManorList[x].ManorAcres += acres;
                        _fiefService.ManorList[x].ManorArable += DataModel.IncomeListFief[x - 1].Agricultural;
                        _fiefService.ManorList[x].ManorPasture += DataModel.IncomeListFief[x - 1].Pasture;

                        str += $"{acres} tunnland domänjord har lagts till på godset.{Environment.NewLine}";
                        str += $"{DataModel.IncomeListFief[x - 1].Pasture.ToString().PadLeft(3)} tunnland betesmark.{Environment.NewLine}";
                        str += $"{DataModel.IncomeListFief[x - 1].Agricultural.ToString().PadLeft(3)} tunnland åkermark.{Environment.NewLine}";

                    }

                    str += Environment.NewLine;
                    wood += DataModel.IncomeListFief[x - 1].FellingModel.Result;
                }

                #endregion

                #region Population

                int population = DataModel.IncomeListFief[x - 1].PopulationModel.ResultPopulation;

                int nrBurgess = 0;
                int nrInnkeepers = 0;
                int nrCarpenters = 0;
                int nrSmiths = 0;
                int nrTailors = 0;
                int nrFurriers = 0;
                int nrMillers = 0;
                int nrTanners = 0;
                int nrBoatbuilders = 0;
                int nrFarmers = 0;
                int nrSerfdoms = 0;
                int nrDeadBurgess = 0;
                int nrDeadInnkeepers = 0;
                int nrDeadCarpenters = 0;
                int nrDeadSmiths = 0;
                int nrDeadTailors = 0;
                int nrDeadFurriers = 0;
                int nrDeadMillers = 0;
                int nrDeadTanners = 0;
                int nrDeadBoatbuilders = 0;
                int nrDeadFarmers = 0;
                int nrDeadSerfdoms = 0;

                if (DataModel.IncomeListFief[x - 1].PopulationModel.AddPopulation)
                {
                    if (DataModel.IncomeListFief[x - 1].PopulationModel.ResultPopulation > 99)
                    {
                        amor--;
                        amor--;
                    }
                    else if (DataModel.IncomeListFief[x - 1].PopulationModel.ResultPopulation > 49)
                    {
                        amor--;
                    }

                    if (DataModel.IncomeListFief[x - 1].PopulationModel.ResultPopulation > 59)
                    {
                        _fiefService.ManorList[x].VillagesCollection.Add(new VillageModel()
                        {
                            Population = 60,
                            Serfdoms = 45,
                            Farmers = 15
                        });
                        DataModel.IncomeListFief[x - 1].PopulationModel.ResultPopulation -= 60;
                        int removePop = 60;
                        bool popRemoved = false;
                        if (_fiefService.MinesList[x].MinesCollection.Count > 0)
                        {
                            for (int i = 0; i < _fiefService.MinesList[x].MinesCollection.Count; i++)
                            {
                                if (_fiefService.MinesList[x].MinesCollection[i].Population > 0)
                                {
                                    if (_fiefService.MinesList[x].MinesCollection[i].Population >= removePop)
                                    {
                                        _fiefService.MinesList[x].MinesCollection[i].Population -= removePop;
                                        popRemoved = true;
                                    }
                                    else
                                    {
                                        removePop -= _fiefService.MinesList[x].MinesCollection[i].Population;
                                        _fiefService.MinesList[x].MinesCollection[i].Population = 0;
                                    }
                                }
                            }
                        }

                        if (_fiefService.MinesList[x].QuarriesCollection.Count > 0
                            && !popRemoved)
                        {
                            for (int i = 0; i < _fiefService.MinesList[x].QuarriesCollection.Count; i++)
                            {
                                if (_fiefService.MinesList[x].QuarriesCollection[i].Population > 0)
                                {
                                    if (Convert.ToInt32(_fiefService.MinesList[x].QuarriesCollection[i].Population) >= removePop)
                                    {
                                        _fiefService.MinesList[x].QuarriesCollection[i].Population -= removePop;
                                        popRemoved = true;
                                    }
                                    else
                                    {
                                        removePop -= Convert.ToInt32(_fiefService.MinesList[x].QuarriesCollection[i].Population);
                                        _fiefService.MinesList[x].QuarriesCollection[i].Population = 0M;
                                    }
                                }
                            }
                        }

                        str += $"En ny by grundades på dina ägor. (60 invånare){Environment.NewLine}";
                    }

                    for (int i = 0; i < DataModel.IncomeListFief[x - 1].PopulationModel.ResultPopulation; i++)
                    {
                        int roll = 0;
                        int nrVillages = _fiefService.ManorList[x].VillagesCollection.Count;
                        if (nrVillages > 1)
                        {
                            roll = _baseService.RollDie(0, nrVillages);
                        }

                        _fiefService.ManorList[x].VillagesCollection[roll].Population++;
                        int type = _baseService.RollDie(1, 101);
                        if (type > 96)
                        {
                            _fiefService.ManorList[x].VillagesCollection[roll].Burgess++;
                            nrBurgess++;
                            int burgess = _baseService.RollDie(1, 101);
                            if (burgess > 90)
                            {
                                _fiefService.ManorList[x].VillagesCollection[roll].Innkeepers++;
                                nrInnkeepers++;
                            }
                            else if (burgess > 70)
                            {
                                _fiefService.ManorList[x].VillagesCollection[roll].Carpenters++;
                                nrCarpenters++;
                            }
                            else if (burgess > 50)
                            {
                                _fiefService.ManorList[x].VillagesCollection[roll].Smiths++;
                                nrSmiths++;
                            }
                            else if (burgess > 45)
                            {
                                _fiefService.ManorList[x].VillagesCollection[roll].Tailors++;
                                nrTailors++;
                            }
                            else if (burgess > 40)
                            {
                                _fiefService.ManorList[x].VillagesCollection[roll].Furriers++;
                                nrFurriers++;
                            }
                            else if (burgess > 20)
                            {
                                _fiefService.ManorList[x].VillagesCollection[roll].Millers++;
                                nrMillers++;
                            }
                            else if (burgess > 10)
                            {
                                _fiefService.ManorList[x].VillagesCollection[roll].Tanners++;
                                nrTanners++;
                            }
                            else
                            {
                                if (_fiefService.InformationList[x].Coast.Equals("Ja")
                                    || _fiefService.InformationList[x].River.Equals("Ja")
                                    || _fiefService.InformationList[x].Lake.Equals("Ja"))
                                {
                                    _fiefService.ManorList[x].VillagesCollection[roll].Boatbuilders++;
                                    nrBoatbuilders++;
                                }
                                else
                                {
                                    _fiefService.ManorList[x].VillagesCollection[roll].Tanners++;
                                    nrTanners++;
                                }
                            }
                        }
                        else if (type > 70)
                        {
                            _fiefService.ManorList[x].VillagesCollection[roll].Farmers++;
                            nrFarmers++;
                        }
                        else
                        {
                            _fiefService.ManorList[x].VillagesCollection[roll].Serfdoms++;
                            nrSerfdoms++;
                        }
                    }
                }
                else
                {
                    if (DataModel.IncomeListFief[x - 1].PopulationModel.ResultPopulation > 99)
                    {
                        amor++;
                        amor++;
                    }
                    else if (DataModel.IncomeListFief[x - 1].PopulationModel.ResultPopulation > 49)
                    {
                        amor++;
                    }

                    for (int i = 0; i > DataModel.IncomeListFief[x - 1].PopulationModel.ResultPopulation; i--)
                    {
                        int roll = 0;
                        int nrVillages = _fiefService.ManorList[x].VillagesCollection.Count;

                        if (nrVillages > 1)
                        {
                            roll = _baseService.RollDie(0, nrVillages);
                        }

                        _fiefService.ManorList[x].VillagesCollection[roll].Population--;
                        int totPop = _fiefService.ManorList[x].VillagesCollection[roll].Boatbuilders
                                     + _fiefService.ManorList[x].VillagesCollection[roll].Carpenters
                                     + _fiefService.ManorList[x].VillagesCollection[roll].Farmers
                                     + _fiefService.ManorList[x].VillagesCollection[roll].Furriers
                                     + _fiefService.ManorList[x].VillagesCollection[roll].Millers
                                     + _fiefService.ManorList[x].VillagesCollection[roll].Serfdoms
                                     + _fiefService.ManorList[x].VillagesCollection[roll].Tailors
                                     + _fiefService.ManorList[x].VillagesCollection[roll].Tanners
                                     + _fiefService.ManorList[x].VillagesCollection[roll].Innkeepers
                                     + _fiefService.ManorList[x].VillagesCollection[roll].Smiths;

                        int burgess = Convert.ToInt32(Math.Round((decimal)_fiefService.ManorList[x].VillagesCollection[roll].Burgess / totPop, 2) * 100);
                        int farmers = Convert.ToInt32(Math.Round((decimal)_fiefService.ManorList[x].VillagesCollection[roll].Farmers / totPop, 2) * 100);
                        int type = _baseService.RollDie(1, 101);

                        if (type > 100 - burgess)
                        {
                            nrDeadBurgess++;
                            _fiefService.ManorList[x].VillagesCollection[roll].Burgess--;
                            List<string> worker = new List<string>();
                            if (_fiefService.ManorList[x].VillagesCollection[roll].Boatbuilders > 0)
                            {
                                for (int y = 0; y < _fiefService.ManorList[x].VillagesCollection[roll].Boatbuilders; y++)
                                {
                                    worker.Add("Boatbuilder");
                                }
                            }
                            if (_fiefService.ManorList[x].VillagesCollection[roll].Carpenters > 0)
                            {
                                for (int y = 0; y < _fiefService.ManorList[x].VillagesCollection[roll].Carpenters; y++)
                                {
                                    worker.Add("Carpenter");
                                }
                            }
                            if (_fiefService.ManorList[x].VillagesCollection[roll].Millers > 0)
                            {
                                for (int y = 0; y < _fiefService.ManorList[x].VillagesCollection[roll].Millers; y++)
                                {
                                    worker.Add("Miller");
                                }
                            }
                            if (_fiefService.ManorList[x].VillagesCollection[roll].Tailors > 0)
                            {
                                for (int y = 0; y < _fiefService.ManorList[x].VillagesCollection[roll].Tailors; y++)
                                {
                                    worker.Add("Tailor");
                                }
                            }
                            if (_fiefService.ManorList[x].VillagesCollection[roll].Furriers > 0)
                            {
                                for (int y = 0; y < _fiefService.ManorList[x].VillagesCollection[roll].Furriers; y++)
                                {
                                    worker.Add("Furrier");
                                }
                            }
                            if (_fiefService.ManorList[x].VillagesCollection[roll].Tanners > 0)
                            {
                                for (int y = 0; y < _fiefService.ManorList[x].VillagesCollection[roll].Tanners; y++)
                                {
                                    worker.Add("Tanner");
                                }
                            }
                            if (_fiefService.ManorList[x].VillagesCollection[roll].Innkeepers > 0)
                            {
                                for (int y = 0; y < _fiefService.ManorList[x].VillagesCollection[roll].Innkeepers; y++)
                                {
                                    worker.Add("Innkeeper");
                                }
                            }
                            if (_fiefService.ManorList[x].VillagesCollection[roll].Smiths > 0)
                            {
                                for (int y = 0; y < _fiefService.ManorList[x].VillagesCollection[roll].Smiths; y++)
                                {
                                    worker.Add("Smith");
                                }
                            }
                            int died = _baseService.RollDie(1, worker.Count + 1);

                            if (worker[died - 1] == "Boatbuilder")
                            {
                                _fiefService.ManorList[x].VillagesCollection[roll].Boatbuilders--;
                                nrDeadBoatbuilders++;
                            }
                            else if (worker[died - 1] == "Carpenter")
                            {
                                _fiefService.ManorList[x].VillagesCollection[roll].Carpenters--;
                                nrDeadCarpenters++;
                            }
                            else if (worker[died - 1] == "Miller")
                            {
                                _fiefService.ManorList[x].VillagesCollection[roll].Millers--;
                                nrDeadMillers++;
                            }
                            else if (worker[died - 1] == "Tailor")
                            {
                                _fiefService.ManorList[x].VillagesCollection[roll].Tailors--;
                                nrDeadTailors++;
                            }
                            else if (worker[died - 1] == "Furrier")
                            {
                                _fiefService.ManorList[x].VillagesCollection[roll].Furriers--;
                                nrDeadFurriers++;
                            }
                            else if (worker[died - 1] == "Tanner")
                            {
                                _fiefService.ManorList[x].VillagesCollection[roll].Tanners--;
                                nrDeadTanners++;
                            }
                            else if (worker[died - 1] == "Innkeeper")
                            {
                                _fiefService.ManorList[x].VillagesCollection[roll].Innkeepers--;
                                nrDeadInnkeepers++;
                            }
                            else if (worker[died - 1] == "Smith")
                            {
                                _fiefService.ManorList[x].VillagesCollection[roll].Smiths--;
                                nrDeadSmiths++;
                            }
                        }
                        else if (type > 100 - burgess - farmers)
                        {
                            _fiefService.ManorList[x].VillagesCollection[roll].Farmers--;
                            nrDeadFarmers++;
                        }
                        else
                        {
                            _fiefService.ManorList[x].VillagesCollection[roll].Serfdoms--;
                            nrDeadSerfdoms++;
                        }
                    }
                }

                str += $"Befolkningstillväxt:{Environment.NewLine}";

                if (DataModel.IncomeListFief[x - 1].PopulationModel.AddPopulation)
                {
                    str += $"Befolkningen ökade med {DataModel.IncomeListFief[x - 1].PopulationModel.ResultPopulation}{Environment.NewLine}";
                    if (nrBurgess > 0)
                    {
                        str += $"{nrBurgess.ToString().PadLeft(3)} hanterverkare flyttade in i förläningen.{Environment.NewLine}";
                    }
                    if (nrFarmers > 0)
                    {
                        str += $"{nrFarmers.ToString().PadLeft(3)} friabönder bröt ny mark.{Environment.NewLine}";
                    }
                    if (nrSerfdoms > 0)
                    {
                        str += $"{nrSerfdoms.ToString().PadLeft(3)} ofriabönder flyttade in.{Environment.NewLine}";
                    }
                    str += Environment.NewLine;
                }
                else
                {
                    int result = DataModel.IncomeListFief[x - 1].PopulationModel.ResultPopulation;
                    double temp = Math.Pow(result, 2);
                    int deadPop = Convert.ToInt32(Math.Sqrt(temp));

                    str += $"Befolkningen minskade med {deadPop}{Environment.NewLine}";
                    if (nrDeadBurgess > 0)
                    {
                        str += $"{nrDeadBurgess.ToString().PadLeft(3)} hantverkare";
                    }
                    if (nrDeadFarmers > 0)
                    {
                        str += $"{nrDeadFarmers.ToString().PadLeft(3)} friabönder";
                    }
                    if (nrDeadSerfdoms > 0)
                    {
                        str += $"{nrDeadSerfdoms.ToString().PadLeft(3)} ofriabönder";
                    }

                    if (nrDeadBurgess > 0
                        || nrDeadFarmers > 0
                        || nrDeadSerfdoms > 0)
                    {
                        str += $".{Environment.NewLine}";
                    }
                    else
                    {
                        str += Environment.NewLine;
                    }
                }

                #endregion

                #region Residents

                bool boendeDead = false;
                str += $"Boende:{Environment.NewLine}";
                if (_fiefService.ManorList[x].ResidentsList.Count > 0
                    && _fiefService.ManorList[x].ResidentsList != null)
                {
                    for (int i = _fiefService.ManorList[x].ResidentsList.Count; i > 0; i--)
                    {
                        int chance = Convert.ToInt32(Math.Round(Math.Pow(_fiefService.ManorList[x].ResidentsList[i].Age, 1.97D) / 85 - 6, MidpointRounding.ToEven));
                        if (_baseService.RollDie(1, 101) + chance > 100)
                        {
                            str += $"{_fiefService.ManorList[x].ResidentsList[i].PersonName} dog vid en ålder av {_fiefService.ManorList[x].ResidentsList[i].Age}. (Boende){Environment.NewLine}";
                            _fiefService.ManorList[x].ResidentsList.RemoveAt(i);
                            boendeDead = true;
                        }
                    }
                }

                if (_fiefService.ArmyList[x].TemplarKnightsList.Count > 0
                    && _fiefService.ArmyList[x].TemplarKnightsList != null)
                {
                    for (int i = _fiefService.ArmyList[x].TemplarKnightsList.Count; i > 0; i--)
                    {
                        int chance = Convert.ToInt32(Math.Round(Math.Pow(_fiefService.ArmyList[x].TemplarKnightsList[i].Age, 1.97D) / 85 - 6, MidpointRounding.ToEven));
                        if (_baseService.RollDie(1, 101) + chance > 100)
                        {
                            str += $"{_fiefService.ArmyList[x].TemplarKnightsList[i].PersonName} dog vid en ålder av {_fiefService.ArmyList[x].TemplarKnightsList[i].Age}. (Tempelriddare){Environment.NewLine}";
                            _fiefService.ArmyList[x].TemplarKnightsList.RemoveAt(i);
                            boendeDead = true;
                        }
                    }
                }

                if (_fiefService.ArmyList[x].KnightsList.Count > 0
                    && _fiefService.ArmyList[x].KnightsList != null)
                {
                    for (int i = _fiefService.ArmyList[x].KnightsList.Count; i > 0; i--)
                    {
                        int chance = Convert.ToInt32(Math.Round(Math.Pow(_fiefService.ArmyList[x].KnightsList[i].Age, 1.97D) / 85 - 6, MidpointRounding.ToEven));
                        if (_baseService.RollDie(1, 101) + chance > 100)
                        {
                            str += $"{_fiefService.ArmyList[x].KnightsList[i].PersonName} dog vid en ålder av {_fiefService.ArmyList[x].KnightsList[i].Age}. (Tempelriddare){Environment.NewLine}";
                            _fiefService.ArmyList[x].KnightsList.RemoveAt(i);
                            boendeDead = true;
                        }
                    }
                }

                if (_fiefService.ArmyList[x].CavalryTemplarKnightsList.Count > 0
                    && _fiefService.ArmyList[x].CavalryTemplarKnightsList != null)
                {
                    for (int i = _fiefService.ArmyList[x].CavalryTemplarKnightsList.Count; i > 0; i--)
                    {
                        int chance = Convert.ToInt32(Math.Round(Math.Pow(_fiefService.ArmyList[x].CavalryTemplarKnightsList[i].Age, 1.97D) / 85 - 6, MidpointRounding.ToEven));
                        if (_baseService.RollDie(1, 101) + chance > 100)
                        {
                            str += $"{_fiefService.ArmyList[x].CavalryTemplarKnightsList[i].PersonName} dog vid en ålder av {_fiefService.ArmyList[x].CavalryTemplarKnightsList[i].Age}. (Tempelriddare){Environment.NewLine}";
                            _fiefService.ArmyList[x].CavalryTemplarKnightsList.RemoveAt(i);
                            boendeDead = true;
                        }
                    }
                }

                if (_fiefService.ArmyList[x].OfficerCaptainsList.Count > 0
                    && _fiefService.ArmyList[x].OfficerCaptainsList != null)
                {
                    for (int i = _fiefService.ArmyList[x].OfficerCaptainsList.Count; i > 0; i--)
                    {
                        int chance = Convert.ToInt32(Math.Round(Math.Pow(_fiefService.ArmyList[x].OfficerCaptainsList[i].Age, 1.97D) / 85 - 6, MidpointRounding.ToEven));
                        if (_baseService.RollDie(1, 101) + chance > 100)
                        {
                            str += $"{_fiefService.ArmyList[x].OfficerCaptainsList[i].PersonName} dog vid en ålder av {_fiefService.ArmyList[x].OfficerCaptainsList[i].Age}. (Kapten){Environment.NewLine}";
                            _fiefService.ArmyList[x].OfficerCaptainsList.RemoveAt(i);
                            boendeDead = true;
                        }
                    }
                }

                if (_fiefService.ArmyList[x].OfficerCorporalsList.Count > 0
                    && _fiefService.ArmyList[x].OfficerCorporalsList != null)
                {
                    for (int i = _fiefService.ArmyList[x].OfficerCorporalsList.Count; i > 0; i--)
                    {
                        int chance = Convert.ToInt32(Math.Round(Math.Pow(_fiefService.ArmyList[x].OfficerCorporalsList[i].Age, 1.97D) / 85 - 6, MidpointRounding.ToEven));
                        if (_baseService.RollDie(1, 101) + chance > 100)
                        {
                            str += $"{_fiefService.ArmyList[x].OfficerCorporalsList[i].PersonName} dog vid en ålder av {_fiefService.ArmyList[x].OfficerCorporalsList[i].Age}. (Korpral){Environment.NewLine}";
                            _fiefService.ArmyList[x].OfficerCorporalsList.RemoveAt(i);
                            boendeDead = true;
                        }
                    }
                }

                if (_fiefService.ArmyList[x].OfficerSergeantsList.Count > 0
                    && _fiefService.ArmyList[x].OfficerSergeantsList != null)
                {
                    for (int i = _fiefService.ArmyList[x].OfficerSergeantsList.Count; i > 0; i--)
                    {
                        int chance = Convert.ToInt32(Math.Round(Math.Pow(_fiefService.ArmyList[x].OfficerSergeantsList[i].Age, 1.97D) / 85 - 6, MidpointRounding.ToEven));
                        if (_baseService.RollDie(1, 101) + chance > 100)
                        {
                            str += $"{_fiefService.ArmyList[x].OfficerSergeantsList[i].PersonName} dog vid en ålder av {_fiefService.ArmyList[x].OfficerSergeantsList[i].Age}. (Sergeant){Environment.NewLine}";
                            _fiefService.ArmyList[x].OfficerSergeantsList.RemoveAt(i);
                            boendeDead = true;
                        }
                    }
                }

                if (boendeDead)
                {
                    str += Environment.NewLine;
                }
                else
                {
                    str += $"Ingen inneboende har dött under året.{Environment.NewLine}{Environment.NewLine}";
                }

                #endregion

                #region TradeModule

                if (DataModel.IncomeListFief[x - 1].TradeCollection.Count > 0
                    && DataModel.IncomeListFief[x - 1].TradeCollection != null)
                {
                    str += $"Handel:{Environment.NewLine}";
                    for (int i = 0; i < DataModel.IncomeListFief[x - 1].TradeCollection.Count; i++)
                    {
                        int deadGuards = 0;
                        int chance = 0;
                        bool deadMerchant = false;
                        bool tradeLost = false;

                        if (DataModel.IncomeListFief[x - 1].TradeCollection[i].Guards > 0)
                        {
                            chance = (int)(Math.Floor(DataModel.IncomeListFief[x - 1].TradeCollection[i].SilverWith / 360D)
                                           + DataModel.IncomeListFief[x - 1].TradeCollection[i].BaseToSell
                                           + DataModel.IncomeListFief[x - 1].TradeCollection[i].LuxuryToSell * 3
                                           + DataModel.IncomeListFief[x - 1].TradeCollection[i].IronToSell
                                           + DataModel.IncomeListFief[x - 1].TradeCollection[i].StoneToSell * 2
                                           + DataModel.IncomeListFief[x - 1].TradeCollection[i].WoodToSell * 2)
                                     / (DataModel.IncomeListFief[x - 1].TradeCollection[i].Guards
                                        * DataModel.IncomeListFief[x - 1].TradeCollection[i].Guards);
                        }
                        else
                        {
                            chance = (int)(Math.Floor(DataModel.IncomeListFief[x - 1].TradeCollection[i].SilverWith / 360D)
                                           + DataModel.IncomeListFief[x - 1].TradeCollection[i].BaseToSell
                                           + DataModel.IncomeListFief[x - 1].TradeCollection[i].LuxuryToSell * 3
                                           + DataModel.IncomeListFief[x - 1].TradeCollection[i].IronToSell
                                           + DataModel.IncomeListFief[x - 1].TradeCollection[i].StoneToSell * 2
                                           + DataModel.IncomeListFief[x - 1].TradeCollection[i].WoodToSell * 2)
                                     * 2;
                        }

                        if (chance > 74)
                        {
                            chance = 75;
                        }

                        if (_baseService.RollDie(1, 101) <= chance)
                        {
                            // RÅNADE
                            int roll = _baseService.RollDie(1, 101);
                            if (roll <= DataModel.IncomeListFief[x - 1].TradeCollection[i].Guards * 2
                                || roll == 100)
                            {
                                str += $"";
                                for (int nr = 0; nr < DataModel.IncomeListFief[x - 1].TradeCollection[i].Guards; nr++)
                                {
                                    if (_baseService.RollDie(1, 101) > 74)
                                    {
                                        deadGuards++;
                                    }
                                }
                                if (_baseService.RollDie(1, 101) >= 80)
                                {
                                    deadMerchant = true;
                                }
                                if (deadGuards >= DataModel.IncomeListFief[x - 1].TradeCollection[i].Guards)
                                {
                                    tradeLost = true;
                                }
                            }
                            else
                            {
                                deadMerchant = true;
                                deadGuards = DataModel.IncomeListFief[x - 1].TradeCollection[i].Guards;
                                tradeLost = true;
                            }
                        }

                        // Lägg till att tabort fartyget om det var på ett fartyg handelsmannen åkte.
                        // tabort döda merchants och vakter.
                        if (deadMerchant)
                        {
                            if (tradeLost)
                            {
                                str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].PersonName} återkom inte under året. ({DataModel.IncomeListFief[x - 1].TradeCollection[i].Assignment}){Environment.NewLine}";
                            }
                            else
                            {
                                str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].PersonName} dog i ett rånförsök.{Environment.NewLine}";
                                str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].Guards - deadGuards} vakter kom tillbaka med varorna.{Environment.NewLine}";

                                if (DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearSilver > 0)
                                {
                                    str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearSilver.ToString().PadLeft(7)} Silver{Environment.NewLine}";
                                    silver += DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearSilver;
                                }
                                if (DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearBase > 0)
                                {
                                    str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearBase.ToString().PadLeft(7)} Bas{Environment.NewLine}";
                                    bas += DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearBase;
                                }
                                if (DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearLuxury > 0)
                                {
                                    str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearLuxury.ToString().PadLeft(7)} Lyx{Environment.NewLine}";
                                    lyx += DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearLuxury;
                                }
                                if (DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearIron > 0)
                                {
                                    str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearIron.ToString().PadLeft(7)} Järn{Environment.NewLine}";
                                    iron += DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearIron;
                                }
                                if (DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearStone > 0)
                                {
                                    str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearStone.ToString().PadLeft(7)} Sten{Environment.NewLine}";
                                    stone += DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearStone;
                                }
                                if (DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearWood > 0)
                                {
                                    str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearWood.ToString().PadLeft(7)} Timmer{Environment.NewLine}";
                                    wood += DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearWood;
                                }
                            }
                        }
                        else
                        {
                            if (tradeLost)
                            {
                                str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].PersonName} kom tillbaka tomhänt. ({DataModel.IncomeListFief[x - 1].TradeCollection[i].Assignment}){Environment.NewLine}";
                            }
                            else
                            {
                                str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].PersonName} kom tillbaka med ({DataModel.IncomeListFief[x - 1].TradeCollection[i].Assignment}){Environment.NewLine}";

                                if (DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearSilver > 0)
                                {
                                    str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearSilver.ToString().PadLeft(7)} Silver{Environment.NewLine}";
                                    silver += DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearSilver;
                                }
                                if (DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearBase > 0)
                                {
                                    str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearBase.ToString().PadLeft(7)} Bas{Environment.NewLine}";
                                    bas += DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearBase;
                                }
                                if (DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearLuxury > 0)
                                {
                                    str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearLuxury.ToString().PadLeft(7)} Lyx{Environment.NewLine}";
                                    lyx += DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearLuxury;
                                }
                                if (DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearIron > 0)
                                {
                                    str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearIron.ToString().PadLeft(7)} Järn{Environment.NewLine}";
                                    iron += DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearIron;
                                }
                                if (DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearStone > 0)
                                {
                                    str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearStone.ToString().PadLeft(7)} Sten{Environment.NewLine}";
                                    stone += DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearStone;
                                }
                                if (DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearWood > 0)
                                {
                                    str += $"{DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearWood.ToString().PadLeft(7)} Timmer{Environment.NewLine}";
                                    wood += DataModel.IncomeListFief[x - 1].TradeCollection[i].EndOfYearWood;
                                }
                            }
                        }

                        str += Environment.NewLine;
                    }
                }

                #endregion

                str += incomeStr + Environment.NewLine;
                _fiefService.InformationList[x].Loyalty = ConvertToT6(loyalty);
                _fiefService.InformationList[x].Amor = ConvertToT6(amor);
            }

            #region Stewards

            List<int> deadStewards = new List<int>();
            if (_fiefService.StewardsDataModel.StewardsCollection.Count > 0
                && _fiefService.StewardsDataModel.StewardsCollection != null)
            {
                bas -= _fiefService.StewardsDataModel.StewardsCollection.Count * 4;
                lyx -= _fiefService.StewardsDataModel.StewardsCollection.Count * 2;

                str += $"Förvaltare:{Environment.NewLine}";
                for (int i = _fiefService.StewardsDataModel.StewardsCollection.Count - 1; i > 0; i--)
                {
                    int chance = Convert.ToInt32(Math.Round(Math.Pow(_fiefService.StewardsDataModel.StewardsCollection[i].Age, 1.97D) / 85 - 6, MidpointRounding.ToEven));
                    if (_baseService.RollDie(1, 101) + chance > 100)
                    {
                        str += $"{_fiefService.StewardsDataModel.StewardsCollection[i].PersonName} dog {_fiefService.StewardsDataModel.StewardsCollection[i].Age} år gammal. "
                               + $"({_fiefService.StewardsDataModel.StewardsCollection[i].Industry}){Environment.NewLine}";
                        deadStewards.Add(_fiefService.StewardsDataModel.StewardsCollection[i].Id);
                    }
                }

                if (deadStewards.Count > 0)
                {
                    foreach (var id in deadStewards)
                    {
                        _baseService.RemoveSteward(id);
                    }
                }
            }
            str += Environment.NewLine;

            #endregion

            str += $"TOTALT:{Environment.NewLine}";
            str += $"{silver.ToString().PadRight(8)} Silver{Environment.NewLine}";
            str += $"{bas.ToString().PadRight(8)} Bas{Environment.NewLine}";
            str += $"{lyx.ToString().PadRight(8)} Lyx{Environment.NewLine}";
            str += $"{iron.ToString().PadRight(8)} Järn{Environment.NewLine}";
            str += $"{stone.ToString().PadRight(8)} Sten{Environment.NewLine}";
            str += $"{wood.ToString().PadRight(8)} Timmer{Environment.NewLine}";

            _fiefService.Year++;
            _eventAggregator.GetEvent<EndOfYearEvent>().Unsubscribe(LoadData);
            _eventAggregator.GetEvent<EndOfYearEvent>().Publish();

            string filePath = $@"{AppDomain.CurrentDomain.BaseDirectory}/Reports/Bokslut_{_fiefService.Year - 1}.txt";
            System.IO.FileInfo file = new System.IO.FileInfo(filePath);
            file.Directory?.Create();
            File.WriteAllText(file.FullName, str);
            try
            {
                System.Diagnostics.Process.Start(file.FullName);
            }
            catch (IOException e)
            {
                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }

            _eventAggregator.GetEvent<EndOfYearEvent>().Subscribe(LoadData);
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

        private int ConvertToNumeric(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            if (str.IndexOf('T') != -1 || str.Length < 3)
            {
                bool isNegative;
                string temp;

                if (str.IndexOf('-') == -1)
                {
                    temp = str;
                    isNegative = false;
                }
                else
                {
                    temp = str;
                    temp = temp.Substring(1);
                    isNegative = true;
                }
                if (temp.IndexOf('+') == 0)
                {
                    temp = temp.Substring(1);
                }


                string temp2;
                var value = 0;
                var loop = true;
                var holder = "";

                if (temp.Length < 3)
                {
                    value = Convert.ToInt32(temp);
                }
                else
                {
                    int x;
                    for (x = 0; x < temp.Length && loop; x++)
                    {
                        if (Char.IsDigit(temp[x]))
                        {
                            temp2 = temp;
                            holder = holder + temp2[x];
                        }
                        else
                        {
                            value = Convert.ToInt32(holder);
                            value = value * 4;
                            loop = false;
                        }
                    }
                }
                if (temp.IndexOf('+') != -1)
                {
                    var pos = temp.IndexOf('+');
                    pos = pos + 1;
                    var y = temp.Substring(pos, 1);
                    value = value + Convert.ToInt32(y);
                }
                if (isNegative)
                {
                    return -value;
                }
                return value;
            }
            return 0;
        }

        private string ConvertToT6(int skill)
        {
            string str = skill.ToString();
            str.ToUpper();
            if (str.IndexOf('T') != -1
                || str.Length < 3)
            {
                bool isNegative;
                string temp;

                if (skill.ToString().IndexOf('-') == -1)
                {
                    temp = skill.ToString();
                    isNegative = false;
                }
                else
                {
                    temp = skill.ToString();
                    temp = temp.Substring(1);
                    isNegative = true;
                }
                if (temp.IndexOf('+') == 0)
                {
                    temp = temp.Substring(1);
                }


                string temp2;
                var value = 0;
                var loop = true;
                var holder = "";

                if (temp.Length < 3)
                {
                    value = Convert.ToInt32(temp);
                }
                else
                {
                    int x;
                    for (x = 0; x < temp.Length && loop; x++)
                    {
                        if (Char.IsDigit(temp[x]))
                        {
                            temp2 = temp;
                            holder = holder + temp2[x];
                        }
                        else
                        {
                            value = Convert.ToInt32(holder);
                            value = value * 4;
                            loop = false;
                        }
                    }
                }
                if (temp.IndexOf('+') != -1)
                {
                    var pos = temp.IndexOf('+');
                    pos = pos + 1;
                    var y = temp.Substring(pos, 1);
                    value = value + Convert.ToInt32(y);
                }
                if (isNegative)
                {
                    return "-" + value;
                }
                return value.ToString();
            }
            return "0";
        }

        #endregion
    }
}
