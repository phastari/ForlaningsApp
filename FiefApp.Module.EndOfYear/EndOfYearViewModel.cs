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
            DataModel.EnableButton = false;
            bool checkStewardsDeaths = true;
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

                #region IncomeModule

                str += $"Inkomster:{Environment.NewLine}";
                for (int i = 0; i < DataModel.IncomeListFief[x - 1].OtherIncomesList.Count; i++)
                {
                    if (DataModel.IncomeListFief[x - 1].OtherIncomesList[i].Silver > -1)
                    {
                        silver += DataModel.IncomeListFief[x - 1].OtherIncomesList[i].Silver;
                        str += $"{DataModel.IncomeListFief[x - 1].OtherIncomesList[i].Income.PadRight(15)}{Convert.ToString(DataModel.IncomeListFief[x - 1].OtherIncomesList[i].Silver).PadLeft(8)} Silver{Environment.NewLine}";
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

                str += Environment.NewLine;

                #endregion

                #region ArmyModule

                //silver += _fiefService.ArmyList[x].TotalSilver;
                //bas += _fiefService.ArmyList[x].TotalBase;

                #endregion

            }


            str += $"TOTALT:{Environment.NewLine}";
            str += $"{silver.ToString().PadRight(8)} Silver{Environment.NewLine}";
            str += $"{bas.ToString().PadRight(8)} Bas{Environment.NewLine}";
            str += $"{lyx.ToString().PadRight(8)} Lyx{Environment.NewLine}";
            str += $"{iron.ToString().PadRight(8)} Järn{Environment.NewLine}";
            str += $"{stone.ToString().PadRight(8)} Sten{Environment.NewLine}";
            str += $"{wood.ToString().PadRight(8)} Timmer{Environment.NewLine}";

            _eventAggregator.GetEvent<EndOfYearEvent>().Publish();

            string filePath = $@"{AppDomain.CurrentDomain.BaseDirectory}/Reports/Bokslut_{_fiefService.Year - 1}.txt";
            System.IO.FileInfo file = new System.IO.FileInfo(filePath);
            file.Directory?.Create();
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
