using FiefApp.Common.Infrastructure.Settings.SettingsModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface ISettingsService
    {
        ArmySettingsModel ArmySettingsModel { get; }
        EmployeeSettingsModel EmployeeSettingsModel { get; }
        InformationSettingsModel InformationSettingsModel { get; }
        ManorSettingsModel ManorSettingsModel { get; }

        ArmySettingsModel LoadArmySettingsFromXml();
        EmployeeSettingsModel LoadEmployeeSettingsFromXml();
        InformationSettingsModel LoadInformationSettingsFromXml();
        ManorSettingsModel LoadManorSettingsFromXml();
        

        void CreateDefaultArmySettingsXmlFile();
        void CreateDefaultEmployeeSettingsXmlFile();
        void CreateDefaultInformationSettingsXmlFile();
        void CreateDefaultManorSettingsXmlFile();
    }
}