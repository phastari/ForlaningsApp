using System;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IFiefService
    {
        int Index { get; set; }
        int Year { get; set; }
        List<InformationDataModel> InformationList { get; set; }
        List<ArmyDataModel> ArmyList { get; set; }
        List<EmployeesDataModel> EmployeesList { get; set; }
        List<ManorDataModel> ManorList { get; set; }
        List<BoatbuildingDataModel> BoatbuildingList { get; set; }
        List<ExpensesDataModel> ExpensesList { get; set; }
        ObservableCollection<StewardModel> StewardsCollection { get; set; }
        List<SubsidiaryDataModel> SubsidiaryList { get; set; }
        List<SubsidiaryModel> CustomSubsidiaryList { get; set; }
        List<IncomeDataModel> IncomeList { get; set; }
        List<BuildingsDataModel> BuildingsList { get; set; }
        List<WeatherDataModel> WeatherList { get; set; }
        List<TradeDataModel> TradeList { get; set; }
        List<PortDataModel> PortsList { get; set; }
        List<MinesDataModel> MinesList { get; set; }

        SupplyDataModel SupplyDataModel { get; set; }

        int GetRandom(int min, int max);
    }
}