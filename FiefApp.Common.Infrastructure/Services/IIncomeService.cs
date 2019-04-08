using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IIncomeService
    {
        ObservableCollection<IncomeModel> GetAllIncomes(int index);
        ObservableCollection<StewardModel> GetAllStewards(int index);
        void ChangeSteward(int stewardId, int manorId, string manor);
        ObservableCollection<IncomeModel> GetIncomeCollection(int index);
    }
}