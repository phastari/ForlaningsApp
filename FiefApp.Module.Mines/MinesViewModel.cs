using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Mines.RoutedEvents;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FiefApp.Module.Mines
{
    public class MinesViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IMinesService _minesService;
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
        private bool _triggerLoad = true;
        private bool _ignoreNextIncomeUpdate = false;

        public MinesViewModel(
            IBaseService baseService,
            IMinesService minesService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            TabName = "Stenbrott/Gruvor";

            _baseService = baseService;
            _minesService = minesService;
            _eventAggregator = eventAggregator;

            AddQuarryCommand = new DelegateCommand(ExecuteAddQuarryCommand);
            ConstructQuarryUIEventHandler = new CustomDelegateCommand(ExecuteConstructQuarryUIEventHandler, o => true);
            AddMineUIEventHandler = new CustomDelegateCommand(ExecuteAddMineUIEventHandler, o => true);
            MineUIEventHandler = new CustomDelegateCommand(ExecuteMineUIEventHandler, o => true);
            QuarryUIEventHandler = new CustomDelegateCommand(ExecuteQuarryUIEventHandler, o => true);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
            _eventAggregator.GetEvent<SaveDataModelBeforeSaveFileIsCreatedEvent>().Subscribe(ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent);
            _eventAggregator.GetEvent<UpdateAllEvent>().Subscribe(UpdateAndRespond);
            _eventAggregator.GetEvent<UpdateEvent>().Subscribe(UpdateResponse);
            _eventAggregator.GetEvent<UpdateResponseEvent>().Subscribe(HandleUpdateEvent);
        }

        private void HandleUpdateEvent(UpdateEventParameters param)
        {
            if (param.Publisher == "Mines"
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
            if (str != "Mines")
            {
                UpdateFiefCollection();
                for (int x = 1; x < FiefCollection.Count; x++)
                {
                    DataModel = _baseService.GetDataModel<MinesDataModel>(x);
                    GetInformationSetDataModel();
                    DataModel.UpdateTotals();
                    SaveData(x);
                }

                _eventAggregator.GetEvent<UpdateResponseEvent>().Publish(new UpdateEventParameters()
                {
                    ModuleName = "Mines",
                    Completed = true,
                    Publisher = str
                });
            }
        }

        private void CompleteLoadData()
        {
            DataModel = Index
                        == 0 ? _minesService.GetAllMinesDataModel()
                : _baseService.GetDataModel<MinesDataModel>(Index);

            GetInformationSetDataModel();
            DataModel.UpdateTotals();

            UpdateFiefCollection();
            _triggerLoad = true;
        }

        private void UpdateAndRespond()
        {
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<MinesDataModel>(x);
                GetInformationSetDataModel();
                DataModel.UpdateTotals();
                SaveData(x);
            }

            _eventAggregator.GetEvent<UpdateAllResponseEvent>().Publish(new UpdateAllEventParameters()
            {
                ModuleName = "Mines",
                Completed = true
            });
        }

        #region DelegateCommand : AddQuarryCommand

        public DelegateCommand AddQuarryCommand { get; set; }
        private void ExecuteAddQuarryCommand()
        {
            DataModel.UpdateTotals();
        }

        #endregion

        #region CustomDelegateCommand : ConstructQuarryUIEventHandler

        public CustomDelegateCommand ConstructQuarryUIEventHandler { get; set; }
        private void ExecuteConstructQuarryUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is ConstructQuarryUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Save")
            {
                e.Model.Id = _baseService.GetNewIndustryId();
                e.Model.IsFirstYear = true;

                DataModel.QuarriesCollection.Add(e.Model);
                DataModel.UpdateTotals();

                GetInformationSetDataModel();
                SaveData();
            }
        }

        #endregion

        #region CustomDelegateCommand : AddMineUIEventHandler

        public CustomDelegateCommand AddMineUIEventHandler { get; set; }
        private void ExecuteAddMineUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is AddMineUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Save")
            {
                e.Model.Id = _baseService.GetNewIndustryId();
                e.Model.StewardId = -1;
                e.Model.Steward = "";
                e.Model.StewardsCollection = DataModel.StewardsCollection;
                e.Model.BaseIncome = e.Model.Income;
                e.Model.Income = 0;
                DataModel.MinesCollection.Add(e.Model);
                DataModel.UpdateTotals();

                GetInformationSetDataModel();
                SaveData();
            }
        }

        #endregion

        #region CustomDelegateCommand : MineUIEventHandler

        public CustomDelegateCommand MineUIEventHandler { get; set; }
        private void ExecuteMineUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is MineUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Changed":
                {
                    SaveData();
                    _baseService.ChangeSteward(e.StewardId, e.MineId, "Mine");
                    List<MineModel> tempList = new List<MineModel>(DataModel.MinesCollection);
                    List<QuarryModel> quarryList = new List<QuarryModel>(DataModel.QuarriesCollection);
                    DataModel.MinesCollection.Clear();
                    DataModel.QuarriesCollection.Clear();
                    DataModel.MinesCollection = new ObservableCollection<MineModel>(tempList);
                    DataModel.QuarriesCollection = new ObservableCollection<QuarryModel>(quarryList);
                    UpdateStewardsCollectionInMines();
                    UpdateStewardsCollectionInQuarries();
                    DataModel.UpdateTotals();
                    break;
                }

                case "Guards":
                {
                    if (_minesService.SetUsedGuards(Index, e.Guards))
                    {
                        for (int x = 0; x < DataModel.MinesCollection.Count; x++)
                        {
                            if (e.MineId == DataModel.MinesCollection[x].Id)
                            {
                                DataModel.MinesCollection[x].Guards += e.Guards;
                                break;
                            }
                        }

                        SaveData();
                        DataModel.UpdateTotals();
                        UpdateAvailableGuardsInMines();
                    }
                    else
                    {
                        List<MineModel> tempList = new List<MineModel>(DataModel.MinesCollection);
                        DataModel.MinesCollection.Clear();
                        DataModel.MinesCollection = new ObservableCollection<MineModel>(tempList);
                        DataModel.UpdateTotals();
                        _ignoreNextIncomeUpdate = true;
                    }
                    break;
                }

                case "Delete":
                {
                    for (int x = 0; x < DataModel.MinesCollection.Count; x++)
                    {
                        if (DataModel.MinesCollection[x].Id == e.MineId)
                        {
                            DataModel.MinesCollection.RemoveAt(x);
                            DataModel.UpdateTotals();
                            break;
                        }
                    }
                    break;
                }

                case "IncomeUpdated":
                {
                    if (!_ignoreNextIncomeUpdate)
                    {
                        for (int x = 0; x < DataModel.MinesCollection.Count; x++)
                        {
                            if (DataModel.MinesCollection[x].Id == e.MineId)
                            {
                                DataModel.MinesCollection[x].Income = e.Income;
                                DataModel.UpdateTotals();
                                SaveData();
                                break;
                            }
                        }
                    }
                    else
                    {
                        _ignoreNextIncomeUpdate = false;
                    }
                    
                    break;
                }
            }
        }

        #endregion

        #region CustomDelegateCommand : QuarryUIEventHandler

        public CustomDelegateCommand QuarryUIEventHandler { get; set; }
        private void ExecuteQuarryUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is QuarryUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Delete":
                {
                    for (int x = 0; x < DataModel.QuarriesCollection.Count; x++)
                    {
                        if (DataModel.QuarriesCollection[x].Id == e.QuarryId)
                        {
                            DataModel.QuarriesCollection.RemoveAt(x);
                            DataModel.UpdateTotals();
                            break;
                        }
                    }
                    break;
                }

                case "DaysWork":
                {
                    for (int x = 0; x < DataModel.QuarriesCollection.Count; x++)
                    {
                        if (DataModel.QuarriesCollection[x].Id == e.QuarryId)
                        {
                            DataModel.QuarriesCollection[x].DaysWorkThisYear = e.DaysWork;
                            DataModel.UpdateTotals();
                            break;
                        }
                    }
                    break;
                }

                case "Changed":
                {
                    SaveData();
                    _baseService.ChangeSteward(e.StewardId, e.QuarryId, "Quarry");
                    List<MineModel> tempList = new List<MineModel>(DataModel.MinesCollection);
                    List<QuarryModel> quarryList = new List<QuarryModel>(DataModel.QuarriesCollection);
                    DataModel.MinesCollection.Clear();
                    DataModel.QuarriesCollection.Clear();
                    DataModel.MinesCollection = new ObservableCollection<MineModel>(tempList);
                    DataModel.QuarriesCollection = new ObservableCollection<QuarryModel>(quarryList);
                    DataModel.UpdateTotals();
                    break;
                }

                case "IncomeUpdated":
                {
                    for (int x = 0; x < DataModel.QuarriesCollection.Count; x++)
                    {
                        if (DataModel.QuarriesCollection[x].Id == e.QuarryId)
                        {
                            DataModel.QuarriesCollection[x].ApproximateIncome = e.Income;
                            DataModel.UpdateTotals();
                            break;
                        }
                    }
                    break;
                }
            }
        }

        #endregion

        #region DataModel

        private MinesDataModel _dataModel = new MinesDataModel();
        public MinesDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
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

        protected override void LoadData()
        {
            //if (_triggerLoad)
            //{
            //    _triggerLoad = false;
            //    for (int x = 0; x < _awaitResponsList.Count; x++)
            //    {
            //        if (_awaitResponsList[x].ModuleName == "Mines")
            //        {
            //            _awaitResponsList[x].Completed = true;
            //        }
            //        else
            //        {
            //            _awaitResponsList[x].Completed = false;
            //        }
            //    }
            //    _eventAggregator.GetEvent<UpdateEvent>().Publish("Mines");
            //}
            CompleteLoadData();
        }

        private void GetInformationSetDataModel(int index = -1)
        {
            if (index == -1)
            {
                UpdateDifficultyInMines();
                UpdateStewardsCollectionInMines();
                UpdateAvailableGuardsInMines();
                UpdateDifficultyInQuarries();
                UpdateStewardsCollectionInQuarries();
            }
            else
            {
                UpdateDifficultyInMines(index);
                UpdateStewardsCollectionInMines();
                UpdateAvailableGuardsInMines();
                UpdateDifficultyInQuarries(index);
                UpdateStewardsCollectionInQuarries();
            }
        }

        private void UpdateDifficultyInMines(int index = -1)
        {
            if (index == -1)
            {
                if (DataModel.MinesCollection.Count > 0)
                {
                    for (int x = 0; x < DataModel.MinesCollection.Count; x++)
                    {
                        DataModel.MinesCollection[x].Difficulty = _minesService.GetMinesDifficulty(Index);
                    }
                }
            }
            else
            {
                if (DataModel.MinesCollection.Count > 0)
                {
                    for (int x = 0; x < DataModel.MinesCollection.Count; x++)
                    {
                        DataModel.MinesCollection[x].Difficulty = _minesService.GetMinesDifficulty(index);
                    }
                }
            }
        }

        private void UpdateDifficultyInQuarries(int index = -1)
        {
            if (index == -1)
            {
                if (DataModel.QuarriesCollection.Count > 0)
                {
                    for (int x = 0; x < DataModel.QuarriesCollection.Count; x++)
                    {
                        DataModel.QuarriesCollection[x].Difficulty = _minesService.GetQuarriesDifficulty(Index);
                    }
                }
            }
            else
            {
                if (DataModel.QuarriesCollection.Count > 0)
                {
                    for (int x = 0; x < DataModel.QuarriesCollection.Count; x++)
                    {
                        DataModel.QuarriesCollection[x].Difficulty = _minesService.GetQuarriesDifficulty(index);
                    }
                }
            }
        }

        private void UpdateStewardsCollectionInMines()
        {
            if (DataModel.MinesCollection.Count > 0)
            {
                for (int x = 0; x < DataModel.MinesCollection.Count; x++)
                {
                    DataModel.MinesCollection[x].StewardsCollection = new ObservableCollection<StewardModel>(_baseService.GetStewardsCollection());
                }
            }
        }

        private void UpdateStewardsCollectionInQuarries()
        {
            if (DataModel.QuarriesCollection.Count > 0)
            {
                for (int x = 0; x < DataModel.QuarriesCollection.Count; x++)
                {
                    DataModel.QuarriesCollection[x].StewardsCollection = new ObservableCollection<StewardModel>(_baseService.GetStewardsCollection());
                }
            }
        }

        private void UpdateAvailableGuardsInMines()
        {
            int guards = _minesService.GetAvailableGuards(Index);

            if (DataModel.MinesCollection.Count > 0)
            {
                for (int x = 0; x < DataModel.MinesCollection.Count; x++)
                {
                    DataModel.MinesCollection[x].AvailableGuards = guards;
                }
            }
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            _triggerLoad = false;
            Index = 1;
            CompleteLoadData();
        }

        private void ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent()
        {
            SaveData();
        }

        #endregion
    }
}
