using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IIncomeService
    {
        ObservableCollection<IncomeModel> CheckIncomesCollection(int index);
    }
}