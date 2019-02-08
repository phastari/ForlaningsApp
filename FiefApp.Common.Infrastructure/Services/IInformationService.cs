using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IInformationService
    {
        InformationDataModel GetInformationDataModel(int index);
        void SetInformationDataModel(InformationDataModel dataModel, int index);
        InformationDataModel GetAllInformationDataModel();
    }
}