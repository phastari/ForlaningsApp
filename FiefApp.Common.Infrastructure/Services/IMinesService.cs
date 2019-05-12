using System.Collections.Generic;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IMinesService
    {
        MinesDataModel GetAllMinesDataModel();
        int GetNewIdForQuarry();
        int GetNewIdForMine();
        int GetMinesDifficulty(int index);
        int GetQuarriesDifficulty(int index);
        List<StewardModel> GetStewardsCollection();
        void ChangeStewardInMinesCollection(int stewardId, int mineId, string steward);
        void ChangeStewardInQuarriesCollection(int stewardId, int quarryId, string steward);
        List<MineModel> GetMinesCollection(int index);
        int GetAvailableGuards(int index);
        bool SetUsedGuards(int index, int amount);
    }
}