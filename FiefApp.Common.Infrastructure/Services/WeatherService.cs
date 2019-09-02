using System;
using System.Collections.Generic;
using FiefApp.Common.Infrastructure.DataModels;
using System.Linq;

namespace FiefApp.Common.Infrastructure.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IFiefService _fiefService;

        public WeatherService(IFiefService fiefService)
        {
            _fiefService = fiefService;
        }

        public WeatherDataModel GetAllWeatherDataModels()
        {
            return new WeatherDataModel();
        }

        public int GetTotalAmountOfSerfs(int index)
        {
            if (index != 0)
            {
                return _fiefService.ManorList[index].VillagesCollection.Sum(t => t.Serfdoms);
            }

            int serfs = 0;
            for (int x = 1; x < _fiefService.ManorList.Count; x++)
            {
                serfs += _fiefService.ManorList[x].VillagesCollection.Sum(t => t.Serfdoms);
            }
            return serfs;
        }

        public int GetTotalAmountOfSlaves(int index)
        {
            if (index != 0)
            {
                return _fiefService.ExpensesList[index].Slaves;
            }

            int slaves = 0;
            for (int x = 1; x < _fiefService.ExpensesList.Count; x++)
            {
                slaves += _fiefService.ExpensesList[x].Slaves;
            }
            return slaves;
        }

        public int GetTotalNumberOfSubsidaries(int index)
        {
            int nr = 0;
            if (index != 0)
            {
                nr += _fiefService.SubsidiaryList[index].SubsidiaryCollection.Count
                      + _fiefService.SubsidiaryList[index].ConstructingCollection.Count;

                if (_fiefService.PortsList[index].BuildingShipyard)
                {
                    nr++;
                }

                return nr;
            }

            int subsidiaries = 0;
            for (int x = 1; x < _fiefService.SubsidiaryList.Count; x++)
            {
                subsidiaries += _fiefService.SubsidiaryList[x].SubsidiaryCollection.Count
                                + _fiefService.SubsidiaryList[x].ConstructingCollection.Count;

                if (_fiefService.PortsList[x].BuildingShipyard
                    || _fiefService.PortsList[x].GotShipyard)
                {
                    subsidiaries++;
                }
            }
            return subsidiaries;
        }

        public int GetTotalAmountOfDaysworkFromSubsidiaries(int index)
        {
            int subsidiariesDayswork = 0;

            if (index != 0)
            {
                subsidiariesDayswork += _fiefService.SubsidiaryList[index].SubsidiaryCollection.Sum(t => t.DaysWorkThisYear)
                                     + _fiefService.SubsidiaryList[index].ConstructingCollection.Sum(t => t.DaysWorkThisYear);

                if (_fiefService.PortsList[index].GotShipyard
                    || _fiefService.PortsList[index].BuildingShipyard)
                {
                    subsidiariesDayswork += _fiefService.PortsList[index].Shipyard.DaysWorkThisYear;
                }

                return subsidiariesDayswork;
            }

            for (int x = 1; x < _fiefService.SubsidiaryList.Count; x++)
            {
                subsidiariesDayswork += _fiefService.SubsidiaryList[x].SubsidiaryCollection.Sum(t => t.DaysWorkThisYear)
                                        + _fiefService.SubsidiaryList[x].ConstructingCollection.Sum(t => t.DaysWorkThisYear);

                if (_fiefService.PortsList[x].GotShipyard
                    || _fiefService.PortsList[x].BuildingShipyard)
                {
                    subsidiariesDayswork += _fiefService.PortsList[x].Shipyard.DaysWorkThisYear;
                }
            }
            return subsidiariesDayswork;
        }

        public int GetNumberOfMinesAndQuarries(int index)
        {
            if (index != 0)
            {
                return _fiefService.MinesList[index].MinesCollection.Count
                       + _fiefService.MinesList[index].QuarriesCollection.Count;
            }
            int nr = 0;
            for (int x = 1; x < _fiefService.MinesList.Count; x++)
            {
                nr += _fiefService.MinesList[x].MinesCollection.Count
                   + _fiefService.MinesList[x].QuarriesCollection.Count;
            }

            return nr;
        }

        public int GetTotalAmountOfDaysWorkFromQuarries(int index)
        {
            if (index != 0)
            {
                if (_fiefService.MinesList[index].QuarriesCollection != null)
                {
                    return _fiefService.MinesList[index].QuarriesCollection.Sum(o => o.DaysWorkThisYear);
                }
            }

            int dayswork = 0;
            for (int x = 1; x < _fiefService.MinesList.Count; x++)
            {
                dayswork += _fiefService.MinesList[x].QuarriesCollection.Sum(o => o.DaysWorkThisYear);
            }

            return dayswork;
        }

        public int GetForecastForSilver(int index)
        {
            int silver = 0;
            if (index != 0)
            {

                silver = _fiefService.IncomeList[index].TotalSilver
                         - _fiefService.PortsList[index].TotalSilver
                       - _fiefService.ExpensesList[index].ExpensesSilver;

                for (int x = 0; x < _fiefService.MinesList[index].MinesCollection.Count; x++)
                {
                    if (int.TryParse(_fiefService.MinesList[index].MinesCollection[x].Result, out var temp))
                    {
                        silver += temp;
                    }
                }

                return silver;
            }

            for (int x = 1; x < _fiefService.IncomeList.Count; x++)
            {
                silver += _fiefService.IncomeList[x].TotalSilver
                          - _fiefService.ExpensesList[x].ExpensesSilver;

                for (int z = 0; z < _fiefService.MinesList[x].MinesCollection.Count; z++)
                {
                    if (int.TryParse(_fiefService.MinesList[x].MinesCollection[z].Result, out var temp))
                    {
                        silver += temp;
                    }
                }
            }

            return silver;
        }

        public int GetForecastForBase(int index)
        {
            if (index != 0)
            {
                int b = 0;
                b += _fiefService.IncomeList[index].TotalBase
                     - _fiefService.ExpensesList[index].ExpensesBase
                     + (int)_fiefService.SubsidiaryList[index].SubsidiaryCollection.Sum(o => o.IncomeBase)
                     - _fiefService.PortsList[index].TotalBase;
            }

            int i = 0;
            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {
                i += _fiefService.IncomeList[x].TotalBase
                     - _fiefService.ExpensesList[x].ExpensesBase;
            }

            return i;
        }

        public int GetForecastForLuxury(int index)
        {
            if (index != 0)
            {
                return _fiefService.IncomeList[index].TotalLuxury
                    - _fiefService.ExpensesList[index].ExpensesLuxury;
            }

            int luxury = 0;
            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {
                luxury += _fiefService.IncomeList[x].TotalLuxury
                          - _fiefService.ExpensesList[x].ExpensesLuxury;
            }

            return luxury;
        }

        public int GetForecastForIron(int index)
        {
            if (index != 0)
            {
                return _fiefService.IncomeList[index].TotalIron
                    - _fiefService.ExpensesList[index].ExpensesIron;
            }

            int iron = 0;
            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {
                iron += _fiefService.IncomeList[x].TotalIron
                        - _fiefService.ExpensesList[x].ExpensesIron;
            }

            return iron;
        }

        public int GetForecastForStone(int index)
        {
            if (index != 0)
            {
                return _fiefService.IncomeList[index].TotalStone
                    - _fiefService.ExpensesList[index].ExpensesStone;
            }

            int stone = 0;
            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {
                stone += _fiefService.IncomeList[x].TotalStone
                         - _fiefService.ExpensesList[x].ExpensesStone;
            }

            return stone;
        }

        public int GetForecastForWood(int index)
        {
            if (index != 0)
            {
                return _fiefService.IncomeList[index].TotalWood
                    - _fiefService.ExpensesList[index].ExpensesWood;
            }

            int wood = 0;
            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {
                wood += _fiefService.IncomeList[x].TotalWood
                        - _fiefService.ExpensesList[x].ExpensesWood;
            }

            return wood;
        }

        public int GetManorAcres(int index)
        {
            if (index != 0)
            {
                return _fiefService.ManorList[index].ManorAcres;
            }

            int acres = 0;
            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {
                acres += _fiefService.ManorList[x].ManorAcres;
            }

            return acres;
        }

        public int GetNumberOfFishingboats(int index)
        {
            if (index != 0)
            {
                return _fiefService.PortsList[index].FishingBoats;
            }

            int fishingBoats = 0;
            for (int x = 1; x < _fiefService.PortsList.Count; x++)
            {
                fishingBoats += _fiefService.PortsList[x].FishingBoats;
            }

            return fishingBoats;
        }

        public void SetNumberOfFishingboats(int index, int fishingboats)
        {
            _fiefService.PortsList[index].FishingBoats = fishingboats;
        }

        public int GetMaxLandClearing(int index)
        {
            if (index != 0)
            {
                return _fiefService.ManorList[index].ManorWoodland;
            }

            int landClearing = 0;
            for (int x = 1; x < _fiefService.ManorList.Count; x++)
            {
                landClearing += _fiefService.ManorList[x].ManorWoodland;
            }

            return landClearing;
        }

        public int GetMaxFelling(int index)
        {
            if (index != 0)
            {
                return _fiefService.ManorList[index].ManorWoodland;
            }

            int felling = 0;
            for (int x = 1; x < _fiefService.ManorList.Count; x++)
            {
                felling += _fiefService.ManorList[x].ManorWoodland;
            }

            return felling;
        }

        public int GetMaxUseless(int index)
        {
            if (index != 0)
            {
                return _fiefService.ManorList[index].ManorUseless;
            }

            int useless = 0;
            for (int x = 1; x < _fiefService.ManorList.Count; x++)
            {
                useless += _fiefService.ManorList[x].ManorUseless;
            }

            return useless;
        }

        public int GetMaxLandClearFelling(int index)
        {
            if (index != 0)
            {
                return _fiefService.ManorList[index].ManorFelling;
            }

            int landClearFelling = 0;
            for (int x = 1; x < _fiefService.ManorList.Count; x++)
            {
                landClearFelling += _fiefService.ManorList[x].ManorFelling;
            }

            return landClearFelling;
        }

        public bool CheckAllWeather()
        {
            List<bool> tempList = new List<bool>();
            for (int x = 1; x < _fiefService.WeatherList.Count; x++)
            {
                if (_fiefService.WeatherList[x].SpringRoll != null)
                {
                    if (_fiefService.WeatherList[x].SpringRoll > 0)
                    {
                        tempList.Add(true);
                    }
                    else
                    {
                        tempList.Add(false);
                    }
                }
                else
                {
                    tempList.Add(false);
                }

                if (_fiefService.WeatherList[x].SummerRoll != null)
                {
                    if (_fiefService.WeatherList[x].SummerRoll > 0)
                    {
                        tempList.Add(true);
                    }
                    else
                    {
                        tempList.Add(false);
                    }
                }
                else
                {
                    tempList.Add(false);
                }

                if (_fiefService.WeatherList[x].FallRoll != null)
                {
                    if (_fiefService.WeatherList[x].FallRoll > 0)
                    {
                        tempList.Add(true);
                    }
                    else
                    {
                        tempList.Add(false);
                    }
                }
                else
                {
                    tempList.Add(false);
                }

                if (_fiefService.WeatherList[x].WinterRoll != null)
                {
                    if (_fiefService.WeatherList[x].WinterRoll > 0)
                    {
                        tempList.Add(true);
                    }
                    else
                    {
                        tempList.Add(false);
                    }
                }
                else
                {
                    tempList.Add(false);
                }
            }

            if (tempList.Contains(false))
            {
                return false;
            }
            return true;
        }
    }
}
