using System;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using Prism.Commands;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using Prism.Events;

namespace FiefApp.Module.Expenses
{
    public class ExpensesViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IExpensesService _expensesService;
        private readonly ISettingsService _settingsService;
        private readonly IEventAggregator _eventAggregator;
        private List<UpdateEventParameters> _awaitResponsList = new List<UpdateEventParameters>()
        {
            new UpdateEventParameters()
            {
                ModuleName = "Army",
                Completed = true
            },
            new UpdateEventParameters()
            {
                ModuleName = "Boatbuilding",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Buildings",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Employees",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Expenses",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Income",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Information",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Manor",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Mines",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Port",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Stewards",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Subsidiary",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Trade",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Weather",
                Completed = false
            }
        };

        public ExpensesViewModel(
            IBaseService baseService,
            IExpensesService expensesService,
            ISettingsService settingsService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            _baseService = baseService;
            _expensesService = expensesService;
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;

            TabName = "Utgifter";

            LivingconditionsList = _expensesService.GetLivingconditionList();

            EditButtonCommand = new DelegateCommand(ExecuteEditButtonCommand);
            CancelEditingButtonCommand = new DelegateCommand(ExecuteCancelEditingButtonCommand);
            SaveEditedButtonCommand = new DelegateCommand(ExecuteSaveEditedButtonCommand);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
            _eventAggregator.GetEvent<SaveDataModelBeforeSaveFileIsCreatedEvent>().Subscribe(ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent);
            _eventAggregator.GetEvent<UpdateAllEvent>().Subscribe(UpdateAndRespond);
            _eventAggregator.GetEvent<UpdateEvent>().Subscribe(UpdateResponse);
            _eventAggregator.GetEvent<UpdateResponseEvent>().Subscribe(HandleUpdateEvent);
            _eventAggregator.GetEvent<EndOfYearCompletedEvent>().Subscribe(HandleEndOfYearComplete);
        }

