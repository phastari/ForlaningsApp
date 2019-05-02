using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Trade.RoutedEvents;
using Prism.Commands;
using Prism.Events;
using System;
using System.Windows;

namespace FiefApp.Module.Trade
{
    public class TradeViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly ITradeService _tradeService;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISupplyService _supplyService;

        public TradeViewModel(
            IBaseService baseService,
            ITradeService tradeService,
            IEventAggregator eventAggregator,
            ISupplyService supplyService
            ) : base(baseService)
        {
            TabName = "Handel";

            _baseService = baseService;
            _tradeService = tradeService;
            _eventAggregator = eventAggregator;
            _supplyService = supplyService;

            AddMerchant = new DelegateCommand(ExecuteAddMerchant);
            MerchantUIEventHandler = new CustomDelegateCommand(ExecuteMerchantUIEventHandler, o => true);
            SendMerchantUIEventHandler = new CustomDelegateCommand(ExecuteSendMerchantUIEventHandler, o => true);

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

            switch (e.Action)
            {
                case "Delete":
                    {
                        for (int x = 0; x < DataModel.MerchantsCollection.Count; x++)
                        {
                            if (e.Id == DataModel.MerchantsCollection[x].Id)
                            {
                                DataModel.MerchantsCollection.RemoveAt(x);
                                break;
                            }
                        }
                        break;
                    }

                case "Save":
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
                        break;
                    }

                case "Send":
                    DataModel.SendMerchantId = e.Id;
                    DataModel.SendMerchantVisibility = Visibility.Visible;
                    DataModel.RootGridIsEnabled = false;
                    break;
            }

        }

        #endregion
        #region CustomDelegateCommand : SendMerchantUIEventHandler

        public CustomDelegateCommand SendMerchantUIEventHandler { get; set; }
        private void ExecuteSendMerchantUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is SendMerchantUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Cancel":
                    DataModel.SendMerchantVisibility = Visibility.Collapsed;
                    DataModel.RootGridIsEnabled = true;
                    break;

                case "Send":
                    DataModel.SendMerchantVisibility = Visibility.Collapsed;
                    DataModel.RootGridIsEnabled = true;

                    bool silver = true;
                    bool baseW = true;
                    bool luxury = true;
                    bool wood = true;
                    bool stone = true;
                    bool iron = true;

                    if (e.Model.SilverWith > 0)
                    {
                        silver = _supplyService.WithdrawSilver(e.Model.SilverWith);
                    }

                    if (e.Model.BaseToSell > 0)
                    {
                        baseW = _supplyService.WithdrawBase(e.Model.BaseToSell);
                    }

                    if (e.Model.LuxuryToSell > 0)
                    {
                        luxury = _supplyService.WithdrawLuxury(e.Model.LuxuryToSell);
                    }

                    if (e.Model.IronToSell > 0)
                    {
                        iron = _supplyService.WithdrawIron(e.Model.IronToSell);
                    }

                    if (e.Model.WoodToSell > 0)
                    {
                        wood = _supplyService.WithdrawWood(e.Model.WoodToSell);
                    }

                    if (e.Model.StoneToSell > 0)
                    {
                        stone = _supplyService.WithdrawStone(e.Model.StoneToSell);
                    }

                    if (silver && baseW && luxury && iron && wood && stone)
                    {
                        for (int x = 0; x < DataModel.MerchantsCollection.Count; x++)
                        {
                            if (DataModel.MerchantsCollection[x].Id == e.Model.Id)
                            {
                                DataModel.MerchantsCollection[x].SendByCarriage = e.Model.SendByCarriage;
                                DataModel.MerchantsCollection[x].SendWithCaravan = e.Model.SendWithCaravan;
                                DataModel.MerchantsCollection[x].Guards = e.Model.Guards;
                                DataModel.MerchantsCollection[x].SilverWith = e.Model.SilverWith;
                                DataModel.MerchantsCollection[x].SilverBack = e.Model.SilverBack;
                                DataModel.MerchantsCollection[x].BaseToSell = e.Model.BaseToSell;
                                DataModel.MerchantsCollection[x].BuyBase = e.Model.BuyBase;
                                DataModel.MerchantsCollection[x].LuxuryToSell = e.Model.LuxuryToSell;
                                DataModel.MerchantsCollection[x].BuyLuxury = e.Model.BuyLuxury;
                                DataModel.MerchantsCollection[x].IronToSell = e.Model.IronToSell;
                                DataModel.MerchantsCollection[x].BuyIron = e.Model.BuyIron;
                                DataModel.MerchantsCollection[x].StoneToSell = e.Model.StoneToSell;
                                DataModel.MerchantsCollection[x].BuyStone = e.Model.BuyStone;
                                DataModel.MerchantsCollection[x].WoodToSell = e.Model.WoodToSell;
                                DataModel.MerchantsCollection[x].BuyWood = e.Model.BuyWood;
                                DataModel.MerchantsCollection[x].ShipId = e.Model.ShipId;

                                if (e.Model.SendWithCaravan)
                                {
                                    DataModel.MerchantsCollection[x].Assignment = "Karavan";
                                }
                                else if (e.Model.SendByCarriage)
                                {
                                    DataModel.MerchantsCollection[x].Assignment = "Häst och vagn";
                                }
                                else
                                {
                                    DataModel.MerchantsCollection[x].Assignment = "Fartyg";

                                    if (e.Model.ShipId != -1)
                                    {
                                        for (int i = 0; i < DataModel.ShipsCollection.Count; i++)
                                        {
                                            if (DataModel.ShipsCollection[i].Id == e.Model.ShipId)
                                            {
                                                DataModel.MerchantsCollection[x].ShipModel = (BoatModel)DataModel.ShipsCollection[i].Clone();
                                                DataModel.ShipsCollection.RemoveAt(i);
                                                break;
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (e.Model.SilverWith > 0 && silver)
                        {
                            silver = _supplyService.DepositSilver(e.Model.SilverWith);
                        }

                        if (e.Model.BaseToSell > 0 && baseW)
                        {
                            baseW = _supplyService.DepositBase(e.Model.BaseToSell);
                        }

                        if (e.Model.LuxuryToSell > 0 && luxury)
                        {
                            luxury = _supplyService.DepositLuxury(e.Model.LuxuryToSell);
                        }

                        if (e.Model.IronToSell > 0 && iron)
                        {
                            iron = _supplyService.DepositIron(e.Model.IronToSell);
                        }

                        if (e.Model.WoodToSell > 0 && wood)
                        {
                            wood = _supplyService.DepositWood(e.Model.WoodToSell);
                        }

                        if (e.Model.StoneToSell > 0 && stone)
                        {
                            stone = _supplyService.DepositStone(e.Model.StoneToSell);
                        }
                    }
                    break;
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
