﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public class ArmyService : IArmyService
    {
        private readonly IFiefService _fiefService;
        private readonly ISettingsService _settingsService;

        public ArmyService(
            IFiefService fiefService, 
            ISettingsService settingsService
            )
        {
            _fiefService = fiefService;
            _settingsService = settingsService;
        }

        public ArmyDataModel GetAllArmyDataModel()
        {
            ArmyDataModel tempDataModel = _fiefService.ArmyList[0];

            tempDataModel.ArmyCrossbowmen = 0;
            tempDataModel.ArmyBowmen = 0;
            tempDataModel.ArmyMedics = 0;
            tempDataModel.ArmyMedicsSkilled = 0;
            tempDataModel.ArmyInfantry = 0;
            tempDataModel.ArmyInfantryMedium = 0;
            tempDataModel.ArmyInfantryHeavy = 0;
            tempDataModel.ArmyInfantryElite = 0;
            tempDataModel.ArmyLongbowmen = 0;
            tempDataModel.ArmyMercenary = 0;
            tempDataModel.ArmyMercenaryElite = 0;
            tempDataModel.ArmyMercenaryBowmen = 0;
            tempDataModel.ArmyEngineers = 0;
            tempDataModel.ArmySpearmen = 0;
            tempDataModel.ArmyScouts = 0;
            tempDataModel.ArmyScoutsSkilled = 0;
            tempDataModel.ArmyKnightTemplars = 0;
            tempDataModel.ArmyGuards = 0;
            tempDataModel.ArmyWeaponmasters = 0;
            tempDataModel.CavalryBowmen = 0;
            tempDataModel.CavalryCourier = 0;
            tempDataModel.CavalryLight = 0;
            tempDataModel.CavalryKnights = 0;
            tempDataModel.CavalryScouts = 0;
            tempDataModel.CavalryKnightTemplars = 0;
            tempDataModel.CavalryHeavy = 0;
            tempDataModel.CavalryElite = 0;
            tempDataModel.OfficersCorporal = 0;
            tempDataModel.OfficersSergeant = 0;
            tempDataModel.OfficersCaptain = 0;

            for (int x = 1; x < _fiefService.ArmyList.Count; x++)
            {
                tempDataModel.ArmyCrossbowmen += _fiefService.ArmyList[x].ArmyCrossbowmen;
                tempDataModel.ArmyBowmen += _fiefService.ArmyList[x].ArmyBowmen;
                tempDataModel.ArmyMedics += _fiefService.ArmyList[x].ArmyMedics;
                tempDataModel.ArmyMedicsSkilled += _fiefService.ArmyList[x].ArmyMedicsSkilled;
                tempDataModel.ArmyInfantry += _fiefService.ArmyList[x].ArmyInfantry;
                tempDataModel.ArmyInfantryMedium += _fiefService.ArmyList[x].ArmyInfantryMedium;
                tempDataModel.ArmyInfantryHeavy += _fiefService.ArmyList[x].ArmyInfantryHeavy;
                tempDataModel.ArmyInfantryElite += _fiefService.ArmyList[x].ArmyInfantryElite;
                tempDataModel.ArmyLongbowmen += _fiefService.ArmyList[x].ArmyLongbowmen;
                tempDataModel.ArmyMercenary += _fiefService.ArmyList[x].ArmyMercenary;
                tempDataModel.ArmyMercenaryElite += _fiefService.ArmyList[x].ArmyMercenaryElite;
                tempDataModel.ArmyMercenaryBowmen += _fiefService.ArmyList[x].ArmyMercenaryBowmen;
                tempDataModel.ArmyEngineers += _fiefService.ArmyList[x].ArmyEngineers;
                tempDataModel.ArmySpearmen += _fiefService.ArmyList[x].ArmySpearmen;
                tempDataModel.ArmyScouts += _fiefService.ArmyList[x].ArmyScouts;
                tempDataModel.ArmyScoutsSkilled += _fiefService.ArmyList[x].ArmyScoutsSkilled;
                tempDataModel.ArmyKnightTemplars += _fiefService.ArmyList[x].ArmyKnightTemplars;
                tempDataModel.ArmyGuards += _fiefService.ArmyList[x].ArmyGuards;
                tempDataModel.ArmyWeaponmasters += _fiefService.ArmyList[x].ArmyWeaponmasters;
                tempDataModel.CavalryBowmen += _fiefService.ArmyList[x].CavalryBowmen;
                tempDataModel.CavalryCourier += _fiefService.ArmyList[x].CavalryCourier;
                tempDataModel.CavalryLight += _fiefService.ArmyList[x].CavalryLight;
                tempDataModel.CavalryKnights += _fiefService.ArmyList[x].CavalryKnights;
                tempDataModel.CavalryScouts += _fiefService.ArmyList[x].CavalryScouts;
                tempDataModel.CavalryKnightTemplars += _fiefService.ArmyList[x].CavalryKnightTemplars;
                tempDataModel.CavalryHeavy += _fiefService.ArmyList[x].CavalryHeavy;
                tempDataModel.CavalryElite += _fiefService.ArmyList[x].CavalryElite;
                tempDataModel.OfficersCorporal += _fiefService.ArmyList[x].OfficersCorporal;
                tempDataModel.OfficersSergeant += _fiefService.ArmyList[x].OfficersSergeant;
                tempDataModel.OfficersCaptain += _fiefService.ArmyList[x].OfficersCaptain;
            }

            return tempDataModel;
        }

        public int GetPeopleId(int index)
        {
            List<int> tempList = new List<int>();

            for (int x = 0; x < _fiefService.ArmyList[index].TemplarKnightsList.Count; x++)
            {
                tempList.Add(_fiefService.ArmyList[index].TemplarKnightsList[x].Id);
            }

            for (int x = 0; x < _fiefService.ArmyList[index].CavalryTemplarKnightsList.Count; x++)
            {
                tempList.Add(_fiefService.ArmyList[index].CavalryTemplarKnightsList[x].Id);
            }

            for (int x = 0; x < _fiefService.ArmyList[index].KnightsList.Count; x++)
            {
                tempList.Add(_fiefService.ArmyList[index].KnightsList[x].Id);
            }

            for (int x = 0; x < _fiefService.ArmyList[index].OfficerCorporalsList.Count; x++)
            {
                tempList.Add(_fiefService.ArmyList[index].OfficerCorporalsList[x].Id);
            }

            for (int x = 0; x < _fiefService.ArmyList[index].OfficerSergeantsList.Count; x++)
            {
                tempList.Add(_fiefService.ArmyList[index].OfficerSergeantsList[x].Id);
            }

            for (int x = 0; x < _fiefService.ArmyList[index].OfficerCaptainsList.Count; x++)
            {
                tempList.Add(_fiefService.ArmyList[index].OfficerCaptainsList[x].Id);
            }

            for (int x = 0; x < _fiefService.ManorList[index].ResidentsList.Count; x++)
            {
                tempList.Add(_fiefService.ManorList[index].ResidentsList[x].Id);
            }

            if (tempList.Count > 0)
            {
                return tempList.Max() + 1;
            }
            else
            {
                return 0;
            }
        }

        public void UpdateSilverExpenses(int index, int silver)
        {
            int oldSilver = _fiefService.ExpensesList[index].ArmySilver;
            _fiefService.ExpensesList[index].ArmySilver = silver;
            _fiefService.ExpensesList[index].ExpensesSilver += -oldSilver + silver;
        }

        public void UpdateBaseExpenses(int index, int abase)
        {
            int oldBase = _fiefService.ExpensesList[index].ArmyBase;
            _fiefService.ExpensesList[index].ArmyBase = abase;
            _fiefService.ExpensesList[index].ExpensesBase += -oldBase + abase;
        }
    }
}
