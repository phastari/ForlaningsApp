using System;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using System.Collections.Generic;
using System.ComponentModel;
using Prism.Commands;

namespace FiefApp.Module.Expenses
{
    public class ExpensesViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IExpensesService _expensesService;

        public ExpensesViewModel(
            IBaseService baseService,
            IExpensesService expensesService
            ) : base(baseService)
        {
            _baseService = baseService;
            _expensesService = expensesService;

            TabName = "Utgifter";

            LivingconditionsList = _expensesService.GetLivingconditionList();

            EditButtonCommand = new DelegateCommand(ExecuteEditButtonCommand);
            CancelEditingButtonCommand = new DelegateCommand(ExecuteCancelEditingButtonCommand);
            SaveEditedButtonCommand = new DelegateCommand(ExecuteSaveEditedButtonCommand);
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
            _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
        }
        protected override void LoadData()
        {
            DataModel = Index
                        == 0 ? _expensesService.GetAllExpensesDataModel()
                : _baseService.GetDataModel<ExpensesDataModel>(Index);

            DataModel.ResidentAdults = _expensesService.SetAdultResidents(Index);
            DataModel.ResidentChildren = _expensesService.SetChildrenResidents(Index);
            DataModel.PropertyChanged += DataModelPropertyChanged;

            UpdateFiefCollection();
            DataModel.Army = _expensesService.GetArmyNumbers(Index);
            DataModel.ArmyBase = _expensesService.GetArmyBaseCost(Index);
            DataModel.ArmySilver = _expensesService.GetArmySilverCost(Index);
            DataModel.Employees = _expensesService.GetEmployeeNumbers(Index);
            DataModel.EmployeesBase = _expensesService.GetEmployeeBaseCost(Index);
            DataModel.EmployeesLuxury = _expensesService.GetEmployeeLuxuryCost(Index);
        }

        private void DataModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FeedingPoor")
            {
                DataModel.FeedingPoorBase = DataModel.FeedingPoor
                    ? _expensesService.CalculateFeedingPoorBaseCost(Index)
                    : 0;
            }
            else if (e.PropertyName == "FeedingDayworkers")
            {
                DataModel.FeedingDayworkersBase = DataModel.FeedingDayworkers
                    ? _expensesService.CalculateFeedingDayworkers(Index)
                    : 0;
            }
            else if (e.PropertyName == "ImproveRoads")
            {
                if (DataModel.ImproveRoads)
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
                }
                else
                {
                    DataModel.ImproveRoadsBase = 0;
                    DataModel.ImproveRoadsStone = 0;
                }
            }
        }

        #endregion
    }
}
