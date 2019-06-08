using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

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
            BuildingsDataModel model = new BuildingsDataModel();
            int totalBuildings = 0;
            int totalUpkeep = 0;
            int stoneworkers = 0;
            int stoneworkersBase = 0;
            int stoneworkersDayswork = 0;
            int stoneworkersDaysworkLeft = 0;
            int smiths = 0;
            int smithsBase = 0;
            int smithsDayswork = 0;
            int smithsDaysworkLeft = 0;
            int woodworkers = 0;
            int woodworkersBase = 0;
            int woodworkersDayswork = 0;
            int woodworkersDaysworkLeft = 0;
            List<BuildingModel> buildingsList = new List<BuildingModel>();
            List<BuilderModel> buildersList = new List<BuilderModel>();
            List<BuildingModel> buildsList = new List<BuildingModel>();

            for (int x = 1; x < _fiefService.BuildingsList.Count; x++)
            {
                totalBuildings += _fiefService.BuildingsList[x].TotalBuildings;
                totalUpkeep += _fiefService.BuildingsList[x].TotalUpkeep;
                stoneworkers += _fiefService.BuildingsList[x].Stoneworkers;
                stoneworkersBase += _fiefService.BuildingsList[x].StoneworkersBase;
                stoneworkersDayswork += _fiefService.BuildingsList[x].StoneworkersDaysWork;
                stoneworkersDaysworkLeft += _fiefService.BuildingsList[x].StoneworkersDaysWorkLeft;
                smiths += _fiefService.BuildingsList[x].Smiths;
                smithsBase += _fiefService.BuildingsList[x].SmithsBase;
                smithsDayswork += _fiefService.BuildingsList[x].SmithsDaysWork;
                smithsDaysworkLeft += _fiefService.BuildingsList[x].SmithsDaysWorkLeft;
                woodworkers += _fiefService.BuildingsList[x].Woodworkers;
                woodworkersBase += _fiefService.BuildingsList[x].WoodworkersBase;
                woodworkersDayswork += _fiefService.BuildingsList[x].WoodworkersDaysWork;
                woodworkersDaysworkLeft += _fiefService.BuildingsList[x].WoodworkersDaysWorkLeft;
                buildingsList.AddRange(_fiefService.BuildingsList[x].BuildingsCollection);
                buildersList.AddRange(_fiefService.BuildingsList[x].BuildersCollection);
                buildsList.AddRange(_fiefService.BuildingsList[x].BuildsCollection);
            }

            for (int y = 0; y < buildsList.Count; y++)
            {
                buildsList[y].IsAll = true;
            }

            model.TotalBuildings = totalBuildings;
            model.TotalUpkeep = totalUpkeep;
            model.Stoneworkers = stoneworkers;
            model.StoneworkersBase = stoneworkersBase;
            model.StoneworkersDaysWork = stoneworkersDayswork;
            model.StoneworkersDaysWorkLeft = stoneworkersDaysworkLeft;
            model.Smiths = smiths;
            model.SmithsBase = smithsBase;
            model.SmithsDaysWork = smithsDayswork;
            model.SmithsDaysWorkLeft = smithsDaysworkLeft;
            model.Woodworkers = woodworkers;
            model.WoodworkersBase = woodworkersBase;
            model.WoodworkersDaysWork = woodworkersDayswork;
            model.WoodworkersDaysWorkLeft = woodworkersDaysworkLeft;
            model.BuildingsCollection = new System.Collections.ObjectModel.ObservableCollection<BuildingModel>(buildingsList);
            model.BuildersCollection = new System.Collections.ObjectModel.ObservableCollection<BuilderModel>(buildersList);
            model.BuildsCollection = new System.Collections.ObjectModel.ObservableCollection<BuildingModel>(buildsList);
            model.IsAll = true;

            return model;
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

        public int GetNewIdForBuilder()
        {
            List<int> tempList = new List<int>();

            for (int x = 1; x < _fiefService.BuildingsList.Count; x++)
            {
                if (_fiefService.BuildingsList[x].BuildersCollection.Count > 0)
                {
                    tempList.Add(_fiefService.BuildingsList[x].BuildersCollection.Max(t => t.Id));
                }
            }

            if (tempList.Count > 0)
            {
                return tempList.Max() + 1;
            }
            return 1;
        }

        public void SetAllBuildsCollectionIsAll(int index)
        {
            for (int x = 0; x < _fiefService.BuildingsList[index].BuildsCollection.Count; x++)
            {
                _fiefService.BuildingsList[index].BuildsCollection[x].IsAll = false;
            }
        }
    }
}
