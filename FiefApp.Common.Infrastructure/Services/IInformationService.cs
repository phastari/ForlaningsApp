using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IInformationService
    {
        InformationDataModel GetAllInformationDataModel();
        void SetupPopulationReligion(int index);
        int GetTotalPopulation(int index);
    }
}