using FiefApp.Common.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace FiefApp.Common.Infrastructure.Services
{
    public class StewardsService : IStewardsService
    {
        private readonly IFiefService _fiefService;
        private readonly IBaseService _baseService;

        public StewardsService(
            IFiefService fiefService,
            IBaseService baseService)
        {
            _fiefService = fiefService;
            _baseService = baseService;
        }

        public List<IndustryModel> GetIndustryModels(int index)
        {
            return null;
        }

        public int GetNextStewardId()
        {
            return _fiefService.StewardsDataModel.StewardsCollection.Count > 0 ? _fiefService.StewardsDataModel.StewardsCollection.Max(o => o.Id) + 1 : 1;
        }

        public List<StewardIndustryModel> GetIndustries(int index)
        {
            List<StewardIndustryModel> tempList = new List<StewardIndustryModel>();
            int newId = _baseService.GetNewIndustryId();

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
                        IndustryId = _fiefService.IncomeList[index].IncomesCollection[x].Id,
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
                    Industry = "Hamn",
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

            for (int x = 0; x < _fiefService.SubsidiaryList[index].ConstructingCollection.Count; x++)
            {
                tempList.Add(new StewardIndustryModel()
                {
                    Industry = _fiefService.SubsidiaryList[index].ConstructingCollection[x].Subsidiary,
                    IndustryId = _fiefService.SubsidiaryList[index].ConstructingCollection[x].Id,
                    IndustryType = "Subsidiary",
                    FiefId = index,
                    StewardId = _fiefService.SubsidiaryList[index].ConstructingCollection[x].StewardId,
                    CanBeDeveloped = _fiefService.SubsidiaryList[index].ConstructingCollection[x].CanBeDeveloped,
                    BeingDeveloped = _fiefService.SubsidiaryList[index].ConstructingCollection[x].BeingDeveloped
                });
            }

            if (_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection.Count > 0)
            {
                for (int x = 0; x < _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection.Count; x++)
                {
                    tempList.Add(new StewardIndustryModel()
                    {
                        Industry = _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].Industry,
                        IndustryId = _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].Id,
                        IndustryType = "Development",
                        StewardId = _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].StewardId,
                        BeingDeveloped = _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].BeingDeveloped
                    });
                }
            }

            return tempList;
        }

        public void SetBeingDevelopedInIndustries(int industryId, bool beingDeveloped)
        {
            bool industryFound = false;
            string industry = "";

            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {
                for (int y = 0; y < _fiefService.IncomeList[x].IncomesCollection.Count; y++)
                {
                    if (_fiefService.IncomeList[x].IncomesCollection[y].Id == industryId)
                    {
                        industryFound = true;
                        _fiefService.IncomeList[x].IncomesCollection[y].BeingDeveloped = beingDeveloped;
                        industry = $"Utveckla {_fiefService.IncomeList[x].IncomesCollection[y].Income}";
                        break;
                    }
                }

                if (industryFound)
                {
                    break;
                }
                else
                {
                    for (int y = 0; y < _fiefService.MinesList[x].MinesCollection.Count; y++)
                    {
                        if (_fiefService.MinesList[x].MinesCollection[y].Id == industryId)
                        {
                            industryFound = true;
                            _fiefService.MinesList[x].MinesCollection[y].BeingDeveloped = beingDeveloped;
                            industry = $"Utveckla {_fiefService.MinesList[x].MinesCollection[y].MineType} gruva";
                            break;
                        }
                    }
                }

                if (industryFound)
                {
                    break;
                }
                else
                {
                    for (int y = 0; y < _fiefService.MinesList[x].QuarriesCollection.Count; y++)
                    {
                        if (_fiefService.MinesList[x].QuarriesCollection[y].Id == industryId)
                        {
                            industryFound = true;
                            _fiefService.MinesList[x].QuarriesCollection[y].BeingDeveloped = beingDeveloped;
                            industry = $"Utveckla {_fiefService.MinesList[x].QuarriesCollection[y].QuarryType} stenbrott";
                            break;
                        }
                    }
                }

                if (industryFound)
                {
                    break;
                }
                else
                {
                    if (_fiefService.PortsList[x].Shipyard.Id == industryId)
                    {
                        industryFound = true;
                        _fiefService.PortsList[x].Shipyard.BeingDeveloped = beingDeveloped;
                        industry = "Utveckla Hamnen";
                    }
                }

                if (industryFound)
                {
                    break;
                }
                else
                {
                    for (int y = 0; y < _fiefService.SubsidiaryList[x].SubsidiaryCollection.Count; y++)
                    {
                        if (_fiefService.SubsidiaryList[x].SubsidiaryCollection[y].Id == industryId)
                        {
                            industryFound = true;
                            _fiefService.SubsidiaryList[x].SubsidiaryCollection[y].BeingDeveloped = beingDeveloped;
                            industry = $"Utveckla {_fiefService.SubsidiaryList[x].SubsidiaryCollection[y].Subsidiary}";
                            break;
                        }
                    }
                }

                if (industryFound)
                {
                    break;
                }
            }

            if (beingDeveloped)
            {
                bool inList = false;
                for (int x = 0; x < _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection.Count; x++)
                {
                    if (_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].Id == industryId)
                    {
                        inList = true;
                    }
                }

                if (!inList)
                {
                    _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection.Add(
                        new IndustryBeingDevelopedModel()
                        {
                            Id = _baseService.GetNewIndustryId(),
                            IndustryId = industryId,
                            Industry = industry,
                            StewardId = -1,
                            BeingDeveloped = true
                        });
                }
            }
            else
            {
                int id = -1;
                for (int x = 0; x < _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection.Count; x++)
                {
                    if (_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].IndustryId == industryId)
                    {
                        id = _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].Id;
                        if (_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].Industry != "Utveckla Medicin"
                            || _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].Industry != "Utveckla Militär"
                            || _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].Industry != "Utveckla Utbildning")
                        {
                            _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection.RemoveAt(x);
                        }
                        else
                        {
                            _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].BeingDeveloped = false;
                        }
                        break;
                    }
                }

                if (id != -1)
                {
                    for (int x = 0; x < _fiefService.StewardsDataModel.StewardsCollection.Count; x++)
                    {
                        if (_fiefService.StewardsDataModel.StewardsCollection[x].IndustryId == id)
                        {
                            _fiefService.StewardsDataModel.StewardsCollection[x].IndustryId = -1;
                        }
                    }
                }
            }
        }
    }
}
