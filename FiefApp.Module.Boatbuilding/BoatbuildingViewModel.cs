using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Services;
using Prism.Commands;

namespace FiefApp.Module.Boatbuilding
{
    public class BoatbuildingViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IBoatbuildingService _boatbuildingService;

        public BoatbuildingViewModel(
            IBaseService baseService,
            IBoatbuildingService boatbuildingService
            ) : base(baseService)
        {
            _baseService = baseService;
            _boatbuildingService = boatbuildingService;

            TabName = "Skeppsbygge";

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
            DataModel.Shipyard.DaysWorkNeeded = 2500;
        }

        #endregion

        #region DataModel

        private BoatbuildingDataModel _dataModel;
        public BoatbuildingDataModel DataModel
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
                        == 0 ? _boatbuildingService.GetAllBoatbuildingDataModel()
                : _baseService.GetDataModel<BoatbuildingDataModel>(Index);

            if (!DataModel.BuildingShipyard && !DataModel.GotShipyard)
            {
                DataModel.CanBuildShipyard = _boatbuildingService.CheckShipyardPossibility(Index);
            }

            UpdateFiefCollection();
        }
    }
}
