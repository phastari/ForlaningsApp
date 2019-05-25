using System;
using System.Collections.Generic;
using System.Linq;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public class ExpensesService : IExpensesService
    {
        private readonly ISettingsService _settingsService;
        private readonly IFiefService _fiefService;

        public ExpensesService(
            ISettingsService settingsService, 
            IFiefService fiefService
            )
        {
            _settingsService = settingsService;
            _fiefService = fiefService;
        }

        public List<LivingconditionModel> GetLivingconditionList()
        {
            return _settingsService.LivingconditionsSettingsModel.LivingconditionsList;
        }

        public ExpensesDataModel GetAllExpensesDataModel()
        {
            return null;
        }

        public int SetAdultResidents(int index)
        {
            int i = 0;

            for (int x = 1; x < _fiefService.ManorList[index].ResidentsCollection.Count; x++)
            {
                if (_fiefService.ManorList[index].ResidentsCollection[x].Age > 13)
                {
                    i++;
                }
            }

            return i;
        }

        public int SetChildrenResidents(int index)
        {
            int i = 0;

            for (int x = 1; x < _fiefService.ManorList[index].ResidentsCollection.Count; x++)
            {
                if (_fiefService.ManorList[index].ResidentsCollection[x].Age < 14)
                {
                    i++;
                }
            }

            return i;
        }

        public int CalculateFeedingPoorBaseCost(int index)
        {
            if (_fiefService.ExpensesList[index].FeedingPoor)
            {
                int pop = _fiefService.ManorList[index].VillagesCollection.Sum(t => t.Population);

                return Convert.ToInt32(Math.Ceiling(pop * _settingsService.ExpensesSettingsModel.FeedingPoorFactor));
            }
            return 0;
        }

        public int CalculateFeedingDayworkers(int index)
        {
            if (_fiefService.ExpensesList[index].FeedingDayworkers)
            {
                int workers = _fiefService.ManorList[index].VillagesCollection.Sum(t => t.Serfdoms);

                return Convert.ToInt32(Math.Ceiling(workers * _settingsService.ExpensesSettingsModel.FeedingDaysWorkFactor));
            }
            return 0;
        }

        public RoadModel CheckRoadUpgradeCost(int index)
        {
            if (
                _fiefService.InformationList[index].Roads != "" 
                || _fiefService.InformationList[index].Roads == null
            )
            {
                for (int x = 0; x < _settingsService.ExpensesSettingsModel.RoadsList.Count; x++)
                {
                    if (_fiefService.InformationList[index].Roads == _settingsService.ExpensesSettingsModel.RoadsList[x].Road)
                    {
                        if (x == _settingsService.ExpensesSettingsModel.RoadsList.Count - 1)
                        {
                            return new RoadModel()
                            {
                                Road = "NOUPGRADE!",
                                BaseCost = 0,
                                StoneCost = 0
                            };
                        }
                        else
                        {
                            return new RoadModel()
                            {
                                Road = _settingsService.ExpensesSettingsModel.RoadsList[x + 1].Road,
                                BaseCost = _settingsService.ExpensesSettingsModel.RoadsList[x + 1].BaseCost,
                                StoneCost = _settingsService.ExpensesSettingsModel.RoadsList[x + 1].StoneCost
                            };
                        }
                    }
                }
                return null;
            }
            return null;
        }

        public int GetArmyNumbers(int index)
        {
            return _fiefService.ArmyList[index].ArmyBowmen
                   + _fiefService.ArmyList[index].ArmyCrossbowmen
                   + _fiefService.ArmyList[index].ArmyEngineers
                   + _fiefService.ArmyList[index].ArmyGuards
                   + _fiefService.ArmyList[index].ArmyInfantry
                   + _fiefService.ArmyList[index].ArmyInfantryElite
                   + _fiefService.ArmyList[index].ArmyInfantryHeavy
                   + _fiefService.ArmyList[index].ArmyInfantryMedium
                   + _fiefService.ArmyList[index].ArmyKnightTemplars
                   + _fiefService.ArmyList[index].ArmyLongbowmen
                   + _fiefService.ArmyList[index].ArmyMedics
                   + _fiefService.ArmyList[index].ArmyMedicsSkilled
                   + _fiefService.ArmyList[index].ArmyMercenary
                   + _fiefService.ArmyList[index].ArmyMercenaryBowmen
                   + _fiefService.ArmyList[index].ArmyMercenaryElite
                   + _fiefService.ArmyList[index].ArmyScouts
                   + _fiefService.ArmyList[index].ArmyScoutsSkilled
                   + _fiefService.ArmyList[index].ArmySpearmen
                   + _fiefService.ArmyList[index].ArmyWeaponmasters
                   + _fiefService.ArmyList[index].CavalryBowmen
                   + _fiefService.ArmyList[index].CavalryCourier
                   + _fiefService.ArmyList[index].CavalryElite
                   + _fiefService.ArmyList[index].CavalryHeavy
                   + _fiefService.ArmyList[index].CavalryKnightTemplars
                   + _fiefService.ArmyList[index].CavalryKnights
                   + _fiefService.ArmyList[index].CavalryLight
                   + _fiefService.ArmyList[index].CavalryScouts
                   + _fiefService.ArmyList[index].OfficersCaptain
                   + _fiefService.ArmyList[index].OfficersCorporal
                   + _fiefService.ArmyList[index].OfficersSergeant;
        }

        public int GetArmyBaseCost(int index)
        {
            return _fiefService.ArmyList[index].TotalBase;
        }

        public int GetArmySilverCost(int index)
        {
            return _fiefService.ArmyList[index].TotalSilver;
        }

        public int GetEmployeeNumbers(int index)
        {
            return _fiefService.EmployeesList[index].Bailiff
                   + _fiefService.EmployeesList[index].Cupbearer
                   + _fiefService.EmployeesList[index].Falconer
                   + _fiefService.EmployeesList[index].Herald
                   + _fiefService.EmployeesList[index].Hunter
                   + _fiefService.EmployeesList[index].Physician
                   + _fiefService.EmployeesList[index].Prospector
                   + _fiefService.EmployeesList[index].Scholar
                   + _fiefService.EmployeesList[index].EmployeesCollection.Count
                   + _fiefService.StewardsDataModel.StewardsCollection.Count;
        }

        public int GetEmployeeBaseCost(int index)
        {
            return _fiefService.EmployeesList[index].TotalBase
                   + _fiefService.StewardsDataModel.StewardsCollection.Count * 4;
        }

        public int GetEmployeeLuxuryCost(int index)
        {
            return _fiefService.EmployeesList[index].TotalLuxury
                   + _fiefService.StewardsDataModel.StewardsCollection.Count * 2;
        }

        public int GetManorUpkeep(int index)
        {
            return _fiefService.WeatherList[index].ManorUpkeep;
        }

        public int CalculateManorUpkeepBaseCost(int index)
        {
            if (_fiefService.WeatherList[index].ManorUpkeep > 0)
            {
                return Convert.ToInt32(Math.Ceiling((decimal)_fiefService.ManorList[index].ManorAcres / 40 * _fiefService.ManorList[index].ManorAcres / _fiefService.WeatherList[index].ManorUpkeep));
            }
            else
            {
                return Convert.ToInt32(Math.Ceiling((decimal)_fiefService.ManorList[index].ManorAcres / 40 * _fiefService.ManorList[index].ManorAcres));
            }
        }

        public string GetLivingcondition(int index)
        {
            if (string.IsNullOrEmpty(_fiefService.ManorList[index].ManorLivingconditions))
            {
                return "";
            }
            else
            {
                return _fiefService.ManorList[index].ManorLivingconditions;
            }
        }

        public int GetNumberOfBuildings(int index)
        {

            return _fiefService.BuildingsList[index].BuildingsCollection.Sum(t => t.Amount);
        }

        public int GetIronCostOfBuildings(int index)
        {

            return _fiefService.BuildingsList[index].BuildingsCollection.Sum(t => t.IronThisYear);
        }

        public int GetWoodCostOfBuildings(int index)
        {

            return _fiefService.BuildingsList[index].BuildingsCollection.Sum(t => t.WoodThisYear);
        }

        public int GetStoneCostOfBuildings(int index)
        {

            return _fiefService.BuildingsList[index].BuildingsCollection.Sum(t => t.StoneThisYear);
        }

        public int GetNumberOfBoatsBuilding(int index)
        {
            return _fiefService.BoatbuildingList[index].BoatsBuildingCollection.Sum(t => t.Amount);
        }

        public int GetBoatbuildingSilverCost(int index)
        {
            return _fiefService.BoatbuildingList[index].BoatsBuildingCollection.Where(t => t.BuildTimeInDaysAll < 365).Sum(t => t.CostWhenFinishedSilver);
        }
    }
}
