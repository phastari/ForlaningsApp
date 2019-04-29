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
            return null;
        }

        public int GetTotalAmountOfSerfs(int index)
        {
            return _fiefService.ManorList[index].VillagesCollection.Sum(t => t.Serfdoms);
        }

        public int GetTotalAmountOfSlaves(int index)
        {
            return _fiefService.ExpensesList[index].Slaves;
        }

        public int GetTotalNumberOfSubsidaries(int index)
        {
            return _fiefService.SubsidiaryList[index].SubsidiaryCollection.Count
                   + _fiefService.SubsidiaryList[index].ConstructingCollection.Count;
        }

        public int GetTotalAmountOfDaysworkFromSubsidiaries(int index)
        {
            return _fiefService.SubsidiaryList[index].SubsidiaryCollection.Sum(t => t.DaysWorkThisYear) 
                   + _fiefService.SubsidiaryList[index].ConstructingCollection.Sum(t => t.DaysWorkThisYear);
        }

        public int GetForecastForSilver(int index)
        {
            return _fiefService.IncomeList[index].IncomesCollection.Where(t => t.Silver != -1).Sum(t => t.Silver);
        }

        public int GetForecastForBase(int index)
        {
            return _fiefService.IncomeList[index].IncomesCollection.Where(t => t.Base != -1).Sum(t => t.Base);
        }

        public int GetForecastForLuxury(int index)
        {
            return _fiefService.IncomeList[index].IncomesCollection.Where(t => t.Luxury != -1).Sum(t => t.Luxury);
        }

        public int GetForecastForIron(int index)
        {
            return _fiefService.IncomeList[index].IncomesCollection.Where(t => t.Iron != -1).Sum(t => t.Iron);
        }

        public int GetForecastForStone(int index)
        {
            return _fiefService.IncomeList[index].IncomesCollection.Where(t => t.Stone != -1).Sum(t => t.Stone);
        }

        public int GetForecastForWood(int index)
        {
            return _fiefService.IncomeList[index].IncomesCollection.Where(t => t.Wood != -1).Sum(t => t.Wood);
        }

        public int GetManorAcres(int index)
        {
            return _fiefService.ManorList[index].ManorAcres;
        }
    }
}
