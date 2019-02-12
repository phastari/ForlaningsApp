using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public class ManorService : IManorService
    {
        private readonly IFiefService _fiefService;

        public ManorService(IFiefService fiefService)
        {
            _fiefService = fiefService;
        }

        public ManorDataModel GetManorDataModel(int id)
        {
            bool returnNull = true;
            int index = -1;
            for (int x = 0; x < _fiefService.ManorList.Count; x++)
            {
                if (id == _fiefService.ManorList[x].Id)
                {
                    returnNull = false;
                    index = x;
                    break;
                }
            }

            if (returnNull)
            {
                return null;
            }
            else
            {
                return _fiefService.ManorList[index];
            }
        }

        public void SetManorDataModel(ManorDataModel manorDataModel)
        {
            for (int x = 0; x < _fiefService.ManorList.Count; x++)
            {
                if (manorDataModel.Id == _fiefService.ManorList[x].Id)
                {
                    _fiefService.ManorList.RemoveAt(x);
                    _fiefService.ManorList.Insert(x, manorDataModel);
                    break;
                }
            }
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
    }
}
