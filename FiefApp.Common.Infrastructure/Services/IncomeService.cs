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
                            IsStewardNeeded = false
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
                            IsStewardNeeded = false
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
                            IsStewardNeeded = false
                        });

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
                            IsStewardNeeded = true
                        });

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
                            IsStewardNeeded = true
                        });

                    if (_fiefService.InformationList[index].River == "Ja"
                        || _fiefService.InformationList[index].Coast == "Ja"
                        || _fiefService.InformationList[index].Lake == "Ja")
                    {
                        tempList.Add(
                            new IncomeModel()
                            {

                            });
                    }
                }
            }

            return new ObservableCollection<IncomeModel>(tempList);
        }
    }
}
