using System.Collections.Generic;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IEndOfYearService
    {
        List<EndOfYearIncomeFiefModel> Initialize();
    }
}