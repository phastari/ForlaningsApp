using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IBaseService
    {
        int GetIndex();
        void SetIndex(int index);
        T GetDataModel<T>(int index);
        void SetDataModel(IDataModelBase dataModel, int index);
        int CreateNewFief();
        int RemoveFief(int id);
        ObservableCollection<string> GetFiefCollection();
        ObservableCollection<IndustryBeingDevelopedModel> CreateNewBeingDevelopedIndustries();
        ObservableCollection<IndustryBeingDevelopedModel> UpdateIndustriesBeingDevelopedCollection(ObservableCollection<IndustryBeingDevelopedModel> collection);
        int RollObDice(int skill);
        int RollDie(int x, int y);
        int ConvertToNumeric(string str);
        string ConvertToT6(int num);
        string GetCommonerName();
        string GetNobleName();
        int GetNewIndustryId();
        void RemoveSteward(int stewardId);
        void ChangeSteward(int stewardId, int industryId, string industryType = "");
        ObservableCollection<StewardModel> GetStewardsCollection();
        void SaveStewardsCollection(ObservableCollection<StewardModel> collection);
    }
}