using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

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

        public List<BuildingModel> GetAvailableBuildings()
        {
            return _settingsService.BuildingsSettingsList.Select(t => new BuildingModel()
                {
                    Building = t.Building,
                    Woodwork = t.Woodwork,
                    Stonework = t.Stonework,
                    Smithswork = t.Smithwork,
                    Wood = t.Wood,
                    Stone = t.Stone,
                    Iron = t.Iron,
                    Upkeep = t.Upkeep
                })
                .ToList();
        }
    }
}
