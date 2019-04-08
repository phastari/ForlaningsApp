using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IWeatherService
    {
        WeatherDataModel GetAllWeatherDataModels();
        int GetTotalAmountOfSerfs(int index);
        int GetTotalAmountOfSlaves(int index);
        int GetTotalNumberOfSubsidaries(int index);
        int GetTotalAmountOfDaysworkFromSubsidiaries(int index);
        int GetForecastForSilver(int index);
        int GetForecastForBase(int index);
        int GetForecastForLuxury(int index);
        int GetForecastForIron(int index);
        int GetForecastForStone(int index);
        int GetForecastForWood(int index);
        int GetManorAcres(int index);
    }
}