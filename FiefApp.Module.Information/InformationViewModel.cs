using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;
using Prism.Commands;
using Prism.Events;

namespace FiefApp.Module.Information
{
    public class InformationViewModel : ViewModelBaseClass
    {
        private readonly IInformationService _informationService;
        private readonly IBaseService _baseService;
        private readonly ISettingsService _settingsService;
        private readonly IEventAggregator _eventAggregator;

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
            int newIndex = _baseService.RemoveFief(Index);
            Index = -1;
            Index = newIndex;
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

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            LoadData();
        }

        private void ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent()
        {
            SaveData();
        }

        #endregion
    }
}
