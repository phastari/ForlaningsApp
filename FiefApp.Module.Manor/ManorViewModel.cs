﻿using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;
using FiefApp.Module.Manor.RoutedEvents;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FiefApp.Module.Manor
{
    public class ManorViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IManorService _manorService;
        private readonly ISettingsService _settingsService;
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

        public ManorViewModel(
            IBaseService baseService,
            IManorService manorService,
            ISettingsService settingsService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            _baseService = baseService;
            _manorService = manorService;
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;

            SettingsModel = _settingsService.ManorSettingsModel;

            TabName = "Gods/Byar";

            ResidentUIEventHandler = new CustomDelegateCommand(ExecuteResidentUIEventHandler, o => true);
            AddResidentUIEventHandler = new CustomDelegateCommand(ExecuteAddResidentUIEventHandler, o => true);
            VillageUIEventHandler = new CustomDelegateCommand(ExecuteVillageUIEventHandler, o => true);
            AddVillageCommand = new DelegateCommand(ExecuteAddVillageCommand);
            EditButtonCommand = new DelegateCommand(ExecuteEditButtonCommand);
            CancelEditButton = new DelegateCommand(ExecuteCancelEditButton);
            SaveEditButton = new DelegateCommand(ExecuteSaveEditButton);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
            _eventAggregator.GetEvent<SaveDataModelBeforeSaveFileIsCreatedEvent>().Subscribe(ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent);
            _eventAggregator.GetEvent<UpdateAllEvent>().Subscribe(UpdateAndRespond);
            _eventAggregator.GetEvent<UpdateEvent>().Subscribe(UpdateResponse);
            _eventAggregator.GetEvent<UpdateResponseEvent>().Subscribe(HandleUpdateEvent);
            _eventAggregator.GetEvent<EndOfYearCompletedEvent>().Subscribe(HandleEndOfYearComplete);
            _eventAggregator.GetEvent<SaveEvent>().Subscribe(ExecuteSaveEventResponse);
        }

        private void ExecuteSaveEventResponse()
        {
            SaveData(Index);
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<ManorDataModel>(x);
                DataModel.ResidentsCollection = new ObservableCollection<IPeopleModel>(_manorService.GetResidentsCollection(x));
                SaveData(x);
            }

            _eventAggregator.GetEvent<SaveEventResponse>().Publish(new SaveEventParameters()
            {
                Completed = true,
                ModuleName = "Manor"
            });

            DataModel = _baseService.GetDataModel<ManorDataModel>(Index);
        }

        private void HandleEndOfYearComplete()
        {
            DataModel = null;
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<ManorDataModel>(x);
                DataModel.ResidentsCollection = new ObservableCollection<IPeopleModel>(_manorService.GetResidentsCollection(x));
                SaveData(x);
            }
            DataModel = _baseService.GetDataModel<ManorDataModel>(Index);
        }

        private void HandleUpdateEvent(UpdateEventParameters param)
        {
            if (param.Publisher == "Manor"
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
            if (str != "Manor")
            {
                UpdateFiefCollection();
                for (int x = 1; x < FiefCollection.Count; x++)
                {
                    DataModel = _baseService.GetDataModel<ManorDataModel>(x);
                    DataModel.ResidentsCollection = new ObservableCollection<IPeopleModel>(_manorService.GetResidentsCollection(x));
                    SaveData(x);
                }

                _eventAggregator.GetEvent<UpdateResponseEvent>().Publish(new UpdateEventParameters()
                {
                    ModuleName = "Manor",
                    Completed = true,
                    Publisher = str
                });
            }
        }

        private void CompleteLoadData()
        {
            if (Index == 0)
            {
                DataModel = _manorService.GetAllManorDataModel();
            }
            else
            {
                DataModel = _baseService.GetDataModel<ManorDataModel>(Index);
                DataModel.ResidentsCollection.Clear();
                DataModel.ResidentsCollection = new ObservableCollection<IPeopleModel>(_manorService.GetResidentsCollection(Index));
                GetInformationSetDataModel();
            }

            UpdateFiefCollection();
        }

        private void UpdateAndRespond()
        {
            SaveData(Index);
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<ManorDataModel>(x);
                DataModel.ResidentsCollection = new ObservableCollection<IPeopleModel>(_manorService.GetResidentsCollection(x));
                SaveData(x);
            }

            _eventAggregator.GetEvent<UpdateAllResponseEvent>().Publish(new UpdateAllEventParameters()
            {
                ModuleName = "Manor",
                Completed = true
            });
        }

        #region CustomDelegateCommand : ResidentUIEventHandler

        public CustomDelegateCommand ResidentUIEventHandler { get; set; }

        private void ExecuteResidentUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is ResidentUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Save")
            {
                if (e.Model.Type == "Soldier")
                {
                    _manorService.SetSoldierModel(e.Id, Index, e.Model as SoldierModel);
                }
                else if (e.Model.Type == "Resident")
                {
                    for (int x = 0; x < DataModel.ResidentsList.Count; x++)
                    {
                        if (DataModel.ResidentsList[x].Id == e.Id)
                        {
                            DataModel.ResidentsList[x].PersonName = e.Model.PersonName;
                            DataModel.ResidentsList[x].Position = e.Model.Position;
                            DataModel.ResidentsList[x].Age = e.Model.Age;
                        }
                    }
                }

                SaveData();
                DataModel.ResidentsCollection = _manorService.GetResidentsCollection(Index);
            }
            else if (e.Action == "Delete")
            {
                _manorService.DeletePeople(e.Id, Index);

                SaveData();
                DataModel.ResidentsCollection = _manorService.GetResidentsCollection(Index);
            }
        }

        #endregion
        #region CustomDelegateCommand : AddResidentUIEventHandler

        public CustomDelegateCommand AddResidentUIEventHandler { get; set; }
        private void ExecuteAddResidentUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is AddResidentUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Save")
            {
                DataModel.ResidentsList.Add(new ResidentModel()
                {
                    Id = _manorService.GetPeopleId(Index),
                    PersonName = e.ResidentModel.PersonName,
                    Position = "Boende",
                    Age = e.ResidentModel.Age
                });

                SaveData();
                DataModel.ResidentsCollection = _manorService.GetResidentsCollection(Index);
            }
        }

        #endregion

        #region CustomDelegateCommand : VillageUIEventHandler

        public CustomDelegateCommand VillageUIEventHandler { get; set; }
        private void ExecuteVillageUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is VillagesUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Expanded":
                    {
                        for (int x = 0; x < DataModel.VillagesCollection.Count; x++)
                        {
                            if (e.Id != DataModel.VillagesCollection[x].Id)
                            {
                                DataModel.VillagesCollection[x].IsExpanded = false;
                            }
                        }
                        break;
                    }

                case "Save":
                    {
                        DataModel.VillagesCollection[e.Id] = e.VillageModel;
                        UpdateManorPopulationFromVillages();
                        break;
                    }

                case "Delete":
                    {
                        for (int x = 0; x < DataModel.VillagesCollection.Count; x++)
                        {
                            if (e.Id == DataModel.VillagesCollection[x].Id)
                            {
                                DataModel.VillagesCollection.RemoveAt(x);
                            }
                        }
                        break;
                    }
            }
        }

        #endregion

        #region DelegateCommand : AddVillageCommand

        public DelegateCommand AddVillageCommand { get; set; }
        private void ExecuteAddVillageCommand()
        {
            for (int x = 0; x < DataModel.VillagesCollection.Count; x++)
            {
                DataModel.VillagesCollection[x].IsExpanded = false;
            }

            DataModel.VillagesCollection.Add(new VillageModel()
            {
                Id = DataModel.VillagesCollection.Count,
                Village = "",
                Population = 0,
                Burgess = 0,
                Farmers = 0,
                Serfdoms = 0,
                Boatbuilders = 0,
                Tanners = 0,
                Millers = 0,
                Furriers = 0,
                Tailors = 0,
                Smiths = 0,
                Carpenters = 0,
                Innkeepers = 0,
                IsExpanded = false
            });

            DataModel.VillagesCollection[DataModel.VillagesCollection.Count - 1].IsExpanded = true;
        }

        #endregion

        #region DelegateCommand : EditButtonCommand

        public DelegateCommand EditButtonCommand { get; set; }
        private void ExecuteEditButtonCommand()
        {
            OldManorModel = new ManorModel()
            {
                Acres = DataModel.ManorAcres,
                Arable = DataModel.ManorArable,
                Felling = DataModel.ManorFelling,
                Livingconditions = DataModel.ManorLivingconditions,
                ManorName = DataModel.ManorName,
                Population = DataModel.ManorPopulation,
                Pasture = DataModel.ManorPasture,
                Useless = DataModel.ManorUseless,
                Wealth = DataModel.ManorWealth,
                Woodland = DataModel.ManorWoodland
            };
        }

        #endregion

        #region DelegateCommand : CancelEditButton

        public DelegateCommand CancelEditButton { get; set; }
        private void ExecuteCancelEditButton()
        {
            DataModel.ManorAcres = OldManorModel.Acres;
            DataModel.ManorArable = OldManorModel.Arable;
            DataModel.ManorFelling = OldManorModel.Felling;
            DataModel.ManorLivingconditions = OldManorModel.Livingconditions;
            DataModel.ManorName = OldManorModel.ManorName;
            DataModel.ManorPopulation = OldManorModel.Population;
            DataModel.ManorPasture = OldManorModel.Pasture;
            DataModel.ManorUseless = OldManorModel.Useless;
            DataModel.ManorWealth = OldManorModel.Wealth;
            DataModel.ManorWoodland = OldManorModel.Woodland;
        }

        #endregion

        #region DelegateCommand : SaveEditButton

        public DelegateCommand SaveEditButton { get; set; }
        private void ExecuteSaveEditButton()
        {
            SaveData(Index);
        }

        #endregion

        #region View Data Model Properties

        private ManorDataModel _dataModel;
        public ManorDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        private ManorModel _oldManorModel = new ManorModel();
        public ManorModel OldManorModel
        {
            get => _oldManorModel;
            set => SetProperty(ref _oldManorModel, value);
        }

        #endregion

        #region Settings

        private ManorSettingsModel _settingsModel;
        public ManorSettingsModel SettingsModel
        {
            get => _settingsModel;
            set => SetProperty(ref _settingsModel, value);
        }

        #endregion

        #region Methods 

        protected override void SaveData(int index = -1)
        {
            if (Index != 0)
            {
                if (index == -1)
                {
                    _baseService.SetDataModel(DataModel, Index);
                    //_manorService.SetLivingconditions(Index, DataModel.ManorLivingconditions);
                }
                else
                {
                    _baseService.SetDataModel(DataModel, index);
                    _manorService.SetLivingconditions(index, DataModel.ManorLivingconditions);
                }
            }
        }

        protected override void LoadData()
        {
            //if (_triggerLoad)
            //{
            //    _triggerLoad = false;
            //    for (int x = 0; x < _awaitResponsList.Count; x++)
            //    {
            //        if (_awaitResponsList[x].ModuleName == "Manor")
            //        {
            //            _awaitResponsList[x].Completed = true;
            //        }
            //        else
            //        {
            //            _awaitResponsList[x].Completed = false;
            //        }
            //    }
            //    _eventAggregator.GetEvent<UpdateEvent>().Publish("Manor");
            //}
            CompleteLoadData();
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            CompleteLoadData();
        }

        private void GetInformationSetDataModel()
        {
            UpdateManorPopulationFromVillages();
            GetLivingCondition();
        }

        private void UpdateManorPopulationFromVillages()
        {
            int population = 0;

            for (int x = 0; x < DataModel.VillagesCollection.Count; x++)
            {
                population += DataModel.VillagesCollection[x].Population;
            }

            DataModel.ManorPopulation = population;
        }

        private void GetLivingCondition()
        {
            DataModel.ManorLivingconditions = _manorService.GetLivingcondition(Index);
        }

        #endregion

        private void ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent()
        {
            SaveData();
        }
    }
}
