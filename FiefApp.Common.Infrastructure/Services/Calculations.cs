using System;

namespace FiefApp.Common.Infrastructure.Services
{
    public class Calculations : ICalculations
    {
        private readonly IFiefService _fiefService;
        private readonly ISettingsService _settingsService;

        public Calculations(
            IFiefService fiefService, 
            ISettingsService settingsService
            )
        {
            _fiefService = fiefService;
            _settingsService = settingsService;
        }

        public int GetAvrad(int index)
        {
            decimal avrad = 0;
            decimal temp = 0;
            for (int y = 0; y < _fiefService.ManorList[index].VillagesCollection.Count; y++)
            {
                temp = (decimal)_fiefService.ManorList[index].VillagesCollection[y].Serfdoms
                       * 4
                       * (
                           Convert.ToDecimal(_fiefService.InformationList[index].AgricultureQuality)
                           + Convert.ToDecimal(_fiefService.InformationList[index].AnimalHusbandryQuality)
                       )
                       / 180;
                avrad += temp + temp / 100M
                         * (
                             Convert.ToDecimal(_fiefService.InformationList[index].AgricultureDevelopmentLevel)
                             + Convert.ToDecimal(_fiefService.InformationList[index].AnimalHusbandryDevelopmentLevel)
                         )
                         * 2.5M;
            }

            return Convert.ToInt32(Math.Floor(avrad));
        }

        public int GetTaxes(int index)
        {
            decimal tax = 0;

            for (int x = 0; x < _fiefService.ManorList[index].VillagesCollection.Count; x++)
            {
                tax += (decimal)_fiefService.ManorList[index].VillagesCollection[x].Farmers / 5
                       + (decimal)_fiefService.ManorList[index].VillagesCollection[x].Farmers
                       / 500
                       * Convert.ToDecimal(_fiefService.ManorList[index].ManorWealth);
            }

            return Convert.ToInt32(Math.Floor(tax));
        }

        public int GetLicenseFees(int index)
        {
            decimal license = 0;

            for (int x = 0; x < _fiefService.ManorList[index].VillagesCollection.Count; x++)
            {
                license += _fiefService.ManorList[index].VillagesCollection[x].Boatbuilders * 200;
                license += _fiefService.ManorList[index].VillagesCollection[x].Tanners * 100;
                license += _fiefService.ManorList[index].VillagesCollection[x].Millers * 100;
                license += _fiefService.ManorList[index].VillagesCollection[x].Furriers * 200;
                license += _fiefService.ManorList[index].VillagesCollection[x].Tailors * 150;
                license += _fiefService.ManorList[index].VillagesCollection[x].Smiths * 150;
                license += _fiefService.ManorList[index].VillagesCollection[x].Carpenters * 150;
                license += _fiefService.ManorList[index].VillagesCollection[x].Innkeepers * 200;
            }

            license += license / 100 * (Convert.ToDecimal(_fiefService.ManorList[index].ManorWealth) * 5);

            return Convert.ToInt32(Math.Floor(license));
        }

        public int GetAnimalHusbandryIncome(int index)
        {
            return Convert.ToInt32(
                Math.Floor(
                    Convert.ToDecimal(_fiefService.InformationList[index].AnimalHusbandryQuality)
                    * _fiefService.ManorList[index].ManorPasture
                    / 9
                    + Convert.ToDecimal(_fiefService.InformationList[index].AnimalHusbandryQuality)
                    * _fiefService.ManorList[index].ManorPasture
                    / 100
                    * Convert.ToDecimal(_fiefService.InformationList[index].AnimalHusbandryDevelopmentLevel)
                    * 5));
        }

        public int GetAgricultureIncome(int index)
        {
            return Convert.ToInt32(
                Math.Floor(
                    Convert.ToDecimal(_fiefService.InformationList[index].AgricultureQuality)
                    * _fiefService.ManorList[index].ManorArable
                    / 9
                    + Convert.ToDecimal(_fiefService.InformationList[index].AgricultureQuality)
                    * _fiefService.ManorList[index].ManorArable
                    / 100
                    * Convert.ToDecimal(_fiefService.InformationList[index].AgricultureDevelopmentLevel)
                    * 5));
        }
    }
}
