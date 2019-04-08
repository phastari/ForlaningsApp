using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Services;

namespace FiefApp.Module.Trade
{
    public class TradeViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly ITradeService _tradeService;

        public TradeViewModel(
            IBaseService baseService,
            ITradeService tradeService
            ) : base(baseService)
        {
            TabName = "Handel";

            _baseService = baseService;
            _tradeService = tradeService;
        }

        #region DataModel

        private TradeDataModel _dataModel = new TradeDataModel();
        public TradeDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        protected override void SaveData(int index = -1)
        {
            _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
        }

        protected override void LoadData()
        {
            DataModel = Index
                        == 0 ? _tradeService.GetAllTradeDataModel()
                : _baseService.GetDataModel<TradeDataModel>(Index);

            UpdateFiefCollection();
        }
    }
}
