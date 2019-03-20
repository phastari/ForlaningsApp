using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IIncomeService
    {
        ObservableCollection<IncomeModel> GetAllIncomes(int index);
    }
}