using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IManorService
    {
        ManorDataModel GetManorDataModel(int id);
        void SetManorDataModel(ManorDataModel manorDataModel);
    }
}