using System;
using FiefApp.Common.Infrastructure.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FiefApp.Common.Infrastructure.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IFiefService _fiefService;
        private readonly ISettingsService _settingsService;
        private readonly ICalculations _calculations;

        public IncomeService(
            IFiefService fiefService,
            ISettingsService settingsService,
            ICalculations calculations
            )
        {
            _fiefService = fiefService;
            _settingsService = settingsService;
            _calculations = calculations;
        }

        public ObservableCollection<IncomeModel> GetAllIncomes(int index)
        {
            List<IncomeModel> tempList = new List<IncomeModel>();


            if (index > 0)
            {
                for (int x = 1; x < _fiefService.InformationList.Count; x++)
                {
                    tempList.Add(
                        new IncomeModel()
                        {
                            Income = "Avrad",
                            Manor = _fiefService.InformationList[index].FiefName,
                            ManorId = index,
                            Base = _calculations.GetAvrad(index),
                            Silver = -1,
                            Luxury = -1,
                            Wood = -1,
                            Stone = -1,
                            Iron = -1,
                            IsStewardNeeded = false,
                            Spring = -1M,
                            Summer = -1M,
                            Fall = -1M,
                            Winter = -1M
                        });

                    tempList.Add(
                        new IncomeModel()
                        {
                            Income = "Skatter",
                            Manor = _fiefService.InformationList[index].FiefName,
                            ManorId = index,
                            Base = _calculations.GetTaxes(index),
                            Silver = -1,
                            Luxury = -1,
                            Wood = -1,
                            Stone = -1,
                            Iron = -1,
                            IsStewardNeeded = false,
                            Spring = -1M,
                            Summer = -1M,
                            Fall = -1M,
                            Winter = -1M
                        });

                    tempList.Add(
                        new IncomeModel()
                        {
                            Income = "Licensavgifter",
                            Manor = _fiefService.InformationList[index].FiefName,
                            ManorId = index,
                            Base = -1,
                            Silver = _calculations.GetLicenseFees(index),
                            Luxury = -1,
                            Wood = -1,
                            Stone = -1,
                            Iron = -1,
                            IsStewardNeeded = false,
                            Spring = -1M,
                            Summer = -1M,
                            Fall = -1M,
                            Winter = -1M
                        });

                    decimal silver = _fiefService.WeatherList[index].ThisYearLuxury * 33.85M
                                     + _fiefService.WeatherList[index].ThisYearBase * 9.35M
                                     + _fiefService.WeatherList[index].ThisYearStone * 37.25M
                                     + _fiefService.WeatherList[index].ThisYearWood * 25.25M
                                     + _fiefService.WeatherList[index].ThisYearIron * 213;

                    silver /= 10;
                    silver *= _fiefService.WeatherList[index].Tariffs;

                    if (_fiefService.InformationList[index].Roads == "Stigar")
                    {
                        silver *= 0.3M;
                    }
                    else if (_fiefService.InformationList[index].Roads == "Stenlagd väg")
                    {
                        silver *= 2.75M;
                    }
                    else if (_fiefService.InformationList[index].Roads == "Kunglig landsväg")
                    {
                        silver *= 5.25M;
                    }

                    tempList.Add(
                        new IncomeModel()
                        {
                            Income = "Tullar",
                            Manor = _fiefService.InformationList[index].FiefName,
                            ManorId = index,
                            Base = -1,
                            Silver = Convert.ToInt32(Math.Floor(silver)),
                            Luxury = -1,
                            Wood = -1,
                            Stone = -1,
                            Iron = -1,
                            IsStewardNeeded = false,
                            Spring = -1M,
                            Summer = -1M,
                            Fall = -1M,
                            Winter = -1M
                        });

                    int difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.25 * _fiefService.WeatherList[index].SpringRollMod
                                                                  + (decimal)0.25 * _fiefService.WeatherList[index].SummerRollMod
                                                                  + (decimal)0.2 * _fiefService.WeatherList[index].FallRollMod
                                                                  + (decimal)0.2 * _fiefService.WeatherList[index].WinterRollMod
                                                                  + 8));
                    if (difficulty < 5)
                    {
                        difficulty = 4;
                    }

                    int stewardId = -1;
                    for (int i = 0; i < _fiefService.StewardsList[0].StewardsCollection.Count; i++)
                    {
                        if (_fiefService.StewardsList[0].StewardsCollection[i].Industry == "Djurhållning" && _fiefService.StewardsList[0].StewardsCollection[i].ManorId == index)
                        {
                            stewardId = _fiefService.StewardsList[0].StewardsCollection[i].Id;
                        }
                    }

                    tempList.Add(
                        new IncomeModel()
                        {
                            Income = "Djurhållning",
                            Manor = _fiefService.InformationList[index].FiefName,
                            ManorId = index,
                            Base = _calculations.GetAnimalHusbandryIncome(index),
                            Silver = -1,
                            Luxury = -1,
                            Wood = -1,
                            Stone = -1,
                            Iron = -1,
                            IsStewardNeeded = true,
                            StewardsCollection = _fiefService.StewardsList[0].StewardsCollection,
                            StewardId = stewardId,
                            Spring = 0.3M,
                            Summer = 0.3M,
                            Fall = 0.25M,
                            Winter = 0.25M,
                            Difficulty = difficulty
                        });

                    difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.9 * _fiefService.WeatherList[index].SpringRollMod
                                                              + (decimal)0.9 * _fiefService.WeatherList[index].SummerRollMod
                                                              + (decimal)0.25 * _fiefService.WeatherList[index].FallRollMod
                                                              + (decimal)0.1 * _fiefService.WeatherList[index].WinterRollMod
                                                              + 8));

                    if (difficulty < 5)
                    {
                        difficulty = 4;
                    }

                    stewardId = -1;
                    for (int i = 0; i < _fiefService.StewardsList[0].StewardsCollection.Count; i++)
                    {
                        if (_fiefService.StewardsList[0].StewardsCollection[i].Industry == "Jordbruk" && _fiefService.StewardsList[0].StewardsCollection[i].ManorId == index)
                        {
                            stewardId = _fiefService.StewardsList[0].StewardsCollection[i].Id;
                        }
                    }

                    tempList.Add(
                        new IncomeModel()
                        {
                            Income = "Jordbruk",
                            Manor = _fiefService.InformationList[index].FiefName,
                            ManorId = index,
                            Base = _calculations.GetAgricultureIncome(index),
                            Silver = -1,
                            Luxury = -1,
                            Wood = -1,
                            Stone = -1,
                            Iron = -1,
                            IsStewardNeeded = true,
                            StewardsCollection = _fiefService.StewardsList[0].StewardsCollection,
                            StewardId = stewardId,
                            Spring = 0.9M,
                            Summer = 0.9M,
                            Fall = 0.1M,
                            Winter = 0.1M,
                            Difficulty = difficulty
                        });

                    difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.2 * _fiefService.WeatherList[index].SpringRollMod
                                                              + (decimal)0.2 * _fiefService.WeatherList[index].SummerRollMod
                                                              + (decimal)0.2 * _fiefService.WeatherList[index].FallRollMod
                                                              + (decimal)0.2 * _fiefService.WeatherList[index].WinterRollMod
                                                              + 8));

                    if (difficulty < 5)
                    {
                        difficulty = 4;
                    }

                    stewardId = -1;
                    for (int i = 0; i < _fiefService.StewardsList[0].StewardsCollection.Count; i++)
                    {
                        if (_fiefService.StewardsList[0].StewardsCollection[i].Industry == "Jakt" && _fiefService.StewardsList[0].StewardsCollection[i].ManorId == index)
                        {
                            stewardId = _fiefService.StewardsList[0].StewardsCollection[i].Id;
                        }
                    }

                    tempList.Add(
                        new IncomeModel()
                        {
                            Income = "Jakt",
                            Manor = _fiefService.InformationList[index].FiefName,
                            ManorId = index,
                            Base = Convert.ToInt32(Math.Floor((decimal)_fiefService.EmployeesList[index].Hunter * Convert.ToInt32(_fiefService.InformationList[index].HuntingQuality))),
                            Silver = -1,
                            Luxury = -1,
                            Wood = -1,
                            Stone = -1,
                            Iron = -1,
                            IsStewardNeeded = true,
                            StewardsCollection = _fiefService.StewardsList[0].StewardsCollection,
                            StewardId = stewardId,
                            Spring = 0.3M,
                            Summer = 0.3M,
                            Fall = 0.3M,
                            Winter = 0.3M,
                            Difficulty = difficulty
                        });

                    if (_fiefService.InformationList[index].River == "Ja"
                        || _fiefService.InformationList[index].Coast == "Ja"
                        || _fiefService.InformationList[index].Lake == "Ja")
                    {
                        difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.3 * _fiefService.WeatherList[index].SpringRollMod
                                                                  + (decimal)0.3 * _fiefService.WeatherList[index].SummerRollMod
                                                                  + (decimal)0.3 * _fiefService.WeatherList[index].FallRollMod
                                                                  + 8));

                        if (difficulty < 5)
                        {
                            difficulty = 4;
                        }

                        stewardId = -1;
                        for (int i = 0; i < _fiefService.StewardsList[0].StewardsCollection.Count; i++)
                        {
                            if (_fiefService.StewardsList[0].StewardsCollection[i].Industry == "Fiske" && _fiefService.StewardsList[0].StewardsCollection[i].ManorId == index)
                            {
                                stewardId = _fiefService.StewardsList[0].StewardsCollection[i].Id;
                            }
                        }

                        tempList.Add(
                            new IncomeModel()
                            {
                                Income = "Fiske",
                                Manor = _fiefService.InformationList[index].FiefName,
                                ManorId = index,
                                Base = Convert.ToInt32(Math.Floor((decimal)_fiefService.WeatherList[index].NumberUsedFishingBoats * Convert.ToInt32(_fiefService.InformationList[index].FishingQuality))),
                                Silver = -1,
                                Luxury = -1,
                                Wood = -1,
                                Stone = -1,
                                Iron = -1,
                                IsStewardNeeded = true,
                                StewardsCollection = _fiefService.StewardsList[0].StewardsCollection,
                                StewardId = stewardId,
                                Spring = 0.4M,
                                Summer = 0.4M,
                                Fall = 0.4M,
                                Winter = 0.0M,
                                Difficulty = difficulty
                            });
                    }

                    if (_fiefService.WeatherList[index].Felling > 0
                        || _fiefService.WeatherList[index].LandClearing > 0)
                    {
                        difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.15 * _fiefService.WeatherList[index].SpringRollMod
                                                                  + (decimal)0.15 * _fiefService.WeatherList[index].SummerRollMod
                                                                  + (decimal)0.15 * _fiefService.WeatherList[index].FallRollMod
                                                                  + 8));

                        if (difficulty < 5)
                        {
                            difficulty = 4;
                        }

                        stewardId = -1;
                        for (int i = 0; i < _fiefService.StewardsList[0].StewardsCollection.Count; i++)
                        {
                            if (_fiefService.StewardsList[0].StewardsCollection[i].Industry == "Skogsavverkning" && _fiefService.StewardsList[0].StewardsCollection[i].ManorId == index)
                            {
                                stewardId = _fiefService.StewardsList[0].StewardsCollection[i].Id;
                            }
                        }

                        tempList.Add(
                            new IncomeModel()
                            {
                                Income = "Skogsavverkning",
                                Manor = _fiefService.InformationList[index].FiefName,
                                ManorId = index,
                                Base = -1,
                                Silver = -1,
                                Luxury = -1,
                                Wood = Convert.ToInt32(Math.Floor((decimal)_fiefService.WeatherList[index].Felling * 6 + (decimal)_fiefService.WeatherList[index].LandClearing * 6)),
                                Stone = -1,
                                Iron = -1,
                                IsStewardNeeded = true,
                                StewardsCollection = _fiefService.StewardsList[0].StewardsCollection,
                                StewardId = stewardId,
                                Spring = 0.3M,
                                Summer = 0.3M,
                                Fall = 0.3M,
                                Winter = 0.3M,
                                Difficulty = difficulty
                            });
                    }
                }
            }

            return new ObservableCollection<IncomeModel>(tempList);
        }

        public ObservableCollection<StewardModel> GetAllStewards(int index)
        {
            return _fiefService.StewardsList[index].StewardsCollection;
        }

        public void ChangeSteward(int stewardId, int manorId, string income)
        {
            for (int x = 0; x < _fiefService.StewardsList[0].StewardsCollection.Count; x++)
            {
                if (_fiefService.StewardsList[0].StewardsCollection[x].IndustryId == manorId && _fiefService.StewardsList[0].StewardsCollection[x].Industry == income)
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
                    _fiefService.StewardsList[0].StewardsCollection[x].IndustryId = -1;
                    _fiefService.StewardsList[0].StewardsCollection[x].ManorId = manorId;
                    _fiefService.StewardsList[0].StewardsCollection[x].Industry = income;
                }
            }

            for (int x = 1; x < _fiefService.StewardsList.Count; x++)
            {
                _fiefService.StewardsList[x].StewardsCollection = _fiefService.StewardsList[0].StewardsCollection;
            }

            for (int x = 0; x < _fiefService.SubsidiaryList.Count; x++)
            {
                for (int y = 0; y < _fiefService.SubsidiaryList[x].ConstructingCollection.Count; y++)
                {
                    if (_fiefService.SubsidiaryList[x].ConstructingCollection[y].StewardId == stewardId)
                    {
                        _fiefService.SubsidiaryList[x].ConstructingCollection[y].StewardId = -1;
                        _fiefService.SubsidiaryList[x].ConstructingCollection[y].Steward = "";
                    }
                }

                for (int z = 0; z < _fiefService.SubsidiaryList[x].SubsidiaryCollection.Count; z++)
                {
                    if (_fiefService.SubsidiaryList[x].SubsidiaryCollection[z].StewardId == stewardId)
                    {
                        _fiefService.SubsidiaryList[x].SubsidiaryCollection[z].StewardId = -1;
                        _fiefService.SubsidiaryList[x].SubsidiaryCollection[z].Steward = "";
                    }
                }
            }
        }

        public ObservableCollection<IncomeModel> GetIncomeCollection(int index)
        {
            return _fiefService.IncomeList[index].IncomesCollection;
        }
    }
}
