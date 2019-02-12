using FiefApp.Common.Infrastructure.DataModels;
using System.Collections.Generic;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IArmyService
    {
        ArmyDataModel GetAllArmyDataModel();
        List<int> GetAllResidentIds();
    }
}