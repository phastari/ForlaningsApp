using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public class BoatbuildingService : IBoatbuildingService
    {
        private readonly ISettingsService _settingsService;
        private readonly IFiefService _fiefService;

        public BoatbuildingService(
            ISettingsService settingsService,
            IFiefService fiefService
            )
        {
            _settingsService = settingsService;
            _fiefService = fiefService;
        }

        public bool CheckShipyardPossibility(int index)
        {
            return _fiefService.InformationList[index].River == "Ja" || _fiefService.InformationList[index].Coast == "Ja";
        }

        public BoatbuildingDataModel GetAllBoatbuildingDataModel()
        {
            return null;
        }

        public ShipyardTypeSettingsModel GetShipyardTypeSettingsModel(int size)
        {
            return _settingsService.ShipyardTypeSettingsList[size];
        }
    }
}
