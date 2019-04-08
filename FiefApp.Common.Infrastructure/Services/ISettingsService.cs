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
        BoatbuildingSettingsModel BoatbuildingSettingsModel { get; }
        LivingconditionsSettingsModel LivingconditionsSettingsModel { get; }
        StableSettingsModel StableSettingsModel { get; }
        ExpensesSettingsModel ExpensesSettingsModel { get; }
        List<SubsidiarySettingsModel> SubsidiarySettingsList { get; }
        List<BuildingsSettingsModel> BuildingsSettingsList { get; }

        ArmySettingsModel LoadArmySettingsFromXml();
        EmployeeSettingsModel LoadEmployeeSettingsFromXml();
        InformationSettingsModel LoadInformationSettingsFromXml();
        ManorSettingsModel LoadManorSettingsFromXml();
        BoatbuildingSettingsModel LoadBoatbuildingSettingsFromXml();
        List<ShipyardTypeSettingsModel> LoadShipyardTypeSettingsFromXml();
        LivingconditionsSettingsModel LoadLivingconditionsSettingsFromXml();
        StableSettingsModel LoadStableSettingsFromXml();
        ExpensesSettingsModel LoadExpensesSettingsFromXml();
        List<SubsidiarySettingsModel> LoadSubsidiarySettingsFromXml();
        List<BuildingsSettingsModel> LoadBuildingsSettingsFromXml();

        void CreateDefaultArmySettingsXmlFile();
        void CreateDefaultEmployeeSettingsXmlFile();
        void CreateDefaultInformationSettingsXmlFile();
        void CreateDefaultManorSettingsXmlFile();
        void CreateDefaultBoatbuildingSettingsXmlFile();
        void CreateDefaultShipyardTypeSettingsXmlFile();
        void CreateDefaultLivingconditionsSettingsXmlFile();
        void CreateDefaultStableSettingsXmlFile();
        void CreateDefaultExpensesSettingsXmlFile();
        void CreateDefaultSubsidiarySettingsXmlFile();
        void CreateDefaultBuildingsSettingsXmlFile();
    }
}