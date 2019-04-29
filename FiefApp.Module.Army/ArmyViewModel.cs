using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.Controls.iTextBox.RoutedEvents;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using System;
using System.ComponentModel;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using Prism.Events;

namespace FiefApp.Module.Army
{
    public class ArmyViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IArmyService _armyService;
        private readonly ISettingsService _settingsService;
        private readonly IEventAggregator _eventAggregator;

        public ArmyViewModel(
            IBaseService baseService,
            IArmyService armyService,
            ISettingsService settingsService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            _baseService = baseService;
            _armyService = armyService;
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;

            TabName = "Arme";

            BoundToResidentEventHandler = new CustomDelegateCommand(ExecuteBoundToResidentEventHandler, o => true);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
            _eventAggregator.GetEvent<SaveDataModelBeforeSaveFileIsCreatedEvent>().Subscribe(ExecuteSaveDataModelBeforeSaveFileIsCreatedEvent);
        }

        #region CustomDelegateCommand : BoundToResidentEventHandler

        public CustomDelegateCommand BoundToResidentEventHandler { get; set; }
        private void ExecuteBoundToResidentEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is BoundToResidentEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Increase")
            {
                SoldierModel soldierModel = e.SoldierModel;

                soldierModel.Id = _armyService.GetPeopleId(Index);

                switch (soldierModel.Position)
                {
                    case "KnightTemplars":
                        soldierModel.Position = "Tempelriddare";
                        DataModel.TemplarKnightsList.Add(soldierModel);
                        break;

                    case "Knights":
                        soldierModel.Position = "Riddare";
                        DataModel.KnightsList.Add(soldierModel);
                        break;

                    case "CavalryTemplarKnights":
                        soldierModel.Position = "Tempelriddare";
                        DataModel.CavalryTemplarKnightsList.Add(soldierModel);
                        break;

                    case "OfficerCorporals":
                        soldierModel.Position = "Korpral";
                        DataModel.OfficerCorporalsList.Add(soldierModel);
                        break;

                    case "OfficerSergeants":
                        soldierModel.Position = "Sergeant";
                        DataModel.OfficerSergeantsList.Add(soldierModel);
                        break;

                    case "OfficerCaptains":
                        soldierModel.Position = "Kapten";
                        DataModel.OfficerCaptainsList.Add(soldierModel);
                        break;
                }
            }
            else if (e.Action == "Decrease")
            {
                SoldierModel soldierModel = e.SoldierModel;

                switch (soldierModel.Position)
                {
                    case "KnightTemplars":
                        DataModel.TemplarKnightsList.RemoveAt(DataModel.TemplarKnightsList.Count - 1);
                        break;

                    case "Knights":
                        DataModel.KnightsList.RemoveAt(DataModel.KnightsList.Count - 1);
                        break;

                    case "CavalryTemplarKnights":
                        DataModel.CavalryTemplarKnightsList.RemoveAt(DataModel.CavalryTemplarKnightsList.Count - 1);
                        break;

                    case "OfficerCorporals":
                        DataModel.OfficerCorporalsList.RemoveAt(DataModel.OfficerCorporalsList.Count - 1);
                        break;

                    case "OfficerSergeants":
                        DataModel.OfficerSergeantsList.RemoveAt(DataModel.OfficerSergeantsList.Count - 1);
                        break;

                    case "OfficerCaptains":
                        DataModel.OfficerCaptainsList.RemoveAt(DataModel.OfficerCaptainsList.Count - 1);
                        break;
                }

            }
        }

        #endregion

        #region View Data Model Properties

        private ArmyDataModel _dataModel;
        public ArmyDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        #region Methods 

        protected override void SaveData(int index = -1)
        {
            _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
            if (DataModel != null)
            {
                _armyService.UpdateSilverExpenses(Index, DataModel.TotalSilver);
                _armyService.UpdateBaseExpenses(Index, DataModel.TotalBase);
            }
        }

        protected override void LoadData()
        {
            DataModel = Index
                        == 0 ? _armyService.GetAllArmyDataModel()
                : _baseService.GetDataModel<ArmyDataModel>(Index);

            if (DataModel != null)
            {
                DataModel.PropertyChanged += DataModelPropertyChanged;
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

        #region DataModels PropertyChanged

        private void DataModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "ArmyCrossbowmen":
                    DataModel.ArmyCrossbowmenSilver = _settingsService.ArmySettingsModel.ArmyCrossbowmenSilver * DataModel.ArmyCrossbowmen;
                    DataModel.ArmyCrossbowmenBase = _settingsService.ArmySettingsModel.ArmyCrossbowmenBase * DataModel.ArmyCrossbowmen;
                    break;

                case "ArmyBowmen":
                    DataModel.ArmyBowmenSilver = _settingsService.ArmySettingsModel.ArmyBowmenSilver * DataModel.ArmyBowmen;
                    DataModel.ArmyBowmenBase = _settingsService.ArmySettingsModel.ArmyBowmenBase * DataModel.ArmyBowmen;
                    break;

                case "ArmyMedics":
                    DataModel.ArmyMedicsSilver = _settingsService.ArmySettingsModel.ArmyMedicsSilver * DataModel.ArmyMedics;
                    DataModel.ArmyMedicsBase = _settingsService.ArmySettingsModel.ArmyMedicsBase * DataModel.ArmyMedics;
                    break;

                case "ArmyMedicsSkilled":
                    DataModel.ArmyMedicsSkilledSilver = _settingsService.ArmySettingsModel.ArmyMedicsSkilledSilver * DataModel.ArmyMedicsSkilled;
                    DataModel.ArmyMedicsSkilledBase = _settingsService.ArmySettingsModel.ArmyMedicsSkilledBase * DataModel.ArmyMedicsSkilled;
                    break;

                case "ArmyInfantry":
                    DataModel.ArmyInfantrySilver = _settingsService.ArmySettingsModel.ArmyInfantrySilver * DataModel.ArmyInfantry;
                    DataModel.ArmyInfantryBase = _settingsService.ArmySettingsModel.ArmyInfantryBase * DataModel.ArmyInfantry;
                    break;

                case "ArmyInfantryMedium":
                    DataModel.ArmyInfantryMediumSilver = _settingsService.ArmySettingsModel.ArmyInfantryMediumSilver * DataModel.ArmyInfantryMedium;
                    DataModel.ArmyInfantryMediumBase = _settingsService.ArmySettingsModel.ArmyInfantryMediumBase * DataModel.ArmyInfantryMedium;
                    break;

                case "ArmyInfantryHeavy":
                    DataModel.ArmyInfantryHeavySilver = _settingsService.ArmySettingsModel.ArmyInfantryHeavySilver * DataModel.ArmyInfantryHeavy;
                    DataModel.ArmyInfantryHeavyBase = _settingsService.ArmySettingsModel.ArmyInfantryHeavyBase * DataModel.ArmyInfantryHeavy;
                    break;

                case "ArmyInfantryElite":
                    DataModel.ArmyInfantryEliteSilver = _settingsService.ArmySettingsModel.ArmyInfantryEliteSilver * DataModel.ArmyInfantryElite;
                    DataModel.ArmyInfantryEliteBase = _settingsService.ArmySettingsModel.ArmyInfantryEliteBase * DataModel.ArmyInfantryElite;
                    break;

                case "ArmyLongbowmen":
                    DataModel.ArmyLongbowmenSilver = _settingsService.ArmySettingsModel.ArmyLongbowmenSilver * DataModel.ArmyLongbowmen;
                    DataModel.ArmyLongbowmenBase = _settingsService.ArmySettingsModel.ArmyLongbowmenBase * DataModel.ArmyLongbowmen;
                    break;

                case "ArmyMercenary":
                    DataModel.ArmyMercenarySilver = _settingsService.ArmySettingsModel.ArmyMercenarySilver * DataModel.ArmyMercenary;
                    DataModel.ArmyMercenaryBase = _settingsService.ArmySettingsModel.ArmyMercenaryBase * DataModel.ArmyMercenary;
                    break;

                case "ArmyMercenaryElite":
                    DataModel.ArmyMercenaryEliteSilver = _settingsService.ArmySettingsModel.ArmyMercenaryEliteSilver * DataModel.ArmyMercenaryElite;
                    DataModel.ArmyMercenaryEliteBase = _settingsService.ArmySettingsModel.ArmyMercenaryEliteBase * DataModel.ArmyMercenaryElite;
                    break;

                case "ArmyMercenaryBowmen":
                    DataModel.ArmyMercenaryBowmenSilver = _settingsService.ArmySettingsModel.ArmyMercenaryBowmenSilver * DataModel.ArmyMercenaryBowmen;
                    DataModel.ArmyMercenaryBowmenBase = _settingsService.ArmySettingsModel.ArmyMercenaryBowmenBase * DataModel.ArmyMercenaryBowmen;
                    break;

                case "ArmyEngineers":
                    DataModel.ArmyEngineersSilver = _settingsService.ArmySettingsModel.ArmyEngineersSilver * DataModel.ArmyEngineers;
                    DataModel.ArmyEngineersBase = _settingsService.ArmySettingsModel.ArmyEngineersBase * DataModel.ArmyEngineers;
                    break;

                case "ArmySpearmen":
                    DataModel.ArmySpearmenSilver = _settingsService.ArmySettingsModel.ArmySpearmenSilver * DataModel.ArmySpearmen;
                    DataModel.ArmySpearmenBase = _settingsService.ArmySettingsModel.ArmySpearmenBase * DataModel.ArmySpearmen;
                    break;

                case "ArmyScouts":
                    DataModel.ArmyScoutsSilver = _settingsService.ArmySettingsModel.ArmyScoutsSilver * DataModel.ArmyScouts;
                    DataModel.ArmyScoutsBase = _settingsService.ArmySettingsModel.ArmyScoutsBase * DataModel.ArmyScouts;
                    break;

                case "ArmyScoutsSkilled":
                    DataModel.ArmyScoutsSkilledSilver = _settingsService.ArmySettingsModel.ArmyScoutsSkilledSilver * DataModel.ArmyScoutsSkilled;
                    DataModel.ArmyScoutsSkilledBase = _settingsService.ArmySettingsModel.ArmyScoutsSkilledBase * DataModel.ArmyScoutsSkilled;
                    break;

                case "ArmyKnightTemplars":
                    DataModel.ArmyKnightTemplarsSilver = _settingsService.ArmySettingsModel.ArmyKnightTemplarsSilver * DataModel.ArmyKnightTemplars;
                    DataModel.ArmyKnightTemplarsBase = _settingsService.ArmySettingsModel.ArmyKnightTemplarsBase * DataModel.ArmyKnightTemplars;
                    break;

                case "ArmyGuards":
                    DataModel.ArmyGuardsSilver = _settingsService.ArmySettingsModel.ArmyGuardsSilver * DataModel.ArmyGuards;
                    DataModel.ArmyGuardsBase = _settingsService.ArmySettingsModel.ArmyGuardsBase * DataModel.ArmyGuards;
                    break;

                case "ArmyWeaponmasters":
                    DataModel.ArmyWeaponmastersSilver = _settingsService.ArmySettingsModel.ArmyWeaponmastersSilver * DataModel.ArmyWeaponmasters;
                    DataModel.ArmyWeaponmastersBase = _settingsService.ArmySettingsModel.ArmyWeaponmastersBase * DataModel.ArmyWeaponmasters;
                    break;

                case "CavalryBowmen":
                    DataModel.CavalryBowmenSilver = _settingsService.ArmySettingsModel.CavalryBowmenSilver * DataModel.CavalryBowmen;
                    DataModel.CavalryBowmenBase = _settingsService.ArmySettingsModel.CavalryBowmenBase * DataModel.CavalryBowmen;
                    break;

                case "CavalryCourier":
                    DataModel.CavalryCourierSilver = _settingsService.ArmySettingsModel.CavalryCourierSilver * DataModel.CavalryCourier;
                    DataModel.CavalryCourierBase = _settingsService.ArmySettingsModel.CavalryCourierBase * DataModel.CavalryCourier;
                    break;

                case "CavalryLight":
                    DataModel.CavalryLightSilver = _settingsService.ArmySettingsModel.CavalryLightSilver * DataModel.CavalryLight;
                    DataModel.CavalryLightBase = _settingsService.ArmySettingsModel.CavalryLightBase * DataModel.CavalryLight;
                    break;

                case "CavalryKnights":
                    DataModel.CavalryKnightsSilver = _settingsService.ArmySettingsModel.CavalryKnightsSilver * DataModel.CavalryKnights;
                    DataModel.CavalryKnightsBase = _settingsService.ArmySettingsModel.CavalryKnightsBase * DataModel.CavalryKnights;
                    break;

                case "CavalryScouts":
                    DataModel.CavalryScoutsSilver = _settingsService.ArmySettingsModel.CavalryScoutsSilver * DataModel.CavalryScouts;
                    DataModel.CavalryScoutsBase = _settingsService.ArmySettingsModel.CavalryScoutsBase * DataModel.CavalryScouts;
                    break;

                case "CavalryKnightTemplars":
                    DataModel.CavalryKnightTemplarsSilver = _settingsService.ArmySettingsModel.CavalryKnightTemplarsSilver * DataModel.CavalryKnightTemplars;
                    DataModel.CavalryKnightTemplarsBase = _settingsService.ArmySettingsModel.CavalryKnightTemplarsBase * DataModel.CavalryKnightTemplars;
                    break;

                case "CavalryHeavy":
                    DataModel.CavalryHeavySilver = _settingsService.ArmySettingsModel.CavalryHeavySilver * DataModel.CavalryHeavy;
                    DataModel.CavalryHeavyBase = _settingsService.ArmySettingsModel.CavalryHeavyBase * DataModel.CavalryHeavy;
                    break;

                case "CavalryElite":
                    DataModel.CavalryEliteSilver = _settingsService.ArmySettingsModel.CavalryEliteSilver * DataModel.CavalryElite;
                    DataModel.CavalryEliteBase = _settingsService.ArmySettingsModel.CavalryEliteBase * DataModel.CavalryElite;
                    break;

                case "OfficersCorporal":
                    DataModel.OfficersCorporalSilver = _settingsService.ArmySettingsModel.OfficersCorporalSilver * DataModel.OfficersCorporal;
                    break;

                case "OfficersSergeant":
                    DataModel.OfficersSergeantSilver = _settingsService.ArmySettingsModel.OfficersSergeantSilver * DataModel.OfficersSergeant;
                    break;

                case "OfficersCaptain":
                    DataModel.OfficersCaptainSilver = _settingsService.ArmySettingsModel.OfficersCaptainSilver * DataModel.OfficersCaptain;
                    break;
            }
        }

        #endregion
    }
}
