using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface ITradeService
    {
        TradeDataModel GetAllTradeDataModel();
        int GetNewMerchantId();
    }
}