using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.Controls.iTextBox.RoutedEvents;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FiefApp.Module.Army
{
    public class ArmyViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IArmyService _armyService;
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
            _eventAggregator.GetEvent<UpdateAllEvent>().Subscribe(UpdateAndRespond);
            _eventAggregator.GetEvent<UpdateEvent>().Subscribe(UpdateResponse);
            _eventAggregator.GetEvent<UpdateResponseEvent>().Subscribe(HandleUpdateEvent);
            _eventAggregator.GetEvent<EndOfYearCompletedEvent>().Subscribe(HandleEndOfYearComplete);
            _eventAggregator.GetEvent<SaveEvent>().Subscribe(ExecuteSaveEventResponse);
        }

        private void ExecuteSaveEventResponse()
        {
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<ArmyDataModel>(x);
                _armyService.UpdateSilverExpenses(x, DataModel.TotalSilver);
                _armyService.UpdateBaseExpenses(x, DataModel.TotalBase);
                SaveData(x);
            }

            _eventAggregator.GetEvent<SaveEventResponse>().Publish(new SaveEventParameters()
            {
                Completed = true,
                ModuleName = "Army"
            });

            DataModel = _baseService.GetDataModel<ArmyDataModel>(Index);
        }

        #region Event Handlers

        private void HandleEndOfYearComplete()
        {
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<ArmyDataModel>(x);
                _armyService.UpdateSilverExpenses(x, DataModel.TotalSilver);
                _armyService.UpdateBaseExpenses(x, DataModel.TotalBase);
                SaveData(x);
            }
        }

        private void HandleUpdateEvent(UpdateEventParameters param)
        {
            if (param.Publisher == "Army"
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
            if (str != "Army")
            {
                UpdateFiefCollection();
                for (int x = 1; x < FiefCollection.Count; x++)
                {
                    DataModel = _baseService.GetDataModel<ArmyDataModel>(x);
                    _armyService.UpdateSilverExpenses(x, DataModel.TotalSilver);
                    _armyService.UpdateBaseExpenses(x, DataModel.TotalBase);
                    SaveData(x);
                }

                _eventAggregator.GetEvent<UpdateResponseEvent>().Publish(new UpdateEventParameters()
                {
                    ModuleName = "Army",
                    Completed = true,
                    Publisher = str
                });
            }
        }

        private void UpdateAndRespond()
        {
            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<ArmyDataModel>(x);
                _armyService.UpdateSilverExpenses(x, DataModel.TotalSilver);
                _armyService.UpdateBaseExpenses(x, DataModel.TotalBase);
                SaveData(x);
            }

            _eventAggregator.GetEvent<UpdateAllResponseEvent>().Publish(new UpdateAllEventParameters()
            {
                ModuleName = "Army",
                Completed = true
            });
        }

        #endregion

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

        private void CompleteLoadData()
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

        protected override void SaveData(int index = -1)
        {
            _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
            if (DataModel != null
                && index == -1)
            {
                _armyService.UpdateSilverExpenses(Index, DataModel.TotalSilver);
                _armyService.UpdateBaseExpenses(Index, DataModel.TotalBase);
            }
            else
            {
                _armyService.UpdateSilverExpenses(index, DataModel.TotalSilver);
                _armyService.UpdateBaseExpenses(index, DataModel.TotalBase);
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
