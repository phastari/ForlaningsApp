using FiefApp.Common.Infrastructure.Settings.SettingsModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface ISettingsService
    {
        ArmySettingsModel ArmySettingsModel { get; }
        EmployeeSettingsModel EmployeeSettingsModel { get; }
        InformationSettingsModel InformationSettingsModel { get; }

        ArmySettingsModel LoadArmySettingsFromXml();
        void CreateDefaultArmySettingsXmlFile();
        EmployeeSettingsModel LoadEmployeeSettingsFromXml();
        void CreateDefaultEmployeeSettingsXmlFile();
        InformationSettingsModel LoadInformationSettingsFromXml();
        void CreateDefaultInformationSettingsXmlFile();
    }
}