using System.Collections.Generic;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IExpensesService
    {
        List<LivingconditionModel> GetLivingconditionList();
        ExpensesDataModel GetAllExpensesDataModel();

        int GetEmployeeSilverCost(int index);
        int SetAdultResidents(int index);
        int SetChildrenResidents(int index);
        int CalculateFeedingPoorBaseCost(int index);
        int CalculateFeedingDayworkers(int index);
        RoadModel CheckRoadUpgradeCost(int index);
        int GetArmyNumbers(int index);
        int GetArmyBaseCost(int index);
        int GetArmySilverCost(int index);
        int GetEmployeeNumbers(int index);
        int GetEmployeeBaseCost(int index);
        int GetEmployeeLuxuryCost(int index);
        int GetManorUpkeep(int index);
        int CalculateManorUpkeepBaseCost(int index);
        string GetLivingcondition(int index);
        int GetNumberOfBuildings(int index);
        int GetIronCostOfBuildings(int index);
        int GetWoodCostOfBuildings(int index);
        int GetStoneCostOfBuildings(int index);
        int GetNumberOfBoatsBuilding(int index);
        int GetBoatbuildingSilverCost(int index);
        int GetNumberOfQuarries(int index);
        int GetQuarriesBaseCost(int index);
    }
}