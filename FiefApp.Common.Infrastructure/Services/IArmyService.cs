using FiefApp.Common.Infrastructure.DataModels;
using System.Collections.Generic;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IArmyService
    {
        ArmyDataModel GetAllArmyDataModel();
        int GetPeopleId(int index);
        void UpdateSilverExpenses(int index, int silver);
        void UpdateBaseExpenses(int index, int abase);
    }
}