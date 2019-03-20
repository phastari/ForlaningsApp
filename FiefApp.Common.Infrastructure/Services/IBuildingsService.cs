using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IBuildingsService
    {
        BuildingsDataModel GetAllBuildingsDataModel();
    }
}