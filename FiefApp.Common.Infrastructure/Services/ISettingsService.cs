using FiefApp.Common.Infrastructure.Settings.SettingsModels;
using System.Collections.Generic;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface ISettingsService
    {
        ArmySettingsModel ArmySettingsModel { get; }
        EmployeeSettingsModel EmployeeSettingsModel { get; }
        InformationSettingsModel InformationSettingsModel { get; }
        ManorSettingsModel ManorSettingsModel { get; }
        List<ShipyardTypeSettingsModel> ShipyardTypeSettingsList { get; }
        LivingconditionsSettingsModel LivingconditionsSettingsModel { get; }
        StableSettingsModel StableSettingsModel { get; }
        ExpensesSettingsModel ExpensesSettingsModel { get; }

        ArmySettingsModel LoadArmySettingsFromXml();
        EmployeeSettingsModel LoadEmployeeSettingsFromXml();
        InformationSettingsModel LoadInformationSettingsFromXml();
        ManorSettingsModel LoadManorSettingsFromXml();
        BoatbuildingSettingsModel LoadBoatbuildingSettingsFromXml();
        LivingconditionsSettingsModel LoadLivingconditionsSettingsFromXml();
        StableSettingsModel LoadStableSettingsFromXml();
        ExpensesSettingsModel LoadExpensesSettingsFromXml();

        void CreateDefaultArmySettingsXmlFile();
        void CreateDefaultEmployeeSettingsXmlFile();
        void CreateDefaultInformationSettingsXmlFile();
        void CreateDefaultManorSettingsXmlFile();
        void CreateDefaultBoatbuildingSettingsXmlFile();
        void CreateDefaultLivingconditionsSettingsXmlFile();
        void CreateDefaultStableSettingsXmlFile();
        void CreateDefaultExpensesSettingsXmlFile();
    }
}