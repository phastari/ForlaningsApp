using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Port.RoutedEvents;
using Prism.Commands;
using Prism.Events;

namespace FiefApp.Module.Port
{
    public class PortViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IPortService _portService;
        private readonly ISettingsService _settingsService;
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

        public PortViewModel(
            IBaseService baseService,
            IPortService portService,
            ISettingsService settingsService,
            IEventAggregator eventAggregator,
            ISupplyService supplyService
            ) : base(baseService)
        {
            TabName = "Hamn";

            _baseService = baseService;
            _portService = portService;
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;
            _supplyService = supplyService;

            AddCaptain = new DelegateCommand(ExecuteAddCaptain);
            CaptainUIEventHandler = new CustomDelegateCommand(ExecuteCaptainUIEventHandler, o => true);
            BoatUIEventHandler = new CustomDelegateCommand(ExecuteBoatUIEventHandler, o => true);
            CrewBoatUIEventHandler = new CustomDelegateCommand(ExecuteCrewBoatUIEventHandler, o => true);
            ConstructShipyardEventHandler = new CustomDelegateCommand(ExecuteConstructShipyardEventHandler, o => true);
            GotShipyardUIEventHandler = new CustomDelegateCommand(ExecuteGotShipyardUIEventHandler, o => true);
            BuildingShipyardUIEventHandler = new CustomDelegateCommand(ExecuteBuildingShipyardUIEventHandler, o => true);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
            _eventAggregator.GetEvent<SaveDataModelBeforeSaveFileIsCreatedEvent>().Subscribe(ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent);
            _eventAggregator.GetEvent<UpdateAllEvent>().Subscribe(UpdateAndRespond);
            _eventAggregator.GetEvent<UpdateEvent>().Subscribe(UpdateResponse);
            _eventAggregator.GetEvent<UpdateResponseEvent>().Subscribe(HandleUpdateEvent);
            _eventAggregator.GetEvent<FishingBoatsAdded>().Subscribe(HandleFishingBoatsAdded);
            _eventAggregator.GetEvent<EndOfYearCompletedEvent>().Subscribe(HandleEndOfYearComplete);
            _eventAggregator.GetEvent<SaveEvent>().Subscribe(ExecuteSaveEventResponse);
        }

        #region Event Handlers

        private void ExecuteSaveEventResponse()
        {
            SaveData(Index);
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<PortDataModel>(x);
                SaveData(x);
            }

            _eventAggregator.GetEvent<SaveEventResponse>().Publish(new SaveEventParameters()
            {
                Completed = true,
                ModuleName = "Port"
            });

