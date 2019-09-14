using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.HelpClasses.StringToFormula;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Employees.RoutedEvents;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FiefApp.Module.Employees
{
    public class EmployeesViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IEmployeeService _employeeService;
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

        public EmployeesViewModel(
            IBaseService baseService,
            IEmployeeService employeeService,
            ISettingsService settingsService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            _baseService = baseService;
            _employeeService = employeeService;
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;

            TabName = "Anställda";

            EmployeeUIEventHandler = new CustomDelegateCommand(ExecuteEmployeeUIEventHandler, o => true);
            AddEmployeeUIEventHandler = new CustomDelegateCommand(ExecuteAddEmployeeUIEventHandler, o => true);

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
                DataModel = _baseService.GetDataModel<EmployeesDataModel>(x);
                DataModel.UpdateTotalCosts();
                SaveData(x);
            }
        }

        private void HandleUpdateEvent(UpdateEventParameters param)
        {
            if (param.Publisher == "Employees"
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
            if (str != "Employees")
            {
                UpdateFiefCollection();
                for (int x = 1; x < FiefCollection.Count; x++)
                {
                    DataModel = _baseService.GetDataModel<EmployeesDataModel>(x);
                    DataModel.UpdateTotalCosts();
                    SaveData(x);
                }

                _eventAggregator.GetEvent<UpdateResponseEvent>().Publish(new UpdateEventParameters()
                {
                    ModuleName = "Employees",
                    Completed = true,
                    Publisher = str
                });
            }
        }

        private void CompleteLoadData()
        {
            DataModel = Index
                        == 0 ? _employeeService.GetAllEmployeesDataModel()
                : _baseService.GetDataModel<EmployeesDataModel>(Index);

            DataModel.PropertyChanged += DataModelPropertyChanged;

            if (Index == 0)
            {
                DataModel.Falconer = DataModel.Falconer;
                DataModel.Bailiff = DataModel.Bailiff;
                DataModel.Herald = DataModel.Herald;
                DataModel.Hunter = DataModel.Hunter;
                DataModel.Physician = DataModel.Physician;
                DataModel.Scholar = DataModel.Scholar;
                DataModel.Cupbearer = DataModel.Cupbearer;
                DataModel.Prospector = DataModel.Prospector;
                DataModel.UpdateTotalCosts();
                DataModel.IsAll = true;

            }

            UpdateFiefCollection();
        }

        private void UpdateAndRespond()
        {
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<EmployeesDataModel>(x);
                DataModel.UpdateTotalCosts();
                SaveData(x);
            }

            _eventAggregator.GetEvent<UpdateAllResponseEvent>().Publish(new UpdateAllEventParameters()
            {
                ModuleName = "Employees",
                Completed = true
            });
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
                        DataModel.EmployeesCollection[x].PersonName = e.EmployeeModel.PersonName;
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
                    PersonName = e.EmployeeModel.PersonName,
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
            //        if (_awaitResponsList[x].ModuleName == "Employees")
            //        {
            //            _awaitResponsList[x].Completed = true;
            //        }
            //        else
            //        {
            //            _awaitResponsList[x].Completed = false;
            //        }
            //    }
            //    _eventAggregator.GetEvent<UpdateEvent>().Publish("Employees");
            //}
            CompleteLoadData();
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            CompleteLoadData();
        }

        #endregion

        #region DataModel PropertyChanged

        private void DataModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Falconer":
                    DataModel.BaseFalconer = _settingsService.EmployeeSettingsModel.FalconerBase * DataModel.Falconer;
                    DataModel.LuxuryFalconer = _settingsService.EmployeeSettingsModel.FalconerLuxury * DataModel.Falconer;
                    break;

                case "Bailiff":
                    DataModel.BaseBailiff = _settingsService.EmployeeSettingsModel.BailiffBase * DataModel.Bailiff;
                    DataModel.LuxuryBailiff = _settingsService.EmployeeSettingsModel.BailiffLuxury * DataModel.Bailiff;
                    break;

                case "Herald":
                    DataModel.BaseHerald = _settingsService.EmployeeSettingsModel.HeraldBase * DataModel.Herald;
                    DataModel.LuxuryHerald = _settingsService.EmployeeSettingsModel.HeraldLuxury * DataModel.Herald;
                    break;

                case "Hunter":
                    DataModel.BaseHunter = _settingsService.EmployeeSettingsModel.HunterBase * DataModel.Hunter;
                    DataModel.LuxuryHunter = _settingsService.EmployeeSettingsModel.HunterLuxury * DataModel.Hunter;
                    break;

                case "Physician":
                    DataModel.BasePhysician = _settingsService.EmployeeSettingsModel.PhysicianBase * DataModel.Physician;
                    DataModel.LuxuryPhysician = _settingsService.EmployeeSettingsModel.PhysicianLuxury * DataModel.Physician;
                    break;

                case "Scholar":
                    DataModel.BaseScholar = _settingsService.EmployeeSettingsModel.ScholarBase * DataModel.Scholar;
                    DataModel.LuxuryScholar = _settingsService.EmployeeSettingsModel.ScholarLuxury * DataModel.Scholar;
                    break;

                case "Cupbearer":
                    DataModel.BaseCupbearer = _settingsService.EmployeeSettingsModel.CupbearerBase * DataModel.Cupbearer;
                    DataModel.LuxuryCupbearer = _settingsService.EmployeeSettingsModel.CupbearerLuxury * DataModel.Cupbearer;
                    break;

                case "Prospector":
                    if (DataModel.Prospector == 0)
                    {
                        DataModel.BaseProspector = 0;
                        DataModel.LuxuryProspector = 0;
                    }
                    else
                    {
                        DataModel.BaseProspector = _settingsService.EmployeeSettingsModel.ProspectorBase * DataModel.Prospector;

                        string formula = $"{DataModel.Prospector}{_settingsService.EmployeeSettingsModel.ProspectorLuxury}";
                        StringToFormula stf = new StringToFormula();
                        double result = stf.Eval(formula);
                        result = Math.Ceiling(result);
                        DataModel.LuxuryProspector = Convert.ToInt16(result);
                    }
                    break;
            }
        }

        private void ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent()
        {
            SaveData();
        }

        #endregion
    }
}
