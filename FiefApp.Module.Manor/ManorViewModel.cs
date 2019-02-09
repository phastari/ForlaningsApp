using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;

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

        public readonly ManorSettingsModel SettingsModel;

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
