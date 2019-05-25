using System;
using FiefApp.Common.Infrastructure.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FiefApp.Common.Infrastructure.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IFiefService _fiefService;
        private readonly ISettingsService _settingsService;
        private readonly ICalculations _calculations;
        private readonly IBaseService _baseService;

        public IncomeService(
            IFiefService fiefService,
            ISettingsService settingsService,
            ICalculations calculations,
            IBaseService baseService
            )
        {
            _fiefService = fiefService;
            _settingsService = settingsService;
            _calculations = calculations;
            _baseService = baseService;
        }

        public ObservableCollection<IncomeModel> CheckIncomesCollection(int index)
        {
            List<IncomeModel> tempList = new List<IncomeModel>();
            int id = _baseService.GetNewIndustryId();

            if (index > 0)
            {
                IncomeModel temp =
                    new IncomeModel()
                    {
                        Id = -1,
                        Income = "Avrad",
                        Manor = _fiefService.InformationList[index].FiefName,
                        ManorId = index,
                        Silver = -1,
                        Luxury = -1,
                        Wood = -1,
                        Stone = -1,
                        Iron = -1,
                        IsStewardNeeded = false,
                        ShowInIncomes = true,
                        Spring = -1M,
                        Summer = -1M,
                        Fall = -1M,
                        Winter = -1M
                    };

                if (_fiefService.EmployeesList[index].Bailiff > 0)
                {
                    temp.Base = _calculations.GetAvrad(index);
                }
                else
                {
                    temp.Base = 0;
                }

                tempList.Add(temp);



                temp =
                    new IncomeModel()
                    {
                        Id = -1,
                        Income = "Skatter",
                        Manor = _fiefService.InformationList[index].FiefName,
                        ManorId = index,
                        Silver = -1,
                        Luxury = -1,
                        Wood = -1,
                        Stone = -1,
                        Iron = -1,
                        IsStewardNeeded = false,
                        ShowInIncomes = true,
                        Spring = -1M,
                        Summer = -1M,
                        Fall = -1M,
                        Winter = -1M
                    };

                if (_fiefService.EmployeesList[index].Bailiff > 0)
                {
                    temp.Base = _calculations.GetTaxes(index);
                }
                else
                {
                    temp.Base = 0;
                }

                tempList.Add(temp);



                temp =
                    new IncomeModel()
                    {
                        Id = -1,
                        Income = "Licensavgifter",
                        Manor = _fiefService.InformationList[index].FiefName,
                        ManorId = index,
                        Base = -1,
                        Luxury = -1,
                        Wood = -1,
                        Stone = -1,
                        Iron = -1,
                        IsStewardNeeded = false,
                        ShowInIncomes = true,
                        Spring = -1M,
                        Summer = -1M,
                        Fall = -1M,
                        Winter = -1M
                    };

                if (_fiefService.EmployeesList[index].Bailiff > 0)
                {
                    temp.Silver = _calculations.GetLicenseFees(index);
                }
                else
                {
                    temp.Silver = 0;
                }

                tempList.Add(temp);



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

                temp =
                    new IncomeModel()
                    {
                        Id = -1,
                        Income = "Tullar",
                        Manor = _fiefService.InformationList[index].FiefName,
                        ManorId = index,
                        Base = -1,
                        Luxury = -1,
                        Wood = -1,
                        Stone = -1,
                        Iron = -1,
                        IsStewardNeeded = false,
                        ShowInIncomes = true,
                        Spring = -1M,
                        Summer = -1M,
                        Fall = -1M,
                        Winter = -1M
                    };

                if (_fiefService.EmployeesList[index].Bailiff > 0)
                {
                    temp.Silver = Convert.ToInt32(Math.Floor(silver));
                }
                else
                {
                    temp.Silver = 0;
                }

                tempList.Add(temp);



                int difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.25 * _fiefService.WeatherList[index].SpringRollMod
                                                              + (decimal)0.25 * _fiefService.WeatherList[index].SummerRollMod
                                                              + (decimal)0.2 * _fiefService.WeatherList[index].FallRollMod
                                                              + (decimal)0.2 * _fiefService.WeatherList[index].WinterRollMod
                                                              + 8));
                if (difficulty < 5)
                {
                    difficulty = 4;
                }

                temp =
                    new IncomeModel()
                    {
                        Income = "Djurhållning",
                        Manor = _fiefService.InformationList[index].FiefName,
                        ManorId = index,
                        Silver = -1,
                        Luxury = -1,
                        Wood = -1,
                        Stone = -1,
                        Iron = -1,
                        IsStewardNeeded = true,
                        ShowInIncomes = true,
                        StewardsCollection = _baseService.GetStewardsCollection(),
                        Spring = 0.3M,
                        Summer = 0.3M,
                        Fall = 0.25M,
                        Winter = 0.25M,
                        Difficulty = difficulty
                    };

                if (!_fiefService.IncomeList[index].IncomesCollection.Any(o => o.Income == "Djurhållning"))
                {
                    temp.Id = id;
                    id++;
                    temp.StewardId = -1;
                    temp.Base = 0;
                }
                else
                {
                    temp.Id = _fiefService.IncomeList[index].IncomesCollection.FirstOrDefault(o => o.Income == "Djurhållning").Id;
                    if (_fiefService.StewardsDataModel.StewardsCollection.Count > 0)
                    {
                        if (_fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.IndustryId == temp.Id) != null)
                        {
                            temp.StewardId = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.IndustryId == temp.Id).Id;
                        }
                        else
                        {
                            temp.StewardId = -1;
                        }
                    }
                    else
                    {
                        temp.StewardId = -1;
                    }

                    if (temp.StewardId != -1)
                    {
                        temp.Base = _calculations.GetAnimalHusbandryIncome(index);
                    }
                    else
                    {
                        temp.Base = 0;
                    }
                }

                tempList.Add(temp);



                difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.9 * _fiefService.WeatherList[index].SpringRollMod
                                                          + (decimal)0.9 * _fiefService.WeatherList[index].SummerRollMod
                                                          + (decimal)0.25 * _fiefService.WeatherList[index].FallRollMod
                                                          + (decimal)0.1 * _fiefService.WeatherList[index].WinterRollMod
                                                          + 8));

                if (difficulty < 5)
                {
                    difficulty = 4;
                }

                temp =
                    new IncomeModel()
                    {
                        Income = "Jordbruk",
                        Manor = _fiefService.InformationList[index].FiefName,
                        ManorId = index,
                        Silver = -1,
                        Luxury = -1,
                        Wood = -1,
                        Stone = -1,
                        Iron = -1,
                        IsStewardNeeded = true,
                        ShowInIncomes = true,
                        StewardsCollection = _baseService.GetStewardsCollection(),
                        Spring = 0.9M,
                        Summer = 0.9M,
                        Fall = 0.1M,
                        Winter = 0.1M,
                        Difficulty = difficulty
                    };

                if (!_fiefService.IncomeList[index].IncomesCollection.Any(o => o.Income == "Jordbruk"))
                {
                    temp.Id = id;
                    id++;
                    temp.StewardId = -1;
                    temp.Base = 0;
                }
                else
                {
                    temp.Id = _fiefService.IncomeList[index].IncomesCollection.FirstOrDefault(o => o.Income == "Jordbruk").Id;
                    if (_fiefService.StewardsDataModel.StewardsCollection.Count > 0)
                    {
                        if (_fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.IndustryId == temp.Id) != null)
                        {
                            temp.StewardId = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.IndustryId == temp.Id).Id;
                        }
                        else
                        {
                            temp.StewardId = -1;
                        }
                    }
                    else
                    {
                        temp.StewardId = -1;
                    }

                    if (temp.StewardId != -1)
                    {
                        temp.Base = _calculations.GetAgricultureIncome(index);
                    }
                    else
                    {
                        temp.Base = 0;
                    }
                }

                tempList.Add(temp);



                difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.2 * _fiefService.WeatherList[index].SpringRollMod
                                                          + (decimal)0.2 * _fiefService.WeatherList[index].SummerRollMod
                                                          + (decimal)0.2 * _fiefService.WeatherList[index].FallRollMod
                                                          + (decimal)0.2 * _fiefService.WeatherList[index].WinterRollMod
                                                          + 8));

                if (difficulty < 5)
                {
                    difficulty = 4;
                }

                temp =
                    new IncomeModel()
                    {
                        Income = "Jakt",
                        Manor = _fiefService.InformationList[index].FiefName,
                        ManorId = index,
                        Silver = -1,
                        Luxury = -1,
                        Wood = -1,
                        Stone = -1,
                        Iron = -1,
                        IsStewardNeeded = true,
                        StewardsCollection = _baseService.GetStewardsCollection(),
                        Spring = 0.3M,
                        Summer = 0.3M,
                        Fall = 0.3M,
                        Winter = 0.3M,
                        Difficulty = difficulty
                    };

                if (!_fiefService.IncomeList[index].IncomesCollection.Any(o => o.Income == "Jakt"))
                {
                    temp.Id = id;
                    id++;
                    temp.StewardId = -1;
                    temp.Base = 0;
                }
                else
                {
                    temp.Id = _fiefService.IncomeList[index].IncomesCollection.FirstOrDefault(o => o.Income == "Jakt").Id;
                    if (_fiefService.StewardsDataModel.StewardsCollection.Count > 0)
                    {
                        if (_fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.IndustryId == temp.Id) != null)
                        {
                            temp.StewardId = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.IndustryId == temp.Id).Id;
                        }
                        else
                        {
                            temp.StewardId = -1;
                        }
                    }
                    else
                    {
                        temp.StewardId = -1;
                    }

                    if (temp.StewardId != -1)
                    {
                        temp.Base = Convert.ToInt32(Math.Floor((decimal)_fiefService.EmployeesList[index].Hunter * Convert.ToInt32(_fiefService.InformationList[index].HuntingQuality)));
                    }
                    else
                    {
                        temp.Base = 0;
                    }
                }

                if (_fiefService.EmployeesList[index].Hunter > 0)
                {
                    temp.ShowInIncomes = true;
                }
                else
                {
                    temp.ShowInIncomes = false;
                }

                tempList.Add(temp);



                difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.3 * _fiefService.WeatherList[index].SpringRollMod
                                                            + (decimal)0.3 * _fiefService.WeatherList[index].SummerRollMod
                                                            + (decimal)0.3 * _fiefService.WeatherList[index].FallRollMod
                                                            + 8));

                if (difficulty < 5)
                {
                    difficulty = 4;
                }

                temp =
                    new IncomeModel()
                    {
                        Income = "Fiske",
                        Manor = _fiefService.InformationList[index].FiefName,
                        ManorId = index,
                        Silver = -1,
                        Luxury = -1,
                        Wood = -1,
                        Stone = -1,
                        Iron = -1,
                        IsStewardNeeded = true,
                        StewardsCollection = _baseService.GetStewardsCollection(),
                        Spring = 0.4M,
                        Summer = 0.4M,
                        Fall = 0.4M,
                        Winter = 0.0M,
                        Difficulty = difficulty
                    };

                if (!_fiefService.IncomeList[index].IncomesCollection.Any(o => o.Income == "Fiske"))
                {
                    temp.Id = id;
                    id++;
                    temp.StewardId = -1;
                    temp.Base = 0;
                }
                else
                {
                    temp.Id = _fiefService.IncomeList[index].IncomesCollection.FirstOrDefault(o => o.Income == "Fiske").Id;
                    if (_fiefService.StewardsDataModel.StewardsCollection.Count > 0)
                    {
                        if (_fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.IndustryId == temp.Id) != null)
                        {
                            temp.StewardId = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.IndustryId == temp.Id).Id;
                        }
                        else
                        {
                            temp.StewardId = -1;
                        }
                    }
                    else
                    {
                        temp.StewardId = -1;
                    }

                    if (temp.StewardId != -1)
                    {
                        temp.Base = Convert.ToInt32(Math.Floor((decimal)_fiefService.WeatherList[index].NumberUsedFishingBoats * Convert.ToInt32(_fiefService.InformationList[index].FishingQuality)));
                    }
                    else
                    {
                        temp.Base = 0;
                    }
                }

                if (_fiefService.InformationList[index].River == "Ja"
                    || _fiefService.InformationList[index].Coast == "Ja"
                    || _fiefService.InformationList[index].Lake == "Ja")
                {
                    if (_fiefService.WeatherList[index].NumberOfFishingBoats > 0)
                    {
                        temp.ShowInIncomes = true;
                    }
                    else
                    {
                        temp.ShowInIncomes = false;
                    }
                }
                else
                {
                    temp.ShowInIncomes = false;
                }

                tempList.Add(temp);



                difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.15 * _fiefService.WeatherList[index].SpringRollMod
                                                            + (decimal)0.15 * _fiefService.WeatherList[index].SummerRollMod
                                                            + (decimal)0.15 * _fiefService.WeatherList[index].FallRollMod
                                                            + 8));

                if (difficulty < 5)
                {
                    difficulty = 4;
                }

                temp =
                    new IncomeModel()
                    {
                        Income = "Skogsavverkning",
                        Manor = _fiefService.InformationList[index].FiefName,
                        ManorId = index,
                        Base = -1,
                        Silver = -1,
                        Luxury = -1,
                        Stone = -1,
                        Iron = -1,
                        IsStewardNeeded = true,
                        StewardsCollection = _baseService.GetStewardsCollection(),
                        Spring = 0.3M,
                        Summer = 0.3M,
                        Fall = 0.3M,
                        Winter = 0.3M,
                        Difficulty = difficulty
                    };

                if (!_fiefService.IncomeList[index].IncomesCollection.Any(o => o.Income == "Skogsavverkning"))
                {
                    temp.Id = id;
                    id++;
                    temp.StewardId = -1;
                    temp.Wood = 0;
                }
                else
                {
                    temp.Id = _fiefService.IncomeList[index].IncomesCollection.FirstOrDefault(o => o.Income == "Skogsavverkning").Id;
                    if (_fiefService.StewardsDataModel.StewardsCollection.Count > 0)
                    {
                        if (_fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.IndustryId == temp.Id) != null)
                        {
                            temp.StewardId = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.IndustryId == temp.Id).Id;
                        }
                        else
                        {
                            temp.StewardId = -1;
                        }
                    }
                    else
                    {
                        temp.StewardId = -1;
                    }

                    if (temp.StewardId != -1)
                    {
                        temp.Wood = Convert.ToInt32(Math.Floor((decimal)_fiefService.WeatherList[index].Felling * 6 + (decimal)_fiefService.WeatherList[index].LandClearing * 6));
                    }
                    else
                    {
                        temp.Wood = 0;
                    }
                }

                if (_fiefService.WeatherList[index].Felling > 0
                    || _fiefService.WeatherList[index].LandClearing > 0)
                {
                    temp.ShowInIncomes = true;
                }
                else
                {
                    temp.ShowInIncomes = false;
                }

                tempList.Add(temp);
            }

            _fiefService.IncomeList[index].IncomesCollection = new ObservableCollection<IncomeModel>(tempList);
            return new ObservableCollection<IncomeModel>(tempList);
        }
    }
}
