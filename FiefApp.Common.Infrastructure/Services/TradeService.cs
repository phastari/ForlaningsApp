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
            return null;
        }
    }
}
