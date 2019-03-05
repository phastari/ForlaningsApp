using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public class StewardsService : IStewardsService
    {
        private readonly IFiefService _fiefService;

        public StewardsService(IFiefService fiefService)
        {
            _fiefService = fiefService;
        }

        public List<IndustryModel> GetIndustryModels(int index)
        {
            return null;
        }

        public int GetNextStewardId()
        {
            List<int> tempList = new List<int>();

            for (int x = 1; x < _fiefService.StewardsList.Count; x++)
            {
                for (int y = 0; y < _fiefService.StewardsList[x].StewardsCollection.Count; y++)
                {
                    tempList.Add(_fiefService.StewardsList[x].StewardsCollection[y].Id);
                }
            }

            return tempList.Count > 0 ? tempList.Max() + 1 : 0;
        }

        public StewardsDataModel GetAllStewardsDataModel()
        {
            StewardsDataModel tempDataModel = new StewardsDataModel();
            tempDataModel.StewardsCollection = new ObservableCollection<StewardModel>();

            for (int x = 1; x < _fiefService.StewardsList.Count; x++)
            {
                for (int y = 0; y < _fiefService.StewardsList[x].StewardsCollection.Count; y++)
                {
                    tempDataModel.StewardsCollection.Add(_fiefService.StewardsList[x].StewardsCollection[y]);
                }
            }

            return tempDataModel;
        }
    }
}