            DataModel = _baseService.GetDataModel<PortDataModel>(Index);
        }

        private void HandleEndOfYearComplete()
        {
            DataModel = null;
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<PortDataModel>(x);
                SaveData(x);
            }
            DataModel = _baseService.GetDataModel<PortDataModel>(Index);
        }

        private void HandleFishingBoatsAdded(int index)
        {
            DataModel = _baseService.GetDataModel<PortDataModel>(index);
            SaveData(index);
        }

        private void HandleUpdateEvent(UpdateEventParameters param)
        {
            if (param.Publisher == "Port"
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
            SaveData(Index);
            if (str != "Port")
            {
                SaveData(Index);
                UpdateFiefCollection();
                for (int x = 1; x < FiefCollection.Count; x++)
                {
                    DataModel = _baseService.GetDataModel<PortDataModel>(x);
                    SaveData(x);
                }
                DataModel = null;
                DataModel = _baseService.GetDataModel<PortDataModel>(Index);

                _eventAggregator.GetEvent<UpdateResponseEvent>().Publish(new UpdateEventParameters()
                {
                    ModuleName = "Port",
                    Completed = true,
                    Publisher = str
                });
            }
        }

        private void UpdateAndRespond()
        {
            SaveData(Index);
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<PortDataModel>(x);
                SaveData(x);
            }

            _eventAggregator.GetEvent<UpdateAllResponseEvent>().Publish(new UpdateAllEventParameters()
            {
                ModuleName = "Port",
                Completed = true
            });
        }

        #endregion

        #region DelegateCommand : AddCaptain

        public DelegateCommand AddCaptain { get; set; }
        private void ExecuteAddCaptain()
        {
            DataModel.CaptainsCollection.Add(new CaptainModel()
            {
                Id = _portService.GetNewCaptainId(Index),
                PersonName = _baseService.GetCommonerName(),
                Age = _baseService.RollDie(20,61)
            });
        }

        #endregion
        #region CustomDelegateCommand : CaptainUIEventHandler

        public CustomDelegateCommand CaptainUIEventHandler { get; set; }
        private void ExecuteCaptainUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is CaptainUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Delete")
            {
                for (int x = 0; x < DataModel.CaptainsCollection.Count; x++)
                {
                    if (e.Id == DataModel.CaptainsCollection[x].Id)
                    {
                        DataModel.CaptainsCollection.RemoveAt(x);
                        break;
                    }
                }
            }

            if (e.Action == "Save")
            {
                for (int x = 0; x < DataModel.CaptainsCollection.Count; x++)
                {
                    if (e.Id == DataModel.CaptainsCollection[x].Id)
                    {
                        DataModel.CaptainsCollection[x].PersonName = e.Model.PersonName;
                        DataModel.CaptainsCollection[x].Age = e.Model.Age;
                        DataModel.CaptainsCollection[x].Skill = e.Model.Skill;
                        DataModel.CaptainsCollection[x].Resources = e.Model.Resources;
                        DataModel.CaptainsCollection[x].Loyalty = e.Model.Loyalty;
                        break;
                    }
                }
            }

            if (e.Action == "Change")
            {
                for (int x = 0; x < DataModel.CaptainsCollection.Count; x++)
                {
                    if (DataModel.CaptainsCollection[x].CaptainOfId == e.BoatId)
                    {
                        if (DataModel.CaptainsCollection[x].Id != e.Id)
                        {
                            DataModel.CaptainsCollection[x].CaptainOfId = -1;
                            DataModel.CaptainsCollection[x].CaptainOf = "";
                        }
                        else
                        {
                            DataModel.CaptainsCollection[x].CaptainOfId = e.BoatId;
                            DataModel.CaptainsCollection[x].CaptainOf = e.BoatName;
                        }
                    }
                }
            }
        }

        #endregion
        #region CustomDelegateCommand : CrewBoatUIEventHandler

        public CustomDelegateCommand CrewBoatUIEventHandler { get; set; }
        private void ExecuteCrewBoatUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is CrewBoatUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Cancel":
                    CrewBoatVisibility = Visibility.Collapsed;
                    break;

                case "Save":
                    CrewBoatVisibility = Visibility.Collapsed;

                    for (int x = 0; x < DataModel.BoatsCollection.Count; x++)
                    {
                        if (e.Model.Id == DataModel.BoatsCollection[x].Id)
                        {
                            DataModel.BoatsCollection[x].CrewNeeded = e.Model.CrewNeeded;
                            DataModel.BoatsCollection[x].RowersNeeded = e.Model.RowersNeeded;
                            DataModel.BoatsCollection[x].CrewedSailors = e.Model.CrewedSailors;
                            DataModel.BoatsCollection[x].CrewedMariners = e.Model.CrewedMariners;
                            DataModel.BoatsCollection[x].CrewedRowers = e.Model.CrewedRowers;
                            DataModel.BoatsCollection[x].CrewedSeamens = e.Model.CrewedSeamens;
                            DataModel.BoatsCollection[x].AmountOfficers = e.Model.AmountOfficers;
                            DataModel.BoatsCollection[x].AmountNavigators = e.Model.AmountNavigators;
                            DataModel.BoatsCollection[x].AmountGuards = e.Model.AmountGuards;
                            DataModel.BoatsCollection[x].Seaworthiness = e.Model.Seaworthiness;
                            DataModel.BoatsCollection[x].Defense = e.Model.Defense;
                            DataModel.BoatsCollection[x].CostSilver = e.Model.CostSilver;
                        }
                    }
                    break;
            }
        }

        #endregion
        #region CustomDelegateCommand : BoatUIEventHandler

        public CustomDelegateCommand BoatUIEventHandler { get; set; }
        private void ExecuteBoatUIEventHandler(object obj)

        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is BoatUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Crew":
                {
                    int id = -1;

                    for (int x = 0; x < DataModel.BoatsCollection.Count; x++)
                    {
                        if (DataModel.BoatsCollection[x].Id == e.Id)
                        {
                            id = x;
                            break;
                        }
                    }

                    if (id != -1)
                    {
                        DataModel.CrewBoat = DataModel.BoatsCollection[id];
                        DataModel.CrewBoat.AmountSailors = DataModel.Sailors;
                        DataModel.CrewBoat.AmountSeamens = DataModel.Seaman;
                        DataModel.CrewBoat.AmountRowers = DataModel.Rowers;
                        DataModel.CrewBoat.AmountMariners = DataModel.Mariner;

                        CrewBoatVisibility = Visibility.Visible;
                    }
                    break;
                }

                case "Changed":
                {
                    break;
                }
            }
        }

        #endregion
        #region CustomDelegateCommand : ConstructShipyardEventHandler

        public CustomDelegateCommand ConstructShipyardEventHandler { get; set; }
        private void ExecuteConstructShipyardEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is ConstructShipyardEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (_supplyService.Withdraw(0, 25, 0, 0, 0, 30))
            {
                DataModel.BuildingShipyard = true;
                DataModel.CanBuildShipyard = false;
                DataModel.Shipyard.Id = _baseService.GetNewIndustryId();
                DataModel.Shipyard.DaysWorkNeeded = 2500;
                SaveData();
            }
            else
            {
                MessageBox.Show("Du har inte råd!");
                DataModel.CanBuildShipyard = true;
                DataModel.BuildingShipyard = false;
            }
        }

        #endregion
        #region CustomDelegateCommand : GotShipyardUIEventHandler

        public CustomDelegateCommand GotShipyardUIEventHandler { get; set; }
        private void ExecuteGotShipyardUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is GotShipyardUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Changed":
                {
                    SaveData();
                    _baseService.ChangeSteward(e.Model.StewardId, e.Model.Id, "Port");
                    break;
                }

                case "Upgrading":
                {
                    if (_supplyService.Withdraw(0, DataModel.Shipyard.BuildCostBase, 0, DataModel.Shipyard.BuildCostIron, DataModel.Shipyard.BuildCostStone, DataModel.Shipyard.BuildCostWood))
                    {
                        DataModel.UpgradingShipyard = true;
                        DataModel.Shipyard.Upgrading = true;
                    }
                    else
                    {
                        MessageBox.Show("Du har inte råd att upgradera hamnen!");
                        DataModel.Shipyard.IsBeingUpgraded = false;
                    }
                    break;
                }
            }
        }

        #endregion
        #region CustomDelegateCommand : BuildingShipyardUIEventHandler

        public CustomDelegateCommand BuildingShipyardUIEventHandler { get; set; }
        private void ExecuteBuildingShipyardUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is BuildingShipyardUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Changed":
                {
                    SaveData();
                    _baseService.ChangeSteward(e.StewardId, DataModel.Shipyard.Id, "Port");
                    break;
                }

                case "DaysWorkChanged":
                {
                    DataModel.Shipyard.DaysWorkThisYear = e.DaysWorkThisYear;
                    break;
                }
            }
            SaveData();
        }

        #endregion

        #region DataModel

        private PortDataModel _dataModel = new PortDataModel();
        public PortDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        #region UI Properties

        private Visibility _crewBoatVisibility = Visibility.Collapsed;
        public Visibility CrewBoatVisibility
        {
            get => _crewBoatVisibility;
            set => SetProperty(ref _crewBoatVisibility, value);
        }

        #endregion

        #region Methods

        protected override void SaveData(int index = -1)
        {
            if (Index != 0)
            {
                _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
            }
        }

        private void CompleteLoadData()
        {
            DataModel = Index
                        == 0 ? _portService.GetAllPortDataModel()
                : _baseService.GetDataModel<PortDataModel>(Index);

            if (!DataModel.BuildingShipyard || !DataModel.GotShipyard)
            {
                DataModel.CanBuildShipyard = _portService.CheckShipyardPossibility(Index);
                DataModel.CheckShipyardStates();
            }

            GetInformationSetDataModel();

            UpdateFiefCollection();
        }

        protected override void LoadData()
        {
            CompleteLoadData();
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            CompleteLoadData();
        }

        private void GetInformationSetDataModel(int index = -1)
        {
            if (index == -1)
            {
                GetStewardsCollection();
                SetShipyardsStewardsCollection();
                SetAvailableGuards(Index);
            }
            else
            {
                GetStewardsCollection();
                SetShipyardsStewardsCollection();
                SetAvailableGuards(index);
            }
        }

        private void GetStewardsCollection()
        {
            DataModel.StewardsCollection = _baseService.GetStewardsCollection();
        }

        private void SetShipyardsStewardsCollection()
        {
            DataModel.Shipyard.StewardsCollection = DataModel.StewardsCollection;
        }

        private void SetAvailableGuards(int index)
        {
            DataModel.Shipyard.AvailableGuards = _portService.GetAvailableGuards(index);
        }

        #endregion

        private void ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent()
        {
            SaveData();
        }
    }
}
