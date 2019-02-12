using FiefApp.Common.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class ArmyDataModel : INotifyPropertyChanged, IDataModelBase, ICloneable
    {
        private readonly ISettingsService _settingsService;

        public ArmyDataModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyCrossbowmen;
        public int ArmyCrossbowmen
        {
            get => _armyCrossbowmen;
            set
            {
                if (value > -1)
                {
                    _armyCrossbowmen = value;
                    ArmyCrossbowmenSilver = _settingsService.ArmySettingsModel.ArmyCrossbowmenSilver * value;
                    ArmyCrossbowmenBase = _settingsService.ArmySettingsModel.ArmyCrossbowmenBase * value;
                }
                else
                {
                    _armyCrossbowmen = 0;
                    ArmyCrossbowmenSilver = 0;
                    ArmyCrossbowmenBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyCrossbowmenSilver;
        public int ArmyCrossbowmenSilver
        {
            get => _armyCrossbowmenSilver;
            set
            {
                _armyCrossbowmenSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyCrossbowmenBase;
        public int ArmyCrossbowmenBase
        {
            get => _armyCrossbowmenBase;
            set
            {
                _armyCrossbowmenBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyBowmen;
        public int ArmyBowmen
        {
            get => _armyBowmen;
            set
            {
                if (value > -1)
                {
                    _armyBowmen = value;
                    ArmyBowmenSilver = _settingsService.ArmySettingsModel.ArmyBowmenSilver * value;
                    ArmyBowmenBase = _settingsService.ArmySettingsModel.ArmyBowmenBase * value;
                }
                else
                {
                    _armyBowmen = 0;
                    ArmyBowmenSilver = 0;
                    ArmyBowmenBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyBowmenSilver;
        public int ArmyBowmenSilver
        {
            get => _armyBowmenSilver;
            set
            {
                _armyBowmenSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyBowmenBase;
        public int ArmyBowmenBase
        {
            get => _armyBowmenBase;
            set
            {
                _armyBowmenBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyMedics;
        public int ArmyMedics
        {
            get => _armyMedics;
            set
            {
                if (value > -1)
                {
                    _armyMedics = value;
                    ArmyMedicsSilver = _settingsService.ArmySettingsModel.ArmyMedicsSilver * value;
                    ArmyMedicsBase = _settingsService.ArmySettingsModel.ArmyMedicsBase * value;
                }
                else
                {
                    _armyMedics = 0;
                    ArmyMedicsSilver = 0;
                    ArmyMedicsBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyMedicsSilver;
        public int ArmyMedicsSilver
        {
            get => _armyMedicsSilver;
            set
            {
                _armyMedicsSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyMedicsBase;
        public int ArmyMedicsBase
        {
            get => _armyMedicsBase;
            set
            {
                _armyMedicsBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyMedicsSkilled;
        public int ArmyMedicsSkilled
        {
            get => _armyMedicsSkilled;
            set
            {
                if (value > -1)
                {
                    _armyMedicsSkilled = value;
                    ArmyMedicsSkilledSilver = _settingsService.ArmySettingsModel.ArmyMedicsSkilledSilver * value;
                    ArmyMedicsSkilledBase = _settingsService.ArmySettingsModel.ArmyMedicsSkilledBase * value;
                }
                else
                {
                    _armyMedicsSkilled = 0;
                    ArmyMedicsSkilledSilver = 0;
                    ArmyMedicsSkilledBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyMedicsSkilledSilver;
        public int ArmyMedicsSkilledSilver
        {
            get => _armyMedicsSkilledSilver;
            set
            {
                _armyMedicsSkilledSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyMedicsSkilledBase;
        public int ArmyMedicsSkilledBase
        {
            get => _armyMedicsSkilledBase;
            set
            {
                _armyMedicsSkilledBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyInfantry;
        public int ArmyInfantry
        {
            get => _armyInfantry;
            set
            {
                if (value > -1)
                {
                    _armyInfantry = value;
                    ArmyInfantrySilver = _settingsService.ArmySettingsModel.ArmyInfantrySilver * value;
                    ArmyInfantryBase = _settingsService.ArmySettingsModel.ArmyInfantryBase * value;
                }
                else
                {
                    _armyInfantry = 0;
                    ArmyInfantrySilver = 0;
                    ArmyInfantryBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyInfantrySilver;
        public int ArmyInfantrySilver
        {
            get => _armyInfantrySilver;
            set
            {
                _armyInfantrySilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyInfantryBase;
        public int ArmyInfantryBase
        {
            get => _armyInfantryBase;
            set
            {
                _armyInfantryBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyInfantryMedium;
        public int ArmyInfantryMedium
        {
            get => _armyInfantryMedium;
            set
            {
                if (value > -1)
                {
                    _armyInfantryMedium = value;
                    ArmyInfantryMediumSilver = _settingsService.ArmySettingsModel.ArmyInfantryMediumSilver * value;
                    ArmyInfantryMediumBase = _settingsService.ArmySettingsModel.ArmyInfantryMediumBase * value;
                }
                else
                {
                    _armyInfantryMedium = 0;
                    ArmyInfantryMediumSilver = 0;
                    ArmyInfantryMediumBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyInfantryMediumSilver;
        public int ArmyInfantryMediumSilver
        {
            get => _armyInfantryMediumSilver;
            set
            {
                _armyInfantryMediumSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyInfantryMediumBase;
        public int ArmyInfantryMediumBase
        {
            get => _armyInfantryMediumBase;
            set
            {
                _armyInfantryMediumBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyInfantryHeavy;
        public int ArmyInfantryHeavy
        {
            get => _armyInfantryHeavy;
            set
            {
                if (value > -1)
                {
                    _armyInfantryHeavy = value;
                    ArmyInfantryHeavySilver = _settingsService.ArmySettingsModel.ArmyInfantryHeavySilver * value;
                    ArmyInfantryHeavyBase = _settingsService.ArmySettingsModel.ArmyInfantryHeavyBase * value;
                }
                else
                {
                    _armyInfantryHeavy = 0;
                    ArmyInfantryHeavySilver = 0;
                    ArmyInfantryHeavyBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyInfantryHeavySilver;
        public int ArmyInfantryHeavySilver
        {
            get => _armyInfantryHeavySilver;
            set
            {
                _armyInfantryHeavySilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyInfantryHeavyBase;
        public int ArmyInfantryHeavyBase
        {
            get => _armyInfantryHeavyBase;
            set
            {
                _armyInfantryHeavyBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyInfantryElite;
        public int ArmyInfantryElite
        {
            get => _armyInfantryElite;
            set
            {
                if (value > -1)
                {
                    _armyInfantryElite = value;
                    ArmyInfantryEliteSilver = _settingsService.ArmySettingsModel.ArmyInfantryEliteSilver * value;
                    ArmyInfantryEliteBase = _settingsService.ArmySettingsModel.ArmyInfantryEliteBase * value;
                }
                else
                {
                    _armyInfantryElite = 0;
                    ArmyInfantryEliteSilver = 0;
                    ArmyInfantryEliteBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyInfantryEliteSilver;
        public int ArmyInfantryEliteSilver
        {
            get => _armyInfantryEliteSilver;
            set
            {
                _armyInfantryEliteSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyInfantryEliteBase;
        public int ArmyInfantryEliteBase
        {
            get => _armyInfantryEliteBase;
            set
            {
                _armyInfantryEliteBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyLongbowmen;
        public int ArmyLongbowmen
        {
            get => _armyLongbowmen;
            set
            {
                if (value > -1)
                {
                    _armyLongbowmen = value;
                    ArmyLongbowmenSilver = _settingsService.ArmySettingsModel.ArmyLongbowmenSilver * value;
                    ArmyLongbowmenBase = _settingsService.ArmySettingsModel.ArmyLongbowmenBase * value;
                }
                else
                {
                    _armyLongbowmen = 0;
                    ArmyLongbowmenSilver = 0;
                    ArmyLongbowmenBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyLongbowmenSilver;
        public int ArmyLongbowmenSilver
        {
            get => _armyLongbowmenSilver;
            set
            {
                _armyLongbowmenSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyLongbowmenBase;
        public int ArmyLongbowmenBase
        {
            get => _armyLongbowmenBase;
            set
            {
                _armyLongbowmenBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyMercenary;
        public int ArmyMercenary
        {
            get => _armyMercenary;
            set
            {
                if (value > -1)
                {
                    _armyMercenary = value;
                    ArmyMercenarySilver = _settingsService.ArmySettingsModel.ArmyMercenarySilver * value;
                    ArmyMercenaryBase = _settingsService.ArmySettingsModel.ArmyMercenaryBase * value;
                }
                else
                {
                    _armyMercenary = 0;
                    ArmyMercenarySilver = 0;
                    ArmyMercenaryBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyMercenarySilver;
        public int ArmyMercenarySilver
        {
            get => _armyMercenarySilver;
            set
            {
                _armyMercenarySilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyMercenaryBase;
        public int ArmyMercenaryBase
        {
            get => _armyMercenaryBase;
            set
            {
                _armyMercenaryBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyMercenaryElite;
        public int ArmyMercenaryElite
        {
            get => _armyMercenaryElite;
            set
            {
                if (value > -1)
                {
                    _armyMercenaryElite = value;
                    ArmyMercenaryEliteSilver = _settingsService.ArmySettingsModel.ArmyMercenaryEliteSilver * value;
                    ArmyMercenaryEliteBase = _settingsService.ArmySettingsModel.ArmyMercenaryEliteBase * value;
                }
                else
                {
                    _armyMercenaryElite = 0;
                    ArmyMercenaryEliteSilver = 0;
                    ArmyMercenaryEliteBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyMercenaryEliteSilver;
        public int ArmyMercenaryEliteSilver
        {
            get => _armyMercenaryEliteSilver;
            set
            {
                _armyMercenaryEliteSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyMercenaryEliteBase;
        public int ArmyMercenaryEliteBase
        {
            get => _armyMercenaryEliteBase;
            set
            {
                _armyMercenaryEliteBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyMercenaryBowmen;
        public int ArmyMercenaryBowmen
        {
            get => _armyMercenaryBowmen;
            set
            {
                if (value > -1)
                {
                    _armyMercenaryBowmen = value;
                    ArmyMercenaryBowmenSilver = _settingsService.ArmySettingsModel.ArmyMercenaryBowmenSilver * value;
                    ArmyMercenaryBowmenBase = _settingsService.ArmySettingsModel.ArmyMercenaryBowmenBase * value;
                }
                else
                {
                    _armyMercenaryBowmen = 0;
                    ArmyMercenaryBowmenSilver = 0;
                    ArmyMercenaryBowmenBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyMercenaryBowmenSilver;
        public int ArmyMercenaryBowmenSilver
        {
            get => _armyMercenaryBowmenSilver;
            set
            {
                _armyMercenaryBowmenSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyMercenaryBowmenBase;
        public int ArmyMercenaryBowmenBase
        {
            get => _armyMercenaryBowmenBase;
            set
            {
                _armyMercenaryBowmenBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyEngineers;
        public int ArmyEngineers
        {
            get => _armyEngineers;
            set
            {
                if (value > -1)
                {
                    _armyEngineers = value;
                    ArmyEngineersSilver = _settingsService.ArmySettingsModel.ArmyEngineersSilver * value;
                    ArmyEngineersBase = _settingsService.ArmySettingsModel.ArmyEngineersBase * value;
                }
                else
                {
                    _armyEngineers = 0;
                    ArmyEngineersSilver = 0;
                    ArmyEngineersBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyEngineersSilver;
        public int ArmyEngineersSilver
        {
            get => _armyEngineersSilver;
            set
            {
                _armyEngineersSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyEngineersBase;
        public int ArmyEngineersBase
        {
            get => _armyEngineersBase;
            set
            {
                _armyEngineersBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armySpearmen;
        public int ArmySpearmen
        {
            get => _armySpearmen;
            set
            {
                if (value > -1)
                {
                    _armySpearmen = value;
                    ArmySpearmenSilver = _settingsService.ArmySettingsModel.ArmySpearmenSilver * value;
                    ArmySpearmenBase = _settingsService.ArmySettingsModel.ArmySpearmenBase * value;
                }
                else
                {
                    _armySpearmen = 0;
                    ArmySpearmenSilver = 0;
                    ArmySpearmenBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armySpearmenSilver;
        public int ArmySpearmenSilver
        {
            get => _armySpearmenSilver;
            set
            {
                _armySpearmenSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armySpearmenBase;
        public int ArmySpearmenBase
        {
            get => _armySpearmenBase;
            set
            {
                _armySpearmenBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyScouts;
        public int ArmyScouts
        {
            get => _armyScouts;
            set
            {
                if (value > -1)
                {
                    _armyScouts = value;
                    ArmyScoutsSilver = _settingsService.ArmySettingsModel.ArmyScoutsSilver * value;
                    ArmyScoutsBase = _settingsService.ArmySettingsModel.ArmyScoutsBase * value;
                }
                else
                {
                    _armyScouts = 0;
                    ArmyScoutsSilver = 0;
                    ArmyScoutsBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyScoutsSilver;
        public int ArmyScoutsSilver
        {
            get => _armyScoutsSilver;
            set
            {
                _armyScoutsSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyScoutsBase;
        public int ArmyScoutsBase
        {
            get => _armyScoutsBase;
            set
            {
                _armyScoutsBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyScoutsSkilled;
        public int ArmyScoutsSkilled
        {
            get => _armyScoutsSkilled;
            set
            {
                if (value > -1)
                {
                    _armyScoutsSkilled = value;
                    ArmyScoutsSkilledSilver = _settingsService.ArmySettingsModel.ArmyScoutsSkilledSilver * value;
                    ArmyScoutsSkilledBase = _settingsService.ArmySettingsModel.ArmyScoutsSkilledBase * value;
                }
                else
                {
                    _armyScoutsSkilled = 0;
                    ArmyScoutsSkilledSilver = 0;
                    ArmyScoutsSkilledBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyScoutsSkilledSilver;
        public int ArmyScoutsSkilledSilver
        {
            get => _armyScoutsSkilledSilver;
            set
            {
                _armyScoutsSkilledSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyScoutsSkilledBase;
        public int ArmyScoutsSkilledBase
        {
            get => _armyScoutsSkilledBase;
            set
            {
                _armyScoutsSkilledBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyKnightTemplars;
        public int ArmyKnightTemplars
        {
            get => _armyKnightTemplars;
            set
            {
                if (value > -1)
                {
                    _armyKnightTemplars = value;
                    ArmyKnightTemplarsSilver = _settingsService.ArmySettingsModel.ArmyKnightTemplarsSilver * value;
                    ArmyKnightTemplarsBase = _settingsService.ArmySettingsModel.ArmyKnightTemplarsBase * value;
                }
                else
                {
                    _armyKnightTemplars = 0;
                    ArmyKnightTemplarsSilver = 0;
                    ArmyKnightTemplarsBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyKnightTemplarsSilver;
        public int ArmyKnightTemplarsSilver
        {
            get => _armyKnightTemplarsSilver;
            set
            {
                _armyKnightTemplarsSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyKnightTemplarsBase;
        public int ArmyKnightTemplarsBase
        {
            get => _armyKnightTemplarsBase;
            set
            {
                _armyKnightTemplarsBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyGuards;
        public int ArmyGuards
        {
            get => _armyGuards;
            set
            {
                if (value > -1)
                {
                    _armyGuards = value;
                    ArmyGuardsSilver = _settingsService.ArmySettingsModel.ArmyGuardsSilver * value;
                    ArmyGuardsBase = _settingsService.ArmySettingsModel.ArmyGuardsBase * value;
                }
                else
                {
                    _armyGuards = 0;
                    ArmyGuardsSilver = 0;
                    ArmyGuardsBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyGuardsSilver;
        public int ArmyGuardsSilver
        {
            get => _armyGuardsSilver;
            set
            {
                _armyGuardsSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyGuardsBase;
        public int ArmyGuardsBase
        {
            get => _armyGuardsBase;
            set
            {
                _armyGuardsBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyWeaponmasters;
        public int ArmyWeaponmasters
        {
            get => _armyWeaponmasters;
            set
            {
                if (value > -1)
                {
                    _armyWeaponmasters = value;
                    ArmyWeaponmastersSilver = _settingsService.ArmySettingsModel.ArmyWeaponmastersSilver * value;
                    ArmyWeaponmastersBase = _settingsService.ArmySettingsModel.ArmyWeaponmastersBase * value;
                }
                else
                {
                    _armyWeaponmasters = 0;
                    ArmyWeaponmastersSilver = 0;
                    ArmyWeaponmastersBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _armyWeaponmastersSilver;
        public int ArmyWeaponmastersSilver
        {
            get => _armyWeaponmastersSilver;
            set
            {
                _armyWeaponmastersSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyWeaponmastersBase;
        public int ArmyWeaponmastersBase
        {
            get => _armyWeaponmastersBase;
            set
            {
                _armyWeaponmastersBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _cavalryBowmen;
        public int CavalryBowmen
        {
            get => _cavalryBowmen;
            set
            {
                if (value > -1)
                {
                    _cavalryBowmen = value;
                    CavalryBowmenSilver = _settingsService.ArmySettingsModel.CavalryBowmenSilver * value;
                    CavalryBowmenBase = _settingsService.ArmySettingsModel.CavalryBowmenBase * value;
                }
                else
                {
                    _cavalryBowmen = 0;
                    CavalryBowmenSilver = 0;
                    CavalryBowmenBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _cavalryBowmenSilver;
        public int CavalryBowmenSilver
        {
            get => _cavalryBowmenSilver;
            set
            {
                _cavalryBowmenSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _cavalryBowmenBase;
        public int CavalryBowmenBase
        {
            get => _cavalryBowmenBase;
            set
            {
                _cavalryBowmenBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _cavalryCourier;
        public int CavalryCourier
        {
            get => _cavalryCourier;
            set
            {
                if (value > -1)
                {
                    _cavalryCourier = value;
                    CavalryCourierSilver = _settingsService.ArmySettingsModel.CavalryCourierSilver * value;
                    CavalryCourierBase = _settingsService.ArmySettingsModel.CavalryCourierBase * value;
                }
                else
                {
                    _cavalryCourier = 0;
                    CavalryCourierSilver = 0;
                    CavalryCourierBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _cavalryCourierSilver;
        public int CavalryCourierSilver
        {
            get => _cavalryCourierSilver;
            set
            {
                _cavalryCourierSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _cavalryCourierBase;
        public int CavalryCourierBase
        {
            get => _cavalryCourierBase;
            set
            {
                _cavalryCourierBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _cavalryLight;
        public int CavalryLight
        {
            get => _cavalryLight;
            set
            {
                if (value > -1)
                {
                    _cavalryLight = value;
                    CavalryLightSilver = _settingsService.ArmySettingsModel.CavalryLightSilver * value;
                    CavalryLightBase = _settingsService.ArmySettingsModel.CavalryLightBase * value;
                }
                else
                {
                    _cavalryLight = 0;
                    CavalryLightSilver = 0;
                    CavalryLightBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _cavalryLightSilver;
        public int CavalryLightSilver
        {
            get => _cavalryLightSilver;
            set
            {
                _cavalryLightSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _cavalryLightBase;
        public int CavalryLightBase
        {
            get => _cavalryLightBase;
            set
            {
                _cavalryLightBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _cavalryKnights;
        public int CavalryKnights
        {
            get => _cavalryKnights;
            set
            {
                if (value > -1)
                {
                    _cavalryKnights = value;
                    CavalryKnightsSilver = _settingsService.ArmySettingsModel.CavalryKnightsSilver * value;
                    CavalryKnightsBase = _settingsService.ArmySettingsModel.CavalryKnightsBase * value;
                }
                else
                {
                    _cavalryKnights = 0;
                    CavalryKnightsSilver = 0;
                    CavalryKnightsBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _cavalryKnightsSilver;
        public int CavalryKnightsSilver
        {
            get => _cavalryKnightsSilver;
            set
            {
                _cavalryKnightsSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _cavalryKnightsBase;
        public int CavalryKnightsBase
        {
            get => _cavalryKnightsBase;
            set
            {
                _cavalryKnightsBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _cavalryScouts;
        public int CavalryScouts
        {
            get => _cavalryScouts;
            set
            {
                if (value > -1)
                {
                    _cavalryScouts = value;
                    CavalryScoutsSilver = _settingsService.ArmySettingsModel.CavalryScoutsSilver * value;
                    CavalryScoutsBase = _settingsService.ArmySettingsModel.CavalryScoutsBase * value;
                }
                else
                {
                    _cavalryScouts = 0;
                    CavalryScoutsSilver = 0;
                    CavalryScoutsBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _cavalryScoutsSilver;
        public int CavalryScoutsSilver
        {
            get => _cavalryScoutsSilver;
            set
            {
                _cavalryScoutsSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _cavalryScoutsBase;
        public int CavalryScoutsBase
        {
            get => _cavalryScoutsBase;
            set
            {
                _cavalryScoutsBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _cavalryKnightTemplars;
        public int CavalryKnightTemplars
        {
            get => _cavalryKnightTemplars;
            set
            {
                if (value > -1)
                {
                    _cavalryKnightTemplars = value;
                    CavalryKnightTemplarsSilver = _settingsService.ArmySettingsModel.CavalryKnightTemplarsSilver * value;
                    CavalryKnightTemplarsBase = _settingsService.ArmySettingsModel.CavalryKnightTemplarsBase * value;
                }
                else
                {
                    _cavalryKnightTemplars = 0;
                    CavalryKnightTemplarsSilver = 0;
                    CavalryKnightTemplarsBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _cavalryKnightTemplarsSilver;
        public int CavalryKnightTemplarsSilver
        {
            get => _cavalryKnightTemplarsSilver;
            set
            {
                _cavalryKnightTemplarsSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _cavalryKnightTemplarsBase;
        public int CavalryKnightTemplarsBase
        {
            get => _cavalryKnightTemplarsBase;
            set
            {
                _cavalryKnightTemplarsBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _cavalryHeavy;
        public int CavalryHeavy
        {
            get => _cavalryHeavy;
            set
            {
                if (value > -1)
                {
                    _cavalryHeavy = value;
                    CavalryHeavySilver = _settingsService.ArmySettingsModel.CavalryHeavySilver * value;
                    CavalryHeavyBase = _settingsService.ArmySettingsModel.CavalryHeavyBase * value;
                }
                else
                {
                    _cavalryHeavy = 0;
                    CavalryHeavySilver = 0;
                    CavalryHeavyBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _cavalryHeavySilver;
        public int CavalryHeavySilver
        {
            get => _cavalryHeavySilver;
            set
            {
                _cavalryHeavySilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _cavalryHeavyBase;
        public int CavalryHeavyBase
        {
            get => _cavalryHeavyBase;
            set
            {
                _cavalryHeavyBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _cavalryElite;
        public int CavalryElite
        {
            get => _cavalryElite;
            set
            {
                if (value > -1)
                {
                    _cavalryElite = value;
                    CavalryEliteSilver = _settingsService.ArmySettingsModel.CavalryEliteSilver * value;
                    CavalryEliteBase = _settingsService.ArmySettingsModel.CavalryEliteBase * value;
                }
                else
                {
                    _cavalryElite = 0;
                    CavalryEliteSilver = 0;
                    CavalryEliteBase = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _cavalryEliteSilver;
        public int CavalryEliteSilver
        {
            get => _cavalryEliteSilver;
            set
            {
                _cavalryEliteSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _cavalryEliteBase;
        public int CavalryEliteBase
        {
            get => _cavalryEliteBase;
            set
            {
                _cavalryEliteBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _officersCorporal;
        public int OfficersCorporal
        {
            get => _officersCorporal;
            set
            {
                if (value > -1)
                {
                    _officersCorporal = value;
                    OfficersCorporalSilver = _settingsService.ArmySettingsModel.OfficersCorporalSilver * value;
                }
                
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _officersCorporalSilver;
        public int OfficersCorporalSilver
        {
            get => _officersCorporalSilver;
            set
            {
                _officersCorporalSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _officersSergeant;
        public int OfficersSergeant
        {
            get => _officersSergeant;
            set
            {
                if (value > -1)
                {
                    _officersSergeant = value;
                    OfficersSergeantSilver = _settingsService.ArmySettingsModel.OfficersSergeantSilver * value;
                }
                else
                {
                    _officersSergeant = 0;
                    OfficersSergeantSilver = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _officersSergeantSilver;
        public int OfficersSergeantSilver
        {
            get => _officersSergeantSilver;
            set
            {
                _officersSergeantSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _officersCaptain;
        public int OfficersCaptain
        {
            get => _officersCaptain;
            set
            {
                if (value > -1)
                {
                    _officersCaptain = value;
                    OfficersCaptainSilver = _settingsService.ArmySettingsModel.OfficersCaptainSilver * value;
                }
                else
                {
                    _officersCaptain = 0;
                    OfficersCaptainSilver = 0;
                }
                NotifyPropertyChanged();
                UpdateTotals();
            }
        }

        private int _officersCaptainSilver;
        public int OfficersCaptainSilver
        {
            get => _officersCaptainSilver;
            set
            {
                _officersCaptainSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _totalSilver;
        public int TotalSilver
        {
            get => _totalSilver;
            set
            {
                _totalSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _totalBase;
        public int TotalBase
        {
            get => _totalBase;
            set
            {
                _totalBase = value;
                NotifyPropertyChanged();
            }
        }

        private List<SoldierModel> _templarKnightsList = new List<SoldierModel>();
        public List<SoldierModel> TemplarKnightsList
        {
            get => _templarKnightsList;
            set => _templarKnightsList = value;
        }

        private List<SoldierModel> _knightsList = new List<SoldierModel>();
        public List<SoldierModel> KnightsList
        {
            get => _knightsList;
            set => _knightsList = value;
        }

        private List<SoldierModel> _cavalryTemplarKnightsList = new List<SoldierModel>();
        public List<SoldierModel> CavalryTemplarKnightsList
        {
            get => _cavalryTemplarKnightsList;
            set => _cavalryTemplarKnightsList = value;
        }

        private List<SoldierModel> _officerCorporalsList = new List<SoldierModel>();
        public List<SoldierModel> OfficerCorporalsList
        {
            get => _officerCorporalsList;
            set => _officerCorporalsList = value;
        }

        private List<SoldierModel> _officerSergeantsList = new List<SoldierModel>();
        public List<SoldierModel> OfficerSergeantsList
        {
            get => _officerSergeantsList;
            set => _officerSergeantsList = value;
        }

        private List<SoldierModel> _officerCaptainsList = new List<SoldierModel>();
        public List<SoldierModel> OfficerCaptainsList
        {
            get => _officerCaptainsList;
            set => _officerCaptainsList = value;
        }

        #region Methods

        private void UpdateTotals()
        {
            TotalSilver = ArmyCrossbowmenSilver
                          + ArmyBowmenSilver
                          + ArmyMedicsSilver
                          + ArmyMedicsSkilledSilver
                          + ArmyInfantrySilver
                          + ArmyInfantryEliteSilver
                          + ArmyInfantryMediumSilver
                          + ArmyInfantryHeavySilver
                          + ArmyLongbowmenSilver
                          + ArmyMercenarySilver
                          + ArmyMercenaryBowmenSilver
                          + ArmyMercenaryEliteSilver
                          + ArmyEngineersSilver
                          + ArmySpearmenSilver
                          + ArmyScoutsSilver
                          + ArmyScoutsSkilledSilver
                          + ArmyKnightTemplarsSilver
                          + ArmyGuardsSilver
                          + ArmyWeaponmastersSilver
                          + CavalryBowmenSilver
                          + CavalryCourierSilver
                          + CavalryLightSilver
                          + CavalryKnightsSilver
                          + CavalryKnightTemplarsSilver
                          + CavalryScoutsSilver
                          + CavalryHeavySilver
                          + CavalryEliteSilver
                          + OfficersCorporalSilver
                          + OfficersSergeantSilver
                          + OfficersCaptainSilver;

            TotalBase = ArmyCrossbowmenBase
                        + ArmyBowmenBase
                        + ArmyMedicsBase
                        + ArmyMedicsSkilledBase
                        + ArmyInfantryBase
                        + ArmyInfantryEliteBase
                        + ArmyInfantryMediumBase
                        + ArmyInfantryHeavyBase
                        + ArmyLongbowmenBase
                        + ArmyMercenaryBase
                        + ArmyMercenaryBowmenBase
                        + ArmyMercenaryEliteBase
                        + ArmyEngineersBase
                        + ArmySpearmenBase
                        + ArmyScoutsBase
                        + ArmyScoutsSkilledBase
                        + ArmyKnightTemplarsBase
                        + ArmyGuardsBase
                        + ArmyWeaponmastersBase
                        + CavalryBowmenBase
                        + CavalryCourierBase
                        + CavalryLightBase
                        + CavalryKnightsBase
                        + CavalryKnightTemplarsBase
                        + CavalryScoutsBase
                        + CavalryHeavyBase
                        + CavalryEliteBase;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
