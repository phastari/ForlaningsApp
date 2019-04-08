using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using Prism.Commands;

namespace FiefApp.Module.Port
{
    public class PortViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IPortService _portService;
        private readonly ISettingsService _settingsService;

        public PortViewModel(
            IBaseService baseService,
            IPortService portService,
            ISettingsService settingsService
            ) : base(baseService)
        {
            TabName = "Hamn";

            _baseService = baseService;
            _portService = portService;
            _settingsService = settingsService;

            ConstructShipyardCommand = new DelegateCommand(ExecuteConstructShipyardCommand);

            // Test Delegate Commands
            SetGotShipyard = new DelegateCommand(ExecuteSetGotShipyard);
            SetBuildingShipyard = new DelegateCommand(ExecuteSetBuildingShipyard);
            SetUpgradingShipyard = new DelegateCommand(ExecuteSetUpgradingShipyard);
            SetCanBuildShipyard = new DelegateCommand(ExecuteSetCanBuildShipyard);
        }

        #region Test DelegateCommands

        public DelegateCommand SetGotShipyard { get; set; }
        private void ExecuteSetGotShipyard()
        {
            DataModel.UpgradingShipyard = false;
            DataModel.BuildingShipyard = false;
            DataModel.CanBuildShipyard = false;
            DataModel.GotShipyard = true;
        }

        public DelegateCommand SetBuildingShipyard { get; set; }
        private void ExecuteSetBuildingShipyard()
        {
            DataModel.UpgradingShipyard = false;
            DataModel.BuildingShipyard = true;
            DataModel.CanBuildShipyard = false;
            DataModel.GotShipyard = false;
        }

        public DelegateCommand SetUpgradingShipyard { get; set; }
        private void ExecuteSetUpgradingShipyard()
        {
            DataModel.UpgradingShipyard = true;
            DataModel.BuildingShipyard = false;
            DataModel.CanBuildShipyard = false;
            DataModel.GotShipyard = false;
        }

        public DelegateCommand SetCanBuildShipyard { get; set; }
        private void ExecuteSetCanBuildShipyard()
        {
            DataModel.UpgradingShipyard = false;
            DataModel.BuildingShipyard = false;
            DataModel.CanBuildShipyard = true;
            DataModel.GotShipyard = false;
        }

        #endregion

        #region DelegateCommand : ConstructShipyardCommand

        public DelegateCommand ConstructShipyardCommand { get; set; }
        private void ExecuteConstructShipyardCommand()
        {
            // TA BETALT FÖR BYGGET!

            DataModel.BuildingShipyard = true;
            DataModel.CanBuildShipyard = false;
            DataModel.Shipyard = new ShipyardModel()
            {
                Shipyard = "",
                Size = _settingsService.ShipyardTypeSettingsList[0].DockType,
                UN = "1",
                OperationBaseCost = _settingsService.ShipyardTypeSettingsList[0].OperationBaseCostModifier,
                OperationBaseIncome = _settingsService.ShipyardTypeSettingsList[0].OperationBaseIncomeModifier,
                IsBeingUpgraded = false,
                DockSmall = _settingsService.ShipyardTypeSettingsList[0].DockSmall,
                DockSmallFree = _settingsService.ShipyardTypeSettingsList[0].DockSmall,
                DockMedium = _settingsService.ShipyardTypeSettingsList[0].DockMedium,
                DockMediumFree = _settingsService.ShipyardTypeSettingsList[0].DockMedium,
                DockLarge = _settingsService.ShipyardTypeSettingsList[0].DockLarge,
                DockLargeFree = _settingsService.ShipyardTypeSettingsList[0].DockLarge,
                Taxes = "20"
            };

            SaveData();
        }

        #endregion

        #region DataModel

        private PortDataModel _dataModel = new PortDataModel();
        public PortDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        protected override void SaveData(int index = -1)
        {
            _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
        }

        protected override void LoadData()
        {
            DataModel = Index
                        == 0 ? _portService.GetAllPortDataModel()
                : _baseService.GetDataModel<PortDataModel>(Index);

            if (!DataModel.BuildingShipyard && !DataModel.GotShipyard)
            {
                DataModel.CanBuildShipyard = _portService.CheckShipyardPossibility(Index);
            }
        }
    }
}
