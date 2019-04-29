using System.Collections.Generic;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IBuildingsService
    {
        BuildingsDataModel GetAllBuildingsDataModel();
        List<BuildingModel> GetAvailableBuildings();
    }
}