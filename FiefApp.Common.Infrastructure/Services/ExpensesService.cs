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
            // UPDATE ALL EXPENSESDATAMODELS
            ExpensesDataModel model = new ExpensesDataModel();
            model.Livingcondition = "";

            for (int x = 1; x < _fiefService.ExpensesList.Count; x++)
            {
                model.ExpensesSilver += _fiefService.ExpensesList[x].ExpensesSilver;
                model.ExpensesBase += _fiefService.ExpensesList[x].ExpensesBase;
                model.ExpensesLuxury += _fiefService.ExpensesList[x].ExpensesLuxury;
                model.ExpensesStone += _fiefService.ExpensesList[x].ExpensesStone;
                model.ExpensesWood += _fiefService.ExpensesList[x].ExpensesWood;
                model.ExpensesIron += _fiefService.ExpensesList[x].ExpensesIron;
                model.ResidentAdults += _fiefService.ExpensesList[x].ResidentAdults;
                model.ResidentAdultsBase += _fiefService.ExpensesList[x].ResidentAdultsBase;
                model.ResidentAdultsLuxury += _fiefService.ExpensesList[x].ResidentAdultsLuxury;
                model.ResidentChildren += _fiefService.ExpensesList[x].ResidentChildren;
                model.ResidentChildrenBase += _fiefService.ExpensesList[x].ResidentChildrenBase;
                model.ResidentChildrenLuxury += _fiefService.ExpensesList[x].ResidentChildrenLuxury;
                model.Quarries += _fiefService.MinesList[x].QuarriesCollection.Count;
                model.QuarriesBase += _fiefService.MinesList[x].QuarriesCollection.Sum(o => o.Upkeep);

                if (x == _fiefService.ExpensesList.Count - 1)
                {
                    model.Livingcondition += $"{_fiefService.InformationList[x].FiefName}: {_fiefService.ExpensesList[x].Livingcondition}.";
                }
                else
                {
                    model.Livingcondition += $"{_fiefService.InformationList[x].FiefName}: {_fiefService.ExpensesList[x].Livingcondition}, ";
                }

                model.StableRidingHorses += _fiefService.ExpensesList[x].StableRidingHorses;
                model.StableRidingHorsesBase += _fiefService.ExpensesList[x].StableRidingHorsesBase;
                model.StableWarHorses += _fiefService.ExpensesList[x].StableWarHorses;
                model.StableWarHorsesBase += _fiefService.ExpensesList[x].StableWarHorsesBase;
                model.FeedingPoorBase += _fiefService.ExpensesList[x].FeedingPoorBase;
                model.FeedingDayworkersBase += _fiefService.ExpensesList[x].FeedingDayworkersBase;
                model.Builds += _fiefService.ExpensesList[x].Builds;
                model.BuildsIron += _fiefService.ExpensesList[x].BuildsIron;
                model.BuildsStone += _fiefService.ExpensesList[x].BuildsStone;
                model.BuildsWood += _fiefService.ExpensesList[x].BuildsWood;
                model.BoatBuilds += _fiefService.ExpensesList[x].BoatBuilds;
                model.BoatBuildsSilver += _fiefService.ExpensesList[x].BoatBuildsSilver;
                model.ManorMaintenance += _fiefService.ExpensesList[x].ManorMaintenance;
                model.ManorMaintenanceBase += _fiefService.ExpensesList[x].ManorMaintenanceBase;
                model.ImproveRoadsBase += _fiefService.ExpensesList[x].ImproveRoadsBase;
                model.ImproveRoadsStone += _fiefService.ExpensesList[x].ImproveRoadsStone;
                model.DayWorkers += _fiefService.ExpensesList[x].DayWorkers;
                model.DayWorkersBase += _fiefService.ExpensesList[x].DayWorkersBase;
                model.GiftsReligionLuxury += _fiefService.ExpensesList[x].GiftsReligionLuxury;
                model.Slaves += _fiefService.ExpensesList[x].Slaves;
                model.SlavesBase += _fiefService.ExpensesList[x].SlavesBase;
                model.BuildingsMaintenance += _fiefService.ExpensesList[x].BuildingsMaintenance;
                model.BuildingsMaintenanceBase += _fiefService.ExpensesList[x].BuildingsMaintenanceBase;
                model.Feasts += _fiefService.ExpensesList[x].Feasts;
                model.FeastsBase += _fiefService.ExpensesList[x].FeastsBase;
                model.FeastsLuxury += _fiefService.ExpensesList[x].FeastsLuxury;
                model.PeopleFeasts += _fiefService.ExpensesList[x].PeopleFeasts;
                model.PeopleFeastsBase += _fiefService.ExpensesList[x].PeopleFeastsBase;
                model.PeopleFeastsLuxury += _fiefService.ExpensesList[x].PeopleFeastsLuxury;
                model.ReligiousFeasts += _fiefService.ExpensesList[x].ReligiousFeasts;
                model.ReligiousFeastsBase += _fiefService.ExpensesList[x].ReligiousFeastsBase;
                model.ReligiousFeastsLuxury += _fiefService.ExpensesList[x].ReligiousFeastsLuxury;
                model.ReligiousFeastsSilver += _fiefService.ExpensesList[x].ReligiousFeastsSilver;
                model.Tournaments += _fiefService.ExpensesList[x].Tournaments;
                model.TournamentsBase += _fiefService.ExpensesList[x].TournamentsBase;
                model.TournamentsLuxury += _fiefService.ExpensesList[x].TournamentsLuxury;
                model.TournamentsSilver += _fiefService.ExpensesList[x].TournamentsSilver;
                model.Others += _fiefService.ExpensesList[x].Others;
                model.OthersBase += _fiefService.ExpensesList[x].OthersBase;
                model.OthersLuxury += _fiefService.ExpensesList[x].OthersLuxury;
                model.OthersSilver += _fiefService.ExpensesList[x].OthersSilver;
                model.Bought += _fiefService.ExpensesList[x].Bought;
                model.SoldBase += _fiefService.ExpensesList[x].SoldBase;
                model.SoldIron += _fiefService.ExpensesList[x].SoldIron;
                model.SoldLuxury += _fiefService.ExpensesList[x].SoldLuxury;
                model.SoldStone += _fiefService.ExpensesList[x].SoldStone;
                model.SoldWood += _fiefService.ExpensesList[x].SoldWood;
                model.Employees += _fiefService.ExpensesList[x].Employees;
                model.EmployeesSilver += _fiefService.ExpensesList[x].EmployeesSilver;
                model.EmployeesBase += _fiefService.ExpensesList[x].EmployeesBase;
                model.EmployeesLuxury += _fiefService.ExpensesList[x].EmployeesLuxury;
                model.Army += _fiefService.ExpensesList[x].Army;
                model.ArmyBase += _fiefService.ExpensesList[x].ArmyBase;
                model.ArmySilver += _fiefService.ExpensesList[x].ArmySilver;
                model.Fleet += _fiefService.ExpensesList[x].Fleet;
                model.FleetBase += _fiefService.ExpensesList[x].FleetBase;
                model.FleetLuxury += _fiefService.ExpensesList[x].FleetLuxury;
                model.FleetSilver += _fiefService.ExpensesList[x].FleetSilver;
            }
            model.IsAll = true;
            model.CalculateTotals();
            return model;
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
                   + _fiefService.StewardsDataModel.StewardsCollection.Count - 1
                   + _fiefService.BoatbuildingList[index].BoatBuildersCollection.Count
                   + _fiefService.BuildingsList[index].Smiths
                   + _fiefService.BuildingsList[index].Woodworkers
                   + _fiefService.BuildingsList[index].Stoneworkers
                   + _fiefService.BuildingsList[index].BuildersCollection.Count;
        }

        public int GetEmployeeSilverCost(int index)
        {
            return _fiefService.BuildingsList[index].BuildersCollection.Count * 4850
                   + _fiefService.BoatbuildingList[index].BoatBuildersCollection.Count * 1800;
        }

        public int GetEmployeeBaseCost(int index)
        {
            return _fiefService.EmployeesList[index].TotalBase
                   + (_fiefService.StewardsDataModel.StewardsCollection.Count - 1) * 4
                   + _fiefService.BuildingsList[index].SmithsBase
                   + _fiefService.BuildingsList[index].WoodworkersBase
                   + _fiefService.BuildingsList[index].StoneworkersBase;
        }

        public int GetEmployeeLuxuryCost(int index)
        {
            return _fiefService.EmployeesList[index].TotalLuxury
                   + (_fiefService.StewardsDataModel.StewardsCollection.Count - 1) * 2;
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

        public int GetNumberOfQuarries(int index)
        {
            return _fiefService.MinesList[index].QuarriesCollection.Count;
        }

        public int GetQuarriesBaseCost(int index)
        {
            return _fiefService.MinesList[index].QuarriesCollection.Sum(o => o.Upkeep);
        }
    }
}
