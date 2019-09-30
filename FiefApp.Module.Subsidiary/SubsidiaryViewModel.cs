using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Subsidiary.RoutedEvents;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FiefApp.Module.Subsidiary
{
    public class SubsidiaryViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly ISubsidiaryService _subsidiaryService;
        private readonly IEventAggregator _eventAggregator;
        private List<UpdateAllEventParameters> _awaitAllModulesList = new List<UpdateAllEventParameters>()
        {
            new UpdateAllEventParameters()
            {
                ModuleName = "Army",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Boatbuilding",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Buildings",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Employees",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Expenses",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Income",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Information",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Manor",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Mines",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Port",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Stewards",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Subsidiary",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Trade",
                Completed = false
            }
        };

        public SubsidiaryViewModel(
            IBaseService baseService,
            ISubsidiaryService subsidiaryService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            _baseService = baseService;
            _subsidiaryService = subsidiaryService;
            _eventAggregator = eventAggregator;

            TabName = "Binäringar";

            ConstructSubsidiaryCommand = new CustomDelegateCommand(ExecuteConstructSubsidiaryCommand, o => true);
            AddSubsidiaryUIEventHandler = new CustomDelegateCommand(ExecuteAddSubsidiaryUIEventHandler, o => true);
            ConstructingUIEventHandler = new CustomDelegateCommand(ExecuteConstructingUIEventHandler, o => true);
            SubsidiaryUIEventHandler = new CustomDelegateCommand(ExecuteSubsidiaryUIEventHandler, o => true);
            AddSubsidiaryCommand = new DelegateCommand(ExecuteAddSubsidiaryCommand);
            EditSubsidiaryUIEventHandler = new CustomDelegateCommand(ExecuteEditSubsidiaryUIEventHandler, o => true);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
            _eventAggregator.GetEvent<UpdateAllResponseEvent>().Subscribe(HandleUpdateAllEventResponses);
            _eventAggregator.GetEvent<UpdateAllEvent>().Subscribe(UpdateAllAndRespond);
        }

        #region EventAggregator Events

        private void HandleUpdateAllEventResponses(UpdateAllEventParameters param)
        {
            if (param.Publisher == "Subsidiary"
                && _awaitAllModulesList != null)
            {
                for (int x = 0; x < _awaitAllModulesList.Count; x++)
                {
                    if (_awaitAllModulesList[x].ModuleName == param.ModuleName)
                    {
                        _awaitAllModulesList[x].Completed = param.Completed;
                    }
                }

                if (_awaitAllModulesList.All(o => o.Completed))
                {
                    CompleteLoadData();
                }
            }
        }

        private void UpdateAllAndRespond(string str)
        {
            SaveData(Index);

            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<SubsidiaryDataModel>(x);
                GetInformationSetDataModel(x);
                SaveData(x);
            }

            _eventAggregator.GetEvent<UpdateAllResponseEvent>().Publish(new UpdateAllEventParameters()
            {
                ModuleName = "Subsidiary",
                Completed = true,
                Publisher = str
            });
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            CompleteLoadData();
        }

        #endregion

        #region CustomDelegateCommand : ConstructSubsidiaryCommand

        public CustomDelegateCommand ConstructSubsidiaryCommand { get; set; }
        private void ExecuteConstructSubsidiaryCommand(object obj)
        {
            DataModel.SubsidiaryTypesCollection = _subsidiaryService.GetSubsidiaryTypesCollection();
            DataModel.AddSubsidiaryTag = "ConstructSubsidiary";
        }

        #endregion
        #region CustomDelegateCommand : AddSubsidiaryUIEventHandler

        public CustomDelegateCommand AddSubsidiaryUIEventHandler { get; set; }
        private void ExecuteAddSubsidiaryUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is AddSubsidiaryUIEventArgs e))
            {
                return;
            }

            e.Handled = true;


            if (DataModel.AddSubsidiaryTag == "ConstructSubsidiary")
            {
                if (e.Action == "Save")
                {
                    e.SubsidiaryModel.Id = _baseService.GetNewIndustryId();
                    if (DataModel.SubsidiaryTypesCollection.Any(o => o.Subsidiary != e.SubsidiaryModel.Subsidiary)
                        && e.SubsidiaryModel.Subsidiary != "")
                    {
                        _subsidiaryService.AddCustomSubsidiary(e.SubsidiaryModel);
                    }
                    e.SubsidiaryModel.StewardsCollection = DataModel.StewardsCollection;
                    e.SubsidiaryModel.StewardId = -1;
                    e.SubsidiaryModel.Steward = "";
                    e.SubsidiaryModel.DevelopmentLevel = 1;

                    DataModel.ConstructingCollection.Add(e.SubsidiaryModel);

                    DataModel.AddSubsidiaryTag = "";
                }
            }
            else if (DataModel.AddSubsidiaryTag == "AddSubsidiary")
            {
                if (e.Action == "Save")
                {
                    e.SubsidiaryModel.Id = _baseService.GetNewIndustryId();
                    _subsidiaryService.AddCustomSubsidiary(e.SubsidiaryModel);
                    e.SubsidiaryModel.StewardsCollection = DataModel.StewardsCollection;
                    e.SubsidiaryModel.StewardId = -1;
                    e.SubsidiaryModel.Steward = "";
                    e.SubsidiaryModel.DevelopmentLevel = 1;
                    e.SubsidiaryModel.Difficulty = _subsidiaryService.GetAndSetDifficulty(Index, e.SubsidiaryModel.Spring, e.SubsidiaryModel.Summer, e.SubsidiaryModel.Fall, e.SubsidiaryModel.Winter);

                    DataModel.SubsidiaryCollection.Add(e.SubsidiaryModel);

                    DataModel.AddSubsidiaryTag = "";
                }
            }
        }

        #endregion
        #region CustomDelegateCommand : ConstructingUIEventHandler

        public CustomDelegateCommand ConstructingUIEventHandler { get; set; }
        private void ExecuteConstructingUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is ConstructingUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Changed")
            {
                SaveData();
                _baseService.ChangeSteward(e.StewardId, e.SubsidiaryId, "Subsidiary");

                List<SubsidiaryModel> tempConstructingList = new List<SubsidiaryModel>(DataModel.ConstructingCollection);
                List<SubsidiaryModel> tempSubsidiaryList = new List<SubsidiaryModel>(DataModel.SubsidiaryCollection);

                DataModel.ConstructingCollection.Clear();
                DataModel.ConstructingCollection = new ObservableCollection<SubsidiaryModel>(tempConstructingList);

                DataModel.SubsidiaryCollection.Clear();
                DataModel.SubsidiaryCollection = new ObservableCollection<SubsidiaryModel>(tempSubsidiaryList);

                UpdateStewardsCollectionInCollections();
            }

            if (e.Action == "DaysWorkThisYearChanged")
            {
                for (int x = 0; x < DataModel.ConstructingCollection.Count; x++)
                {
                    if (DataModel.ConstructingCollection[x].Id == e.SubsidiaryId)
                    {
                        DataModel.ConstructingCollection[x].DaysWorkThisYear = e.DaysWorkThisYear;
                    }
                }
            }

            if (e.Action == "Delete")
            {
                for (int x = 0; x < DataModel.ConstructingCollection.Count; x++)
                {
                    if (DataModel.ConstructingCollection[x].Id == e.SubsidiaryId)
                    {
                        DataModel.ConstructingCollection.RemoveAt(x);
                        break;
                    }
                }

                
            }
        }

        #endregion
        #region CustomDelegateCommand : SubsidiaryUIEventHandler

        public CustomDelegateCommand SubsidiaryUIEventHandler { get; set; }
        private void ExecuteSubsidiaryUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is SubsidiaryUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Changed":
                {
                    SaveData();
                    _baseService.ChangeSteward(e.StewardId, e.SubsidiaryId, "Subsidiary");

                    List<SubsidiaryModel> tempConstructingList = new List<SubsidiaryModel>(DataModel.ConstructingCollection);
                    List<SubsidiaryModel> tempSubsidiaryList = new List<SubsidiaryModel>(DataModel.SubsidiaryCollection);

                    DataModel.ConstructingCollection.Clear();
                    DataModel.ConstructingCollection = new ObservableCollection<SubsidiaryModel>(tempConstructingList);

                    DataModel.SubsidiaryCollection.Clear();
                    DataModel.SubsidiaryCollection = new ObservableCollection<SubsidiaryModel>(tempSubsidiaryList);

                    UpdateStewardsCollectionInCollections();
                    break;
                }

                case "Delete":
                {
                    for (int x = 0; x < DataModel.SubsidiaryCollection.Count; x++)
                    {
                        if (e.SubsidiaryId == DataModel.SubsidiaryCollection[x].Id)
                        {
                            DataModel.SubsidiaryCollection.RemoveAt(x);
                        }
                    }
                    break;
                }

                case "Edit":
                {
                    DataModel.EditModel = new SubsidiaryModel();
                    for (int x = 0; x < DataModel.SubsidiaryCollection.Count; x++)
                    {
                        if (DataModel.SubsidiaryCollection[x].Id == e.SubsidiaryId)
                        {
                            DataModel.EditModel.IncomeFactor = DataModel.SubsidiaryCollection[x].IncomeFactor;
                            DataModel.EditModel.IncomeBase = DataModel.SubsidiaryCollection[x].IncomeBase;
                            DataModel.EditModel.IncomeLuxury = DataModel.SubsidiaryCollection[x].IncomeLuxury;
                            DataModel.EditModel.IncomeSilver = DataModel.SubsidiaryCollection[x].IncomeSilver;
                            DataModel.EditModel.DaysWorkUpkeep = DataModel.SubsidiaryCollection[x].DaysWorkUpkeep;
                            DataModel.EditModel.Spring = DataModel.SubsidiaryCollection[x].Spring;
                            DataModel.EditModel.Summer = DataModel.SubsidiaryCollection[x].Summer;
                            DataModel.EditModel.Fall = DataModel.SubsidiaryCollection[x].Fall;
                            DataModel.EditModel.Winter = DataModel.SubsidiaryCollection[x].Winter;
                            DataModel.EditModel.Quality = DataModel.SubsidiaryCollection[x].Quality;
                            DataModel.EditModel.DevelopmentLevel = DataModel.SubsidiaryCollection[x].DevelopmentLevel;
                            break;
                        }
                    }
                    break;
                }

                case "Update":
                {
                    for (int x = 0; x < DataModel.SubsidiaryCollection.Count; x++)
                    {
                        if (DataModel.SubsidiaryCollection[x].Id == e.SubsidiaryId)
                        {
                            DataModel.SubsidiaryCollection[x].Silver = e.Model.Silver;
                            DataModel.SubsidiaryCollection[x].Base = e.Model.Base;
                            DataModel.SubsidiaryCollection[x].Luxury = e.Model.Luxury;
                            DataModel.SubsidiaryCollection[x].DaysWorkThisYear = e.Model.DaysWorkThisYear;
                            break;
                        }
                    }
                    break;
                }
            }
        }

        #endregion

        #region DelegateCommand : AddSubsidiaryCommand

        public DelegateCommand AddSubsidiaryCommand { get; set; }
        private void ExecuteAddSubsidiaryCommand()
        {
            DataModel.SubsidiaryTypesCollection = _subsidiaryService.GetSubsidiaryTypesCollection();
            DataModel.AddSubsidiaryTag = "AddSubsidiary";
        }

        #endregion

        #region CustomDelegateCommand : EditSubsidiaryUIEventHandler

        public CustomDelegateCommand EditSubsidiaryUIEventHandler { get; set; }

        private void ExecuteEditSubsidiaryUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is EditSubsidiaryUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Cancel")
            {
                DataModel.EditModel.Id = -1;
                DataModel.EditModel.IncomeFactor = 0M;
                DataModel.EditModel.IncomeBase = 0M;
                DataModel.EditModel.IncomeLuxury = 0M;
                DataModel.EditModel.IncomeSilver = 0M;
                DataModel.EditModel.DaysWorkUpkeep = 0;
                DataModel.EditModel.Spring = 0M;
                DataModel.EditModel.Summer = 0M;
                DataModel.EditModel.Fall = 0M;
                DataModel.EditModel.Winter = 0M;
                DataModel.EditModel.Quality = 0;
                DataModel.EditModel.DevelopmentLevel = 0;
            }
            else if (e.Action == "Save")
            {
                _subsidiaryService.SetSubsidiary(Index, e.SubsidiaryModel.Id, e.SubsidiaryModel);

                DataModel.EditModel.Id = -1;
                DataModel.EditModel.IncomeFactor = 0M;
                DataModel.EditModel.IncomeBase = 0M;
                DataModel.EditModel.IncomeLuxury = 0M;
                DataModel.EditModel.IncomeSilver = 0M;
                DataModel.EditModel.DaysWorkUpkeep = 0;
                DataModel.EditModel.Spring = 0M;
                DataModel.EditModel.Summer = 0M;
                DataModel.EditModel.Fall = 0M;
                DataModel.EditModel.Winter = 0M;
                DataModel.EditModel.Quality = 0;
                DataModel.EditModel.DevelopmentLevel = 0;
            }
        }

        #endregion

        #region DataModel

        private SubsidiaryDataModel _dataModel;
        public SubsidiaryDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        #region Methods

        protected override void SaveData(int index = -1)
        {
            _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
        }

        protected override void LoadData()
        {
            CompleteLoadData();
        }

        private void CompleteLoadData()
        {
            DataModel = Index
                        == 0 ? _subsidiaryService.GetAllSubsidiaryDataModel()
                : _baseService.GetDataModel<SubsidiaryDataModel>(Index);

            DataModel.StewardsCollection = _baseService.GetStewardsCollection();
            for (int x = 0; x < DataModel.ConstructingCollection.Count; x++)
            {
                DataModel.ConstructingCollection[x].StewardsCollection = DataModel.StewardsCollection;
            }

            GetInformationSetDataModel(Index);

            UpdateFiefCollection();
        }

        #endregion

        #region Methods

        private void UpdateStewardsCollectionInCollections()
        {
            if (DataModel.ConstructingCollection.Count > 0)
            {
                for (int x = 0; x < DataModel.ConstructingCollection.Count; x++)
                {
                    DataModel.ConstructingCollection[x].StewardsCollection = new ObservableCollection<StewardModel>(_baseService.GetStewardsCollection());
                }
            }

            if (DataModel.SubsidiaryCollection.Count > 0)
            {
                for (int x = 0; x < DataModel.SubsidiaryCollection.Count; x++)
                {
                    DataModel.SubsidiaryCollection[x].StewardsCollection = new ObservableCollection<StewardModel>(_baseService.GetStewardsCollection());
                }
            }
        }

        private void GetInformationSetDataModel(int index = -1)
        {
            UpdateStewardsCollectionInCollections();
            if (index == -1)
            {
                SetDifficultyInSubsidiaries(Index);
            }
            else
            {
                SetDifficultyInSubsidiaries(index);
            }
        }

        private void SetDifficultyInSubsidiaries(int index = -1)
        {
            if (index == -1)
            {
                for (int x = 0; x < DataModel.SubsidiaryCollection.Count; x++)
                {
                    DataModel.SubsidiaryCollection[x].Difficulty = _subsidiaryService.GetAndSetDifficulty(Index,
                        DataModel.SubsidiaryCollection[x].Spring,
                        DataModel.SubsidiaryCollection[x].Summer,
                        DataModel.SubsidiaryCollection[x].Fall,
                        DataModel.SubsidiaryCollection[x].Winter);
                }
            }
            else
            {
                for (int x = 0; x < DataModel.SubsidiaryCollection.Count; x++)
                {
                    DataModel.SubsidiaryCollection[x].Difficulty = _subsidiaryService.GetAndSetDifficulty(index,
                        DataModel.SubsidiaryCollection[x].Spring,
                        DataModel.SubsidiaryCollection[x].Summer,
                        DataModel.SubsidiaryCollection[x].Fall,
                        DataModel.SubsidiaryCollection[x].Winter);
                }
            }
        }

        #endregion
    }
}
