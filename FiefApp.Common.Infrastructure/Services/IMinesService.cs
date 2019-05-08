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
        List<StewardModel> GetStewardsCollection();
        void ChangeSteward(int stewardId, int mineId, string steward);
        List<MineModel> GetMinesCollection(int index);
    }
}