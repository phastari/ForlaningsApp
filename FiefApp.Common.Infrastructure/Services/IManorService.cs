using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IManorService
    {
        ManorDataModel GetManorDataModel(int id);
        void SetManorDataModel(ManorDataModel manorDataModel);
        ObservableCollection<IPeopleModel> GetResidentsCollection(int index);
    }
}