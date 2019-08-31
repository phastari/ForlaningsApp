using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        public List<IncomeModel> SetIncomes(int index, ObservableCollection<IncomeModel> model)
        {
            bool containsAvrad = false;
            bool containsSkatter = false;
            bool containsLicensavgifter = false;
            bool containsTullar = false;
            bool containsDjurhallning = false;
            bool containsJordbruk = false;
            bool containsJakt = false;
            bool containsFiske = false;
            bool containsSkogsavverkning = false;

            for (int x = 0; x < model.Count; x++)
            {
                if (model[x].Income.Contains("Avrad"))
                {
                    containsAvrad = true;
                    if (model[x].Income.Contains("("))
                    {
                        int i = model[x].Income.IndexOf('(');
                        model[x].Income = model[x].Income.Remove(i);
                        model[x].Income = model[x].Income.Trim();
                    }
                    model[x].Base = _fiefService.EmployeesList[index].Bailiff > 0 ? _calculations.GetAvrad(index) : 0;
                }

                if (model[x].Income.Contains("Skatter"))
                {
                    containsSkatter = true;
                    if (model[x].Income.Contains("("))
                    {
                        int i = model[x].Income.IndexOf('(');
                        model[x].Income = model[x].Income.Remove(i);
                        model[x].Income = model[x].Income.Trim();
                    }
                    model[x].Base = _fiefService.EmployeesList[index].Bailiff > 0 ? _calculations.GetTaxes(index) : 0;
                }

                if (model[x].Income.Contains("Licensavgifter"))
                {
                    containsLicensavgifter = true;
                    if (model[x].Income.Contains("("))
                    {
                        int i = model[x].Income.IndexOf('(');
                        model[x].Income = model[x].Income.Remove(i);
                        model[x].Income = model[x].Income.Trim();
                    }
                    model[x].Silver = _fiefService.EmployeesList[index].Bailiff > 0 ? _calculations.GetLicenseFees(index) : 0;
                }

                if (model[x].Income.Contains("Tullar"))
                {
                    containsTullar = true;
                    if (model[x].Income.Contains("("))
                    {
                        int i = model[x].Income.IndexOf('(');
                        model[x].Income = model[x].Income.Remove(i);
                        model[x].Income = model[x].Income.Trim();
                    }
                    decimal silver = Convert.ToDecimal(Math.Sqrt(Math.Pow(_fiefService.WeatherList[index].ThisYearLuxury * 33.85, 2)))
                                     + Convert.ToDecimal(Math.Sqrt(Math.Pow(_fiefService.WeatherList[index].ThisYearBase * 9.35, 2)))
                                     + Convert.ToDecimal(Math.Sqrt(Math.Pow(_fiefService.WeatherList[index].ThisYearStone * 37.25, 2)))
                                     + Convert.ToDecimal(Math.Sqrt(Math.Pow(_fiefService.WeatherList[index].ThisYearWood * 25.25, 2)))
                                     + Convert.ToDecimal(Math.Sqrt(Math.Pow(_fiefService.WeatherList[index].ThisYearIron * 213 * 25.25, 2)));

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

                    if (_fiefService.PortsList[index].GotShipyard)
                    {
                        silver += silver * Convert.ToDecimal(Math.Pow(Convert.ToInt32(_fiefService.PortsList[x].Shipyard.Size), 3) / 100);
                    }

                    model[x].Silver = _fiefService.EmployeesList[index].Bailiff > 0 ? Convert.ToInt32(Math.Floor(silver)) : 0;
                }

                if (model[x].Income.Contains("Djurhållning"))
                {
                    containsDjurhallning = true;
                    if (model[x].Income.Contains("("))
                    {
                        int i = model[x].Income.IndexOf('(');
                        model[x].Income = model[x].Income.Remove(i);
                        model[x].Income = model[x].Income.Trim();
                    }
                    int difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.25 * _fiefService.WeatherList[index].SpringRollMod
                                                                  + (decimal)0.25 * _fiefService.WeatherList[index].SummerRollMod
                                                                  + (decimal)0.2 * _fiefService.WeatherList[index].FallRollMod
                                                                  + (decimal)0.2 * _fiefService.WeatherList[index].WinterRollMod
                                                                  + 8));
                    if (difficulty < 5)
                    {
                        difficulty = 4;
                    }

                    model[x].Difficulty = difficulty;

                    model[x].Base = model[x].StewardId > 0 ? _calculations.GetAnimalHusbandryIncome(index) : 0;
                }

                if (model[x].Income.Contains("Jordbruk"))
                {
                    containsJordbruk = true;
                    if (model[x].Income.Contains("("))
                    {
                        int i = model[x].Income.IndexOf('(');
                        model[x].Income = model[x].Income.Remove(i);
                        model[x].Income = model[x].Income.Trim();
                    }
                    int difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.9 * _fiefService.WeatherList[index].SpringRollMod
                                                                  + (decimal)0.9 * _fiefService.WeatherList[index].SummerRollMod
                                                                  + (decimal)0.25 * _fiefService.WeatherList[index].FallRollMod
                                                                  + (decimal)0.1 * _fiefService.WeatherList[index].WinterRollMod
                                                                  + 8));

                    if (difficulty < 5)
                    {
                        difficulty = 4;
                    }

                    model[x].Difficulty = difficulty;

                    model[x].Base = model[x].StewardId > 0 ? _calculations.GetAgricultureIncome(index) : 0;
                }

                if (model[x].Income.Contains("Jakt"))
                {
                    containsJakt = true;
                    if (model[x].Income.Contains("("))
                    {
                        int i = model[x].Income.IndexOf('(');
                        model[x].Income = model[x].Income.Remove(i);
                        model[x].Income = model[x].Income.Trim();
                    }
                    int difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.2 * _fiefService.WeatherList[index].SpringRollMod
                                                                  + (decimal)0.2 * _fiefService.WeatherList[index].SummerRollMod
                                                                  + (decimal)0.2 * _fiefService.WeatherList[index].FallRollMod
                                                                  + (decimal)0.2 * _fiefService.WeatherList[index].WinterRollMod
                                                                  + 8));

                    if (difficulty < 5)
                    {
                        difficulty = 4;
                    }

                    model[x].Difficulty = difficulty;

                    if (model[x].StewardId > 0 && _fiefService.EmployeesList[index].Hunter > 0)
                    {
                        model[x].Base = Convert.ToInt32(Math.Floor((decimal)_fiefService.EmployeesList[index].Hunter * Convert.ToInt32(_fiefService.InformationList[index].HuntingQuality)));
                    }
                    else
                    {
                        model[x].Base = 0;
                    }
                }

                if (model[x].Income.Contains("Fiske"))
                {
                    containsFiske = true;
                    if (model[x].Income.Contains("("))
                    {
                        int i = model[x].Income.IndexOf('(');
                        model[x].Income = model[x].Income.Remove(i);
                        model[x].Income = model[x].Income.Trim();
                    }
                    int difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.3 * _fiefService.WeatherList[index].SpringRollMod
                                                                  + (decimal)0.3 * _fiefService.WeatherList[index].SummerRollMod
                                                                  + (decimal)0.3 * _fiefService.WeatherList[index].FallRollMod
                                                                  + 8));

                    if (difficulty < 5)
                    {
                        difficulty = 4;
                    }

                    model[x].Difficulty = difficulty;

                    if (model[x].StewardId > 0 && _fiefService.WeatherList[index].NumberUsedFishingBoats > 0)
                    {
                        model[x].Base = Convert.ToInt32(Math.Floor((decimal)_fiefService.WeatherList[index].NumberUsedFishingBoats * Convert.ToInt32(_fiefService.InformationList[index].FishingQuality)));
                    }
                    else
                    {
                        model[x].Base = 0;
                    }

                    if (_fiefService.InformationList[index].Coast == "Ja"
                        || _fiefService.InformationList[index].River == "Ja"
                        || _fiefService.InformationList[index].Lake == "Ja")
                    {
                        model[x].ShowInIncomes = true;
                    }
                    else
                    {
                        model[x].ShowInIncomes = false;
                    }
                }

                if (model[x].Income.Contains("Skogsavverkning"))
                {
                    containsSkogsavverkning = true;
                    if (model[x].Income.Contains("("))
                    {
                        int i = model[x].Income.IndexOf('(');
                        model[x].Income = model[x].Income.Remove(i);
                        model[x].Income = model[x].Income.Trim();
                    }
                    int difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.15 * _fiefService.WeatherList[index].SpringRollMod 
                                                                  + (decimal)0.15 * _fiefService.WeatherList[index].SummerRollMod
                                                                  + (decimal)0.15 * _fiefService.WeatherList[index].FallRollMod
                                                                  + 8));

                    if (difficulty < 5)
                    {
                        difficulty = 4;
                    }

                    model[x].Difficulty = difficulty;

                    if (model[x].StewardId > 0 && (_fiefService.WeatherList[index].Felling > 0 || _fiefService.WeatherList[index].LandClearing > 0))
                    {
                        model[x].Wood = Convert.ToInt32(Math.Floor((decimal)_fiefService.WeatherList[index].Felling * 6 + (decimal)_fiefService.WeatherList[index].LandClearing * 6));
                    }
                    else
                    {
                        model[x].Wood = 0;
                    }

                    if (_fiefService.WeatherList[index].Felling > 0
                        || _fiefService.WeatherList[index].LandClearing > 0)
                    {
                        model[x].ShowInIncomes = true;
                    }
                    else
                    {
                        model[x].ShowInIncomes = false;
                    }
                }
            }

            int newId = _baseService.GetNewIndustryId();
            if (!containsAvrad)
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

                temp.Base = _fiefService.EmployeesList[index].Bailiff > 0 ? _calculations.GetAvrad(index) : 0;

                model.Add(temp);
            }

            if (!containsDjurhallning)
            {
                int difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.25 * _fiefService.WeatherList[index].SpringRollMod
                                                              + (decimal)0.25 * _fiefService.WeatherList[index].SummerRollMod
                                                              + (decimal)0.2 * _fiefService.WeatherList[index].FallRollMod
                                                              + (decimal)0.2 * _fiefService.WeatherList[index].WinterRollMod
                                                              + 8));
                if (difficulty < 5)
                {
                    difficulty = 4;
                }

                IncomeModel temp =
                    new IncomeModel()
                    {
                        Id = newId,
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
                        StewardId = 0,
                        Spring = 0.3M,
                        Summer = 0.3M,
                        Fall = 0.25M,
                        Winter = 0.25M,
                        Difficulty = difficulty
                    };

                temp.Base = temp.StewardId > 0 ? _calculations.GetAnimalHusbandryIncome(index) : 0;

                model.Add(temp);
                newId++;
            }

            if (!containsFiske)
            {
                int difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.3 * _fiefService.WeatherList[index].SpringRollMod
                                                              + (decimal)0.3 * _fiefService.WeatherList[index].SummerRollMod
                                                              + (decimal)0.3 * _fiefService.WeatherList[index].FallRollMod
                                                              + 8));

                if (difficulty < 5)
                {
                    difficulty = 4;
                }

                IncomeModel temp =
                    new IncomeModel()
                    {
                        Id = newId,
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
                        StewardId = 0,
                        Spring = 0.4M,
                        Summer = 0.4M,
                        Fall = 0.4M,
                        Winter = 0.0M,
                        Difficulty = difficulty
                    };

                if (temp.StewardId > 0 && _fiefService.WeatherList[index].NumberUsedFishingBoats > 0)
                {
                    temp.Base = Convert.ToInt32(Math.Floor((decimal)_fiefService.WeatherList[index].NumberUsedFishingBoats * Convert.ToInt32(_fiefService.InformationList[index].FishingQuality)));
                }
                else
                {
                    temp.Base = 0;
                }

                if (_fiefService.InformationList[index].River == "Ja"
                    || _fiefService.InformationList[index].Coast == "Ja"
                    || _fiefService.InformationList[index].Lake == "Ja")
                {
                    temp.ShowInIncomes = true;
                }
                else
                {
                    temp.ShowInIncomes = false;
                }

                model.Add(temp);
                newId++;
            }

            if (!containsJakt)
            {
                int difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.2 * _fiefService.WeatherList[index].SpringRollMod
                                                              + (decimal)0.2 * _fiefService.WeatherList[index].SummerRollMod
                                                              + (decimal)0.2 * _fiefService.WeatherList[index].FallRollMod
                                                              + (decimal)0.2 * _fiefService.WeatherList[index].WinterRollMod
                                                              + 8));

                if (difficulty < 5)
                {
                    difficulty = 4;
                }

                IncomeModel temp =
                    new IncomeModel()
                    {
                        Id = newId,
                        Income = "Jakt",
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
                        StewardId = 0,
                        Spring = 0.3M,
                        Summer = 0.3M,
                        Fall = 0.3M,
                        Winter = 0.3M,
                        Difficulty = difficulty
                    };

                if (temp.StewardId > 0 && _fiefService.EmployeesList[index].Hunter > 0)
                {
                    temp.Base = Convert.ToInt32(Math.Floor((decimal)_fiefService.EmployeesList[index].Hunter * Convert.ToInt32(_fiefService.InformationList[index].HuntingQuality)));
                }
                else
                {
                    temp.Base = 0;
                }
                model.Add(temp);
                newId++;
            }

            if (!containsJordbruk)
            {
                int difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.9 * _fiefService.WeatherList[index].SpringRollMod
                                                              + (decimal)0.9 * _fiefService.WeatherList[index].SummerRollMod
                                                              + (decimal)0.25 * _fiefService.WeatherList[index].FallRollMod
                                                              + (decimal)0.1 * _fiefService.WeatherList[index].WinterRollMod
                                                              + 8));

                if (difficulty < 5)
                {
                    difficulty = 4;
                }

                IncomeModel temp =
                    new IncomeModel()
                    {
                        Id = newId,
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
                        StewardId = 0,
                        Spring = 0.9M,
                        Summer = 0.9M,
                        Fall = 0.1M,
                        Winter = 0.1M,
                        Difficulty = difficulty
                    };

                temp.Base = temp.StewardId > 0 ? _calculations.GetAgricultureIncome(index) : 0;

                model.Add(temp);
                newId++;
            }

            if (!containsLicensavgifter)
            {
                IncomeModel temp =
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

                temp.Silver = _fiefService.EmployeesList[index].Bailiff > 0 ? _calculations.GetLicenseFees(index) : 0;

                model.Add(temp);
            }

            if (!containsSkatter)
            {
                IncomeModel temp =
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

                temp.Base = _fiefService.EmployeesList[index].Bailiff > 0 ? _calculations.GetTaxes(index) : 0;

                model.Add(temp);
            }

            if (!containsSkogsavverkning)
            {
                int difficulty = Convert.ToInt32(Math.Ceiling((decimal)0.15 * _fiefService.WeatherList[index].SpringRollMod
                                                              + (decimal)0.15 * _fiefService.WeatherList[index].SummerRollMod
                                                              + (decimal)0.15 * _fiefService.WeatherList[index].FallRollMod
                                                              + 8));

                if (difficulty < 5)
                {
                    difficulty = 4;
                }

                IncomeModel temp =
                    new IncomeModel()
                    {
                        Id = newId,
                        Income = "Skogsavverkning",
                        Manor = _fiefService.InformationList[index].FiefName,
                        ManorId = index,
                        Base = -1,
                        Silver = -1,
                        Luxury = -1,
                        Stone = -1,
                        Iron = -1,
                        IsStewardNeeded = true,
                        ShowInIncomes = true,
                        StewardsCollection = _baseService.GetStewardsCollection(),
                        StewardId = 0,
                        Spring = 0.3M,
                        Summer = 0.3M,
                        Fall = 0.3M,
                        Winter = 0.3M,
                        Difficulty = difficulty
                    };

                if (temp.StewardId > 0 && (_fiefService.WeatherList[index].Felling > 0 || _fiefService.WeatherList[index].LandClearing > 0))
                {
                    temp.Wood = Convert.ToInt32(Math.Floor((decimal)_fiefService.WeatherList[index].Felling * 6 + (decimal)_fiefService.WeatherList[index].LandClearing * 6));
                }
                else
                {
                    temp.Wood = 0;
                }

                model.Add(temp);
            }

            if (!containsTullar)
            {
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

                IncomeModel temp =
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

                temp.Silver = _fiefService.EmployeesList[index].Bailiff > 0 ? Convert.ToInt32(Math.Floor(silver)) : 0;

                model.Add(temp);
            }

            _fiefService.IncomeList[index].IncomesCollection = model;
            return new List<IncomeModel>(model);
        }

        public IncomeDataModel GetAllDataModel()
        {
            IncomeDataModel model = new IncomeDataModel();

            for (int x = 1; x < _fiefService.IncomeList.Count; x++)
            {
                for (int y = 0; y < _fiefService.IncomeList[x].IncomesCollection.Count; y++)
                {
                    model.IncomesCollection.Add(_fiefService.IncomeList[x].IncomesCollection[y]);
                    model.IncomesCollection[model.IncomesCollection.Count - 1].Income = $"{model.IncomesCollection[model.IncomesCollection.Count - 1].Income} ({_fiefService.InformationList[x].FiefName})";
                }
            }

            model.UpdateTotals();
            model.StewardsCollection = _fiefService.StewardsDataModel.StewardsCollection;

            return model;
        }
    }
}
