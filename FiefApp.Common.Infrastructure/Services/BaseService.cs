using FiefApp.Common.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public class BaseService : IBaseService
    {
        private readonly IFiefService _fiefService;
        private readonly ISettingsService _settingsService;

        public BaseService(IFiefService fiefService, ISettingsService settingsService)
        {
            _fiefService = fiefService;
            _settingsService = settingsService;
        }

        public int GetIndex()
        {
            return _fiefService.Index;
        }

        public void SetIndex(int index)
        {
            _fiefService.Index = index;
        }

        public T GetDataModel<T>(int index)
        {
            if (_fiefService.InformationList.Count != 0)
            {
                if (typeof(T) == typeof(InformationDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.InformationList[index].Clone(), typeof(InformationDataModel));
                }
                else if (typeof(T) == typeof(ArmyDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.ArmyList[index].Clone(), typeof(ArmyDataModel));
                }
                else if (typeof(T) == typeof(EmployeesDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.EmployeesList[index].Clone(), typeof(EmployeesDataModel));
                }
                else if (typeof(T) == typeof(ManorDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.ManorList[index].Clone(), typeof(ManorDataModel));
                }
                else if (typeof(T) == typeof(BoatbuildingDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.BoatbuildingList[index].Clone(), typeof(BoatbuildingDataModel));
                }
                else if (typeof(T) == typeof(ExpensesDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.ExpensesList[index].Clone(), typeof(ExpensesDataModel));
                }
                else if (typeof(T) == typeof(StewardsDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.StewardsList[index].Clone(), typeof(StewardsDataModel));
                }
                else if (typeof(T) == typeof(SubsidiaryDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.SubsidiaryList[index].Clone(), typeof(SubsidiaryDataModel));
                }
                else if (typeof(T) == typeof(IncomeDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.IncomeList[index].Clone(), typeof(IncomeDataModel));
                }
                else if (typeof(T) == typeof(BuildingsDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.BuildingsList[index].Clone(), typeof(BuildingsDataModel));
                }
                else if (typeof(T) == typeof(WeatherDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.WeatherList[index].Clone(), typeof(WeatherDataModel));
                }
                else if (typeof(T) == typeof(PortDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.PortsList[index].Clone(), typeof(PortDataModel));
                }
                else if (typeof(T) == typeof(MinesDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.MinesList[index].Clone(), typeof(MinesDataModel));
                }
                else if (typeof(T) == typeof(TradeDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.TradeList[index].Clone(), typeof(TradeDataModel));
                }
                else
                {
                    return default(T);
                }
            }
            else
            {
                return default(T);
            }
        }

        public void SetDataModel(IDataModelBase dataModel, int index)
        {
            if (dataModel != null)
            {
                if (dataModel.GetType() == typeof(InformationDataModel))
                {
                    InformationDataModel tempDataModel = (InformationDataModel)dataModel;
                    _fiefService.InformationList[index] = (InformationDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(ArmyDataModel))
                {
                    ArmyDataModel tempDataModel = (ArmyDataModel)dataModel;
                    _fiefService.ArmyList[index] = (ArmyDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(EmployeesDataModel))
                {
                    EmployeesDataModel tempDataModel = (EmployeesDataModel)dataModel;
                    _fiefService.EmployeesList[index] = (EmployeesDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(ManorDataModel))
                {
                    ManorDataModel tempDataModel = (ManorDataModel)dataModel;
                    _fiefService.ManorList[index] = (ManorDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(BoatbuildingDataModel))
                {
                    BoatbuildingDataModel tempDataModel = (BoatbuildingDataModel)dataModel;
                    _fiefService.BoatbuildingList[index] = (BoatbuildingDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(ExpensesDataModel))
                {
                    ExpensesDataModel tempDataModel = (ExpensesDataModel)dataModel;
                    _fiefService.ExpensesList[index] = (ExpensesDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(StewardsDataModel))
                {
                    StewardsDataModel tempDataModel = (StewardsDataModel)dataModel;
                    for (int x = 0; x < _fiefService.StewardsList.Count; x++)
                    {
                        _fiefService.StewardsList[x] = (StewardsDataModel)tempDataModel.Clone();
                    }
                }
                else if (dataModel.GetType() == typeof(SubsidiaryDataModel))
                {
                    SubsidiaryDataModel tempDataModel = (SubsidiaryDataModel)dataModel;
                    _fiefService.SubsidiaryList[index] = (SubsidiaryDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(IncomeDataModel))
                {
                    IncomeDataModel tempDataModel = (IncomeDataModel)dataModel;
                    _fiefService.IncomeList[index] = (IncomeDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(BuildingsDataModel))
                {
                    BuildingsDataModel tempDataModel = (BuildingsDataModel)dataModel;
                    _fiefService.BuildingsList[index] = (BuildingsDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(WeatherDataModel))
                {
                    WeatherDataModel tempDataModel = (WeatherDataModel)dataModel;
                    _fiefService.WeatherList[index] = (WeatherDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(MinesDataModel))
                {
                    MinesDataModel tempDataModel = (MinesDataModel)dataModel;
                    _fiefService.MinesList[index] = (MinesDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(PortDataModel))
                {
                    PortDataModel tempDataModel = (PortDataModel)dataModel;
                    _fiefService.PortsList[index] = (PortDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(TradeDataModel))
                {
                    TradeDataModel tempDataModel = (TradeDataModel)dataModel;
                    _fiefService.TradeList[index] = (TradeDataModel)tempDataModel.Clone();
                }
                else
                {
                    Console.WriteLine("ERROR!");
                }
            }
        }

        public int RemoveFief(int index)
        {
            bool removedLast = index == _fiefService.InformationList.Count - 1;

            _fiefService.InformationList.RemoveAt(index);
            _fiefService.ArmyList.RemoveAt(index);
            _fiefService.EmployeesList.RemoveAt(index);
            _fiefService.ManorList.RemoveAt(index);
            _fiefService.BoatbuildingList.RemoveAt(index);
            _fiefService.ExpensesList.RemoveAt(index);
            _fiefService.StewardsList.RemoveAt(index);
            _fiefService.SubsidiaryList.RemoveAt(index);
            _fiefService.CustomSubsidiaryList.RemoveAt(index);
            _fiefService.IncomeList.RemoveAt(index);
            _fiefService.BuildingsList.RemoveAt(index);
            _fiefService.WeatherList.RemoveAt(index);
            _fiefService.MinesList.RemoveAt(index);
            _fiefService.PortsList.RemoveAt(index);
            _fiefService.TradeList.RemoveAt(index);

            return removedLast ? index - 1 : index;
        }

        public int CreateNewFief()
        {
            _fiefService.InformationList.Add(new InformationDataModel());
            _fiefService.ArmyList.Add(new ArmyDataModel());
            _fiefService.EmployeesList.Add(new EmployeesDataModel());
            _fiefService.ManorList.Add(new ManorDataModel());
            _fiefService.BoatbuildingList.Add(new BoatbuildingDataModel());
            _fiefService.ExpensesList.Add(new ExpensesDataModel());
            _fiefService.StewardsList.Add(new StewardsDataModel());
            _fiefService.SubsidiaryList.Add(new SubsidiaryDataModel());
            _fiefService.CustomSubsidiaryList.Add(new SubsidiaryModel());
            _fiefService.IncomeList.Add(new IncomeDataModel());
            _fiefService.BuildingsList.Add(new BuildingsDataModel());
            _fiefService.WeatherList.Add(new WeatherDataModel());
            _fiefService.MinesList.Add(new MinesDataModel());
            _fiefService.PortsList.Add(new PortDataModel());
            _fiefService.TradeList.Add(new TradeDataModel());

            return _fiefService.InformationList.Count - 1;
        }

        public ObservableCollection<string> GetFiefCollection()
        {
            ObservableCollection<string> tempCollection = new ObservableCollection<string>();

            for (int x = 0; x < _fiefService.InformationList.Count; x++)
            {
                tempCollection.Add(_fiefService.InformationList[x].FiefName);
            }

            return tempCollection;
        }

        public int RollObDice(int skill)
        {
            int total = 0;
            int nrDice = Convert.ToInt32(Math.Floor((decimal)skill / 4));
            int temp = nrDice;
            int mod = skill - nrDice * 4;
            List<int> tempList = new List<int>();
            string str = "";

            while (nrDice > 0)
            {
                int roll = _fiefService.GetRandom(1, 7);

                if (roll == 6)
                {
                    tempList.Add(6);
                    nrDice++;
                }
                else
                {
                    total += roll;
                    tempList.Add(roll);
                    nrDice--;
                }
            }

            total += mod;

            if (mod > 0)
            {
                str += $"Tärningsslag: { temp }T6+{ mod } = ";
            }
            else
            {
                str += $"Tärningsslag: { temp }T6 = ";
            }
            
            for (int i = 0; i < tempList.Count; i++)
            {
                if (i == 0)
                {
                    str += $"{ tempList[i] } ";
                }
                else
                {
                    str +=$",{ tempList[i] } ";
                }
            }

            str += $"Totalt={ total }";
            Console.WriteLine(str);
            return total;
        }

        public int RollDie(int x, int y)
        {
            return _fiefService.GetRandom(x, y);
        }
    }
}
