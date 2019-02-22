using System;
using FiefApp.Common.Infrastructure.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public class ManorService : IManorService
    {
        private readonly IFiefService _fiefService;

        public ManorService(IFiefService fiefService)
        {
            _fiefService = fiefService;
        }

        public ObservableCollection<IPeopleModel> GetResidentsCollection(int index)
        {
            ObservableCollection<IPeopleModel> tempCollection = new ObservableCollection<IPeopleModel>();

            for (int x = 0; x < _fiefService.ArmyList[index].TemplarKnightsList.Count; x++)
            {
                tempCollection.Add(_fiefService.ArmyList[index].TemplarKnightsList[x]);
            }

            for (int x = 0; x < _fiefService.ArmyList[index].KnightsList.Count; x++)
            {
                tempCollection.Add(_fiefService.ArmyList[index].KnightsList[x]);
            }

            for (int x = 0; x < _fiefService.ArmyList[index].CavalryTemplarKnightsList.Count; x++)
            {
                tempCollection.Add(_fiefService.ArmyList[index].CavalryTemplarKnightsList[x]);
            }

            for (int x = 0; x < _fiefService.ArmyList[index].OfficerCorporalsList.Count; x++)
            {
                tempCollection.Add(_fiefService.ArmyList[index].OfficerCorporalsList[x]);
            }

            for (int x = 0; x < _fiefService.ArmyList[index].OfficerSergeantsList.Count; x++)
            {
                tempCollection.Add(_fiefService.ArmyList[index].OfficerSergeantsList[x]);
            }

            for (int x = 0; x < _fiefService.ArmyList[index].OfficerCaptainsList.Count; x++)
            {
                tempCollection.Add(_fiefService.ArmyList[index].OfficerCaptainsList[x]);
            }

            for (int x = 0; x < _fiefService.ManorList[index].ResidentsList.Count; x++)
            {
                tempCollection.Add(_fiefService.ManorList[index].ResidentsList[x]);
            }

            return tempCollection;
        }

        public void SetSoldierModel(int id, int index, SoldierModel model)
        {
            bool found = false;

            for (int x = 0; x < _fiefService.ArmyList[index].TemplarKnightsList.Count; x++)
            {
                if (_fiefService.ArmyList[index].TemplarKnightsList[x].Id == id)
                {
                    found = true;
                    _fiefService.ArmyList[index].TemplarKnightsList[x].Age = model.Age;
                    _fiefService.ArmyList[index].TemplarKnightsList[x].Name = model.Name;
                    _fiefService.ArmyList[index].TemplarKnightsList[x].Position = model.Position;
                    break;
                }
            }

            if (!found)
            {
                for (int x = 0; x < _fiefService.ArmyList[index].CavalryTemplarKnightsList.Count; x++)
                {
                    if (_fiefService.ArmyList[index].CavalryTemplarKnightsList[x].Id == id)
                    {
                        found = true;
                        _fiefService.ArmyList[index].CavalryTemplarKnightsList[x].Age = model.Age;
                        _fiefService.ArmyList[index].CavalryTemplarKnightsList[x].Name = model.Name;
                        _fiefService.ArmyList[index].CavalryTemplarKnightsList[x].Position = model.Position;
                        break;
                    }
                }
            }

            if (!found)
            {
                for (int x = 0; x < _fiefService.ArmyList[index].KnightsList.Count; x++)
                {
                    if (_fiefService.ArmyList[index].KnightsList[x].Id == id)
                    {
                        found = true;
                        _fiefService.ArmyList[index].KnightsList[x].Age = model.Age;
                        _fiefService.ArmyList[index].KnightsList[x].Name = model.Name;
                        _fiefService.ArmyList[index].KnightsList[x].Position = model.Position;
                        break;
                    }
                }
            }

            if (!found)
            {
                for (int x = 0; x < _fiefService.ArmyList[index].OfficerCorporalsList.Count; x++)
                {
                    if (_fiefService.ArmyList[index].OfficerCorporalsList[x].Id == id)
                    {
                        found = true;
                        _fiefService.ArmyList[index].OfficerCorporalsList[x].Age = model.Age;
                        _fiefService.ArmyList[index].OfficerCorporalsList[x].Name = model.Name;
                        _fiefService.ArmyList[index].OfficerCorporalsList[x].Position = model.Position;
                        break;
                    }
                }
            }

            if (!found)
            {
                for (int x = 0; x < _fiefService.ArmyList[index].OfficerSergeantsList.Count; x++)
                {
                    if (_fiefService.ArmyList[index].OfficerSergeantsList[x].Id == id)
                    {
                        found = true;
                        _fiefService.ArmyList[index].OfficerSergeantsList[x].Age = model.Age;
                        _fiefService.ArmyList[index].OfficerSergeantsList[x].Name = model.Name;
                        _fiefService.ArmyList[index].OfficerSergeantsList[x].Position = model.Position;
                        break;
                    }
                }
            }

            if (!found)
            {
                for (int x = 0; x < _fiefService.ArmyList[index].OfficerCaptainsList.Count; x++)
                {
                    if (_fiefService.ArmyList[index].OfficerCaptainsList[x].Id == id)
                    {
                        found = true;
                        _fiefService.ArmyList[index].OfficerCaptainsList[x].Age = model.Age;
                        _fiefService.ArmyList[index].OfficerCaptainsList[x].Name = model.Name;
                        _fiefService.ArmyList[index].OfficerCaptainsList[x].Position = model.Position;
                        break;
                    }
                }
            }
        }

        public int GetPeopleId(int index)
        {
            List<int> tempList = new List<int>();

            for (int x = 0; x < _fiefService.ArmyList[index].TemplarKnightsList.Count; x++)
            {
                tempList.Add(_fiefService.ArmyList[index].TemplarKnightsList[x].Id);
            }

            for (int x = 0; x < _fiefService.ArmyList[index].CavalryTemplarKnightsList.Count; x++)
            {
                tempList.Add(_fiefService.ArmyList[index].CavalryTemplarKnightsList[x].Id);
            }

            for (int x = 0; x < _fiefService.ArmyList[index].KnightsList.Count; x++)
            {
                tempList.Add(_fiefService.ArmyList[index].KnightsList[x].Id);
            }

            for (int x = 0; x < _fiefService.ArmyList[index].OfficerCorporalsList.Count; x++)
            {
                tempList.Add(_fiefService.ArmyList[index].OfficerCorporalsList[x].Id);
            }

            for (int x = 0; x < _fiefService.ArmyList[index].OfficerSergeantsList.Count; x++)
            {
                tempList.Add(_fiefService.ArmyList[index].OfficerSergeantsList[x].Id);
            }

            for (int x = 0; x < _fiefService.ArmyList[index].OfficerCaptainsList.Count; x++)
            {
                tempList.Add(_fiefService.ArmyList[index].OfficerCaptainsList[x].Id);
            }

            for (int x = 0; x < _fiefService.ManorList[index].ResidentsList.Count; x++)
            {
                tempList.Add(_fiefService.ManorList[index].ResidentsList[x].Id);
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

        public void DeletePeople(int id, int index)
        {
            bool found = false;

            for (int x = 0; x < _fiefService.ArmyList[index].TemplarKnightsList.Count; x++)
            {
                if (_fiefService.ArmyList[index].TemplarKnightsList[x].Id == id)
                {
                    found = true;
                    _fiefService.ArmyList[index].TemplarKnightsList.RemoveAt(x);
                    _fiefService.ArmyList[index].ArmyKnightTemplars -= 1;
                    break;
                }
            }

            if (!found)
            {
                for (int x = 0; x < _fiefService.ArmyList[index].CavalryTemplarKnightsList.Count; x++)
                {
                    if (_fiefService.ArmyList[index].CavalryTemplarKnightsList[x].Id == id)
                    {
                        found = true;
                        _fiefService.ArmyList[index].CavalryTemplarKnightsList.RemoveAt(x);
                        _fiefService.ArmyList[index].CavalryKnightTemplars -= 1;
                        break;
                    }
                }
            }

            if (!found)
            {
                for (int x = 0; x < _fiefService.ArmyList[index].KnightsList.Count; x++)
                {
                    if (_fiefService.ArmyList[index].KnightsList[x].Id == id)
                    {
                        found = true;
                        _fiefService.ArmyList[index].KnightsList.RemoveAt(x);
                        _fiefService.ArmyList[index].CavalryKnights -= 1;
                        break;
                    }
                }
            }

            if (!found)
            {
                for (int x = 0; x < _fiefService.ArmyList[index].OfficerCorporalsList.Count; x++)
                {
                    if (_fiefService.ArmyList[index].OfficerCorporalsList[x].Id == id)
                    {
                        found = true;
                        _fiefService.ArmyList[index].OfficerCorporalsList.RemoveAt(x);
                        _fiefService.ArmyList[index].OfficersCorporal -= 1;
                        break;
                    }
                }
            }

            if (!found)
            {
                for (int x = 0; x < _fiefService.ArmyList[index].OfficerSergeantsList.Count; x++)
                {
                    if (_fiefService.ArmyList[index].OfficerSergeantsList[x].Id == id)
                    {
                        found = true;
                        _fiefService.ArmyList[index].OfficerSergeantsList.RemoveAt(x);
                        _fiefService.ArmyList[index].OfficersSergeant -= 1;
                        break;
                    }
                }
            }

            if (!found)
            {
                for (int x = 0; x < _fiefService.ArmyList[index].OfficerCaptainsList.Count; x++)
                {
                    if (_fiefService.ArmyList[index].OfficerCaptainsList[x].Id == id)
                    {
                        found = true;
                        _fiefService.ArmyList[index].OfficerCaptainsList.RemoveAt(x);
                        _fiefService.ArmyList[index].OfficersCaptain -= 1;
                        break;
                    }
                }
            }

            if (!found)
            {
                for (int x = 0; x < _fiefService.ManorList[index].ResidentsList.Count; x++)
                {
                    if (_fiefService.ManorList[index].ResidentsList[x].Id == id)
                    {
                        found = true;
                        _fiefService.ManorList[index].ResidentsList.RemoveAt(x);
                        break;
                    }
                }
            }
        }

        public ManorDataModel GetAllManorDataModel()
        {
            ManorDataModel tempDataModel = new ManorDataModel();
            List<IPeopleModel> tempPeopleModel = new List<IPeopleModel>();
            List<VillageModel> tempVillageModels = new List<VillageModel>();
            
            List<int> wealth = new List<int>();

            for (int x = 1; x < _fiefService.ManorList.Count; x++)
            {
                tempDataModel.ManorPopulation += _fiefService.ManorList[x].ManorPopulation;
                tempDataModel.ManorAcres += _fiefService.ManorList[x].ManorAcres;
                tempDataModel.ManorPasture += _fiefService.ManorList[x].ManorPasture;
                tempDataModel.ManorArable += _fiefService.ManorList[x].ManorArable;
                tempDataModel.ManorWoodland += _fiefService.ManorList[x].ManorWoodland;
                tempDataModel.ManorFelling += _fiefService.ManorList[x].ManorFelling;
                tempDataModel.ManorUseless += _fiefService.ManorList[x].ManorUseless;

                wealth.Add(Convert.ToInt32(_fiefService.ManorList[x].ManorWealth));
            }

            tempDataModel.ManorWealth = wealth.Min() 
                                        == wealth.Max() 
                ? $"{wealth.Min()}" 
                : $"{wealth.Min()} - {wealth.Max()}";

            for (int x = 1; x < _fiefService.ArmyList.Count; x++)
            {
                for (int y = 0; y < _fiefService.ArmyList[x].TemplarKnightsList.Count; y++)
                {
                    tempPeopleModel.Add(_fiefService.ArmyList[x].TemplarKnightsList[y]);
                }

                for (int y = 0; y < _fiefService.ArmyList[x].KnightsList.Count; y++)
                {
                    tempPeopleModel.Add(_fiefService.ArmyList[x].KnightsList[y]);
                }

                for (int y = 0; y < _fiefService.ArmyList[x].CavalryTemplarKnightsList.Count; y++)
                {
                    tempPeopleModel.Add(_fiefService.ArmyList[x].CavalryTemplarKnightsList[y]);
                }

                for (int y = 0; y < _fiefService.ArmyList[x].OfficerCaptainsList.Count; y++)
                {
                    tempPeopleModel.Add(_fiefService.ArmyList[x].OfficerCaptainsList[y]);
                }

                for (int y = 0; y < _fiefService.ArmyList[x].OfficerCorporalsList.Count; y++)
                {
                    tempPeopleModel.Add(_fiefService.ArmyList[x].OfficerCorporalsList[y]);
                }

                for (int y = 0; y < _fiefService.ArmyList[x].OfficerSergeantsList.Count; y++)
                {
                    tempPeopleModel.Add(_fiefService.ArmyList[x].OfficerSergeantsList[y]);
                }

                for (int y = 0; y < _fiefService.ManorList[x].ResidentsList.Count; y++)
                {
                    tempPeopleModel.Add(_fiefService.ManorList[x].ResidentsList[y]);
                }

                for (int y = 0; y < _fiefService.ManorList[x].VillagesCollection.Count; y++)
                {
                    tempVillageModels.Add(_fiefService.ManorList[x].VillagesCollection[y]);
                }
            }

            tempDataModel.ResidentsCollection = new ObservableCollection<IPeopleModel>(tempPeopleModel);
            tempDataModel.VillagesCollection = new ObservableCollection<VillageModel>(tempVillageModels);

            return tempDataModel;
        }
    }
}
