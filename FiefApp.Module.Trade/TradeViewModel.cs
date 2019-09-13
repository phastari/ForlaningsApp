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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace FiefApp.Module.Trade
{
    public class TradeViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly ITradeService _tradeService;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISupplyService _supplyService;
        private List<UpdateEventParameters> _awaitResponsList = new List<UpdateEventParameters>()
        {
            new UpdateEventParameters()
            {
                ModuleName = "Army",
                Completed = true
            },
            new UpdateEventParameters()
            {
                ModuleName = "Boatbuilding",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Buildings",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Employees",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Expenses",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Income",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Information",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Manor",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Mines",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Port",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Stewards",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Subsidiary",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Trade",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Weather",
                Completed = false
            }
        };

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
            _eventAggregator.GetEvent<SaveDataModelBeforeSaveFileIsCreatedEvent>().Subscribe(ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent);
            _eventAggregator.GetEvent<UpdateAllEvent>().Subscribe(UpdateAndRespond);
            _eventAggregator.GetEvent<UpdateEvent>().Subscribe(UpdateResponse);
            _eventAggregator.GetEvent<UpdateResponseEvent>().Subscribe(HandleUpdateEvent);
        }

        #region Event Handlers

        private void HandleUpdateEvent(UpdateEventParameters param)
        {
            if (param.Publisher == "Trade"
                && _awaitResponsList != null)
            {
                for (int x = 0; x < _awaitResponsList.Count; x++)
                {
                    if (_awaitResponsList[x].ModuleName == param.ModuleName)
                    {
                        _awaitResponsList[x].Completed = param.Completed;
                    }
                }

                if (_awaitResponsList.Any(o => o.Completed == false))
                {
                    Console.WriteLine("Wait!");
                }
                else
                {
                    for (int y = 0; y < _awaitResponsList.Count; y++)
                    {
                        _awaitResponsList[y].Completed = false;
                    }
                    CompleteLoadData();
                }
            }
        }

        private void UpdateResponse(string str)
        {
            if (str != "Trade")
            {
                UpdateFiefCollection();
                for (int x = 1; x < FiefCollection.Count; x++)
                {
                    DataModel = _baseService.GetDataModel<TradeDataModel>(x);
                    GetInformationSetDataModel(x);
                    SaveData(x);
                }

                _eventAggregator.GetEvent<UpdateResponseEvent>().Publish(new UpdateEventParameters()
                {
                    ModuleName = "Trade",
                    Completed = true,
                    Publisher = str
                });
            }
        }

        private void UpdateAndRespond()
        {
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<TradeDataModel>(x);
                SaveData(x);
            }

            _eventAggregator.GetEvent<UpdateAllResponseEvent>().Publish(new UpdateAllEventParameters()
            {
                ModuleName = "Trade",
                Completed = true
            });
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            CompleteLoadData();
        }

        private void ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent()
        {
            SaveData();
        }

        #endregion

        private void CompleteLoadData()
        {
            DataModel = Index
                        == 0 ? _tradeService.GetAllTradeDataModel()
                : _baseService.GetDataModel<TradeDataModel>(Index);

            GetInformationSetDataModel();
            UpdateFiefCollection();
        }

        #region DelegateCommand : AddMerchant

        public DelegateCommand AddMerchant { get; set; }
        private void ExecuteAddMerchant()
        {
            int id = _tradeService.GetNewMerchantId();
            if (id == 0)
            {
                DataModel.MerchantsCollection.Add(new MerchantModel()
                {
                    Id = 0
                });

                DataModel.MerchantsCollection.Add(new MerchantModel()
                {
                    Id = 1,
                    PersonName = _baseService.GetCommonerName(),
                    Age = _baseService.RollDie(10, 61)
                });
            }
            else
            {
                DataModel.MerchantsCollection.Add(new MerchantModel()
                {
                    Id = _tradeService.GetNewMerchantId(),
                    PersonName = _baseService.GetCommonerName(),
                    Age = _baseService.RollDie(10, 61)
                });
            }
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

                    if (_supplyService.Withdraw(e.Model.SilverWith, e.Model.BaseToSell, e.Model.LuxuryToSell, e.Model.IronToSell, e.Model.StoneToSell, e.Model.WoodToSell))
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
                                    DataModel.MerchantsCollection[x].Assignment = "K";
                                }
                                else if (e.Model.SendByCarriage)
                                {
                                    DataModel.MerchantsCollection[x].Assignment = "HV";
                                }
                                else
                                {
                                    DataModel.MerchantsCollection[x].Assignment = "F";

                                    if (e.Model.ShipId != -1)
                                    {
                                        for (int i = 0; i < DataModel.ShipsCollection.Count; i++)
                                        {
                                            if (DataModel.ShipsCollection[i].Id == e.Model.ShipId)
                                            {
                                                DataModel.MerchantsCollection[x].ShipModel = (BoatModel)DataModel.ShipsCollection[i].Clone();
                                                DataModel.ShipsCollection.RemoveAt(i);
                                                _tradeService.RemoveShipInPortListBoatsCollection(i);
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
                        MessageBox.Show("Du har inte nog med resurser!");
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
            CompleteLoadData();
        }

        private void GetInformationSetDataModel(int index = -1)
        {
            if (index == -1)
            {
                GetMarket(Index);
                GetBoatsCollectionFromPortList(Index);
            }
            else
            {
                GetMarket(index);
                GetBoatsCollectionFromPortList(index);
            }
        }

        #region GetInformationSetDataModel

        private void GetMarket(int index)
        {
            if (!DataModel.MarketSetThisYear)
            {
                var model = _tradeService.GetMarket(index);
                DataModel.MarketAvailableBase = model.MarketBase;
                DataModel.MarketAvailableLuxury = model.MarketLuxury;
                DataModel.MarketAvailableIron = model.MarketIron;
                DataModel.MarketAvailableStone = model.MarketStone;
                DataModel.MarketAvailableWood = model.MarketWood;
                DataModel.MarketSetThisYear = true;
                SaveData(index);
            }
        }

        private void GetBoatsCollectionFromPortList(int index)
        {
            DataModel.ShipsCollection = new ObservableCollection<BoatModel>(_tradeService.GetBoatsFromPortLists());
        }

        #endregion
    }
}
