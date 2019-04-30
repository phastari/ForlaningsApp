using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using System.Collections.Generic;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IBuildingsService
    {
        BuildingsDataModel GetAllBuildingsDataModel();
        List<BuildingModel> GetAvailableBuildings();
        int GetNewIdForBuilder();
    }
}