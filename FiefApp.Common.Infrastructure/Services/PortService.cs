using System.Collections.Generic;
using System.Linq;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public class PortService : IPortService
    {
        private readonly IFiefService _fiefService;
        private readonly ISettingsService _settingsService;

        public PortService(
            IFiefService fiefService,
            ISettingsService settingsService
            )
        {
            _fiefService = fiefService;
            _settingsService = settingsService;
        }

        public PortDataModel GetAllPortDataModel()
        {
            PortDataModel model = new PortDataModel();

            model.IsAll = true;
            model.CanBuildShipyard = false;
            model.BuildingShipyard = false;
            model.GotShipyard = false;
            model.UpgradingShipyard = false;

            for (int x = 1; x < _fiefService.PortsList.Count; x++)
            {
                if (_fiefService.PortsList[x].GotShipyard
                    || _fiefService.PortsList[x].UpgradingShipyard)
                {
                    model.IsAllShipyardCollection.Add(new IsAllPortModel()
                    {
                        Fief = _fiefService.InformationList[x].FiefName,
                        CanBuildShipyard = false,
                        GotShipyard = _fiefService.PortsList[x].GotShipyard,
                        BuildingShipyard = false,
                        UpgradingShipyard = _fiefService.PortsList[x].UpgradingShipyard,
                        PortSize = _fiefService.PortsList[x].Shipyard.Size,
                        Income = _fiefService.PortsList[x].Shipyard.Income,
                        DaysworkThisYear = -1,
                        DaysworkNeeded = -1
                    });
                }
                else if (_fiefService.PortsList[x].BuildingShipyard)
                {
                    model.IsAllShipyardCollection.Add(new IsAllPortModel()
                    {
                        Fief = _fiefService.InformationList[x].FiefName,
                        CanBuildShipyard = false,
                        GotShipyard = false,
                        BuildingShipyard = true,
                        UpgradingShipyard = false,
                        PortSize = _fiefService.PortsList[x].Shipyard.Size,
                        Income = _fiefService.PortsList[x].Shipyard.Income,
                        DaysworkThisYear = _fiefService.PortsList[x].Shipyard.DaysWorkThisYear,
                        DaysworkNeeded = _fiefService.PortsList[x].Shipyard.DaysWorkNeeded
                    });
                }
                else if (_fiefService.PortsList[x].CanBuildShipyard)
                {
                    model.IsAllShipyardCollection.Add(new IsAllPortModel()
                    {
                        Fief = _fiefService.InformationList[x].FiefName,
                        CanBuildShipyard = true,
                        GotShipyard = false,
                        BuildingShipyard = false,
                        UpgradingShipyard = false,
                        PortSize = "",
                        Income = -1,
                        DaysworkThisYear = -1,
                        DaysworkNeeded = -1
                    });
                }

                for (int y = 0; y < _fiefService.PortsList[x].BoatsCollection.Count; y++)
                {
                    if (_fiefService.PortsList[x].BoatsCollection[y].BoatName == "")
                    {
                        model.BoatsCollection.Add(_fiefService.PortsList[x].BoatsCollection[y]);
                        model.BoatsCollection[model.BoatsCollection.Count - 1].BoatName = model.BoatsCollection[model.BoatsCollection.Count - 1].BoatType;
                    }
                    else
                    {
                        model.BoatsCollection.Add(_fiefService.PortsList[x].BoatsCollection[y]);
                    }
                }

                for (int z = 0; z < _fiefService.PortsList[x].CaptainsCollection.Count; z++)
                {
                    if (_fiefService.PortsList[x].CaptainsCollection[z].Id > 0)
                    {
                        model.CaptainsCollection.Add(_fiefService.PortsList[x].CaptainsCollection[z]);
                    }
                }

                model.FishingBoats += _fiefService.PortsList[x].FishingBoats;
                model.Sailors += _fiefService.PortsList[x].Sailors;
                model.SailorsBase += _fiefService.PortsList[x].SailorsBase;
                model.SailorsSilver += _fiefService.PortsList[x].SailorsSilver;
                model.Seaman += _fiefService.PortsList[x].Seaman;
                model.SeamanBase += _fiefService.PortsList[x].SeamanBase;
                model.SeamanSilver += _fiefService.PortsList[x].SeamanSilver;
                model.Guards += _fiefService.PortsList[x].Guards;
                model.GuardsBase += _fiefService.PortsList[x].GuardsBase;
                model.GuardsSilver += _fiefService.PortsList[x].GuardsSilver;
                model.Mariner += _fiefService.PortsList[x].Mariner;
                model.MarinerBase += _fiefService.PortsList[x].MarinerBase;
                model.MarinerSilver += _fiefService.PortsList[x].MarinerSilver;
                model.Rowers += _fiefService.PortsList[x].Rowers;
                model.TotalBase += _fiefService.PortsList[x].TotalBase;
                model.TotalLuxury += _fiefService.PortsList[x].TotalLuxury;
                model.TotalSilver += _fiefService.PortsList[x].TotalSilver;
            }

            return model;
        }

        public bool CheckShipyardPossibility(int index)
        {
            return _fiefService.InformationList[index].River == "Ja" || _fiefService.InformationList[index].Coast == "Ja";
        }

        public ShipyardTypeSettingsModel GetShipyardTypeSettingsModel(int size)
        {
            return _settingsService.ShipyardTypeSettingsList[size];
        }

        public int GetNewCaptainId(int index)
        {
            List<int> tempList = new List<int>();

            for (int x = 0; x < _fiefService.PortsList[index].CaptainsCollection.Count; x++)
            {
                tempList.Add(_fiefService.PortsList[index].CaptainsCollection[x].Id);
            }

            if (tempList.Count > 0)
            {
                return tempList.Max() + 1;
            }
            return 0;
        }
    }
}
