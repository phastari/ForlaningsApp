using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Buildings.RoutedEvents;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace FiefApp.Module.Buildings
{
    public class BuildingsViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IBuildingsService _buildingsService;
        private readonly IEventAggregator _eventAggregator;
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

        public BuildingsViewModel(
            IBaseService baseService,
            IBuildingsService buildingsService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            _baseService = baseService;
            _buildingsService = buildingsService;
            _eventAggregator = eventAggregator;

            TabName = "Byggnadsverk";

            AddBuildingUIEventHandler = new CustomDelegateCommand(ExecuteAddBuildingUIEventHandler, o => true);
            BuilderUIEventHandler = new CustomDelegateCommand(ExecuteBuilderUIEventHandler, o => true);
            BuildingBuildingUIEventHandler = new CustomDelegateCommand(ExecuteBuildingBuildingUIEventHandler, o => true);
            AddBuilderCommand = new DelegateCommand(ExecuteAddBuilderCommand);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
            _eventAggregator.GetEvent<SaveDataModelBeforeSaveFileIsCreatedEvent>().Subscribe(ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent);
            _eventAggregator.GetEvent<UpdateAllEvent>().Subscribe(UpdateAndRespond);
            _eventAggregator.GetEvent<UpdateEvent>().Subscribe(UpdateResponse);
            _eventAggregator.GetEvent<UpdateResponseEvent>().Subscribe(HandleUpdateEvent);
        }

        private void HandleUpdateEvent(UpdateEventParameters param)
        {
            if (param.Publisher == "Buildings"
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
            if (str != "Buildings")
            {
                UpdateFiefCollection();
                for (int x = 1; x < FiefCollection.Count; x++)
                {
                    DataModel = _baseService.GetDataModel<BuildingsDataModel>(x);
                    CheckBuildersCollection();
                    SaveData(x);
                }

                _eventAggregator.GetEvent<UpdateResponseEvent>().Publish(new UpdateEventParameters()
                {
                    ModuleName = "Buildings",
                    Completed = true,
                    Publisher = str
                });
            }
        }

        private void CompleteLoadData()
        {
            _buildingsService.SetAllBuildsCollectionIsAll(Index);
            DataModel = Index
                        == 0 ? _buildingsService.GetAllBuildingsDataModel()
                : _baseService.GetDataModel<BuildingsDataModel>(Index);

            CheckBuildersCollection();

            if (Index == 0)
            {
                DataModel.IsAll = true;
                SetBuildersInBuildsCollection();
            }
            else
            {
                DataModel.IsAll = false;
                GetInformationSetDataModel();
            }
            DataModel.BuildersCollection.CollectionChanged += UpdateBuildersCollectionInBuildingsCollection;

            UpdateFiefCollection();
        }

        private void UpdateAndRespond()
        {
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<BuildingsDataModel>(x);
                CheckBuildersCollection();
                SaveData(x);
            }

            _eventAggregator.GetEvent<UpdateAllResponseEvent>().Publish(new UpdateAllEventParameters()
            {
                ModuleName = "Buildings",
                Completed = true
            });
        }

        #region CustomDelegateCommand : AddBuildingUIEvent

        public CustomDelegateCommand AddBuildingUIEventHandler { get; set; }
        private void ExecuteAddBuildingUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is AddBuildingUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Save")
            {
                List<int> tempList = DataModel.BuildsCollection.Select(t => t.Id).ToList();

                e.Model.Id = tempList.Count > 0 ? tempList.Max() + 1 : 0;
                e.Model.BuildersCollection = DataModel.BuildersCollection;

                DataModel.BuildsCollection.Add(e.Model);
                SaveData();
            }
        }

        #endregion
        #region CustomDelegateCommand : BuilderUIEvent

        public CustomDelegateCommand BuilderUIEventHandler { get; set; }
        private void ExecuteBuilderUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is BuilderUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Delete":
                    {
                        for (int x = 0; x < DataModel.BuildersCollection.Count; x++)
                        {
                            if (DataModel.BuildersCollection[x].Id == e.Model.Id)
                            {
                                DataModel.BuildersCollection.RemoveAt(x);
                                break;
                            }
                        }
                        List<BuilderModel> tempList = new List<BuilderModel>(DataModel.BuildersCollection);
                        DataModel.BuildersCollection.Clear();
                        DataModel.BuildersCollection = new ObservableCollection<BuilderModel>(tempList);
                        break;
                    }

                case "Save":
                    for (int x = 0; x < DataModel.BuildersCollection.Count; x++)
                    {
                        if (DataModel.BuildersCollection[x].Id == e.Model.Id)
                        {
                            DataModel.BuildersCollection[x].PersonName = e.Model.PersonName;
                            DataModel.BuildersCollection[x].Age = e.Model.Age;
                            DataModel.BuildersCollection[x].Skill = e.Model.Skill;
                            DataModel.BuildersCollection[x].Resources = e.Model.Resources;
                            DataModel.BuildersCollection[x].Loyalty = e.Model.Loyalty;
                            break;
                        }
                    }
                    break;
            }
        }

        #endregion
        #region CustomDelegateCommand : BuildingBuildingUIEventHandler

        public CustomDelegateCommand BuildingBuildingUIEventHandler { get; set; }
        private void ExecuteBuildingBuildingUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is BuildingBuildingUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Expanded":
                    for (int x = 0; x < DataModel.BuildsCollection.Count; x++)
                    {
                        if (e.Id != DataModel.BuildsCollection[x].Id)
                        {
                            DataModel.BuildsCollection[x].IsExpanded = false;
                        }
                    }
                    break;

                case "Updated":
                    for (int x = 0; x < DataModel.BuildsCollection.Count; x++)
                    {
                        if (e.Id == DataModel.BuildsCollection[x].Id)
                        {
                            DataModel.BuildsCollection[x].SmithsworkThisYear = e.Model.SmithsworkThisYear;
                            DataModel.BuildsCollection[x].IronThisYear = e.Model.IronThisYear;
                            DataModel.BuildsCollection[x].WoodworkThisYear = e.Model.WoodworkThisYear;
                            DataModel.BuildsCollection[x].WoodThisYear = e.Model.WoodThisYear;
                            DataModel.BuildsCollection[x].StoneworkThisYear = e.Model.StoneworkThisYear;
                            DataModel.BuildsCollection[x].StoneThisYear = e.Model.StoneThisYear;
                            DataModel.BuildsCollection[x].BuilderId = e.Model.BuilderId;
                            DataModel.BuildsCollection[x].BuildingTime = e.Model.BuildingTime;
                        }
                        else
                        {
                            if (e.Model.BuilderId == DataModel.BuildsCollection[x].BuilderId)
                            {
                                DataModel.BuildsCollection[x].BuilderId = -1;
                            }
                        }
                    }

                    for (int x = 0; x < DataModel.BuildersCollection.Count; x++)
                    {
                        if (DataModel.BuildersCollection[x].Id == e.Model.BuilderId)
                        {
                            DataModel.BuildersCollection[x].BuildingId = e.Id;
                        }
                        else
                        {
                            if (DataModel.BuildersCollection[x].BuildingId == e.Id)
                            {
                                DataModel.BuildersCollection[x].BuildingId = -1;
                            }
                        }
                    }

                    for (int x = 0; x < DataModel.BuildsCollection.Count; x++)
                    {
                        DataModel.BuildsCollection[x].BuildersCollection = DataModel.BuildersCollection;
                    }

                    List<BuildingModel> tempList = new List<BuildingModel>(DataModel.BuildsCollection);
                    DataModel.BuildsCollection.Clear();
                    DataModel.BuildsCollection = new ObservableCollection<BuildingModel>(tempList);

                    DataModel.SetDaysWorkLeft();
                    break;

                case "Changed":
                    for (int x = 0; x < DataModel.BuildsCollection.Count; x++)
                    {
                        if (e.Id == DataModel.BuildsCollection[x].Id)
                        {
                            DataModel.BuildsCollection[x].SmithsworkThisYear = e.Model.SmithsworkThisYear;
                            DataModel.BuildsCollection[x].IronThisYear = e.Model.IronThisYear;
                            DataModel.BuildsCollection[x].WoodworkThisYear = e.Model.WoodworkThisYear;
                            DataModel.BuildsCollection[x].WoodThisYear = e.Model.WoodThisYear;
                            DataModel.BuildsCollection[x].StoneworkThisYear = e.Model.StoneworkThisYear;
                            DataModel.BuildsCollection[x].StoneThisYear = e.Model.StoneThisYear;
                            DataModel.BuildsCollection[x].BuilderId = e.Model.BuilderId;
                            DataModel.BuildsCollection[x].BuildingTime = e.Model.BuildingTime;
                        }
                    }
                    DataModel.SetDaysWorkLeft();
                    break;
            }
            SaveData();
        }

        #endregion

        #region DelegateCommand : AddBuilderCommand

        public DelegateCommand AddBuilderCommand { get; set; }
        private void ExecuteAddBuilderCommand()
        {
            int id = _buildingsService.GetNewIdForBuilder();
            DataModel.BuildersCollection.Add(new BuilderModel()
            {
                Id = id,
                PersonName = _baseService.GetCommonerName(),
                Age = _baseService.RollDie(14, 61)
            });
            SaveData();
        }

        #endregion

        #region View Data Model Properties

        private BuildingsDataModel _dataModel;
        public BuildingsDataModel DataModel
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
            //if (_triggerLoad)
            //{
            //    _triggerLoad = false;
            //    for (int x = 0; x < _awaitResponsList.Count; x++)
            //    {
            //        if (_awaitResponsList[x].ModuleName == "Buildings")
            //        {
            //            _awaitResponsList[x].Completed = true;
            //        }
            //        else
            //        {
            //            _awaitResponsList[x].Completed = false;
            //        }
            //    }
            //    _eventAggregator.GetEvent<UpdateEvent>().Publish("Buildings");
            //}
            CompleteLoadData();
        }

        private void UpdateBuildersCollectionInBuildingsCollection(
            object sender,
            NotifyCollectionChangedEventArgs e)
        {
            if (DataModel.BuildingsCollection.Count != 0)
            {
                for (int x = 0; x < DataModel.BuildingsCollection.Count; x++)
                {
                    DataModel.BuildingsCollection[x].BuildersCollection = DataModel.BuildersCollection;
                }
            }
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            CompleteLoadData();
        }

        private void GetInformationSetDataModel()
        {
            DataModel.AvailableBuildings = new ObservableCollection<BuildingModel>(_buildingsService.GetAvailableBuildings());
            SetBuildersInBuildsCollection();
        }

        public void SetBuildersInBuildsCollection()
        {
            if (DataModel.BuildsCollection.Count != 0)
            {
                for (int x = 0; x < DataModel.BuildsCollection.Count; x++)
                {
                    DataModel.BuildsCollection[x].BuildersCollection = DataModel.BuildersCollection;
                }
            }
        }

        private void CheckBuildersCollection()
        {
            if (DataModel.BuildersCollection.Count > 1)
            {
                for (int x = DataModel.BuildersCollection.Count - 1; x > 0; x--)
                {
                    if (string.IsNullOrEmpty(DataModel.BuildersCollection[x].PersonName))
                    {
                        DataModel.BuildersCollection.RemoveAt(x);
                    }
                }
            }
        }

        private void ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent()
        {
            SaveData();
        }
    }
}
