using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IBoatbuildingService
    {
        BoatbuildingDataModel GetAllBoatbuildingDataModel();
        int GetNewBuildingBoatId(int index);
        int GetNewBoatbuilderId();
        int GetNrVillageBoatbuilders(int index);
        bool GetGotShipyard(int index);
        bool GetUpgradingShipyard(int index);
        int GetVillageBoatBuilders(int index);
    }
}