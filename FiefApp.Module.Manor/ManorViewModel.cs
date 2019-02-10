using System;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;
using FiefApp.Module.Manor.RoutedEvents;

namespace FiefApp.Module.Manor
{
    public class ManorViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IManorService _manorService;
        private readonly ISettingsService _settingsService;

        public ManorViewModel(
            IBaseService baseService,
            IManorService manorService,
            ISettingsService settingsService
            ) : base(baseService)
        {
            _baseService = baseService;
            _manorService = manorService;
            _settingsService = settingsService;

            SettingsModel = _settingsService.ManorSettingsModel;

            TabName = "Gods/Byar";

            ResidentUIEventHandler = new CustomDelegateCommand(ExecuteResidentUIEventHandler, o => true);
        }

        #region CustomDelegateCommand : ResidentUIEventHandler

        public CustomDelegateCommand ResidentUIEventHandler { get; set; }

        private void ExecuteResidentUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>) obj;

            if (!(tuple.Item2 is ResidentUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Save")
            {
                for (int x = 0; x < DataModel.ResidentsList.Count; x++)
                {
                    if (DataModel.ResidentsList[x].Id == e.Id)
                    {
                        DataModel.ResidentsList[x].Name = e.ResidentModel.Name;
                        DataModel.ResidentsList[x].Position = e.ResidentModel.Position;
                        DataModel.ResidentsList[x].Age = e.ResidentModel.Age;
                    }
                }
            }
            else if (e.Action == "Delete")
            {
                for (int x = 0; x < DataModel.ResidentsList.Count; x++)
                {
                    if (DataModel.ResidentsList[x].Id == e.Id)
                    {
                        DataModel.ResidentsList.RemoveAt(x);
                        break;
                    }
                }
            }

            SaveData();
        }

        #endregion

        #region View Data Model Properties

        private ManorDataModel _dataModel;
        public ManorDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        #region Settings

        private ManorSettingsModel _settingsModel;
        public ManorSettingsModel SettingsModel
        {
            get => _settingsModel;
            set => SetProperty(ref _settingsModel, value);
        }

        #endregion

        #region Methods 

        protected override void SaveData(int index = -1)
        {
            _manorService.SetManorDataModel(DataModel);
        }

        protected override void LoadData()
        {
            if (Index == 0)
            {
                //DataModel = _manorService.GetAllManorDataModel();
            }
            else
            {
                DataModel = _baseService.GetDataModel<ManorDataModel>(Index);
            }

            UpdateFiefCollection();
            CreateFakeData();
        }

        private void CreateFakeData()
        {
            DataModel.ResidentsList.Add(new ResidentModel()
            {
                Age = 32,
                Type = "Resident",
                Name = "Karl Gunnar",
                Position = "Boende"
            });

            DataModel.ResidentsList.Add(new ResidentModel()
            {
                Age = 43,
                Type = "Resident",
                Name = "Sune Svensson",
                Position = "Boende"
            });

            DataModel.ResidentsList.Add(new EmployeeModel()
            {
                Age = 54,
                Name = "Sven Employee",
                Type = "Employee",
                Position = "Anställd"
            });

            DataModel.ResidentsList.Add(new SoldierModel()
            {
                Age = 21,
                Type = "Soldier",
                Name = "Axel Erövraren",
                Position = "Soldat"
            });
        }

        #endregion
    }
}
