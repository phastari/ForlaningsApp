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
            return null;
        }

        public int GetNewBuildingBoatId(int index)
        {
            List<int> tempList = new List<int>();

            for (int x = 0; x < _fiefService.BoatbuildingList[index].BoatsBuildingCollection.Count; x++)
            {
                tempList.Add(_fiefService.BoatbuildingList[index].BoatsBuildingCollection[x].Id);
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
