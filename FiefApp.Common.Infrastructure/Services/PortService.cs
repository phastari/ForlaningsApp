using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public class PortService : IPortService
    {
        private readonly IFiefService _fiefService;
        private readonly ISettingsService _settingsService;

        public PortService(
            IFiefService fiefService,
            ISettingsService settingsService
            )
        {
            _fiefService = fiefService;
            _settingsService = settingsService;
        }

        public PortDataModel GetAllPortDataModel()
        {
            return null;
        }

        public bool CheckShipyardPossibility(int index)
        {
            return _fiefService.InformationList[index].River == "Ja" || _fiefService.InformationList[index].Coast == "Ja";
        }

        public ShipyardTypeSettingsModel GetShipyardTypeSettingsModel(int size)
        {
            return _settingsService.ShipyardTypeSettingsList[size];
        }
    }
}
