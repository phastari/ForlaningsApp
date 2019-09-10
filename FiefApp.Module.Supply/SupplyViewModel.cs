using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Services;
using Prism.Commands;
using Prism.Events;

namespace FiefApp.Module.Supply
{
    public class SupplyViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly ISupplyService _supplyService;
        private readonly IEventAggregator _eventAggregator;

        public SupplyViewModel(
            IBaseService baseService,
            ISupplyService supplyService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            _baseService = baseService;
            _supplyService = supplyService;
            _eventAggregator = eventAggregator;

            ModifySilverToSupply = new DelegateCommand(ExecuteModifySilverToSupply);
            ModifyBaseToSupply = new DelegateCommand(ExecuteModifyBaseToSupply);
            ModifyLuxuryToSupply = new DelegateCommand(ExecuteModifyLuxuryToSupply);
            ModifyIronToSupply = new DelegateCommand(ExecuteModifyIronToSupply);
            ModifyStoneToSupply = new DelegateCommand(ExecuteModifyStoneToSupply);
            ModifyWoodToSupply = new DelegateCommand(ExecuteModifyWoodToSupply);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
        }

        #region DelegateCommand : ModifySilverToSupply

        public DelegateCommand ModifySilverToSupply { get; set; }
        private void ExecuteModifySilverToSupply()
        {
            if (DataModel.AmountSilver != 0)
            {
                if (DataModel.AmountSilver > 0)
                {
                    DataModel.SupplySilver += DataModel.AmountSilver;
                    DataModel.TransactionSilver = "";
                    DataModel.AmountSilver = 0;
                }
                else if (DataModel.SupplySilver - DataModel.AmountSilver >= 0)
                {
                    DataModel.SupplySilver += DataModel.AmountSilver;
                    DataModel.TransactionSilver = "";
                    DataModel.AmountSilver = 0;
                }
                else
                {
                    DataModel.TransactionSilver = "!";
                }
            }
        }

        #endregion
        #region DelegateCommand : ModifyBaseToSupply

        public DelegateCommand ModifyBaseToSupply { get; set; }
        private void ExecuteModifyBaseToSupply()
        {
            if (DataModel.AmountBase != 0)
            {
                if (DataModel.AmountBase > 0)
                {
                    DataModel.SupplyBase += DataModel.AmountBase;
                    DataModel.TransactionBase = "";
                    DataModel.AmountBase = 0;
                }
                else if (DataModel.SupplyBase - DataModel.AmountBase >= 0)
                {
                    DataModel.SupplyBase += DataModel.AmountBase;
                    DataModel.TransactionBase = "";
                    DataModel.AmountBase = 0;
                }
                else
                {
                    DataModel.TransactionBase = "!";
                }
            }
        }

        #endregion
        #region DelegateCommand : ModifyLuxuryToSupply

        public DelegateCommand ModifyLuxuryToSupply { get; set; }
        private void ExecuteModifyLuxuryToSupply()
        {
            if (DataModel.AmountLuxury != 0)
            {
                if (DataModel.AmountLuxury > 0)
                {
                    DataModel.SupplyLuxury += DataModel.AmountLuxury;
                    DataModel.TransactionLuxury = "";
                    DataModel.AmountLuxury = 0;
                }
                else if (DataModel.SupplyLuxury - DataModel.AmountLuxury >= 0)
                {
                    DataModel.SupplyLuxury += DataModel.AmountLuxury;
                    DataModel.TransactionLuxury = "";
                    DataModel.AmountLuxury = 0;
                }
                else
                {
                    DataModel.TransactionLuxury = "!";
                }
            }
        }

        #endregion
        #region DelegateCommand : ModifyIronToSupply

        public DelegateCommand ModifyIronToSupply { get; set; }
        private void ExecuteModifyIronToSupply()
        {
            if (DataModel.AmountIron != 0)
            {
                if (DataModel.AmountIron > 0)
                {
                    DataModel.SupplyIron += DataModel.AmountIron;
                    DataModel.TransactionIron = "";
                    DataModel.AmountIron = 0;
                }
                else if (DataModel.SupplyIron - DataModel.AmountIron >= 0)
                {
                    DataModel.SupplyIron += DataModel.AmountIron;
                    DataModel.TransactionIron = "";
                    DataModel.AmountIron = 0;
                }
                else
                {
                    DataModel.TransactionIron = "!";
                }
            }
        }

        #endregion
        #region DelegateCommand : ModifyStoneToSupply

        public DelegateCommand ModifyStoneToSupply { get; set; }
        private void ExecuteModifyStoneToSupply()
        {
            if (DataModel.AmountStone != 0)
            {
                if (DataModel.AmountStone > 0)
                {
                    DataModel.SupplyStone += DataModel.AmountStone;
                    DataModel.TransactionStone = "";
                    DataModel.AmountStone = 0;
                }
                else if (DataModel.SupplyStone - DataModel.AmountStone >= 0)
                {
                    DataModel.SupplyStone += DataModel.AmountStone;
                    DataModel.TransactionStone = "";
                    DataModel.AmountStone = 0;
                }
                else
                {
                    DataModel.TransactionStone = "!";
                }
            }
        }

        #endregion
        #region DelegateCommand : ModifyWoodToSupply

        public DelegateCommand ModifyWoodToSupply { get; set; }
        private void ExecuteModifyWoodToSupply()
        {
            if (DataModel.AmountWood != 0)
            {
                if (DataModel.AmountWood > 0)
                {
                    DataModel.SupplyWood += DataModel.AmountWood;
                    DataModel.TransactionWood = "";
                    DataModel.AmountWood = 0;
                }
                else if (DataModel.SupplyWood - DataModel.AmountWood >= 0)
                {
                    DataModel.SupplyWood += DataModel.AmountWood;
                    DataModel.TransactionWood = "";
                    DataModel.AmountWood = 0;
                }
                else
                {
                    DataModel.TransactionWood = "!";
                }
            }
        }

        #endregion

        #region DataModel

        private SupplyDataModel _dataModel = new SupplyDataModel();
        public SupplyDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        #region Methods

        protected override void SaveData(int index = -1)
        {
            _supplyService.SaveToFiefService(DataModel);
        }
        protected override void LoadData()
        {
            DataModel = _supplyService.GetDataModelFromFiefService();
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            LoadData();
        }

        #endregion
    }
}
