using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface ISubsidiaryService
    {
        SubsidiaryDataModel GetAllSubsidiaryDataModel();
        ObservableCollection<SubsidiaryModel> GetSubsidiaryTypesCollection();
        void AddCustomSubsidiary(SubsidiaryModel model);
        int GetNewIdForSubsidiary();
        void EditCustomSubsidiary(SubsidiaryModel model);
        ObservableCollection<StewardModel> GetAllStewards();
        void ChangeSteward(int stewardId, int subsidiaryId, string subsidiary);
        ObservableCollection<SubsidiaryModel> GetSubsidiaryCollection(int index);
        ObservableCollection<SubsidiaryModel> GetConstructingCollection(int index);
        void ShowConstructingCollection();
    }
}