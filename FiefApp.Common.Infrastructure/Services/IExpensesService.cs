using System.Collections.Generic;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IExpensesService
    {
        List<LivingconditionModel> GetLivingconditionList();
        ExpensesDataModel GetAllExpensesDataModel();

        int GetNumberOfDayWorkers(int index);
        int GetBaseCostForDayWorkers(int index);
        int GetNumberOfSlaves(int index);
        int GetBaseCostForSlaves(int index);
        int GetEmployeeSilverCost(int index);
        int SetAdultResidents(int index);
        int SetChildrenResidents(int index);
        int GetResidentAdultsBase(int index, int livingConditionIndex);
        int GetResidentAdultsLuxury(int index, int livingConditionIndex);
        int GetResidentChildrenBase(int index, int livingConditionIndex);
        int GetResidentChildrenLuxury(int index, int livingConditionIndex);
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
        int GetBaseCostForBuildings(int index);
        int GetNumberOfBuilds(int index);
        int GetIronCostOfBuilds(int index);
        int GetWoodCostOfBuilds(int index);
        int GetStoneCostOfBuilds(int index);
        int GetNumberOfBoatsBuilding(int index);
        int GetBoatbuildingSilverCost(int index);
        int GetNumberOfQuarries(int index);
        int GetQuarriesBaseCost(int index);
    }
}