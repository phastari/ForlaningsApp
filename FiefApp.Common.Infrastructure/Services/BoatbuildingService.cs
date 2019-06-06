using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public class BoatbuildingService : IBoatbuildingService
    {
        private readonly ISettingsService _settingsService;
        private readonly IFiefService _fiefService;

        public BoatbuildingService(
            ISettingsService settingsService,
            IFiefService fiefService
            )
        {
            _settingsService = settingsService;
            _fiefService = fiefService;
        }

        public BoatbuildingDataModel GetAllBoatbuildingDataModel()
        {
            BoatbuildingDataModel tempModel = new BoatbuildingDataModel();
            List<BoatModel> boatsBuildingList = new List<BoatModel>();
            List<BoatbuilderModel> boatbuildersList = new List<BoatbuilderModel>();
            int villageBoatbuilders = 0;
            int totalBoatbuilders = 0;
            int docksSmall = 0;
            int docksSmallFree = 0;
            int docksMedium = 0;
            int docksMediumFree = 0;
            int docksLarge = 0;
            int docksLargeFree = 0;
            int docksVillage = 0;
            int docksVillageFree = 0;

            for (int x = 1; x < _fiefService.BoatbuildingList.Count; x++)
            {
                boatsBuildingList.AddRange(_fiefService.BoatbuildingList[x].BoatsBuildingCollection);
                boatbuildersList.AddRange(_fiefService.BoatbuildingList[x].BoatBuildersCollection);
                villageBoatbuilders += _fiefService.BoatbuildingList[x].VillageBoatBuilders;
                totalBoatbuilders += _fiefService.BoatbuildingList[x].TotalBoatBuilders;
                docksSmall += _fiefService.BoatbuildingList[x].DocksSmall;
                docksSmallFree += _fiefService.BoatbuildingList[x].DocksSmallFree;
                docksMedium += _fiefService.BoatbuildingList[x].DocksMedium;
                docksMediumFree += _fiefService.BoatbuildingList[x].DocksMediumFree;
                docksLarge += _fiefService.BoatbuildingList[x].DocksLarge;
                docksLargeFree += _fiefService.BoatbuildingList[x].DocksLargeFree;
                docksVillage += _fiefService.BoatbuildingList[x].DocksVillage;
                docksVillageFree += _fiefService.BoatbuildingList[x].DocksVillageFree;
            }

            tempModel.BoatsBuildingCollection = new ObservableCollection<BoatModel>(boatsBuildingList);
            tempModel.BoatBuildersCollection = new ObservableCollection<BoatbuilderModel>(boatbuildersList);
            tempModel.VillageBoatBuilders = villageBoatbuilders;
            tempModel.TotalBoatBuilders = totalBoatbuilders;
            tempModel.DocksSmall = docksSmall;
            tempModel.DocksSmallFree = docksSmallFree;
            tempModel.DocksMedium = docksMedium;
            tempModel.DocksMediumFree = docksMediumFree;
            tempModel.DocksLarge = docksLarge;
            tempModel.DocksLargeFree = docksLargeFree;
            tempModel.DocksVillage = docksVillage;
            tempModel.DocksVillageFree = docksVillageFree;
            tempModel.GotShipyard = true;

            return tempModel;
        }

        public bool RemoveBoatbuilder(int id)
        {
            for (int x = 1; x < _fiefService.BoatbuildingList.Count; x++)
            {
                for (int y = 0; y < _fiefService.BoatbuildingList[x].BoatBuildersCollection.Count; y++)
                {
                    if (_fiefService.BoatbuildingList[x].BoatBuildersCollection[y].Id == id)
                    {
                        _fiefService.BoatbuildingList[x].BoatBuildersCollection.RemoveAt(y);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool SaveBoatbuilder(BoatbuilderModel model)
        {
            for (int x = 1; x < _fiefService.BoatbuildingList.Count; x++)
            {
                for (int y = 0; y < _fiefService.BoatbuildingList[x].BoatBuildersCollection.Count; y++)
                {
                    if (_fiefService.BoatbuildingList[x].BoatBuildersCollection[y].Id == model.Id)
                    {
                        _fiefService.BoatbuildingList[x].BoatBuildersCollection[y].PersonName = model.PersonName;
                        _fiefService.BoatbuildingList[x].BoatBuildersCollection[y].Loyalty = model.Loyalty;
                        _fiefService.BoatbuildingList[x].BoatBuildersCollection[y].Skill = model.Skill;
                        _fiefService.BoatbuildingList[x].BoatBuildersCollection[y].Resources = model.Resources;
                        _fiefService.BoatbuildingList[x].BoatBuildersCollection[y].Age = model.Age;
                        return true;
                    }
                }
            }
            return false;
        }

        public void ChangeBoatbuilder(int boatId, int boatbuilderId)
        {
            string type = "";

            for (int x = 1; x < _fiefService.BoatbuildingList.Count; x++)
            {
                for (int y = 0; y < _fiefService.BoatbuildingList[x].BoatsBuildingCollection.Count; y++)
                {
                    if (_fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].Id == boatId)
                    {
                        _fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].BoatbuilderId = boatbuilderId;
                        type = _fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].BoatType;
                    }
                    else if (_fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].BoatbuilderId == boatbuilderId)
                    {
                        _fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].BoatbuilderId = -1;
                        _fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].SelectedIndex = -1;
                    }
                }

                for (int z = 0; z < _fiefService.BoatbuildingList[x].BoatBuildersCollection.Count; z++)
                {
                    if (_fiefService.BoatbuildingList[x].BoatBuildersCollection[z].Id == boatbuilderId)
                    {
                        _fiefService.BoatbuildingList[x].BoatBuildersCollection[z].BuildingBoatId = boatId;
                        _fiefService.BoatbuildingList[x].BoatBuildersCollection[z].Assignment = type;
                    }
                    else
                    {
                        if (_fiefService.BoatbuildingList[x].BoatBuildersCollection[z].BuildingBoatId == boatId)
                        {
                            _fiefService.BoatbuildingList[x].BoatBuildersCollection[z].BuildingBoatId = -1;
                            _fiefService.BoatbuildingList[x].BoatBuildersCollection[z].Assignment = "";
                        }
                    }
                }
            }
        }

        public void RemoveBoat(int boatId)
        {
            bool found = false;
            for (int x = 1; x < _fiefService.BoatbuildingList.Count; x++)
            {
                for (int y = 0; y < _fiefService.BoatbuildingList[x].BoatsBuildingCollection.Count; y++)
                {
                    if (_fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].Id == boatId)
                    {
                        _fiefService.BoatbuildingList[x].BoatsBuildingCollection.RemoveAt(y);
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }

            for (int x = 1; x < _fiefService.BoatbuildingList.Count; x++)
            {
                for (int y = 0; y < _fiefService.BoatbuildingList[x].BoatBuildersCollection.Count; y++)
                {
                    if (_fiefService.BoatbuildingList[x].BoatBuildersCollection[y].BuildingBoatId == boatId)
                    {
                        _fiefService.BoatbuildingList[x].BoatBuildersCollection[y].BuildingBoatId = -1;
                        _fiefService.BoatbuildingList[x].BoatBuildersCollection[y].Assignment = "";
                    }
                }
            }
        }

        public int GetNewBuildingBoatId(int index)
        {
            List<int> tempList = new List<int>();

            for (int x = 1; x < _fiefService.BoatbuildingList.Count; x++)
            {
                for (int y = 0; y < _fiefService.BoatbuildingList[x].BoatsBuildingCollection.Count; y++)
                {
                    tempList.Add(_fiefService.BoatbuildingList[x].BoatsBuildingCollection[y].Id);
                }
            }

            if (tempList.Count > 0)
            {
                return tempList.Max() + 1;
            }
            else
            {
                return 0;
            }
        }

        public ObservableCollection<IPeopleModel> GetAllBoatBuilders(int index)
        {
            List<IPeopleModel> tempList = new List<IPeopleModel>();

            for (int x = 0; x < _fiefService.EmployeesList[index].EmployeesCollection.Count; x++)
            {
                if (_fiefService.EmployeesList[index].EmployeesCollection[x].Type == "Boatbuilder")
                {
                    tempList.Add(_fiefService.EmployeesList[index].EmployeesCollection[x]);
                }
            }

            return new ObservableCollection<IPeopleModel>(tempList);
        }

        public int GetNewBoatbuilderId()
        {
            List<int> tempList = new List<int>();

            for (int x = 0; x < _fiefService.BoatbuildingList.Count; x++)
            {
                for (int y = 0; y < _fiefService.BoatbuildingList[x].BoatBuildersCollection.Count; y++)
                {
                    tempList.Add(_fiefService.BoatbuildingList[x].BoatBuildersCollection[y].Id);
                }
            }

            if (tempList.Count > 0)
            {
                return tempList.Max() + 1;
            }
            return 0;

        }

        public int GetNrVillageBoatbuilders(int index)
        {

            return _fiefService.ManorList[index].VillagesCollection.Sum(t => t.Boatbuilders);
        }

        public bool GetGotShipyard(int index)
        {
            return _fiefService.PortsList[index].GotShipyard;
        }

        public bool GetUpgradingShipyard(int index)
        {
            return _fiefService.PortsList[index].UpgradingShipyard;
        }

        public int GetVillageBoatBuilders(int index)
        {
            return _fiefService.ManorList[index].VillagesCollection.Sum(t => t.Boatbuilders);
        }

        public void AddBoatToCompletedBoats(int index, BoatModel model)
        {
            _fiefService.PortsList[index].BoatsCollection.Add(model);
        }

        public void AddFishingBoat(int index, int amount)
        {
            _fiefService.PortsList[index].FishingBoats += amount;
        }
    }
}
