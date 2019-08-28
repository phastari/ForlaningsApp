using System;
using System.Linq;
using FiefApp.Common.Infrastructure.DataModels;

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
            else
            {
                int serfs = 0;
                for (int x = 1; x < _fiefService.ManorList.Count; x++)
                {
                    serfs += _fiefService.ManorList[x].VillagesCollection.Sum(t => t.Serfdoms);
                }
                return serfs;
            }
        }

        public int GetTotalAmountOfSlaves(int index)
        {
            if (index != 0)
            {
                return _fiefService.ExpensesList[index].Slaves;
            }
            else
            {
                int slaves = 0;
                for (int x = 1; x < _fiefService.ExpensesList.Count; x++)
                {
                    slaves += _fiefService.ExpensesList[x].Slaves;
                }
                return slaves;
            }
        }

        public int GetTotalNumberOfSubsidaries(int index)
        {
            if (index != 0)
            {
                return _fiefService.SubsidiaryList[index].SubsidiaryCollection.Count
                   + _fiefService.SubsidiaryList[index].ConstructingCollection.Count;
            }
            else
            {
                int subsidiaries = 0;
                for (int x = 1; x < _fiefService.SubsidiaryList.Count; x++)
                {
                    subsidiaries += _fiefService.SubsidiaryList[x].SubsidiaryCollection.Count
                        + _fiefService.SubsidiaryList[x].ConstructingCollection.Count;
                }
                return subsidiaries;
            }
        }

        public int GetTotalAmountOfDaysworkFromSubsidiaries(int index)
        {
            if (index != 0)
            {
                return _fiefService.SubsidiaryList[index].SubsidiaryCollection.Sum(t => t.DaysWorkThisYear)
                   + _fiefService.SubsidiaryList[index].ConstructingCollection.Sum(t => t.DaysWorkThisYear);
            }
            else
            {
                int subsidiariesDayswork = 0;
                for (int x = 1; x < _fiefService.SubsidiaryList.Count; x++)
                {
                    subsidiariesDayswork += _fiefService.SubsidiaryList[x].SubsidiaryCollection.Sum(t => t.DaysWorkThisYear)
                        + _fiefService.SubsidiaryList[x].ConstructingCollection.Sum(t => t.DaysWorkThisYear);
                }
                return subsidiariesDayswork;
            }
        }

        public int GetForecastForSilver(int index)
        {
            if (index != 0)
            {
                return _fiefService.IncomeList[index].TotalSilver
                    - _fiefService.ExpensesList[index].ExpensesSilver;
            }
            else
            {
                int silver = 0;
                for (int x = 1; x < _fiefService.IncomeList.Count; x++)
                {
                    silver += _fiefService.IncomeList[x].TotalSilver
                        - _fiefService.ExpensesList[x].ExpensesSilver;
                }

                return silver;
            }
        }

        public int GetForecastForBase(int index)
        {
            if (index != 0)
            {
                return _fiefService.IncomeList[index].TotalBase
                   - _fiefService.ExpensesList[index].ExpensesBase;
            }
            else
            {
                int i = 0;
                for (int x = 1; x < _fiefService.InformationList.Count; x++)
                {
                    i += _fiefService.IncomeList[x].TotalBase
                   - _fiefService.ExpensesList[x].ExpensesBase;
                }

                return i;
            }
        }

        public int GetForecastForLuxury(int index)
        {
            if (index != 0)
            {
                return _fiefService.IncomeList[index].TotalLuxury
                    - _fiefService.ExpensesList[index].ExpensesLuxury;
            }
            else
            {
                int luxury = 0;
                for (int x = 1; x < _fiefService.InformationList.Count; x++)
                {
                    luxury += _fiefService.IncomeList[x].TotalLuxury
                        - _fiefService.ExpensesList[x].ExpensesLuxury;
                }

                return luxury;
            }
        }

        public int GetForecastForIron(int index)
        {
            if (index != 0)
            {
                return _fiefService.IncomeList[index].TotalIron
                    - _fiefService.ExpensesList[index].ExpensesIron;
            }
            else
            {
                int iron = 0;
                for (int x = 1; x < _fiefService.InformationList.Count; x++)
                {
                    iron += _fiefService.IncomeList[x].TotalIron
                        - _fiefService.ExpensesList[x].ExpensesIron;
                }

                return iron;
            }
        }

        public int GetForecastForStone(int index)
        {
            if (index != 0)
            {
                return _fiefService.IncomeList[index].TotalStone
                    - _fiefService.ExpensesList[index].ExpensesStone;
            }
            else
            {
                int stone = 0;
                for (int x = 1; x < _fiefService.InformationList.Count; x++)
                {
                    stone += _fiefService.IncomeList[x].TotalStone
                        - _fiefService.ExpensesList[x].ExpensesStone;
                }

                return stone;
            }
        }

        public int GetForecastForWood(int index)
        {
            if (index != 0)
            {
                return _fiefService.IncomeList[index].TotalWood
                    - _fiefService.ExpensesList[index].ExpensesWood;
            }
            else
            {
                int wood = 0;
                for (int x = 1; x < _fiefService.InformationList.Count; x++)
                {
                    wood += _fiefService.IncomeList[x].TotalWood
                        - _fiefService.ExpensesList[x].ExpensesWood;
                }

                return wood;
            }
        }

        public int GetManorAcres(int index)
        {
            if (index != 0)
            {
                return _fiefService.ManorList[index].ManorAcres;
            }
            else
            {
                int acres = 0;
                for (int x = 1; x < _fiefService.InformationList.Count; x++)
                {
                    acres += _fiefService.ManorList[x].ManorAcres;
                }

                return acres;
            }
        }
 
        public int GetNumberOfFishingboats(int index)
        {
            if (index != 0)
            {
                return _fiefService.PortsList[index].FishingBoats;
            }
            else
            {
                int fishingBoats = 0;
                for (int x = 1; x < _fiefService.PortsList.Count; x++)
                {
                    fishingBoats += _fiefService.PortsList[x].FishingBoats;
                }

                return fishingBoats;
            }
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
            else
            {
                int landClearing = 0;
                for (int x = 1; x < _fiefService.ManorList.Count; x++)
                {
                    landClearing += _fiefService.ManorList[x].ManorWoodland;
                }

                return landClearing;
            }
        }

        public int GetMaxFelling(int index)
        {
            if (index != 0)
            {
                return _fiefService.ManorList[index].ManorWoodland;
            }
            else
            {
                int felling = 0;
                for (int x = 1; x < _fiefService.ManorList.Count; x++)
                {
                    felling += _fiefService.ManorList[x].ManorWoodland;
                }

                return felling;
            }
        }

        public int GetMaxUseless(int index)
        {
            if (index != 0)
            {
                return _fiefService.ManorList[index].ManorUseless;
            }
            else
            {
                int useless = 0;
                for (int x = 1; x < _fiefService.ManorList.Count; x++)
                {
                    useless += _fiefService.ManorList[x].ManorUseless;
                }

                return useless;
            }
        }

        public int GetMaxLandClearFelling(int index)
        {
            if (index != 0)
            {
                return _fiefService.ManorList[index].ManorFelling;
            }
            else
            {
                int landClearFelling = 0;
                for (int x = 1; x < _fiefService.ManorList.Count; x++)
                {
                    landClearFelling += _fiefService.ManorList[x].ManorFelling;
                }

                return landClearFelling;
            }
        }
    }
}
