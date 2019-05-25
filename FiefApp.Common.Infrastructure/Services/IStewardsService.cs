using System.Collections.Generic;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IStewardsService
    {
        List<IndustryModel> GetIndustryModels(int index);
        int GetNextStewardId();
        List<StewardIndustryModel> GetIndustries(int index);
        void SetBeingDevelopedInIndustries(int industryId, bool beingDeveloped);
    }
}