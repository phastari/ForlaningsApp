using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xaml;

namespace FiefApp.Common.Infrastructure.Services
{
    public class FiefService : IFiefService
    {
        public int Index { get; set; } = 1;
        public int Year { get; set; } = 2967;
        public List<InformationDataModel> InformationList { get; set; } = new List<InformationDataModel>();
        public List<ArmyDataModel> ArmyList { get; set; } = new List<ArmyDataModel>();
        public List<EmployeesDataModel> EmployeesList { get; set; } = new List<EmployeesDataModel>();
        public List<ManorDataModel> ManorList { get; set; } = new List<ManorDataModel>();
        public List<BoatbuildingDataModel> BoatbuildingList { get; set; } = new List<BoatbuildingDataModel>();
        public List<ExpensesDataModel> ExpensesList { get; set; } = new List<ExpensesDataModel>();
        public List<StewardsDataModel> StewardsList { get; set; } = new List<StewardsDataModel>();
        public List<SubsidiaryDataModel> SubsidiaryList { get; set; } = new List<SubsidiaryDataModel>();
        public List<SubsidiaryModel> CustomSubsidiaryList { get; set; } = new List<SubsidiaryModel>();
        public List<IncomeDataModel> IncomeList { get; set; } = new List<IncomeDataModel>();
        public List<BuildingsDataModel> BuildingsList { get; set; } = new List<BuildingsDataModel>();
        public List<WeatherDataModel> WeatherList { get; set; } = new List<WeatherDataModel>();
        public List<MinesDataModel> MinesList { get; set; } = new List<MinesDataModel>();
        public List<PortDataModel> PortsList { get; set; } = new List<PortDataModel>();
        public List<TradeDataModel> TradeList { get; set; } = new List<TradeDataModel>();
        public SupplyDataModel SupplyDataModel { get; set; } = new SupplyDataModel();

        private readonly Random _getRandom = new Random(Guid.NewGuid().GetHashCode() * Guid.NewGuid().GetHashCode());
        public int GetRandom(int min, int max)
        {
            lock (_getRandom)
            {
                return _getRandom.Next(min, max);
            }
        }
    }
}
