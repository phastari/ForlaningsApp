using System;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Trade.RoutedEvents;
using Prism.Commands;
using Prism.Events;

namespace FiefApp.Module.Trade
{
    public class TradeViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly ITradeService _tradeService;
        private readonly IEventAggregator _eventAggregator;

        public TradeViewModel(
            IBaseService baseService,
            ITradeService tradeService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            TabName = "Handel";

            _baseService = baseService;
            _tradeService = tradeService;
            _eventAggregator = eventAggregator;

            AddMerchant = new DelegateCommand(ExecuteAddMerchant);
            MerchantUIEventHandler = new CustomDelegateCommand(ExecuteMerchantUIEventHandler, o => true);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
        }

        #region DelegateCommand : AddMerchant

        public DelegateCommand AddMerchant { get; set; }
        private void ExecuteAddMerchant()
        {
            DataModel.MerchantsCollection.Add(new MerchantModel()
            {
                Id = _tradeService.GetNewMerchantId()
            });
        }

        #endregion
        #region CustomDelegateCommand : MerchantUIEventHandler

        public CustomDelegateCommand MerchantUIEventHandler { get; set; }
        private void ExecuteMerchantUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is MerchantUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Delete")
            {
                for (int x = 0; x < DataModel.MerchantsCollection.Count; x++)
                {
                    if (e.Id == DataModel.MerchantsCollection[x].Id)
                    {
                        DataModel.MerchantsCollection.RemoveAt(x);
                        break;
                    }
                }
            }

            if (e.Action == "Save")
            {
                for (int x = 0; x < DataModel.MerchantsCollection.Count; x++)
                {
                    if (e.Id == DataModel.MerchantsCollection[x].Id)
                    {
                        DataModel.MerchantsCollection[x].PersonName = e.Model.PersonName;
                        DataModel.MerchantsCollection[x].Age = e.Model.Age;
                        DataModel.MerchantsCollection[x].Skill = e.Model.Skill;
                        DataModel.MerchantsCollection[x].Resources = e.Model.Resources;
                        DataModel.MerchantsCollection[x].Loyalty = e.Model.Loyalty;
                        break;
                    }
                }
            }

            if (e.Action == "Change")
            {
                for (int x = 0; x < DataModel.MerchantsCollection.Count; x++)
                {
                    if (DataModel.MerchantsCollection[x].OnBoardBoatId == e.BoatId)
                    {
                        if (DataModel.MerchantsCollection[x].Id != e.Id)
                        {
                            DataModel.MerchantsCollection[x].OnBoardBoatId = -1;
                            DataModel.MerchantsCollection[x].OnBoardBoatName = "";
                        }
                        else
                        {
                            DataModel.MerchantsCollection[x].OnBoardBoatId = e.BoatId;
                            DataModel.MerchantsCollection[x].OnBoardBoatName = e.BoatName;
                        }
                    }
                }
            }
        }

        #endregion

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

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            LoadData();
        }
    }
}
