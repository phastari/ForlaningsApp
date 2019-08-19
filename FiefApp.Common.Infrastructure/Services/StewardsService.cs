using FiefApp.Common.Infrastructure.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

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

        public List<StewardIndustryModel> GetIndustries()
        {
            List<StewardIndustryModel> tempList = new List<StewardIndustryModel>();
            int newId = _baseService.GetNewIndustryId();
            bool doOnce = true;

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

            for (int index = 1; index < _fiefService.InformationList.Count; index++)
            {
                for (int x = 0; x < _fiefService.IncomeList[index].IncomesCollection.Count; x++)
                {
                    if (_fiefService.IncomeList[index].IncomesCollection[x].IsStewardNeeded
                        && _fiefService.IncomeList[index].IncomesCollection[x].ShowInIncomes)
                    {
                        tempList.Add(new StewardIndustryModel()
                        {
                            Industry = _fiefService.IncomeList[index].IncomesCollection[x].Income + " (" + _fiefService.InformationList[index].FiefName + ")",
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
                        Industry = $"{_fiefService.MinesList[index].MinesCollection[x].MineType} gruva (" + _fiefService.InformationList[index].FiefName + ")",
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
                        Industry = $"{_fiefService.MinesList[index].QuarriesCollection[x].QuarryType} stenbrott (" + _fiefService.InformationList[index].FiefName + ")",
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
                        Industry = "Hamn (" + _fiefService.InformationList[index].FiefName + ")",
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
                        Industry = _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].Subsidiary + " (" + _fiefService.InformationList[index].FiefName + ")",
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
                        Industry = _fiefService.SubsidiaryList[index].ConstructingCollection[x].Subsidiary + " (" + _fiefService.InformationList[index].FiefName + ")",
                        IndustryId = _fiefService.SubsidiaryList[index].ConstructingCollection[x].Id,
                        IndustryType = "Subsidiary",
                        FiefId = index,
                        StewardId = _fiefService.SubsidiaryList[index].ConstructingCollection[x].StewardId,
                        CanBeDeveloped = _fiefService.SubsidiaryList[index].ConstructingCollection[x].CanBeDeveloped,
                        BeingDeveloped = _fiefService.SubsidiaryList[index].ConstructingCollection[x].BeingDeveloped
                    });
                }

                if (_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection.Count > 0 && doOnce)
                {
                    doOnce = false;
                    for (int x = 0; x < _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection.Count; x++)
                    {
                        StewardIndustryModel temp = new StewardIndustryModel()
                        {
                            IndustryId = _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].IndustryId,
                            Id = _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].IndustryId,
                            IndustryType = "Development",
                            StewardId = _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].StewardId,
                            BeingDeveloped = _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].BeingDeveloped,
                            FiefId = _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].FiefId
                        };

                        if (_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].Industry.Contains("()"))
                        {
                            string str = _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].Industry + " (" + _fiefService.InformationList[temp.FiefId].FiefName + ")";
                            str = str.Replace("()", "");
                            str = Regex.Replace(str, @"  ", " ");
                            temp.Industry = str;
                            tempList.Add(temp);
                        }
                        else
                        {
                            if (_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].Industry.Contains("("))
                            {
                                temp.Industry = _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].Industry;
                            }
                            else
                            {
                                temp.Industry = _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].Industry + " (" + _fiefService.InformationList[temp.FiefId].FiefName + ")";
                            }
                            
                            tempList.Add(temp);
                        }
                    }
                }
            }

            //for (int x = 0; x < tempList.Count; x++)
            //{
            //    if (tempList[x].Industry.Contains("()"))
            //    {
            //        tempList[x].Industry.Replace("()", $"({_fiefService.InformationList[tempList[x].FiefId].FiefName})");
            //    }
            //}

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
                        industry = $"Utveckla {_fiefService.IncomeList[x].IncomesCollection[y].Income} ({_fiefService.InformationList[x].FiefName})";
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
                            industry = $"Utveckla {_fiefService.MinesList[x].MinesCollection[y].MineType} gruva ({_fiefService.InformationList[x].FiefName})";
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
                            industry = $"Utveckla {_fiefService.MinesList[x].QuarriesCollection[y].QuarryType} stenbrott ({_fiefService.InformationList[x].FiefName})";
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
                        industry = "Utveckla Hamnen ({_fiefService.InformationList[x].FiefName})";
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
                        if (!_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].Industry.Contains("Utveckla Medicin")
                            || !_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].Industry.Contains("Utveckla Militär")
                            || !_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[x].Industry.Contains("Utveckla Utbildning"))
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

        public List<IndustryBeingDevelopedModel> UpdateIndustriesBeingDevelopedCollection(ObservableCollection<IndustryBeingDevelopedModel> collection)
        {
            int nrFiefs = _fiefService.InformationList.Count - 1;
            List<int> fiefs = new List<int>();
            List<int> ids = new List<int>();

            for (int x = 0; x < collection.Count; x++)
            {
                if (collection[x].Industry.Contains("()"))
                {
                    collection[x].Industry.Replace("()", $"({_fiefService.InformationList[collection[x].FiefId].FiefName})");
                }
                if (!fiefs.Contains(collection[x].FiefId))
                {
                    fiefs.Add(collection[x].FiefId);
                }
            }

            if (fiefs.Count != _fiefService.InformationList.Count - 1)
            {
                for (int y = 1; y < _fiefService.InformationList.Count; y++)
                {
                    if (!fiefs.Contains(y))
                    {
                        int id = _baseService.GetNewIndustryId();

                        collection.Add(new IndustryBeingDevelopedModel()
                        {
                            Id = id,
                            Industry = "Utveckla Medicin" + " (" + _fiefService.InformationList[y].FiefName + ")",
                            FiefId = y,
                            IndustryId = id,
                            StewardId = -1
                        });
                        id++;

                        collection.Add(new IndustryBeingDevelopedModel()
                        {
                            Id = id,
                            Industry = "Utveckla Militär" + " (" + _fiefService.InformationList[y].FiefName + ")",
                            FiefId = y,
                            IndustryId = id,
                            StewardId = -1
                        });
                        id++;

                        collection.Add(new IndustryBeingDevelopedModel()
                        {
                            Id = id,
                            Industry = "Utveckla Utbildning" + " (" + _fiefService.InformationList[y].FiefName + ")",
                            FiefId = y,
                            IndustryId = id,
                            StewardId = -1
                        });
                    }
                }
            }

            //ids = collection.GroupBy(o => o.IndustryId)
            //    .Where(g => g.Count() > 1)
            //    .Select(y => y.Key)
            //    .ToList();

            //for (int o = collection.Count - 1; o > -1; o--)
            //{
            //    if (ids.Contains(collection[o].IndustryId))
            //    {
            //        ids.Remove(collection[o].IndustryId);

            //        int newId = _baseService.GetNewIndustryId();
            //        collection[o].IndustryId = newId;
            //        collection[o].Id = newId;
            //        // CHANGE ID FOR STEWARD IF THERE IS ONE
                    
            //    }
            //}

            return new List<IndustryBeingDevelopedModel>(collection);
        }
    }
}
