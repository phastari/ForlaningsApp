using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Employees.RoutedEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FiefApp.Module.Employees
{
    public class EmployeesViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IEmployeeService _employeeService;

        public EmployeesViewModel(
            IBaseService baseService,
            IEmployeeService employeeService
            ) : base(baseService)
        {
            _baseService = baseService;
            _employeeService = employeeService;

            TabName = "Anställda";

            EmployeeUIEventHandler = new CustomDelegateCommand(ExecuteEmployeeUIEventHandler, o => true);
            AddEmployeeUIEventHandler = new CustomDelegateCommand(ExecuteAddEmployeeUIEventHandler, o => true);
        }

        #region CustomDelegateCommand : EmployeeUIEventHandler

        public CustomDelegateCommand EmployeeUIEventHandler { get; set; }
        private void ExecuteEmployeeUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is EmployeeUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Save")
            {
                for (int x = 0; x < DataModel.EmployeesCollection.Count; x++)
                {
                    if (DataModel.EmployeesCollection[x].Id == e.Id)
                    {
                        DataModel.EmployeesCollection[x].Name = e.EmployeeModel.Name;
                        DataModel.EmployeesCollection[x].Base = e.EmployeeModel.Base;
                        DataModel.EmployeesCollection[x].BaseCost = e.EmployeeModel.BaseCost;
                        DataModel.EmployeesCollection[x].Luxury = e.EmployeeModel.Luxury;
                        DataModel.EmployeesCollection[x].LuxuryCost = e.EmployeeModel.LuxuryCost;
                        DataModel.EmployeesCollection[x].Number = e.EmployeeModel.Number;
                        DataModel.UpdateTotalCosts();
                    }
                }
            }
            else if (e.Action == "Delete")
            {
                for (int x = 0; x < DataModel.EmployeesCollection.Count; x++)
                {
                    if (DataModel.EmployeesCollection[x].Id == e.Id)
                    {
                        DataModel.EmployeesCollection.RemoveAt(x);
                        break;
                    }
                }
            }
            else if (e.Action == "Update")
            {
                for (int x = 0; x < DataModel.EmployeesCollection.Count; x++)
                {
                    if (DataModel.EmployeesCollection[x].Id == e.Id)
                    {
                        DataModel.EmployeesCollection[x].Base = e.EmployeeModel.Base;
                        DataModel.EmployeesCollection[x].Luxury = e.EmployeeModel.Luxury;
                        DataModel.UpdateTotalCosts();
                        SaveData();
                    }
                }
            }

            SaveData();
            DataModel.UpdateTotalCosts();
        }

        #endregion
        #region CustomDelegateCommand : AddEmployeeUIEventHandler

        public CustomDelegateCommand AddEmployeeUIEventHandler { get; set; }
        private void ExecuteAddEmployeeUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is AddEmployeeUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            int max = 0;
            if (e.Action == "Save")
            {
                List<int> tempList = new List<int>();

                for (int x = 0; x < DataModel.EmployeesCollection.Count; x++)
                {
                    tempList.Add(DataModel.EmployeesCollection[x].Id);
                }

                if (tempList.Count > 0)
                {
                    max = tempList.Max() + 1;
                }
                else
                {
                    max = 1;
                }

                DataModel.EmployeesCollection.Add(new EmployeeModel()
                {
                    Id = max,
                    Name = e.EmployeeModel.Name,
                    BaseCost = e.EmployeeModel.BaseCost,
                    LuxuryCost = e.EmployeeModel.LuxuryCost,
                    Number = 0,
                    Base = 0,
                    Luxury = 0
                });
            }

            SaveData();
        }

        #endregion

        #region View Data Model Properties

        private EmployeesDataModel _dataModel;
        public EmployeesDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
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
                        == 0 ? _employeeService.GetAllEmployeesDataModel()
                : _baseService.GetDataModel<EmployeesDataModel>(Index);

            UpdateFiefCollection();
        }

        #endregion
    }
}
