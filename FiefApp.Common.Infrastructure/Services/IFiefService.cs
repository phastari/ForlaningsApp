using FiefApp.Common.Infrastructure.DataModels;
using System.Collections.Generic;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IFiefService
    {
        int Index { get; set; }
        List<InformationDataModel> InformationList { get; set; }
        List<ArmyDataModel> ArmyList { get; set; }
        List<EmployeesDataModel> EmployeesList { get; set; }
        List<ManorDataModel> ManorList { get; set; }
    }
}