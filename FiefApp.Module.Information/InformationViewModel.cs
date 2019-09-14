using System;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;
using Prism.Commands;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;

namespace FiefApp.Module.Information
{
    public class InformationViewModel : ViewModelBaseClass
    {
        private readonly IInformationService _informationService;
        private readonly IBaseService _baseService;
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

        public InformationViewModel(
            IBaseService baseService,
            IInformationService informationService,
            ISettingsService settingsService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            TabName = "Information";

            _baseService = baseService;
            _informationService = informationService;
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;

            SettingsModel = _settingsService.InformationSettingsModel;

            // UI DelegateCommands
            EditButtonCommand = new DelegateCommand(ExecuteEditButtonCommand);
            CancelEditingButtonCommand = new DelegateCommand(ExecuteCancelEditingButtonCommand);
            SaveEditedButtonCommand = new DelegateCommand(ExecuteSaveEditedButtonCommand);
            AddFiefCommand = new DelegateCommand(ExecuteAddFiefCommand);
            RemoveFiefCommand = new DelegateCommand(ExecuteRemoveFiefCommand);
            UpdateInformationTextCommand = new DelegateCommand(ExecuteUpdateInformationTextCommand);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
            _eventAggregator.GetEvent<SaveDataModelBeforeSaveFileIsCreatedEvent>().Subscribe(ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent);
            _eventAggregator.GetEvent<UpdateAllEvent>().Subscribe(UpdateAndRespond);
            _eventAggregator.GetEvent<UpdateEvent>().Subscribe(UpdateResponse);
            _eventAggregator.GetEvent<UpdateResponseEvent>().Subscribe(HandleUpdateEvent);
            _eventAggregator.GetEvent<EndOfYearCompletedEvent>().Subscribe(HandleEndOfYearComplete);
        }

        private void HandleEndOfYearComplete()
        {
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<InformationDataModel>(x);
                DataModel.CheckReligionsList();
                DataModel?.SortReligionsListIntoReligionsShowCollection(_informationService.GetTotalPopulation(x));
                SaveData(x);
            }
        }

        private void HandleUpdateEvent(UpdateEventParameters param)
        {
            if (param.Publisher == "Information"
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
            if (str != "Information")
            {
                UpdateFiefCollection();
                for (int x = 1; x < FiefCollection.Count; x++)
                {
                    DataModel = _baseService.GetDataModel<InformationDataModel>(x);
                    DataModel.CheckReligionsList();
                    DataModel?.SortReligionsListIntoReligionsShowCollection(_informationService.GetTotalPopulation(x));
                    SaveData(x);
                }

                _eventAggregator.GetEvent<UpdateResponseEvent>().Publish(new UpdateEventParameters()
                {
                    ModuleName = "Information",
                    Completed = true,
                    Publisher = str
                });
            }
        }

        private void CompleteLoadData()
        {
            if (Index == 0)
            {
                DataModel = _informationService.GetAllInformationDataModel();
            }
            else
            {
                if (RemovedFief)
                {
                    RemovedFief = false;
                }

                _informationService.SetupPopulationReligion(Index);
                DataModel = _baseService.GetDataModel<InformationDataModel>(Index);
                DataModel.CheckReligionsList();
                DataModel?.SortReligionsListIntoReligionsShowCollection(_informationService.GetTotalPopulation(Index));
            }

            UpdateFiefCollection();
        }

        private void UpdateAndRespond()
        {
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<InformationDataModel>(x);
                DataModel.CheckReligionsList();
                DataModel?.SortReligionsListIntoReligionsShowCollection(_informationService.GetTotalPopulation(x));
                SaveData(x);
            }

            _eventAggregator.GetEvent<UpdateAllResponseEvent>().Publish(new UpdateAllEventParameters()
            {
                ModuleName = "Information",
                Completed = true
            });
        }

        #region UI DelegateCommands

        public DelegateCommand EditButtonCommand { get; set; }
        private void ExecuteEditButtonCommand()
        {
            SaveData();
            DataModel = (InformationDataModel)DataModel.Clone();
        }

        public DelegateCommand CancelEditingButtonCommand { get; set; }
        private void ExecuteCancelEditingButtonCommand()
        {
            LoadData();
        }

        public DelegateCommand SaveEditedButtonCommand { get; set; }
        private void ExecuteSaveEditedButtonCommand()
        {
            SaveData();
            UpdateFiefCollection();
        }

        public DelegateCommand AddFiefCommand { get; set; }
        private void ExecuteAddFiefCommand()
        {
            Index = _baseService.CreateNewFief();
        }

        public DelegateCommand RemoveFiefCommand { get; set; }
        private void ExecuteRemoveFiefCommand()
        {
            // MAKE BACKUP FILE!
            RemovedFief = true;
            Index = _baseService.RemoveFief(Index);
            LoadData();
        }

        public DelegateCommand UpdateInformationTextCommand { get; set; }
        private void ExecuteUpdateInformationTextCommand()
        {
            if (DataModel.FiefTypeIndex != -1)
            {
                DataModel.SelectedInformationText = SettingsModel.InformationTextList[DataModel.FiefTypeIndex];
            }
        }

        #endregion

        #region UI Properties

        private string _oldInformationText;
        public string OldInformationText
        {
            get => _oldInformationText;
            set => SetProperty(ref _oldInformationText, value);
        }

        private string _selectedInformationText = "";
        public string SelectedInformationText
        {
            get => _selectedInformationText;
            set => SetProperty(ref _selectedInformationText, value);
        }

        #endregion

        #region View Data Model Properties

        private InformationDataModel _dataModel;
        public InformationDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        private InformationSettingsModel _settingsModel;
        public InformationSettingsModel SettingsModel
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
                _baseService.SetDataModel(DataModel, index == -1 ? Index : index);

                if (Index == 1)
                {
                    _eventAggregator.GetEvent<FiefNameChangedEvent>().Publish();
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
            //        if (_awaitResponsList[x].ModuleName == "Information")
            //        {
            //            _awaitResponsList[x].Completed = true;
            //        }
            //        else
            //        {
            //            _awaitResponsList[x].Completed = false;
            //        }
            //    }
            //    _eventAggregator.GetEvent<UpdateEvent>().Publish("Information");
            //}
            CompleteLoadData();
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
    }
}
