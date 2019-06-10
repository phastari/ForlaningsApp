using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public class TradeService : ITradeService
    {
        private readonly IFiefService _fiefService;

        public TradeService(
            IFiefService fiefService
            )
        {
            _fiefService = fiefService;
        }

        public TradeDataModel GetAllTradeDataModel()
        {
            TradeDataModel model = new TradeDataModel();

            for (int x = 1; x < _fiefService.TradeList.Count; x++)
            {
                model.MarketAvailableBase += _fiefService.TradeList[x].MarketAvailableBase;
                model.MarketAvailableIron += _fiefService.TradeList[x].MarketAvailableIron;
                model.MarketAvailableLuxury += _fiefService.TradeList[x].MarketAvailableLuxury;
                model.MarketAvailableStone += _fiefService.TradeList[x].MarketAvailableStone;
                model.MarketAvailableWood += _fiefService.TradeList[x].MarketAvailableWood;

                model.MerchantsCollection.AddRange(_fiefService.TradeList[x].MerchantsCollection);
                model.ShipsCollection.AddRange(_fiefService.TradeList[x].ShipsCollection);
            }

            return model;
        }

        public int GetNewMerchantId()
        {
            List<int> tempList = new List<int>();

            for (int x = 1; x < _fiefService.TradeList.Count; x++)
            {
                for (int y = 0; y < _fiefService.TradeList[x].MerchantsCollection.Count; y++)
                {
                    tempList.Add(_fiefService.TradeList[x].MerchantsCollection[y].Id);
                }
            }

            if (tempList.Count > 0)
            {
                return tempList.Max() + 1;
            }
            return 0;
        }
    }
}
