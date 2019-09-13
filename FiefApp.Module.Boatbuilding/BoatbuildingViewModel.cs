using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Boatbuilding.RoutedEvents;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace FiefApp.Module.Boatbuilding
{
    public class BoatbuildingViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IBoatbuildingService _boatbuildingService;
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

        public BoatbuildingViewModel(
            IBaseService baseService,
            IBoatbuildingService boatbuildingService,
            ISettingsService settingsService,
            IEventAggregator eventAggregator,
            ISupplyService supplyService
            ) : base(baseService)
        {
            _baseService = baseService;
            _boatbuildingService = boatbuildingService;
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;
            _supplyService = supplyService;

            TabName = "Skeppsbygge";

            BuildingBoatUIEventHandler = new CustomDelegateCommand(ExecuteBuildingBoatUIEventHandler, o => true);
            BoatBuilderUIEventHandler = new CustomDelegateCommand(ExecuteBoatBuilderUIEventHandler, o => true);
            ConstructingBoatUIEventHandler = new CustomDelegateCommand(ExecuteConstructingBoatUIEventHandler, o => true);

            AddBoatbuilderCommand = new DelegateCommand(ExecuteAddBoatbuilderCommand);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
            _eventAggregator.GetEvent<SaveDataModelBeforeSaveFileIsCreatedEvent>().Subscribe(ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent);
            _eventAggregator.GetEvent<UpdateAllEvent>().Subscribe(UpdateAndRespond);
            _eventAggregator.GetEvent<UpdateEvent>().Subscribe(UpdateResponse);
            _eventAggregator.GetEvent<UpdateResponseEvent>().Subscribe(HandleUpdateEvent);
        }

        #region EventHandlers

        private void HandleUpdateEvent(UpdateEventParameters param)
        {
            if (param.Publisher == "Boatbuilding"
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
            if (str != "Boatbuilding")
            {
                UpdateFiefCollection();
                for (int x = 1; x < FiefCollection.Count; x++)
                {
                    DataModel = _baseService.GetDataModel<BoatbuildingDataModel>(x);
                    SetDataModelInformation(x);
                    SaveData(x);
                }

                _eventAggregator.GetEvent<UpdateResponseEvent>().Publish(new UpdateEventParameters()
                {
                    ModuleName = "Boatbuilding",
                    Completed = true,
                    Publisher = str
                });
            }
        }

        private void CompleteLoadData()
        {
            DataModel = Index
                        == 0 ? _boatbuildingService.GetAllBoatbuildingDataModel()
                : _baseService.GetDataModel<BoatbuildingDataModel>(Index);

            DataModel.BoatTypeCollection = new ObservableCollection<BoatModel>(_settingsService.BoatbuildingSettingsModel.BoatSettingsList);
            if (Index != 0)
            {
                SetDataModelInformation();
            }

            DataModel.ShowButtons = Index != 0;
            UpdateFiefCollection();
        }

        private void UpdateAndRespond()
        {
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<BoatbuildingDataModel>(x);
                SetDataModelInformation(x);
                SaveData(x);
            }

            _eventAggregator.GetEvent<UpdateAllResponseEvent>().Publish(new UpdateAllEventParameters()
            {
                ModuleName = "Boatbuilding",
                Completed = true
            });
        }

        #endregion

        #region CustomDelegateCommand : BuildingBoatUIEventHandler

        public CustomDelegateCommand BuildingBoatUIEventHandler { get; set; }
        private void ExecuteBuildingBoatUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is BuildingBoatUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Save")
            {
                if (e.BoatModel.AddAsBuilt)
                {
                    if (e.BoatModel.BoatType == "Fiskebåt")
                    {
                        _boatbuildingService.AddFishingBoat(Index, e.BoatModel.Amount);
                        _eventAggregator.GetEvent<FishingBoatsAdded>().Publish(Index);
                    }
                }
                else
                {
                    if (CheckBoatCost(e.BoatModel))
                    {
                        if (CheckBuildInVillageDocks(e.BoatModel.Length))
                        {
                            e.BoatModel.Id = _boatbuildingService.GetNewBuildingBoatId(Index);
                            e.BoatModel.BoatBuildersCollection = DataModel.BoatBuildersCollection;
                            e.BoatModel.BuildingInVillageDock = true;
                            DataModel.BoatsBuildingCollection.Add(e.BoatModel);
                            DataModel.DocksVillageFree--;
                            SaveData();
                        }
                        else
                        {
                            if (CheckBuildInSmallDocks(e.BoatModel.Length))
                            {
                                e.BoatModel.Id = _boatbuildingService.GetNewBuildingBoatId(Index);
                                e.BoatModel.BoatBuildersCollection = DataModel.BoatBuildersCollection;
                                e.BoatModel.BuildingInSmallDock = true;
                                DataModel.BoatsBuildingCollection.Add(e.BoatModel);
                                DataModel.DocksSmallFree--;
                                SaveData();
                            }
                            else
                            {
                                if (CheckBuildInMediumDocks(e.BoatModel.Length))
                                {
                                    e.BoatModel.Id = _boatbuildingService.GetNewBuildingBoatId(Index);
                                    e.BoatModel.BoatBuildersCollection = DataModel.BoatBuildersCollection;
                                    e.BoatModel.BuildingInMediumDock = true;
                                    DataModel.BoatsBuildingCollection.Add(e.BoatModel);
                                    DataModel.DocksMediumFree--;
                                    SaveData();
                                }
                                else
                                {
                                    if (CheckBuildInLargeDocks(e.BoatModel.Length))
                                    {
                                        e.BoatModel.Id = _boatbuildingService.GetNewBuildingBoatId(Index);
                                        e.BoatModel.BoatBuildersCollection = DataModel.BoatBuildersCollection;
                                        e.BoatModel.BuildingInLargeDock = true;
                                        DataModel.BoatsBuildingCollection.Add(e.BoatModel);
                                        DataModel.DocksLargeFree--;
                                        SaveData();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Det finns inte någon plats för båtbygget.");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Du har inte råd!");
                    }
                }
                SaveData();
            }
        }

        #endregion
        #region CustomDelegateCommand : BoatBuilderUIEventHandler

        public CustomDelegateCommand BoatBuilderUIEventHandler { get; set; }
        private void ExecuteBoatBuilderUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is BoatBuilderUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Delete")
            {
                if (Index == 0)
                {
                    if (_boatbuildingService.RemoveBoatbuilder(e.BoatbuilderModel.Id))
                    {
                        LoadData();
                    }
                }
                else
                {
                    for (int x = 0; x < DataModel.BoatBuildersCollection.Count; x++)
                    {
                        if (e.BoatbuilderModel.Id == DataModel.BoatBuildersCollection[x].Id)
                        {
                            DataModel.BoatBuildersCollection.RemoveAt(x);
                            break;
                        }
                    }
                }
            }

            if (e.Action == "Save")
            {
                if (Index == 0)
                {
                    if (_boatbuildingService.SaveBoatbuilder(e.BoatbuilderModel))
                    {
                        LoadData();
                    }
                }
                else
                {

                    for (int x = 0; x < DataModel.BoatBuildersCollection.Count; x++)
                    {
                        if (e.BoatbuilderModel.Id == DataModel.BoatBuildersCollection[x].Id)
                        {
                            DataModel.BoatBuildersCollection[x].PersonName = e.BoatbuilderModel.PersonName;
                            DataModel.BoatBuildersCollection[x].Loyalty = e.BoatbuilderModel.Loyalty;
                            DataModel.BoatBuildersCollection[x].Skill = e.BoatbuilderModel.Skill;
                            DataModel.BoatBuildersCollection[x].Resources = e.BoatbuilderModel.Resources;
                            DataModel.BoatBuildersCollection[x].Age = e.BoatbuilderModel.Age;
                        }
                    }

                    for (int x = 0; x < DataModel.BoatsBuildingCollection.Count; x++)
                    {
                        DataModel.BoatsBuildingCollection[x].BoatBuildersCollection = DataModel.BoatBuildersCollection;
                    }

                    SaveData();
                }
            }
        }

        #endregion
        #region CustomDelegateCommand : ConstructingBoatUIEventHandler

        public CustomDelegateCommand ConstructingBoatUIEventHandler { get; set; }
        private void ExecuteConstructingBoatUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is ConstructingBoatUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Update")
            {
                if (Index == 0)
                {
                    _boatbuildingService.ChangeBoatbuilder(e.ConstructingBoatId, e.BoatbuilderId);
                    LoadData();
                }
                else
                {
                    int index = -1;

                    for (int x = 0; x < DataModel.BoatsBuildingCollection.Count; x++)
                    {
                        if (DataModel.BoatsBuildingCollection[x].Id == e.ConstructingBoatId)
                        {
                            DataModel.BoatsBuildingCollection[x].BoatbuilderId = e.BoatbuilderId;
                            index = x;
                        }
                        else if (DataModel.BoatsBuildingCollection[x].BoatbuilderId == e.BoatbuilderId)
                        {
                            DataModel.BoatsBuildingCollection[x].BoatbuilderId = -1;
                            DataModel.BoatsBuildingCollection[x].SelectedIndex = -1;
                        }
                    }

                    for (int x = 0; x < DataModel.BoatBuildersCollection.Count; x++)
                    {
                        if (DataModel.BoatBuildersCollection[x].Id == e.BoatbuilderId)
                        {
                            DataModel.BoatBuildersCollection[x].BuildingBoatId = e.ConstructingBoatId;
                            DataModel.BoatBuildersCollection[x].Assignment = DataModel.BoatsBuildingCollection[index].BoatType;
                        }
                        else
                        {
                            if (DataModel.BoatBuildersCollection[x].BuildingBoatId == e.ConstructingBoatId)
                            {
                                DataModel.BoatBuildersCollection[x].BuildingBoatId = -1;
                                DataModel.BoatBuildersCollection[x].Assignment = "";
                            }
                        }
                    }
                }
            }

            if (e.Action == "Delete")
            {
                if (Index == 0)
                {
                    _boatbuildingService.RemoveBoat(e.ConstructingBoatId);
                    LoadData();
                }
                else
                {
                    for (int x = 0; x < DataModel.BoatsBuildingCollection.Count; x++)
                    {
                        if (DataModel.BoatsBuildingCollection[x].Id == e.ConstructingBoatId)
                        {
                            DataModel.BoatsBuildingCollection.RemoveAt(x);
                            break;
                        }
                    }

                    for (int x = 0; x < DataModel.BoatBuildersCollection.Count; x++)
                    {
                        if (DataModel.BoatBuildersCollection[x].BuildingBoatId == e.ConstructingBoatId)
                        {
                            DataModel.BoatBuildersCollection[x].BuildingBoatId = -1;
                            DataModel.BoatBuildersCollection[x].Assignment = "";
                        }
                    }
                }
            }

            SaveData();
        }

        #endregion
        
        #region DelegateCommand : AddBoatbuilderCommand

        public DelegateCommand AddBoatbuilderCommand { get; set; }
        private void ExecuteAddBoatbuilderCommand()
        {
            SaveData();

            DataModel.BoatBuildersCollection.Add(
                new BoatbuilderModel()
                {
                    Id = _boatbuildingService.GetNewBoatbuilderId(),
                    PersonName = _baseService.GetCommonerName(),
                    Age = _baseService.RollDie(14, 61)
                });
        }

        #endregion

        #region DataModel

        private BoatbuildingDataModel _dataModel;
        public BoatbuildingDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        protected override void SaveData(int index = -1)
        {
            if (Index != 0)
            {
                _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
            }
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

        private void SetDataModelInformation(int index = -1)
        {
            if (index == -1)
            {
                DataModel.VillageBoatBuilders = _boatbuildingService.GetNrVillageBoatbuilders(Index);
                DataModel.DocksVillage = _boatbuildingService.GetNrVillageBoatbuilders(Index);
                DataModel.GotShipyard = _boatbuildingService.GetGotShipyard(Index);
                DataModel.UpgradingShipyard = _boatbuildingService.GetUpgradingShipyard(Index);
                DataModel.BoatBuildersCollection.CollectionChanged += UpdateTotalBoatBuilders;
                DataModel.VillageBoatBuilders = _boatbuildingService.GetVillageBoatBuilders(Index);
                CheckDocks(Index);

                if (DataModel.VillageBoatBuilders > 0)
                {
                    DataModel.GotVillageBoatbuilders = true;
                }
            }
            else
            {
                DataModel.VillageBoatBuilders = _boatbuildingService.GetNrVillageBoatbuilders(index);
                DataModel.DocksVillage = _boatbuildingService.GetNrVillageBoatbuilders(index);
                DataModel.GotShipyard = _boatbuildingService.GetGotShipyard(index);
                DataModel.UpgradingShipyard = _boatbuildingService.GetUpgradingShipyard(index);
                DataModel.UpdateTotalBoatBuilders();
                DataModel.VillageBoatBuilders = _boatbuildingService.GetVillageBoatBuilders(index);
                CheckDocks(index);

                if (DataModel.VillageBoatBuilders > 0)
                {
                    DataModel.GotVillageBoatbuilders = true;
                }
            }
        }

        private void UpdateTotalBoatBuilders(object sender, NotifyCollectionChangedEventArgs e)
        {
            DataModel.UpdateTotalBoatBuilders();
        }

        private void ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent()
        {
            SaveData();
        }

        private bool CheckBoatCost(BoatModel model)
        {
            if (_supplyService.Withdraw(model.CostNowSilver, model.CostNowBase, 0, 0, 0, model.CostNowWood))
            {
                return true;
            }
            return false;
        }

        private bool CheckBuildInSmallDocks(int length)
        {
            if (length != 0)
            {
                if (length < 11)
                {
                    if (DataModel.DocksSmallFree > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckBuildInVillageDocks(int length)
        {
            if (length != 0)
            {
                if (length < 11)
                {
                    if (DataModel.DocksVillageFree > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckBuildInMediumDocks(int length)
        {
            if (length != 0)
            {
                if (length < 21)
                {
                    if (DataModel.DocksMediumFree > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckBuildInLargeDocks(int length)
        {
            if (length != 0)
            {
                if (DataModel.DocksLargeFree > 0)
                {
                    return true;
                }
            }

            return false;
        }

        private void CheckDocks(int index)
        {
            DataModel.DocksSmallFree = DataModel.DocksSmall - _boatbuildingService.GetUsedSmallDocks(index);
            DataModel.DocksVillageFree = DataModel.DocksVillage - _boatbuildingService.GetUsedVillageDocks(index);
            DataModel.DocksMediumFree = DataModel.DocksMedium - _boatbuildingService.GetUsedMediumDocks(index);
            DataModel.DocksLargeFree = DataModel.DocksLarge - _boatbuildingService.GetUsedLargeDocks(index);
        }
    }
}
