using FiefApp.Common.Infrastructure.DataModels;
using System.Collections.Generic;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IFiefService
    {
        int Index { get; set; }
        List<InformationDataModel> InformationList { get; set; }
        List<ArmyDataModel> ArmyList { get; set; }
        List<EmployeesDataModel> EmployeesList { get; set; }
        List<ManorDataModel> ManorList { get; set; }
        List<BoatbuildingDataModel> BoatbuildingList { get; set; }
        List<ExpensesDataModel> ExpensesList { get; set; }
        List<StewardsDataModel> StewardsList { get; set; }
        List<SubsidiaryDataModel> SubsidiaryList { get; set; }
        List<SubsidiaryModel> CustomSubsidiaryList { get; set; }
        List<IncomeDataModel> IncomeList { get; set; }
        List<BuildingsDataModel> BuildingsList { get; set; }
    }
}