using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public class BuildingsService : IBuildingsService
    {
        private readonly ISettingsService _settingsService;
        private readonly IFiefService _fiefService;

        public BuildingsService(
            ISettingsService settingsService,
            IFiefService fiefService
            )
        {
            _fiefService = fiefService;
            _settingsService = settingsService;
        }

        public BuildingsDataModel GetAllBuildingsDataModel()
        {
            return null;
        }
    }
}
