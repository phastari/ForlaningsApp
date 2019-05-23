using FiefApp.Common.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace FiefApp.Common.Infrastructure.Services
{
    public class StewardsService : IStewardsService
    {
        private readonly IFiefService _fiefService;

        public StewardsService(IFiefService fiefService)
        {
            _fiefService = fiefService;
        }

        public List<IndustryModel> GetIndustryModels(int index)
        {
            return null;
        }

        public int GetNextStewardId()
        {
            return _fiefService.StewardsCollection.Count > 0 ? _fiefService.StewardsCollection.Max(o => o.Id) + 1 : 0;
        }

        public List<StewardIndustryModel> GetIndustries(int index)
        {
            List<StewardIndustryModel> tempList = new List<StewardIndustryModel>();

            tempList.Add(new StewardIndustryModel()
            {
                Industry = "",
                IndustryId = -1,
                IndustryType = "",
                FiefId = -1,
                StewardId = -1,
                CanBeDeveloped = false,
                BeingDeveloped = false
            });

            for (int x = 0; x < _fiefService.IncomeList[index].IncomesCollection.Count; x++)
            {
                if (_fiefService.IncomeList[index].IncomesCollection[x].IsStewardNeeded
                    && _fiefService.IncomeList[index].IncomesCollection[x].ShowInIncomes)
                {
                    tempList.Add(new StewardIndustryModel()
                    {
                        Industry = _fiefService.IncomeList[index].IncomesCollection[x].Income,
                        IndustryId = -1,
                        IndustryType = "Income",
                        FiefId = index,
                        StewardId = _fiefService.IncomeList[index].IncomesCollection[x].StewardId,
                        CanBeDeveloped = _fiefService.IncomeList[index].IncomesCollection[x].CanBeDeveloped,
                        BeingDeveloped = _fiefService.IncomeList[index].IncomesCollection[x].BeingDeveloped
                    });
                }
            }

            for (int x = 0; x < _fiefService.MinesList[index].MinesCollection.Count; x++)
            {
                tempList.Add(new StewardIndustryModel()
                {
                    Industry = $"{_fiefService.MinesList[index].MinesCollection[x].MineType} gruva",
                    IndustryId = _fiefService.MinesList[index].MinesCollection[x].Id,
                    IndustryType = "Mine",
                    FiefId = index,
                    StewardId = _fiefService.MinesList[index].MinesCollection[x].StewardId,
                    CanBeDeveloped = _fiefService.MinesList[index].MinesCollection[x].CanBeDeveloped,
                    BeingDeveloped = _fiefService.MinesList[index].MinesCollection[x].BeingDeveloped
                });
            }

            for (int x = 0; x < _fiefService.MinesList[index].QuarriesCollection.Count; x++)
            {
                tempList.Add(new StewardIndustryModel()
                {
                    Industry = $"{_fiefService.MinesList[index].QuarriesCollection[x].QuarryType} stenbrott",
                    IndustryId = _fiefService.MinesList[index].QuarriesCollection[x].Id,
                    IndustryType = "Mine",
                    FiefId = index,
                    StewardId = _fiefService.MinesList[index].QuarriesCollection[x].StewardId,
                    CanBeDeveloped = _fiefService.MinesList[index].QuarriesCollection[x].CanBeDeveloped,
                    BeingDeveloped = _fiefService.MinesList[index].QuarriesCollection[x].BeingDeveloped
                });
            }

            if (_fiefService.PortsList[index].GotShipyard || _fiefService.PortsList[index].BuildingShipyard)
            {
                tempList.Add(new StewardIndustryModel()
                {
                    Industry = "Shipyard",
                    IndustryId = _fiefService.PortsList[index].Shipyard.Id,
                    IndustryType = "Shipyard",
                    FiefId = index,
                    StewardId = _fiefService.PortsList[index].Shipyard.StewardId,
                    CanBeDeveloped = _fiefService.PortsList[index].Shipyard.CanBeDeveloped,
                    BeingDeveloped = _fiefService.PortsList[index].Shipyard.BeingDeveloped
                });
            }

            for (int x = 0; x < _fiefService.SubsidiaryList[index].SubsidiaryCollection.Count; x++)
            {
                tempList.Add(new StewardIndustryModel()
                {
                    Industry = _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].Subsidiary,
                    IndustryId = _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].Id,
                    IndustryType = "Subsidiary",
                    FiefId = index,
                    StewardId = _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].StewardId,
                    CanBeDeveloped = _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].CanBeDeveloped,
                    BeingDeveloped = _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].BeingDeveloped
                });
            }

            return tempList;
        }
    }
}
