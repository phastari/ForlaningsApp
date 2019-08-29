using System.Collections.Generic;
using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IIncomeService
    {
        List<IncomeModel> SetIncomes(int index, ObservableCollection<IncomeModel> model);
        IncomeDataModel GetAllDataModel();
    }
}