using System;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using Prism.Commands;
using System.Collections.Generic;
using System.ComponentModel;
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
            DataModel = Index
                        == 0 ? _expensesService.GetAllExpensesDataModel()
                : _baseService.GetDataModel<ExpensesDataModel>(Index);

            if (Index != 0)
            {
                DataModel.ResidentAdults = _expensesService.SetAdultResidents(Index);
                DataModel.ResidentChildren = _expensesService.SetChildrenResidents(Index);
                DataModel.PropertyChanged += DataModelPropertyChanged;

                DataModel.Army = _expensesService.GetArmyNumbers(Index);
                DataModel.ArmyBase = _expensesService.GetArmyBaseCost(Index);
                DataModel.ArmySilver = _expensesService.GetArmySilverCost(Index);
                DataModel.Employees = _expensesService.GetEmployeeNumbers(Index);
                DataModel.EmployeesBase = _expensesService.GetEmployeeBaseCost(Index);
                DataModel.EmployeesLuxury = _expensesService.GetEmployeeLuxuryCost(Index);
                GetInformationSetDataModel();
                DataModel.CalculateTotals();
            }

            UpdateFiefCollection();
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            LoadData();
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

        private void GetInformationSetDataModel()
        {
            GetManorUpkeep();
            CalculateUpkeepManor();
            CalculateFeedingCosts();
            GetLivingcondition();
            GetBuildingsCosts();
            GetBoatCosts();
        }

        private void GetBoatCosts()
        {
            DataModel.BoatBuilds = _expensesService.GetNumberOfBoatsBuilding(Index);
            DataModel.BoatBuildsSilver = _expensesService.GetBoatbuildingSilverCost(Index);
        }

        private void GetBuildingsCosts()
        {
            DataModel.Builds = _expensesService.GetNumberOfBuildings(Index);
            DataModel.BuildsIron = _expensesService.GetIronCostOfBuildings(Index);
            DataModel.BuildsStone = _expensesService.GetStoneCostOfBuildings(Index);
            DataModel.BuildsWood = _expensesService.GetWoodCostOfBuildings(Index);
        }

        private void GetManorUpkeep()
        {
            DataModel.ManorMaintenance = _expensesService.GetManorUpkeep(Index);
        }

        private void CalculateUpkeepManor()
        {
            DataModel.ManorMaintenanceBase = _expensesService.CalculateManorUpkeepBaseCost(Index);
        }

        private void CalculateFeedingCosts()
        {
            DataModel.FeedingPoorBase = _expensesService.CalculateFeedingPoorBaseCost(Index);
            DataModel.FeedingDayworkersBase = _expensesService.CalculateFeedingDayworkers(Index);
        }

        private void GetLivingcondition()
        {
            DataModel.Livingcondition = _expensesService.GetLivingcondition(Index);
        }

        #endregion
    }
}
