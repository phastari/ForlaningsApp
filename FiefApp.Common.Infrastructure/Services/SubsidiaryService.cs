using System;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FiefApp.Common.Infrastructure.Services
{
    public class SubsidiaryService : ISubsidiaryService
    {
        private readonly IFiefService _fiefService;
        private readonly ISettingsService _settingsService;

        public SubsidiaryService(
            IFiefService fiefService,
            ISettingsService settingsService
            )
        {
            _fiefService = fiefService;
            _settingsService = settingsService;
        }

        public SubsidiaryDataModel GetAllSubsidiaryDataModel()
        {
            return null;
        }

        public ObservableCollection<SubsidiaryModel> GetSubsidiaryTypesCollection()
        {
            ObservableCollection<SubsidiaryModel> tempCollection = new ObservableCollection<SubsidiaryModel>();

            for (int x = 0; x < _settingsService.SubsidiarySettingsList.Count; x++)
            {
                tempCollection.Add(new SubsidiaryModel()
                {
                    Id = x,
                    Subsidiary = _settingsService.SubsidiarySettingsList[x].Subsidiary,
                    IncomeFactor = _settingsService.SubsidiarySettingsList[x].IncomeFactor,
                    IncomeSilver = _settingsService.SubsidiarySettingsList[x].IncomeSilver,
                    IncomeBase = _settingsService.SubsidiarySettingsList[x].IncomeBase,
                    IncomeLuxury = _settingsService.SubsidiarySettingsList[x].IncomeLuxury,
                    DaysWorkBuild = _settingsService.SubsidiarySettingsList[x].DaysWorkBuild,
                    DaysWorkUpkeep = _settingsService.SubsidiarySettingsList[x].DaysWorkUpkeep,
                    Spring = _settingsService.SubsidiarySettingsList[x].Spring,
                    Summer = _settingsService.SubsidiarySettingsList[x].Summer,
                    Fall = _settingsService.SubsidiarySettingsList[x].Fall,
                    Winter = _settingsService.SubsidiarySettingsList[x].Winter
                });
            }

            for (int x = 0; x < _fiefService.CustomSubsidiaryList.Count; x++)
            {
                tempCollection.Add(new SubsidiaryModel()
                {
                    Id = _fiefService.CustomSubsidiaryList[x].Id,
                    Subsidiary = _fiefService.CustomSubsidiaryList[x].Subsidiary,
                    IncomeFactor = _fiefService.CustomSubsidiaryList[x].IncomeFactor,
                    IncomeSilver = _fiefService.CustomSubsidiaryList[x].IncomeSilver,
                    IncomeBase = _fiefService.CustomSubsidiaryList[x].IncomeBase,
                    IncomeLuxury = _fiefService.CustomSubsidiaryList[x].IncomeLuxury,
                    DaysWorkBuild = _fiefService.CustomSubsidiaryList[x].DaysWorkBuild,
                    DaysWorkUpkeep = _fiefService.CustomSubsidiaryList[x].DaysWorkUpkeep,
                    Spring = _fiefService.CustomSubsidiaryList[x].Spring,
                    Summer = _fiefService.CustomSubsidiaryList[x].Summer,
                    Fall = _fiefService.CustomSubsidiaryList[x].Fall,
                    Winter = _fiefService.CustomSubsidiaryList[x].Winter
                });
            }

            return tempCollection;
        }

        public void AddCustomSubsidiary(SubsidiaryModel model)
        {
            bool addNew = true;

            for (int x = 0; x < _fiefService.CustomSubsidiaryList.Count; x++)
            {
                if (model.Subsidiary == _fiefService.CustomSubsidiaryList[x].Subsidiary)
                {
                    addNew = false;
                }
            }

            if (addNew)
            {
                _fiefService.CustomSubsidiaryList.Add(model);
            }
        }

        public int GetNewIdForSubsidiary()
        {
            List<int> tempList = new List<int>();

            for (int x = 0; x < _settingsService.SubsidiarySettingsList.Count; x++)
            {
                tempList.Add(x);
            }

            for (int x = 0; x < _fiefService.CustomSubsidiaryList.Count; x++)
            {
                tempList.Add(_fiefService.CustomSubsidiaryList[x].Id);
            }

            for (int x = 1; x < _fiefService.SubsidiaryList.Count; x++)
            {
                for (int y = 0; y < _fiefService.SubsidiaryList[x].SubsidiaryCollection.Count; y++)
                {
                    tempList.Add(_fiefService.SubsidiaryList[x].SubsidiaryCollection[y].Id);
                }
            }

            return tempList.Max() + 1;
        }

        public void EditCustomSubsidiary(SubsidiaryModel model)
        {
            for (int x = 0; x < _fiefService.CustomSubsidiaryList.Count; x++)
            {
                if (model.Id == _fiefService.CustomSubsidiaryList[x].Id)
                {
                    _fiefService.CustomSubsidiaryList.RemoveAt(x);
                    _fiefService.CustomSubsidiaryList.Insert(x, model);
                    break;
                }
            }
        }

        public ObservableCollection<StewardModel> GetAllStewards()
        {
            return _fiefService.StewardsList[0].StewardsCollection;
        }

        public void ChangeSteward(int stewardId, int subsidiaryId, string subsidiary)
        {
            for (int x = 0; x < _fiefService.StewardsList[0].StewardsCollection.Count; x++)
            {
                if (_fiefService.StewardsList[0].StewardsCollection[x].IndustryId == subsidiaryId && _fiefService.StewardsList[0].StewardsCollection[x].Industry == subsidiary)
                {
                    _fiefService.StewardsList[0].StewardsCollection[x].IndustryId = -1;
                    _fiefService.StewardsList[0].StewardsCollection[x].ManorId = -1;
                    _fiefService.StewardsList[0].StewardsCollection[x].Industry = "";
                }
            }

            for (int x = 0; x < _fiefService.StewardsList[0].StewardsCollection.Count; x++)
            {
                if (_fiefService.StewardsList[0].StewardsCollection[x].Id == stewardId)
                {
                    _fiefService.StewardsList[0].StewardsCollection[x].IndustryId = subsidiaryId;
                    _fiefService.StewardsList[0].StewardsCollection[x].ManorId = -1;
                    _fiefService.StewardsList[0].StewardsCollection[x].Industry = subsidiary;
                }
            }

            for (int x = 1; x < _fiefService.StewardsList.Count; x++)
            {
                _fiefService.StewardsList[x].StewardsCollection = _fiefService.StewardsList[0].StewardsCollection;
            }

            for (int x = 1; x < _fiefService.IncomeList.Count; x++)
            {
                for (int y = 0; y < _fiefService.IncomeList[x].IncomesCollection.Count; y++)
                {
                    if (_fiefService.IncomeList[x].IncomesCollection[y].StewardId == stewardId)
                    {
                        _fiefService.IncomeList[x].IncomesCollection[y].StewardId = -1;
                        _fiefService.IncomeList[x].IncomesCollection[y].Steward = "";
                    }
                }
            }
        }

        public ObservableCollection<SubsidiaryModel> GetSubsidiaryCollection(int index)
        {
            return _fiefService.SubsidiaryList[index].SubsidiaryCollection;
        }

        public ObservableCollection<SubsidiaryModel> GetConstructingCollection(int index)
        {
            return _fiefService.SubsidiaryList[index].ConstructingCollection;
        }

        public void ShowConstructingCollection()
        {
            Console.WriteLine($"_fiefService.SubsidiaryList[1].ConstructingCollection.Count : { _fiefService.SubsidiaryList[1].ConstructingCollection.Count }");
        }

        public void SetSubsidiary(int index, int id, SubsidiaryModel model)
        {
            for (int x = 0; x < _fiefService.SubsidiaryList[index].SubsidiaryCollection.Count; x++)
            {
                if (_fiefService.SubsidiaryList[index].SubsidiaryCollection[x].Id == id)
                {
                    _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].IncomeFactor = model.IncomeFactor;
                    _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].IncomeBase = model.IncomeBase;
                    _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].IncomeLuxury = model.IncomeLuxury;
                    _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].IncomeSilver = model.IncomeSilver;
                    _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].DaysWorkUpkeep = model.DaysWorkUpkeep;
                    _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].Spring = model.Spring;
                    _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].Summer = model.Summer;
                    _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].Fall = model.Fall;
                    _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].Winter = model.Winter;
                    _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].Quality = model.Quality;
                    _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].DevelopmentLevel = model.DevelopmentLevel;
                }
            }
        }

        public int GetAndSetDifficulty(int index, decimal spring, decimal summer, decimal fall, decimal winter)
        {
            int difficulty = Convert.ToInt32(Math.Ceiling(spring * _fiefService.WeatherList[index].SpringRollMod
                                                          + summer * _fiefService.WeatherList[index].SummerRollMod
                                                          + fall * _fiefService.WeatherList[index].FallRollMod
                                                          + winter * _fiefService.WeatherList[index].WinterRollMod
                                                          + 8));

            if (difficulty < 5)
            {
                return 4;
            }
            return difficulty;
        }

        public int GetSilverIncome(int quality, int developmentLevel, decimal incomeFactor, decimal silverFactor)
        {
            return Convert.ToInt32(Math.Floor(quality * incomeFactor * silverFactor
                                              + (quality * incomeFactor * silverFactor
                                                 / 100
                                                 * (developmentLevel - 1)
                                                 * 5)));
        }

        public int GetBaseIncome(int quality, int developmentLevel, decimal incomeFactor, decimal baseFactor)
        {
            return Convert.ToInt32(Math.Floor(quality * incomeFactor * baseFactor
                                              + (quality * incomeFactor * baseFactor
                                                 / 100
                                                 * (developmentLevel - 1)
                                                 * 5)));
        }

        public int GetLuxuryIncome(int quality, int developmentLevel, decimal incomeFactor, decimal luxuryFactor)
        {
            return Convert.ToInt32(Math.Floor(quality * incomeFactor * luxuryFactor
                                              + (quality * incomeFactor * luxuryFactor
                                                 / 100
                                                 * (developmentLevel - 1)
                                                 * 5)));
        }
    }
}
