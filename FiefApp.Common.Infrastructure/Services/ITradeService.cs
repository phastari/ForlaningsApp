using System.Collections.Generic;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface ITradeService
    {
        TradeDataModel GetAllTradeDataModel();
        int GetNewMerchantId();
        MarketModel GetMarket(int index);
        List<BoatModel> GetBoatsFromPortLists();
        void RemoveShipInPortListBoatsCollection(int id);
    }
}