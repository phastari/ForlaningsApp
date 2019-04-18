using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IMinesService
    {
        MinesDataModel GetAllMinesDataModel();
        int GetNewIdForQuarry(int index);
    }
}