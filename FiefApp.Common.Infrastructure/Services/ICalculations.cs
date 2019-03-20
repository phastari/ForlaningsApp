namespace FiefApp.Common.Infrastructure.Services
{
    public interface ICalculations
    {
        int GetAvrad(int index);
        int GetTaxes(int index);
        int GetLicenseFees(int index);
        int GetAnimalHusbandryIncome(int index);
        int GetAgricultureIncome(int index);
    }
}