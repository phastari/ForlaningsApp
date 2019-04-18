using System.Collections.Generic;
using System.Linq;
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

        public int GetNewCaptainId(int index)
        {
            List<int> tempList = new List<int>();

            for (int x = 0; x < _fiefService.PortsList[index].CaptainsCollection.Count; x++)
            {
                tempList.Add(_fiefService.PortsList[index].CaptainsCollection[x].Id);
            }

            if (tempList.Count > 0)
            {
                return tempList.Max() + 1;
            }
            return 0;
        }
    }
}
