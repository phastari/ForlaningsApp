using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;

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

        public bool CheckShipyardPossibility(int index)
        {
            return _fiefService.InformationList[index].River == "Ja" || _fiefService.InformationList[index].Coast == "Ja";
        }

        public BoatbuildingDataModel GetAllBoatbuildingDataModel()
        {
            return null;
        }

        public ShipyardTypeSettingsModel GetShipyardTypeSettingsModel(int size)
        {
            return _settingsService.ShipyardTypeSettingsList[size];
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

        public int GetNewBoatbuilderId(int index)
        {
            List<int> tempList = new List<int>();

            for (int x = 0; x < _fiefService.BoatbuildingList[index].BoatBuildersCollection.Count; x++)
            {
                tempList.Add(_fiefService.BoatbuildingList[index].BoatBuildersCollection[x].Id);
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
    }
}
