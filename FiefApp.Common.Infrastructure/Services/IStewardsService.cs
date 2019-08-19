using System.Collections.Generic;
using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IStewardsService
    {
        List<IndustryModel> GetIndustryModels(int index);
        int GetNextStewardId();
        List<StewardIndustryModel> GetIndustries();
        void SetBeingDevelopedInIndustries(int industryId, bool beingDeveloped);
        List<IndustryBeingDevelopedModel> UpdateIndustriesBeingDevelopedCollection(ObservableCollection<IndustryBeingDevelopedModel> collection);
    }
}