        private void HandleEndOfYearComplete()
        {
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<ExpensesDataModel>(x);
                GetInformationSetDataModel(x);
                SaveData(x);
            }
        }

        private void HandleUpdateEvent(UpdateEventParameters param)
        {
            if (param.Publisher == "Expenses"
                && _awaitResponsList != null)
            {
                for (int x = 0; x < _awaitResponsList.Count; x++)
                {
                    if (_awaitResponsList[x].ModuleName == param.ModuleName)
                    {
                        _awaitResponsList[x].Completed = param.Completed;
                    }
                }

                if (_awaitResponsList.Any(o => o.Completed == false))
                {
                    Console.WriteLine("Wait!");
                }
                else
                {
                    for (int y = 0; y < _awaitResponsList.Count; y++)
                    {
                        _awaitResponsList[y].Completed = false;
                    }
                    CompleteLoadData();
                }
            }
        }

        private void UpdateResponse(string str)
        {
            if (str != "Expenses")
            {
                UpdateFiefCollection();
                for (int x = 1; x < FiefCollection.Count; x++)
                {
                    DataModel = _baseService.GetDataModel<ExpensesDataModel>(x);
                    GetInformationSetDataModel(x);
                    SaveData(x);
                }

                _eventAggregator.GetEvent<UpdateResponseEvent>().Publish(new UpdateEventParameters()
                {
                    ModuleName = "Expenses",
                    Completed = true,
                    Publisher = str
                });
            }
        }

        private void CompleteLoadData()
        {
            DataModel = Index
                        == 0 ? _expensesService.GetAllExpensesDataModel()
                : _baseService.GetDataModel<ExpensesDataModel>(Index);

            if (Index != 0)
            {
                DataModel.PropertyChanged += DataModelPropertyChanged;

                GetInformationSetDataModel();
                DataModel.CalculateTotals();
            }

            UpdateFiefCollection();
        }

        private void UpdateAndRespond()
        {
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<ExpensesDataModel>(x);
                GetInformationSetDataModel(x);
                SaveData(x);
            }

            _eventAggregator.GetEvent<UpdateAllResponseEvent>().Publish(new UpdateAllEventParameters()
            {
                ModuleName = "Expenses",
                Completed = true
            });
        }

        #region DelegateCommand : EditButtonCommand

        public DelegateCommand EditButtonCommand { get; set; }
        private void ExecuteEditButtonCommand()
        {
            SaveData();
            DataModel = (ExpensesDataModel)DataModel.Clone();
        }

        #endregion
        #region DelegateCommand : CancelEditingButtonCommand

        public DelegateCommand CancelEditingButtonCommand { get; set; }
        private void ExecuteCancelEditingButtonCommand()
        {
            LoadData();
        }

        #endregion
        #region DelegateCommand : SaveEditedButtonCommand

        public DelegateCommand SaveEditedButtonCommand { get; set; }
        private void ExecuteSaveEditedButtonCommand()
        {
            SaveData();
            UpdateFiefCollection();
        }

        #endregion

        #region DataModel

        private ExpensesDataModel _dataModel;
        public ExpensesDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        #region UI Properties

        private List<LivingconditionModel> _livingconditionsList = new List<LivingconditionModel>();
        public List<LivingconditionModel> LivingconditionsList
        {
            get => _livingconditionsList;
            set => SetProperty(ref _livingconditionsList, value);
        }

        #endregion

        #region Methods

        protected override void SaveData(int index = -1)
        {
            if (Index != 0)
            {
                _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
            }
        }
        protected override void LoadData()
        {
            //if (_triggerLoad)
            //{
            //    _triggerLoad = false;
            //    for (int x = 0; x < _awaitResponsList.Count; x++)
            //    {
            //        if (_awaitResponsList[x].ModuleName == "Expenses")
            //        {
            //            _awaitResponsList[x].Completed = true;
            //        }
            //        else
            //        {
            //            _awaitResponsList[x].Completed = false;
            //        }
            //    }
            //    _eventAggregator.GetEvent<UpdateEvent>().Publish("Expenses");
            //}
            CompleteLoadData();
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            CompleteLoadData();
        }

        private void DataModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "FeedingPoor":
                    DataModel.FeedingPoorBase = DataModel.FeedingPoor
                        ? _expensesService.CalculateFeedingPoorBaseCost(Index)
                        : 0;
                    break;

                case "FeedingDayworkers":
                    DataModel.FeedingDayworkersBase = DataModel.FeedingDayworkers
                        ? _expensesService.CalculateFeedingDayworkers(Index)
                        : 0;
                    break;

                case "ImproveRoads" when DataModel.ImproveRoads:
                {
                    RoadModel tempRoadModel = _expensesService.CheckRoadUpgradeCost(Index);
                    if (tempRoadModel != null)
                    {
                        if (tempRoadModel.Road != "NOUPGRADE!")
                        {
                            DataModel.ImproveRoadsBase = tempRoadModel.BaseCost;
                            DataModel.ImproveRoadsStone = tempRoadModel.StoneCost;
                        }
                        else
                        {
                            DataModel.ImproveRoadsBase = 0;
                            DataModel.ImproveRoadsStone = 0;
                        }
                    }
                    else
                    {
                        DataModel.ImproveRoadsBase = 0;
                        DataModel.ImproveRoadsStone = 0;
                    }
                    break;
                }

                case "ImproveRoads":
                    DataModel.ImproveRoadsBase = 0;
                    DataModel.ImproveRoadsStone = 0;
                    break;

                case "StableRidingHorses":
                    DataModel.StableRidingHorsesBase = DataModel.StableRidingHorses * Convert.ToInt32(_settingsService.StableSettingsModel.StableList[0].BaseCost);
                    break;

                case "StableWarHorses":
                    DataModel.StableWarHorsesBase = DataModel.StableWarHorses * Convert.ToInt32(_settingsService.StableSettingsModel.StableList[1].BaseCost);
                    break;

                case "Feasts":
                    DataModel.FeastsBase = DataModel.Feasts * _settingsService.ExpensesSettingsModel.EventList[_settingsService.ExpensesSettingsModel.EventList.FindIndex(o => o.EventName == "Feast")].BaseCost;
                    DataModel.FeastsLuxury = DataModel.Feasts * _settingsService.ExpensesSettingsModel.EventList[_settingsService.ExpensesSettingsModel.EventList.FindIndex(o => o.EventName == "Feast")].LuxuryCost;
                    break;

                case "PeopleFeasts":
                    DataModel.PeopleFeastsBase = DataModel.PeopleFeasts * _settingsService.ExpensesSettingsModel.EventList[_settingsService.ExpensesSettingsModel.EventList.FindIndex(o => o.EventName == "PeopleFeasts")].BaseCost;
                    DataModel.PeopleFeastsLuxury = DataModel.PeopleFeasts * _settingsService.ExpensesSettingsModel.EventList[_settingsService.ExpensesSettingsModel.EventList.FindIndex(o => o.EventName == "PeopleFeasts")].LuxuryCost;
                    break;

                case "ReligiousFeasts":
                    DataModel.ReligiousFeastsSilver = DataModel.ReligiousFeasts * _settingsService.ExpensesSettingsModel.EventList[_settingsService.ExpensesSettingsModel.EventList.FindIndex(o => o.EventName == "Religious")].SilverCost;
                    DataModel.ReligiousFeastsBase = DataModel.ReligiousFeasts * _settingsService.ExpensesSettingsModel.EventList[_settingsService.ExpensesSettingsModel.EventList.FindIndex(o => o.EventName == "Religious")].BaseCost;
                    DataModel.ReligiousFeastsLuxury = DataModel.ReligiousFeasts * _settingsService.ExpensesSettingsModel.EventList[_settingsService.ExpensesSettingsModel.EventList.FindIndex(o => o.EventName == "Religious")].LuxuryCost;
                    break;

                case "Tournaments":
                    DataModel.TournamentsSilver = DataModel.Tournaments * _settingsService.ExpensesSettingsModel.EventList[_settingsService.ExpensesSettingsModel.EventList.FindIndex(o => o.EventName == "Tourney")].SilverCost;
                    DataModel.TournamentsBase = DataModel.Tournaments * _settingsService.ExpensesSettingsModel.EventList[_settingsService.ExpensesSettingsModel.EventList.FindIndex(o => o.EventName == "Tourney")].BaseCost;
                    DataModel.TournamentsLuxury = DataModel.Tournaments * _settingsService.ExpensesSettingsModel.EventList[_settingsService.ExpensesSettingsModel.EventList.FindIndex(o => o.EventName == "Tourney")].LuxuryCost;
                    break;

                case "CalculateAdultResidentsCost":
                    DataModel.ResidentAdultsBase = DataModel.ResidentAdults * _settingsService.LivingconditionsSettingsModel.LivingconditionsList[DataModel.LivingconditionIndex].BaseCost;
                    DataModel.ResidentAdultsLuxury = DataModel.ResidentAdults * _settingsService.LivingconditionsSettingsModel.LivingconditionsList[DataModel.LivingconditionIndex].LuxuryCost;
                    break;

                case "CalculateChildrenResidentsCost":
                    DataModel.ResidentChildrenBase = Convert.ToInt32(Math.Ceiling(DataModel.ResidentChildren * (decimal)_settingsService.LivingconditionsSettingsModel.LivingconditionsList[DataModel.LivingconditionIndex].BaseCost));
                    DataModel.ResidentChildrenLuxury = Convert.ToInt32(Math.Floor(DataModel.ResidentChildren * (decimal)_settingsService.LivingconditionsSettingsModel.LivingconditionsList[DataModel.LivingconditionIndex].LuxuryCost));
                    break;
            }
        }

        #endregion

        #region GetInformationSetDataModel

        private void GetInformationSetDataModel(int index = -1)
        {
            if (index == -1)
            {
                GetManorUpkeep();
                CalculateUpkeepManor();
                CalculateFeedingCosts();
                GetLivingcondition();
                GetBuildsCosts();
                GetBoatCosts();
                GetQuarriesInformation();
                GetArmyInformation();
                GetEmployeesInformation();
                GetResidentsInformation();
                GetDayWorkersInformation();
                GetSlavesInformation();
                GetBuildingsInformation();
            }
            else
            {
                GetManorUpkeep(index);
                CalculateUpkeepManor(index);
                CalculateFeedingCosts(index);
                GetLivingcondition(index);
                GetBuildsCosts(index);
                GetBoatCosts(index);
                GetQuarriesInformation(index);
                GetArmyInformation(index);
                GetEmployeesInformation(index);
                GetResidentsInformation(index);
                GetDayWorkersInformation(index);
                GetSlavesInformation(index);
                GetBuildingsInformation(index);
            }
        }

        private void GetDayWorkersInformation(int index = -1)
        {
            if (index == -1)
            {
                DataModel.DayWorkers = _expensesService.GetNumberOfDayWorkers(Index);
                DataModel.DayWorkersBase = _expensesService.GetBaseCostForDayWorkers(Index);
            }
            else
            {
                DataModel.DayWorkers = _expensesService.GetNumberOfDayWorkers(index);
                DataModel.DayWorkers = _expensesService.GetBaseCostForDayWorkers(index);
            }
        }

        private void GetResidentsInformation(int index = -1)
        {
            if (index == -1)
            {
                DataModel.ResidentAdults = _expensesService.SetAdultResidents(Index);
                DataModel.ResidentChildren = _expensesService.SetChildrenResidents(Index);
            }
            else
            {
                DataModel.ResidentAdults = _expensesService.SetAdultResidents(index);
                DataModel.ResidentChildren = _expensesService.SetChildrenResidents(index);
            }
        }

        private void GetSlavesInformation(int index = -1)
        {
            if (index == -1)
            {
                DataModel.Slaves = _expensesService.GetNumberOfSlaves(Index);
                DataModel.SlavesBase = _expensesService.GetBaseCostForSlaves(Index);
            }
            else
            {
                DataModel.Slaves = _expensesService.GetNumberOfSlaves(index);
                DataModel.SlavesBase = _expensesService.GetBaseCostForSlaves(index);
            }
        }

        private void GetBoatCosts(int index = -1)
        {
            if (index == -1)
            {
                DataModel.BoatBuilds = _expensesService.GetNumberOfBoatsBuilding(Index);
                DataModel.BoatBuildsSilver = _expensesService.GetBoatbuildingSilverCost(Index);
            }
            else
            {
                DataModel.BoatBuilds = _expensesService.GetNumberOfBoatsBuilding(index);
                DataModel.BoatBuildsSilver = _expensesService.GetBoatbuildingSilverCost(index);
            }
        }

        private void GetBuildingsInformation(int index = -1)
        {
            if (index == -1)
            {
                DataModel.BuildingsMaintenance = _expensesService.GetNumberOfBuildings(Index);
                DataModel.BuildingsMaintenanceBase = _expensesService.GetBaseCostForBuildings(Index);
            }
            else
            {
                DataModel.BuildingsMaintenance = _expensesService.GetNumberOfBuildings(index);
                DataModel.BuildingsMaintenanceBase = _expensesService.GetBaseCostForBuildings(index);
            }
        }

        private void GetBuildsCosts(int index = -1)
        {
            if (index == -1)
            {
                DataModel.Builds = _expensesService.GetNumberOfBuilds(Index);
                DataModel.BuildsIron = _expensesService.GetIronCostOfBuilds(Index);
                DataModel.BuildsStone = _expensesService.GetStoneCostOfBuilds(Index);
                DataModel.BuildsWood = _expensesService.GetWoodCostOfBuilds(Index);
            }
            else
            {
                DataModel.Builds = _expensesService.GetNumberOfBuilds(index);
                DataModel.BuildsIron = _expensesService.GetIronCostOfBuilds(index);
                DataModel.BuildsStone = _expensesService.GetStoneCostOfBuilds(index);
                DataModel.BuildsWood = _expensesService.GetWoodCostOfBuilds(index);
            }
        }

        private void GetManorUpkeep(int index = -1)
        {
            if (index == -1)
            {
                DataModel.ManorMaintenance = _expensesService.GetManorUpkeep(Index);
            }
            else
            {
                DataModel.ManorMaintenance = _expensesService.GetManorUpkeep(index);
            }
        }

        private void CalculateUpkeepManor(int index = -1)
        {
            if (index == -1)
            {
                DataModel.ManorMaintenanceBase = _expensesService.CalculateManorUpkeepBaseCost(Index);
            }
            else
            {
                DataModel.ManorMaintenanceBase = _expensesService.CalculateManorUpkeepBaseCost(index);
            }
        }

        private void CalculateFeedingCosts(int index = -1)
        {
            if (index == -1)
            {
                if (DataModel.FeedingPoor)
                {
                    DataModel.FeedingPoorBase = _expensesService.CalculateFeedingPoorBaseCost(Index);
                }
                else
                {
                    DataModel.FeedingPoorBase = 0;
                }

                if (DataModel.FeedingDayworkers)
                {
                    DataModel.FeedingDayworkersBase = _expensesService.CalculateFeedingDayworkers(Index);
                }
                else
                {
                    DataModel.FeedingDayworkersBase = 0;
                }
            }
            else
            {
                if (DataModel.FeedingPoor)
                {
                    DataModel.FeedingPoorBase = _expensesService.CalculateFeedingPoorBaseCost(index);
                }
                else
                {
                    DataModel.FeedingPoorBase = 0;
                }

                if (DataModel.FeedingDayworkers)
                {
                    DataModel.FeedingDayworkersBase = _expensesService.CalculateFeedingDayworkers(index);
                }
                else
                {
                    DataModel.FeedingDayworkersBase = 0;
                }
            }
        }

        private void GetLivingcondition(int index = -1)
        {
            if (index == -1)
            {
                DataModel.Livingcondition = _expensesService.GetLivingcondition(Index);
            }
            else
            {
                DataModel.Livingcondition = _expensesService.GetLivingcondition(index);
            }
        }

        private void GetQuarriesInformation(int index = -1)
        {
            if (index == -1)
            {
                DataModel.Quarries = _expensesService.GetNumberOfQuarries(Index);
                DataModel.QuarriesBase = _expensesService.GetQuarriesBaseCost(Index);
            }
            else
            {
                DataModel.Quarries = _expensesService.GetNumberOfQuarries(index);
                DataModel.QuarriesBase = _expensesService.GetQuarriesBaseCost(index);
            }
        }

        private void GetArmyInformation(int index = -1)
        {
            if (index == -1)
            {
                DataModel.Army = _expensesService.GetArmyNumbers(Index);
                DataModel.ArmyBase = _expensesService.GetArmyBaseCost(Index);
                DataModel.ArmySilver = _expensesService.GetArmySilverCost(Index);
            }
            else
            {
                DataModel.Army = _expensesService.GetArmyNumbers(index);
                DataModel.ArmyBase = _expensesService.GetArmyBaseCost(index);
                DataModel.ArmySilver = _expensesService.GetArmySilverCost(index);
            }
        }

        private void GetEmployeesInformation(int index = -1)
        {
            if (index == -1)
            {
                DataModel.Employees = _expensesService.GetEmployeeNumbers(Index);
                DataModel.EmployeesBase = _expensesService.GetEmployeeBaseCost(Index);
                DataModel.EmployeesLuxury = _expensesService.GetEmployeeLuxuryCost(Index);
                DataModel.EmployeesSilver = _expensesService.GetEmployeeSilverCost(Index);
            }
            else
            {
                DataModel.Employees = _expensesService.GetEmployeeNumbers(index);
                DataModel.EmployeesBase = _expensesService.GetEmployeeBaseCost(index);
                DataModel.EmployeesLuxury = _expensesService.GetEmployeeLuxuryCost(index);
                DataModel.EmployeesSilver = _expensesService.GetEmployeeSilverCost(index);
            }
        }

        #endregion

        private void ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent()
        {
            SaveData();
        }
    }
}
