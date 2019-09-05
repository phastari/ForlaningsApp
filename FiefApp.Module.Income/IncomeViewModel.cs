using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Income.RoutedEvents;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FiefApp.Module.Income
{
    public class IncomeViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IIncomeService _incomeService;
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

        public IncomeViewModel(
            IBaseService baseService,
            IIncomeService incomeService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            _baseService = baseService;
            _incomeService = incomeService;
            _eventAggregator = eventAggregator;

            TabName = "Inkomst";

            IncomeUIEventUIEventHandler = new CustomDelegateCommand(ExecuteIncomeUIEventUIEventHandler, o => true);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
            _eventAggregator.GetEvent<SaveDataModelBeforeSaveFileIsCreatedEvent>().Subscribe(ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent);
            _eventAggregator.GetEvent<UpdateAllEvent>().Subscribe(UpdateAndRespond);
            _eventAggregator.GetEvent<UpdateEvent>().Subscribe(UpdateResponse);
            _eventAggregator.GetEvent<UpdateResponseEvent>().Subscribe(HandleUpdateEvent);
        }

        private void HandleUpdateEvent(UpdateEventParameters param)
        {
            if (param.Publisher == "Income"
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
            if (str != "Income")
            {
                UpdateFiefCollection();
                for (int x = 1; x < FiefCollection.Count; x++)
                {
                    DataModel = _baseService.GetDataModel<IncomeDataModel>(x);
                    DataModel.IncomesCollection = new ObservableCollection<IncomeModel>(_incomeService.SetIncomes(x, DataModel.IncomesCollection));
                    DataModel.UpdateTotals();
                    SaveData(x);
                }

                _eventAggregator.GetEvent<UpdateResponseEvent>().Publish(new UpdateEventParameters()
                {
                    ModuleName = "Income",
                    Completed = true,
                    Publisher = str
                });
            }
        }

        private void CompleteLoadData()
        {
            if (Index != 0)
            {
                DataModel = _baseService.GetDataModel<IncomeDataModel>(Index);
                DataModel.IncomesCollection = new ObservableCollection<IncomeModel>(_incomeService.SetIncomes(Index, DataModel.IncomesCollection));
                DataModel.UpdateTotals();
                DataModel.StewardsCollection = _baseService.GetStewardsCollection();

                for (int x = 0; x < DataModel.IncomesCollection.Count; x++)
                {
                    DataModel.IncomesCollection[x].StewardsCollection = DataModel.StewardsCollection;
                }
            }
            else
            {
                DataModel = _incomeService.GetAllDataModel();
            }

            UpdateFiefCollection();
        }

        private void UpdateAndRespond()
        {
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<IncomeDataModel>(x);
                DataModel.IncomesCollection = new ObservableCollection<IncomeModel>(_incomeService.SetIncomes(x, DataModel.IncomesCollection));
                DataModel.UpdateTotals();
                SaveData(x);
            }

            _eventAggregator.GetEvent<UpdateAllResponseEvent>().Publish(new UpdateAllEventParameters()
            {
                ModuleName = "Income",
                Completed = true
            });
        }

        #region CustomDelegateCommand : IncomeUIEventUIEventHandler

        public CustomDelegateCommand IncomeUIEventUIEventHandler { get; set; }
        private void ExecuteIncomeUIEventUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is IncomeUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Changed")
            {
                SaveData();
                _baseService.ChangeSteward(e.StewardId, e.IncomeId, "Income");
                List<IncomeModel> tempIncome = new List<IncomeModel>(DataModel.IncomesCollection);
                DataModel.IncomesCollection.Clear();
                DataModel.IncomesCollection = new ObservableCollection<IncomeModel>(_incomeService.SetIncomes(Index, new ObservableCollection<IncomeModel>(tempIncome)));
                UpdateStewardsCollectionInIncomes();
            }
        }

        #endregion

        #region DataModel

        private IncomeDataModel _dataModel;
        public IncomeDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        protected override void LoadData()
        {
            //if (_triggerLoad)
            //{
            //    _triggerLoad = false;
            //    for (int x = 0; x < _awaitResponsList.Count; x++)
            //    {
            //        if (_awaitResponsList[x].ModuleName == "Income")
            //        {
            //            _awaitResponsList[x].Completed = true;
            //        }
            //        else
            //        {
            //            _awaitResponsList[x].Completed = false;
            //        }
            //    }
            //    _eventAggregator.GetEvent<UpdateEvent>().Publish("Income");
            //}
            CompleteLoadData();
        }

        protected override void SaveData(int index = -1)
        {
            if (Index != 0)
            {
                _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
            }
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            CompleteLoadData();
        }

        private void UpdateStewardsCollectionInIncomes()
        {
            if (DataModel.IncomesCollection.Count > 0)
            {
                for (int x = 0; x < DataModel.IncomesCollection.Count; x++)
                {
                    DataModel.IncomesCollection[x].StewardsCollection = new ObservableCollection<StewardModel>(_baseService.GetStewardsCollection());
                }
            }
        }

        private void ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent()
        {
            DataModel = _baseService.GetDataModel<IncomeDataModel>(Index);
            DataModel.IncomesCollection = new ObservableCollection<IncomeModel>(_incomeService.SetIncomes(Index, DataModel.IncomesCollection));
            DataModel.UpdateTotals();
            SaveData();
        }
    }
}
