using System.Collections.Generic;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IEndOfYearService
    {
        List<EndOfYearIncomeFiefModel> InitializeIncomes();
        List<EndOfYearSubsidiaryModel> InitializeSubsidiaries(int index, string fief);
    }
}