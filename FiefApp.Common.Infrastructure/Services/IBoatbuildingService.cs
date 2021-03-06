﻿using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IBoatbuildingService
    {
        BoatbuildingDataModel GetAllBoatbuildingDataModel();
        bool RemoveBoatbuilder(int id);
        bool SaveBoatbuilder(BoatbuilderModel model);
        void ChangeBoatbuilder(int boatId, int boatbuilderId);
        void RemoveBoat(int boatId);
        int GetNewBuildingBoatId(int index);
        int GetNewBoatbuilderId();
        int GetNrVillageBoatbuilders(int index);
        bool GetGotShipyard(int index);
        bool GetUpgradingShipyard(int index);
        int GetVillageBoatBuilders(int index);
        void AddBoatToCompletedBoats(int index, BoatModel model);
        void AddFishingBoat(int index, int amount);
        int GetUsedSmallDocks(int index);
        int GetUsedVillageDocks(int index);
        int GetUsedMediumDocks(int index);
        int GetUsedLargeDocks(int index);
    }
}