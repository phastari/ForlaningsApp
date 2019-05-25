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
        void EditCustomSubsidiary(SubsidiaryModel model);
        ObservableCollection<SubsidiaryModel> GetSubsidiaryCollection(int index);
        ObservableCollection<SubsidiaryModel> GetConstructingCollection(int index);
        void ShowConstructingCollection();
        void SetSubsidiary(int index, int id, SubsidiaryModel model);
        int GetAndSetDifficulty(int index, decimal spring, decimal summer, decimal fall, decimal winter);
        int GetSilverIncome(int quality, int developmentLevel, decimal incomeFactor, decimal silverFactor);
        int GetBaseIncome(int quality, int developmentLevel, decimal incomeFactor, decimal baseFactor);
        int GetLuxuryIncome(int quality, int developmentLevel, decimal incomeFactor, decimal luxuryFactor);
    }
}