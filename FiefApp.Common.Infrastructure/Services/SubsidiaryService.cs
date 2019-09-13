using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using System;
using System.Collections.ObjectModel;

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
            SubsidiaryDataModel model = new SubsidiaryDataModel();

            for (int x = 1; x < _fiefService.SubsidiaryList.Count; x++)
            {
                for (int y = 0; y < _fiefService.SubsidiaryList[x].SubsidiaryCollection.Count; y++)
                {
                    model.SubsidiaryCollection.Add(_fiefService.SubsidiaryList[x].SubsidiaryCollection[y]);
                    model.SubsidiaryCollection[model.SubsidiaryCollection.Count - 1].Subsidiary =
                        $"{model.SubsidiaryCollection[model.SubsidiaryCollection.Count - 1].Subsidiary} ({_fiefService.InformationList[x].FiefName})";
                }

                for (int z = 0; z < _fiefService.SubsidiaryList[x].ConstructingCollection.Count; z++)
                {
                    model.ConstructingCollection.Add(_fiefService.SubsidiaryList[x].ConstructingCollection[z]);
                    model.ConstructingCollection[model.ConstructingCollection.Count - 1].Subsidiary =
                        $"{model.ConstructingCollection[model.ConstructingCollection.Count - 1].Subsidiary} ({_fiefService.InformationList[x].FiefName})";
                }
            }

            model.IsAll = true;

            return model;
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
            int difficulty = 8;

            if (_fiefService.WeatherList[index].SpringRollMod > 0)
            {
                spring += _fiefService.WeatherList[index].SpringRollMod * 1.5M;
            }
            else
            {
                spring += _fiefService.WeatherList[index].SpringRollMod * 0.5M;
            }

            if (_fiefService.WeatherList[index].SummerRollMod > 0)
            {
                summer += _fiefService.WeatherList[index].SummerRollMod * 1.5M;
            }
            else
            {
                summer += _fiefService.WeatherList[index].SummerRollMod * 0.5M;
            }

            if (_fiefService.WeatherList[index].FallRollMod > 0)
            {
                fall += _fiefService.WeatherList[index].FallRollMod * 1.5M;
            }
            else
            {
                fall += _fiefService.WeatherList[index].FallRollMod * 0.5M;
            }

            if (_fiefService.WeatherList[index].WinterRollMod > 0)
            {
                winter += _fiefService.WeatherList[index].WinterRollMod * 1.5M;
            }
            else
            {
                winter += _fiefService.WeatherList[index].WinterRollMod * 0.5M;
            }

            difficulty += Convert.ToInt32(Math.Round(spring + summer + fall + winter));

            if (difficulty < 5)
            {
                difficulty = 4;
            }

            return difficulty;
        }

        public int GetSilverIncome(int quality, int developmentLevel, decimal incomeFactor, decimal silverFactor)
        {
            return Convert.ToInt32(Math.Floor(quality * incomeFactor * silverFactor * 540
                                              + (quality * incomeFactor * silverFactor * 540
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
