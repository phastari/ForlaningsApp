using FiefApp.Common.Infrastructure.Models;
using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IManorService
    {
        ObservableCollection<IPeopleModel> GetResidentsCollection(int index);
        void SetSoldierModel(int id, int index, SoldierModel model);
        int GetPeopleId(int index);
        void DeletePeople(int id, int index);
        ManorDataModel GetAllManorDataModel();
        string GetLivingcondition(int index);
    }
}