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
            return null;
        }

        public int GetTotalAmountOfSerfs(int index)
        {
            int amount = 0;

            for (int x = 0; x < _fiefService.ManorList[index].VillagesCollection.Count; x++)
            {
                amount += _fiefService.ManorList[index].VillagesCollection[x].Serfdoms;
            }

            return amount;
        }

        public int GetTotalAmountOfSlaves(int index)
        {
            return _fiefService.ExpensesList[index].Slaves;
        }

        public int GetTotalNumberOfSubsidaries(int index)
        {
            return _fiefService.SubsidiaryList[index].SubsidiaryCollection.Count;
        }

        public int GetTotalAmountOfDaysworkFromSubsidiaries(int index)
        {
            int amount = 0;

            for (int x = 0; x < _fiefService.SubsidiaryList[index].SubsidiaryCollection.Count; x++)
            {
                amount += _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].DaysWorkThisYear;
            }

            return amount;
        }

        public int GetForecastForSilver(int index)
        {
            int income = 0;

            for (int x = 0; x < _fiefService.IncomeList[index].IncomesCollection.Count; x++)
            {
                if (_fiefService.IncomeList[index].IncomesCollection[x].Silver != -1)
                {
                    income += _fiefService.IncomeList[index].IncomesCollection[x].Silver;
                }
            }

            return income - _fiefService.ExpensesList[index].ExpensesSilver;
        }

        public int GetForecastForBase(int index)
        {
            int income = 0;

            for (int x = 0; x < _fiefService.IncomeList[index].IncomesCollection.Count; x++)
            {
                if (_fiefService.IncomeList[index].IncomesCollection[x].Base != -1)
                {
                    income += _fiefService.IncomeList[index].IncomesCollection[x].Base;
                }
            }

            return income - _fiefService.ExpensesList[index].ExpensesBase;
        }

        public int GetForecastForLuxury(int index)
        {
            int income = 0;

            for (int x = 0; x < _fiefService.IncomeList[index].IncomesCollection.Count; x++)
            {
                if (_fiefService.IncomeList[index].IncomesCollection[x].Luxury != -1)
                {
                    income += _fiefService.IncomeList[index].IncomesCollection[x].Luxury;
                }
            }

            return income - _fiefService.ExpensesList[index].ExpensesLuxury;
        }

        public int GetForecastForIron(int index)
        {
            int income = 0;

            for (int x = 0; x < _fiefService.IncomeList[index].IncomesCollection.Count; x++)
            {
                if (_fiefService.IncomeList[index].IncomesCollection[x].Iron != -1)
                {
                    income += _fiefService.IncomeList[index].IncomesCollection[x].Iron;
                }
            }

            return income - _fiefService.ExpensesList[index].ExpensesIron;
        }

        public int GetForecastForStone(int index)
        {
            int income = 0;

            for (int x = 0; x < _fiefService.IncomeList[index].IncomesCollection.Count; x++)
            {
                if (_fiefService.IncomeList[index].IncomesCollection[x].Stone != -1)
                {
                    income += _fiefService.IncomeList[index].IncomesCollection[x].Stone;
                }
            }

            return income - _fiefService.ExpensesList[index].ExpensesStone;
        }

        public int GetForecastForWood(int index)
        {
            int income = 0;

            for (int x = 0; x < _fiefService.IncomeList[index].IncomesCollection.Count; x++)
            {
                if (_fiefService.IncomeList[index].IncomesCollection[x].Wood != -1)
                {
                    income += _fiefService.IncomeList[index].IncomesCollection[x].Wood;
                }
            }

            return income - _fiefService.ExpensesList[index].ExpensesWood;
        }

        public int GetManorAcres(int index)
        {
            return _fiefService.ManorList[index].ManorAcres;
        }
    }
}
