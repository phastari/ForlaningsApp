using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FiefApp.Common.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IFiefService _fiefService;
        private readonly ISettingsService _settingsService;

        public EmployeeService(
            IFiefService fiefService,
            ISettingsService settingsService
            )
        {
            _fiefService = fiefService;
            _settingsService = settingsService;
        }

        public EmployeesDataModel GetAllEmployeesDataModel()
        {
            EmployeesDataModel tempDataModel = _fiefService.EmployeesList[0];
            int prospectorBase = 0;
            int prospectorLuxury = 0;
            List<EmployeeModel> tempEmployeesList = new List<EmployeeModel>();

            tempDataModel.CalculateTotal = false;

            tempDataModel.Falconer = 0;
            tempDataModel.Bailiff = 0;
            tempDataModel.Herald = 0;
            tempDataModel.Hunter = 0;
            tempDataModel.Physician = 0;
            tempDataModel.Scholar = 0;
            tempDataModel.Cupbearer = 0;
            tempDataModel.Prospector = 0;

            for (int x = 1; x < _fiefService.EmployeesList.Count; x++)
            {
                tempDataModel.Falconer += _fiefService.EmployeesList[x].Falconer;
                tempDataModel.Bailiff += _fiefService.EmployeesList[x].Bailiff;
                tempDataModel.Herald += _fiefService.EmployeesList[x].Herald;
                tempDataModel.Hunter += _fiefService.EmployeesList[x].Hunter;
                tempDataModel.Physician += _fiefService.EmployeesList[x].Physician;
                tempDataModel.Scholar += _fiefService.EmployeesList[x].Scholar;
                tempDataModel.Cupbearer += _fiefService.EmployeesList[x].Cupbearer;
                tempDataModel.Prospector += _fiefService.EmployeesList[x].Prospector;
                prospectorBase += _fiefService.EmployeesList[x].BaseProspector;
                prospectorLuxury += _fiefService.EmployeesList[x].LuxuryProspector;
                tempEmployeesList.AddRange(_fiefService.EmployeesList[x].EmployeesCollection);
            }

            tempDataModel.BaseProspector = prospectorBase;
            tempDataModel.LuxuryProspector = prospectorLuxury;

            List<EmployeeModel> employeeList = tempEmployeesList
                .GroupBy(so => new
                {
                    so.Name,
                    so.BaseCost,
                    so.LuxuryCost
                })
                .Select(g => new EmployeeModel
                {
                    Name = g.Key.Name,
                    BaseCost = g.Key.BaseCost,
                    LuxuryCost = g.Key.LuxuryCost,
                    Number = g.Sum(so => so.Number)
                })
                .ToList();

            tempDataModel.EmployeesCollection = new ObservableCollection<EmployeeModel>(employeeList);

            tempDataModel.CalculateTotal = true;
            tempDataModel.UpdateTotalCosts();

            return tempDataModel;
        }
    }
}
