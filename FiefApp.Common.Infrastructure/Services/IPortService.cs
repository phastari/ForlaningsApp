using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IPortService
    {
        PortDataModel GetAllPortDataModel();
        bool CheckShipyardPossibility(int index);
        ShipyardTypeSettingsModel GetShipyardTypeSettingsModel(int size);
        int GetNewCaptainId(int index);
        int GetAvailableGuards(int index);
    }
}