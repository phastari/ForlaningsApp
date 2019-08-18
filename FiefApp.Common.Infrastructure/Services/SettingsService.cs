using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace FiefApp.Common.Infrastructure.Services
{
    public class SettingsService : ISettingsService
    {
        public SettingsService()
        {
            ArmySettingsModel = LoadArmySettingsFromXml();
            EmployeeSettingsModel = LoadEmployeeSettingsFromXml();
            InformationSettingsModel = LoadInformationSettingsFromXml();
            ManorSettingsModel = LoadManorSettingsFromXml();
            BoatbuildingSettingsModel = LoadBoatbuildingSettingsFromXml();
            LivingconditionsSettingsModel = LoadLivingconditionsSettingsFromXml();
            StableSettingsModel = LoadStableSettingsFromXml();
            ExpensesSettingsModel = LoadExpensesSettingsFromXml();
            SubsidiarySettingsList = LoadSubsidiarySettingsFromXml();
            ShipyardTypeSettingsList = LoadShipyardTypeSettingsFromXml();
            BuildingsSettingsList = LoadBuildingsSettingsFromXml();
        }

        public ArmySettingsModel ArmySettingsModel { get; private set; }
        public EmployeeSettingsModel EmployeeSettingsModel { get; private set; }
        public InformationSettingsModel InformationSettingsModel { get; private set; }
        public ManorSettingsModel ManorSettingsModel { get; private set; }
        public BoatbuildingSettingsModel BoatbuildingSettingsModel { get; private set; }
        public List<ShipyardTypeSettingsModel> ShipyardTypeSettingsList { get; private set; }
        public LivingconditionsSettingsModel LivingconditionsSettingsModel { get; private set; }
        public StableSettingsModel StableSettingsModel { get; private set; }
        public ExpensesSettingsModel ExpensesSettingsModel { get; private set; }
        public List<SubsidiarySettingsModel> SubsidiarySettingsList { get; private set; }
        public List<BuildingsSettingsModel> BuildingsSettingsList { get; private set; }

        #region Methods : Army Settings

        public ArmySettingsModel LoadArmySettingsFromXml()
        {
            bool foundError = false;
            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/ArmySettings.xml";
            XmlDocument doc = new XmlDocument();
            ArmySettingsModel tempModel = new ArmySettingsModel();
            if (File.Exists(filePath))
            {
                doc.Load(filePath);
                XmlNodeList elemList = doc.GetElementsByTagName("Army");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        switch (xmlAttributeCollection["PersonName"].Value)
                        {
                            case "ArmyCrossbowmen":
                                tempModel.ArmyCrossbowmenSilver =
                                Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyCrossbowmenBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmyBowmen":
                                tempModel.ArmyBowmenSilver = Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyBowmenBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmyMedics":
                                tempModel.ArmyMedicsSilver = Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyMedicsBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmyMedicsSkilled":
                                tempModel.ArmyMedicsSkilledSilver =
                                    Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyMedicsSkilledBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmyInfantry":
                                tempModel.ArmyInfantrySilver = Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyInfantryBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmyInfantryMedium":
                                tempModel.ArmyInfantryMediumSilver =
                                    Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyInfantryMediumBase =
                                    Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmyInfantryHeavy":
                                tempModel.ArmyInfantryHeavySilver =
                                    Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyInfantryHeavyBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmyInfantryElite":
                                tempModel.ArmyInfantryEliteSilver =
                                    Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyInfantryEliteBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmyLongbowmen":
                                tempModel.ArmyLongbowmenSilver =
                                    Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyLongbowmenBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmyMercenary":
                                tempModel.ArmyMercenarySilver = Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyMercenaryBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmyMercenaryElite":
                                tempModel.ArmyMercenaryEliteSilver =
                                    Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyMercenaryEliteBase =
                                    Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmyMercenaryBowmen":
                                tempModel.ArmyMercenaryBowmenSilver =
                                    Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyMercenaryBowmenBase =
                                    Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmyEngineers":
                                tempModel.ArmyEngineersSilver = Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyEngineersBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmySpearmen":
                                tempModel.ArmySpearmenSilver = Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmySpearmenBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmyScouts":
                                tempModel.ArmyScoutsSilver = Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyScoutsBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmyScoutsSkilled":
                                tempModel.ArmyScoutsSkilledSilver =
                                    Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyScoutsSkilledBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmyKnightTemplars":
                                tempModel.ArmyKnightTemplarsSilver =
                                    Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyKnightTemplarsBase =
                                    Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                tempModel.ArmyKnightTemplarsAccommodation =
                                    Convert.ToBoolean(xmlAttributeCollection["Accommodation"].Value);
                                break;

                            case "ArmyGuards":
                                tempModel.ArmyGuardsSilver = Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyGuardsBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            case "ArmyWeaponmasters":
                                tempModel.ArmyWeaponmastersSilver =
                                    Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                tempModel.ArmyWeaponmastersBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                break;

                            default:
                                foundError = true;
                                break;
                        }
                    }
                    else
                    {
                        foundError = true;
                    }
                }

                if (!foundError)
                {
                    elemList = doc.GetElementsByTagName("Cavalry");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                        if (xmlAttributeCollection != null)
                        {
                            switch (xmlAttributeCollection["PersonName"].Value)
                            {
                                case "CavalryBowmen":
                                    tempModel.CavalryBowmenSilver =
                                        Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                    tempModel.CavalryBowmenBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                    break;

                                case "CavalryCourier":
                                    tempModel.CavalryCourierSilver =
                                        Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                    tempModel.CavalryCourierBase =
                                        Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                    break;

                                case "CavalryLight":
                                    tempModel.CavalryLightSilver =
                                        Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                    tempModel.CavalryLightBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                    break;

                                case "CavalryKnights":
                                    tempModel.CavalryKnightsSilver =
                                        Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                    tempModel.CavalryKnightsBase =
                                        Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                    tempModel.CavalryKnightsAccommodation =
                                        Convert.ToBoolean(xmlAttributeCollection["Accommodation"].Value);
                                    break;

                                case "CavalryScouts":
                                    tempModel.CavalryScoutsSilver =
                                        Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                    tempModel.CavalryScoutsBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                    break;

                                case "CavalryKnightTemplars":
                                    tempModel.CavalryKnightTemplarsSilver =
                                        Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                    tempModel.CavalryKnightTemplarsBase =
                                        Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                    tempModel.CavalryKnightTemplarsAccommodation =
                                        Convert.ToBoolean(xmlAttributeCollection["Accommodation"].Value);
                                    break;

                                case "CavalryHeavy":
                                    tempModel.CavalryHeavySilver =
                                        Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                    tempModel.CavalryHeavyBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                    break;

                                case "CavalryElite":
                                    tempModel.CavalryEliteSilver =
                                        Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                    tempModel.CavalryEliteBase = Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                    break;

                                default:
                                    foundError = true;
                                    break;
                            }
                        }
                        else
                        {
                            foundError = true;
                        }
                    }
                }

                if (!foundError)
                {
                    elemList = doc.GetElementsByTagName("Officers");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                        if (xmlAttributeCollection != null)
                        {
                            switch (xmlAttributeCollection["PersonName"].Value)
                            {
                                case "OfficersCorporal":
                                    tempModel.OfficersCorporalSilver =
                                        Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                    tempModel.OfficersCorporalBase =
                                        Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                    tempModel.OfficersCorporalAccommodation =
                                        Convert.ToBoolean(xmlAttributeCollection["Accommodation"].Value);
                                    break;

                                case "OfficersSergeant":
                                    tempModel.OfficersSergeantSilver =
                                        Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                    tempModel.OfficersSergeantBase =
                                        Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                    tempModel.OfficersSergeantAccommodation =
                                        Convert.ToBoolean(xmlAttributeCollection["Accommodation"].Value);
                                    break;

                                case "OfficersCaptain":
                                    tempModel.OfficersCaptainSilver =
                                        Convert.ToInt16(xmlAttributeCollection["Silver"].Value);
                                    tempModel.OfficersCaptainBase =
                                        Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                    tempModel.OfficersCaptainAccommodation =
                                        Convert.ToBoolean(xmlAttributeCollection["Accommodation"].Value);
                                    break;

                                default:
                                    foundError = true;
                                    break;
                            }
                        }
                        else
                        {
                            foundError = true;
                        }
                    }
                }
            }
            else
            {
                foundError = true;
            }

            if (foundError)
            {
                CreateDefaultArmySettingsXmlFile();
                return null;
            }
            else
            {
                return tempModel;
            }
        }

        public void CreateDefaultArmySettingsXmlFile()
        {
            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", string.Empty),
                new XElement("Settings",
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyCrossbowmen"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyBowmen"),
                        new XAttribute("Silver", 160),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyMedics"),
                        new XAttribute("Silver", 2920),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyMedicsSkilled"),
                        new XAttribute("Silver", 4480),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyInfantry"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyInfantryMedium"),
                        new XAttribute("Silver", 480),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyInfantryHeavy"),
                        new XAttribute("Silver", 800),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyInfantryElite"),
                        new XAttribute("Silver", 1360),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyLongbowmen"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyMercenary"),
                        new XAttribute("Silver", 560),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyMercenaryElite"),
                        new XAttribute("Silver", 1840),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyMercenaryBowmen"),
                        new XAttribute("Silver", 480),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyEngineers"),
                        new XAttribute("Silver", 1360),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmySpearmen"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyScouts"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyScoutsSkilled"),
                        new XAttribute("Silver", 1620),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyKnightTemplars"),
                        new XAttribute("Silver", 2360),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", true)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyGuards"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("PersonName", "ArmyWeaponmasters"),
                        new XAttribute("Silver", 2400),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Cavalry",
                        new XAttribute("PersonName", "CavalryBowmen"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 6),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Cavalry",
                        new XAttribute("PersonName", "CavalryCourier"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 6),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Cavalry",
                        new XAttribute("PersonName", "CavalryLight"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 6),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Cavalry",
                        new XAttribute("PersonName", "CavalryKnights"),
                        new XAttribute("Silver", 3260),
                        new XAttribute("Base", 4),
                        new XAttribute("Accommodation", true)
                    ),
                    new XElement("Cavalry",
                        new XAttribute("PersonName", "CavalryScouts"),
                        new XAttribute("Silver", 540),
                        new XAttribute("Base", 6),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Cavalry",
                        new XAttribute("PersonName", "CavalryKnightTemplars"),
                        new XAttribute("Silver", 2360),
                        new XAttribute("Base", 4),
                        new XAttribute("Accommodation", true)
                    ),
                    new XElement("Cavalry",
                        new XAttribute("PersonName", "CavalryHeavy"),
                        new XAttribute("Silver", 1660),
                        new XAttribute("Base", 6),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Cavalry",
                        new XAttribute("PersonName", "CavalryElite"),
                        new XAttribute("Silver", 2040),
                        new XAttribute("Base", 6),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Officers",
                        new XAttribute("PersonName", "OfficersCorporal"),
                        new XAttribute("Silver", 2340),
                        new XAttribute("Base", 0),
                        new XAttribute("Accommodation", true)
                    ),
                    new XElement("Officers",
                        new XAttribute("PersonName", "OfficersSergeant"),
                        new XAttribute("Silver", 3120),
                        new XAttribute("Base", 0),
                        new XAttribute("Accommodation", true)
                    ),
                    new XElement("Officers",
                        new XAttribute("PersonName", "OfficersCaptain"),
                        new XAttribute("Silver", 4680),
                        new XAttribute("Base", 0),
                        new XAttribute("Accommodation", true)
                    )
                )
            );

            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/ArmySettings.xml";
            xmlDoc.Save(@filePath);
            ArmySettingsModel = LoadArmySettingsFromXml();
        }

        #endregion

        #region Methods : Employee Settings

        public EmployeeSettingsModel LoadEmployeeSettingsFromXml()
        {
            bool foundError = false;
            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/EmployeeSettings.xml";
            XmlDocument doc = new XmlDocument();
            EmployeeSettingsModel tempModel = new EmployeeSettingsModel();
            if (File.Exists(filePath))
            {
                doc.Load(filePath);
                XmlNodeList elemList = doc.GetElementsByTagName("Employee");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        switch (xmlAttributeCollection["PersonName"].Value)
                        {
                            case "Falconer":
                                tempModel.FalconerBase =
                                    Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                tempModel.FalconerLuxury =
                                    Convert.ToInt16(xmlAttributeCollection["Luxury"].Value);
                                break;

                            case "Bailiff":
                                tempModel.BailiffBase =
                                    Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                tempModel.BailiffLuxury =
                                    Convert.ToInt16(xmlAttributeCollection["Luxury"].Value);
                                break;

                            case "Herald":
                                tempModel.HeraldBase =
                                    Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                tempModel.HeraldLuxury =
                                    Convert.ToInt16(xmlAttributeCollection["Luxury"].Value);
                                break;

                            case "Hunter":
                                tempModel.HunterBase =
                                    Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                tempModel.HunterLuxury =
                                    Convert.ToInt16(xmlAttributeCollection["Luxury"].Value);
                                break;

                            case "Physician":
                                tempModel.PhysicianBase =
                                    Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                tempModel.PhysicianLuxury =
                                    Convert.ToInt16(xmlAttributeCollection["Luxury"].Value);
                                break;

                            case "Scholar":
                                tempModel.ScholarBase =
                                    Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                tempModel.ScholarLuxury =
                                    Convert.ToInt16(xmlAttributeCollection["Luxury"].Value);
                                break;

                            case "Cupbearer":
                                tempModel.CupbearerBase =
                                    Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                tempModel.CupbearerLuxury =
                                    Convert.ToInt16(xmlAttributeCollection["Luxury"].Value);
                                break;

                            case "Prospector":
                                tempModel.ProspectorBase =
                                    Convert.ToInt16(xmlAttributeCollection["Base"].Value);
                                if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                                {
                                    tempModel.ProspectorLuxury = xmlAttributeCollection["Luxury"].Value;
                                }
                                else
                                {
                                    tempModel.ProspectorLuxury = xmlAttributeCollection["Luxury"].Value.Replace(",", ".");
                                }
                                break;
                                
                            default:
                                foundError = true;
                                break;
                        }
                    }
                    else
                    {
                        foundError = true;
                    }
                }
            }
            else
            {
                foundError = true;
            }

            if (foundError)
            {
                CreateDefaultEmployeeSettingsXmlFile();
                return null;
            }
            else
            {
                return tempModel;
            }
        }

        public void CreateDefaultEmployeeSettingsXmlFile()
        {
            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", string.Empty),
                new XElement("Settings",
                    new XElement("Employee",
                        new XAttribute("PersonName", "Falconer"),
                        new XAttribute("Base", 2),
                        new XAttribute("Luxury", 1)
                    ),
                    new XElement("Employee",
                        new XAttribute("PersonName", "Bailiff"),
                        new XAttribute("Base", 3),
                        new XAttribute("Luxury", 1)
                    ),
                    new XElement("Employee",
                        new XAttribute("PersonName", "Herald"),
                        new XAttribute("Base", 4),
                        new XAttribute("Luxury", 1)
                    ),
                    new XElement("Employee",
                        new XAttribute("PersonName", "Hunter"),
                        new XAttribute("Base", 2),
                        new XAttribute("Luxury", 0)
                    ),
                    new XElement("Employee",
                        new XAttribute("PersonName", "Physician"),
                        new XAttribute("Base", 3),
                        new XAttribute("Luxury", 3)
                    ),
                    new XElement("Employee",
                        new XAttribute("PersonName", "Scholar"),
                        new XAttribute("Base", 3),
                        new XAttribute("Luxury", 1)
                    ),
                    new XElement("Employee",
                        new XAttribute("PersonName", "Cupbearer"),
                        new XAttribute("Base", 2),
                        new XAttribute("Luxury", 0)
                    ),
                    new XElement("Employee",
                        new XAttribute("PersonName", "Prospector"),
                        new XAttribute("Base", 2),
                        new XAttribute("Luxury", "^2,8/13-1")
                    )
                )
            );

            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/EmployeeSettings.xml";
            xmlDoc.Save(@filePath);
            EmployeeSettingsModel = LoadEmployeeSettingsFromXml();
        }

        #endregion

        #region Methods : Information Settings

        public InformationSettingsModel LoadInformationSettingsFromXml()
        {
            bool foundError = false;
            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/InformationSettings.xml";
            XmlDocument doc = new XmlDocument();
            InformationSettingsModel tempModel = new InformationSettingsModel();
            if (File.Exists(filePath))
            {
                doc.Load(filePath);
                XmlNodeList elemList = doc.GetElementsByTagName("Information");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        tempModel.TypeTextList.Add(xmlAttributeCollection["TypText"].Value);
                        tempModel.InformationTextList.Add(xmlAttributeCollection["InformationText"].Value);
                    }
                    else
                    {
                        foundError = true;
                    }
                }

                elemList = doc.GetElementsByTagName("RoadNetwork");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        tempModel.RoadTypesList.Add(
                            new RoadModel()
                            {
                                Road = xmlAttributeCollection["Road"].Value,
                                BaseCost = Convert.ToInt32(xmlAttributeCollection["BaseCost"].Value),
                                StoneCost = Convert.ToInt32(xmlAttributeCollection["StoneCost"].Value)
                            }
                        );
                    }
                    else
                    {
                        foundError = true;
                    }
                }

                elemList = doc.GetElementsByTagName("Liegelord");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        tempModel.LiegelordTitlesList.Add(xmlAttributeCollection["Title"].Value);
                    }
                    else
                    {
                        foundError = true;
                    }
                }
            }
            else
            {
                foundError = true;
            }

            if (foundError)
            {
                CreateDefaultInformationSettingsXmlFile();
                return null;
            }
            else
            {
                return tempModel;
            }
        }

        public void CreateDefaultInformationSettingsXmlFile()
        {
            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", string.Empty),
                new XElement("Settings",
                    new XElement("Information",
                        new XAttribute("TypText", "Ärftligt län"),
                        new XAttribute("InformationText",
                            "Vasallen får länet mot att han utför vapentjänst och svär trohet till länsherren. När vasallen dör så ärver hans arvinge länet. Har vasallen inte har någon arvinge så går länet tillbaka till länsherren.")
                    ),
                    new XElement("Information",
                        new XAttribute("TypText", "Icke ärftligt län"),
                        new XAttribute("InformationText", "Vasallen får länet mot att han utför vapentjänst och svär trohet till länsherren. När vasallen dör så går länet tillbaka till länsherren.")
                    ),
                    new XElement("Information",
                        new XAttribute("TypText", "Pantlän"),
                        new XAttribute("InformationText",
                            "Ibland händer det att en länsherre måste låna pengar, till exempel i tider av ofred. Länsherren kan då skänka långivaren ett län att disponera fritt tills dess att lånet är återbetalat.")
                    ),
                    new XElement("Information",
                        new XAttribute("TypText", "Län på avgift"),
                        new XAttribute("InformationText", "Vasallen betalar en fast avgift till sin länsherre mot att han fritt får disponera länets inkomster.")
                    ),
                    new XElement("RoadNetwork",
                        new XAttribute("Road", "Stigar"),
                        new XAttribute("BaseCost", "0"),
                        new XAttribute("StoneCost", "0")
                    ),
                    new XElement("RoadNetwork",
                        new XAttribute("Road", "Väg"),
                        new XAttribute("BaseCost", "2"),
                        new XAttribute("StoneCost", "0")
                    ),
                    new XElement("RoadNetwork",
                        new XAttribute("Road", "Stenlagdväg"),
                        new XAttribute("BaseCost", "32"),
                        new XAttribute("StoneCost", "1000")
                    ),
                    new XElement("RoadNetwork",
                        new XAttribute("Road", "Kungliglandsväg"),
                        new XAttribute("BaseCost", "35"),
                        new XAttribute("StoneCost", "5000")
                    ),
                    new XElement("Liegelord",
                        new XAttribute("Title", "Storfurste")
                    ),
                    new XElement("Liegelord",
                        new XAttribute("Title", "Storfurstinna")
                    ),
                    new XElement("Liegelord",
                        new XAttribute("Title", "Furste")
                    ),
                    new XElement("Liegelord",
                        new XAttribute("Title", "Furstinna")
                    ),
                    new XElement("Liegelord",
                        new XAttribute("Title", "Hertig")
                    ),
                    new XElement("Liegelord",
                        new XAttribute("Title", "Hertiginna")
                    ),
                    new XElement("Liegelord",
                        new XAttribute("Title", "Greve")
                    ),
                    new XElement("Liegelord",
                        new XAttribute("Title", "Grevinna")
                    ),
                    new XElement("Liegelord",
                        new XAttribute("Title", "Baron")
                    ),
                    new XElement("Liegelord",
                        new XAttribute("Title", "Baronessa")
                    ),
                    new XElement("Liegelord",
                        new XAttribute("Title", "Godsherre")
                    ),
                    new XElement("Liegelord",
                        new XAttribute("Title", "Godsfru")
                    ),
                    new XElement("Liegelord",
                        new XAttribute("Title", "Riddare")
                    ),
                    new XElement("Liegelord",
                        new XAttribute("Title", "Riddersdam")
                    )
                )

            );

            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/InformationSettings.xml";
            xmlDoc.Save(@filePath);
            InformationSettingsModel = LoadInformationSettingsFromXml();
        }

        #endregion

        #region Methods : Manor Settings

        public ManorSettingsModel LoadManorSettingsFromXml()
        {
            bool foundError = false;
            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/ManorSettings.xml";
            XmlDocument doc = new XmlDocument();
            ManorSettingsModel tempModel = new ManorSettingsModel();
            if (File.Exists(filePath))
            {
                doc.Load(filePath);
                XmlNodeList elemList = doc.GetElementsByTagName("Livingcondition");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        tempModel.LivingconditionsList.Add(new LivingconditionModel()
                        {
                            Livingcondition = xmlAttributeCollection["Text"].Value,
                            BaseCost = Convert.ToInt16(xmlAttributeCollection["Base"].Value),
                            LuxuryCost = Convert.ToInt16(xmlAttributeCollection["Luxury"].Value)
                        });
                    }
                    else
                    {
                        foundError = true;
                    }
                }
            }
            else
            {
                foundError = true;
            }

            if (foundError)
            {
                CreateDefaultManorSettingsXmlFile();
                return null;
            }
            else
            {
                return tempModel;
            }
        }

        public void CreateDefaultManorSettingsXmlFile()
        {
            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", string.Empty),
                new XElement("Settings",
                    new XElement("Livingcondition",
                        new XAttribute("Text", "Nödtorftig"),
                        new XAttribute("Base", "1"),
                        new XAttribute("Luxury", "0")
                    ),
                    new XElement("Livingcondition",
                        new XAttribute("Text", "Gemen"),
                        new XAttribute("Base", "2"),
                        new XAttribute("Luxury", "0")
                    ),
                    new XElement("Livingcondition",
                        new XAttribute("Text", "God"),
                        new XAttribute("Base", "2"),
                        new XAttribute("Luxury", "2")
                    ),
                    new XElement("Livingcondition",
                        new XAttribute("Text", "Mycket god"),
                        new XAttribute("Base", "3"),
                        new XAttribute("Luxury", "2")
                    ),
                    new XElement("Livingcondition",
                        new XAttribute("Text", "Lyxliv"),
                        new XAttribute("Base", "4"),
                        new XAttribute("Luxury", "8")
                    )
                )
            );

            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/ManorSettings.xml";
            xmlDoc.Save(@filePath);
            ManorSettingsModel = LoadManorSettingsFromXml();
        }

        #endregion

        #region Methods : Boatbuilding Settings

        public BoatbuildingSettingsModel LoadBoatbuildingSettingsFromXml()
        {
            bool foundError = false;
            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/BoatbuildingSettings.xml";
            XmlDocument doc = new XmlDocument();
            BoatbuildingSettingsModel tempModel = new BoatbuildingSettingsModel();
            if (File.Exists(filePath))
            {
                doc.Load(filePath);
                XmlNodeList elemList = doc.GetElementsByTagName("BoatType");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                        {
                            tempModel.BoatSettingsList.Add(new BoatModel()
                            {
                                BoatType = xmlAttributeCollection["BoatType"].Value,
                                Masts = Convert.ToInt32(xmlAttributeCollection["Masts"].Value),
                                LengthMin = Convert.ToInt32(xmlAttributeCollection["LengthMin"].Value),
                                LengthMax = Convert.ToInt32(xmlAttributeCollection["LengthMax"].Value),
                                BL = Convert.ToDecimal(xmlAttributeCollection["BL"].Value.Replace(".", ",")),
                                DB = Convert.ToDecimal(xmlAttributeCollection["DB"].Value.Replace(".", ",")),
                                Crew = Convert.ToDecimal(xmlAttributeCollection["Crew"].Value.Replace(".", ",")),
                                Cargo = Convert.ToDecimal(xmlAttributeCollection["Cargo"].Value.Replace(".", ",")),
                                BenchMod = Convert.ToInt32(xmlAttributeCollection["BenchMod"].Value),
                                BenchMulti = Convert.ToDecimal(xmlAttributeCollection["BenchMulti"].Value.Replace(".", ",")),
                                OarsMulti = Convert.ToInt32(xmlAttributeCollection["OarsMulti"].Value),
                                RowerMulti = Convert.ToInt32(xmlAttributeCollection["RowerMulti"].Value),
                                IMGSource = xmlAttributeCollection["IMGSource"].Value,
                                Seaworthiness = xmlAttributeCollection["Seaworthiness"].Value
                            });
                        }
                        else
                        {
                            tempModel.BoatSettingsList.Add(new BoatModel()
                            {
                                BoatType = xmlAttributeCollection["BoatType"].Value,
                                Masts = Convert.ToInt32(xmlAttributeCollection["Masts"].Value),
                                LengthMin = Convert.ToInt32(xmlAttributeCollection["LengthMin"].Value),
                                LengthMax = Convert.ToInt32(xmlAttributeCollection["LengthMax"].Value),
                                BL = Convert.ToDecimal(xmlAttributeCollection["BL"].Value),
                                DB = Convert.ToDecimal(xmlAttributeCollection["DB"].Value),
                                Crew = Convert.ToDecimal(xmlAttributeCollection["Crew"].Value),
                                Cargo = Convert.ToDecimal(xmlAttributeCollection["Cargo"].Value),
                                BenchMod = Convert.ToInt32(xmlAttributeCollection["BenchMod"].Value),
                                BenchMulti = Convert.ToDecimal(xmlAttributeCollection["BenchMulti"].Value),
                                OarsMulti = Convert.ToInt32(xmlAttributeCollection["OarsMulti"].Value),
                                RowerMulti = Convert.ToInt32(xmlAttributeCollection["RowerMulti"].Value),
                                IMGSource = xmlAttributeCollection["IMGSource"].Value,
                                Seaworthiness = xmlAttributeCollection["Seaworthiness"].Value
                            });
                        }
                    }
                    else
                    {
                        foundError = true;
                    }

                }
            }
            else
            {
                foundError = true;
            }

            if (foundError)
            {
                CreateDefaultBoatbuildingSettingsXmlFile();
                return null;
            }
            else
            {
                return tempModel;
            }
        }

        public void CreateDefaultBoatbuildingSettingsXmlFile()
        {
            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", string.Empty),
                new XElement("Settings",
                    new XElement("BoatType",
                        new XAttribute("BoatType", "Bridad"),
                        new XAttribute("Masts", "3"),
                        new XAttribute("LengthMin", "20"),
                        new XAttribute("LengthMax", "28"),
                        new XAttribute("BL", "0.3"),
                        new XAttribute("DB", "0.28"),
                        new XAttribute("Crew", "0.35"),
                        new XAttribute("Cargo", "1.5"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "1.jpg"),
                        new XAttribute("Seaworthiness", "1T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Drakfartyg"),
                        new XAttribute("Masts", "2"),
                        new XAttribute("LengthMin", "23"),
                        new XAttribute("LengthMax", "28"),
                        new XAttribute("BL", "0.25"),
                        new XAttribute("DB", "0.28"),
                        new XAttribute("Crew", "0.5"),
                        new XAttribute("Cargo", "0.5"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "2.jpg"),
                        new XAttribute("Seaworthiness", "2T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Fiskebåt"),
                        new XAttribute("Masts", "1"),
                        new XAttribute("LengthMin", "6"),
                        new XAttribute("LengthMax", "6"),
                        new XAttribute("BL", "0.38"),
                        new XAttribute("DB", "0.35"),
                        new XAttribute("Crew", "0.3"),
                        new XAttribute("Cargo", "0.9"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "3.jpg"),
                        new XAttribute("Seaworthiness", "1T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Flodbåt"),
                        new XAttribute("Masts", "2"),
                        new XAttribute("LengthMin", "5"),
                        new XAttribute("LengthMax", "7"),
                        new XAttribute("BL", "0.44"),
                        new XAttribute("DB", "0.15"),
                        new XAttribute("Crew", "0.4"),
                        new XAttribute("Cargo", "1.8"),
                        new XAttribute("BenchMod", "-3"),
                        new XAttribute("BenchMulti", "0.5"),
                        new XAttribute("OarsMulti", "2"),
                        new XAttribute("RowerMulti", "-1"),
                        new XAttribute("IMGSource", "4.jpg"),
                        new XAttribute("Seaworthiness", "4T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Flodpråm"),
                        new XAttribute("Masts", "2"),
                        new XAttribute("LengthMin", "17"),
                        new XAttribute("LengthMax", "27"),
                        new XAttribute("BL", "0.42"),
                        new XAttribute("DB", "0.12"),
                        new XAttribute("Crew", "0.2"),
                        new XAttribute("Cargo", "2"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "5.jpg"),
                        new XAttribute("Seaworthiness", "4T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Gaffa"),
                        new XAttribute("Masts", "2"),
                        new XAttribute("LengthMin", "13"),
                        new XAttribute("LengthMax", "19"),
                        new XAttribute("BL", "0.28"),
                        new XAttribute("DB", "0.35"),
                        new XAttribute("Crew", "0.25"),
                        new XAttribute("Cargo", "1"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "6.jpg"),
                        new XAttribute("Seaworthiness", "1T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Galloz"),
                        new XAttribute("Masts", "3"),
                        new XAttribute("LengthMin", "22"),
                        new XAttribute("LengthMax", "35"),
                        new XAttribute("BL", "0.24"),
                        new XAttribute("DB", "0.33"),
                        new XAttribute("Crew", "0.35"),
                        new XAttribute("Cargo", "1.6"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "7.jpg"),
                        new XAttribute("Seaworthiness", "2T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Jagol"),
                        new XAttribute("Masts", "1"),
                        new XAttribute("LengthMin", "7"),
                        new XAttribute("LengthMax", "11"),
                        new XAttribute("BL", "0.27"),
                        new XAttribute("DB", "0.28"),
                        new XAttribute("Crew", "0.4"),
                        new XAttribute("Cargo", "1.4"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "8.jpg"),
                        new XAttribute("Seaworthiness", "1T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Jakt"),
                        new XAttribute("Masts", "2"),
                        new XAttribute("LengthMin", "12"),
                        new XAttribute("LengthMax", "17"),
                        new XAttribute("BL", "0.18"),
                        new XAttribute("DB", "0.4"),
                        new XAttribute("Crew", "0.4"),
                        new XAttribute("Cargo", "1"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "9.jpg"),
                        new XAttribute("Seaworthiness", "3T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Kaga"),
                        new XAttribute("Masts", "1"),
                        new XAttribute("LengthMin", "19"),
                        new XAttribute("LengthMax", "23"),
                        new XAttribute("BL", "0.21"),
                        new XAttribute("DB", "0.35"),
                        new XAttribute("Crew", "0.35"),
                        new XAttribute("Cargo", "1"),
                        new XAttribute("BenchMod", "-10"),
                        new XAttribute("BenchMulti", "0.5"),
                        new XAttribute("OarsMulti", "4"),
                        new XAttribute("RowerMulti", "4"),
                        new XAttribute("IMGSource", "10.jpg"),
                        new XAttribute("Seaworthiness", "2T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Kagge"),
                        new XAttribute("Masts", "1"),
                        new XAttribute("LengthMin", "14"),
                        new XAttribute("LengthMax", "22"),
                        new XAttribute("BL", "0.27"),
                        new XAttribute("DB", "0.4"),
                        new XAttribute("Crew", "0.25"),
                        new XAttribute("Cargo", "1.7"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "11.jpg"),
                        new XAttribute("Seaworthiness", "3T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Karack"),
                        new XAttribute("Masts", "4"),
                        new XAttribute("LengthMin", "26"),
                        new XAttribute("LengthMax", "37"),
                        new XAttribute("BL", "0.28"),
                        new XAttribute("DB", "0.36"),
                        new XAttribute("Crew", "0.3"),
                        new XAttribute("Cargo", "1.4"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "12.jpg"),
                        new XAttribute("Seaworthiness", "2T6")
                    ),
                    new XElement("BoatType",
                        new XAttribute("BoatType", "Bridad"),
                        new XAttribute("Masts", "3"),
                        new XAttribute("LengthMin", "20"),
                        new XAttribute("LengthMax", "28"),
                        new XAttribute("BL", "0.3"),
                        new XAttribute("DB", "0.28"),
                        new XAttribute("Crew", "0.35"),
                        new XAttribute("Cargo", "1.5"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "1.jpg"),
                        new XAttribute("Seaworthiness", "1T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Karack"),
                        new XAttribute("Masts", "3"),
                        new XAttribute("LengthMin", "17"),
                        new XAttribute("LengthMax", "29"),
                        new XAttribute("BL", "0.26"),
                        new XAttribute("DB", "0.38"),
                        new XAttribute("Crew", "0.33"),
                        new XAttribute("Cargo", "1.3"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "13.jpg"),
                        new XAttribute("Seaworthiness", "2T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Lanacka"),
                        new XAttribute("Masts", "2"),
                        new XAttribute("LengthMin", "13"),
                        new XAttribute("LengthMax", "24"),
                        new XAttribute("BL", "0.32"),
                        new XAttribute("DB", "0.2"),
                        new XAttribute("Crew", "0.35"),
                        new XAttribute("Cargo", "1.4"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "14.jpg"),
                        new XAttribute("Seaworthiness", "3T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Lemirier"),
                        new XAttribute("Masts", "1"),
                        new XAttribute("LengthMin", "24"),
                        new XAttribute("LengthMax", "34"),
                        new XAttribute("BL", "0.2"),
                        new XAttribute("DB", "0.32"),
                        new XAttribute("Crew", "0.2"),
                        new XAttribute("Cargo", "0.7"),
                        new XAttribute("BenchMod", "-14"),
                        new XAttribute("BenchMulti", "1.5"),
                        new XAttribute("OarsMulti", "2"),
                        new XAttribute("RowerMulti", "4"),
                        new XAttribute("IMGSource", "15.jpg"),
                        new XAttribute("Seaworthiness", "3T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Rundskepp, ett däck"),
                        new XAttribute("Masts", "1"),
                        new XAttribute("LengthMin", "12"),
                        new XAttribute("LengthMax", "25"),
                        new XAttribute("BL", "0.3"),
                        new XAttribute("DB", "0.28"),
                        new XAttribute("Crew", "0.36"),
                        new XAttribute("Cargo", "1.5"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "16.jpg"),
                        new XAttribute("Seaworthiness", "2T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Rundskepp, två däck"),
                        new XAttribute("Masts", "2"),
                        new XAttribute("LengthMin", "25"),
                        new XAttribute("LengthMax", "36"),
                        new XAttribute("BL", "0.25"),
                        new XAttribute("DB", "0.32"),
                        new XAttribute("Crew", "0.38"),
                        new XAttribute("Cargo", "1.6"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "17.jpg"),
                        new XAttribute("Seaworthiness", "2T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Sabrier, tremastad"),
                        new XAttribute("Masts", "3"),
                        new XAttribute("LengthMin", "23"),
                        new XAttribute("LengthMax", "33"),
                        new XAttribute("BL", "0.22"),
                        new XAttribute("DB", "0.35"),
                        new XAttribute("Crew", "0.4"),
                        new XAttribute("Cargo", "1.6"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "18.jpg"),
                        new XAttribute("Seaworthiness", "2T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Sabrier, tvåmastad"),
                        new XAttribute("Masts", "2"),
                        new XAttribute("LengthMin", "17"),
                        new XAttribute("LengthMax", "25"),
                        new XAttribute("BL", "0.2"),
                        new XAttribute("DB", "0.4"),
                        new XAttribute("Crew", "0.4"),
                        new XAttribute("Cargo", "1.5"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "19.jpg"),
                        new XAttribute("Seaworthiness", "2T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Slurp"),
                        new XAttribute("Masts", "1"),
                        new XAttribute("LengthMin", "8"),
                        new XAttribute("LengthMax", "14"),
                        new XAttribute("BL", "0.35"),
                        new XAttribute("DB", "0.24"),
                        new XAttribute("Crew", "0.35"),
                        new XAttribute("Cargo", "1.2"),
                        new XAttribute("BenchMod", "-5"),
                        new XAttribute("BenchMulti", "0.4"),
                        new XAttribute("OarsMulti", "2"),
                        new XAttribute("RowerMulti", "-1"),
                        new XAttribute("IMGSource", "20.jpg"),
                        new XAttribute("Seaworthiness", "3T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Umbura"),
                        new XAttribute("Masts", "1"),
                        new XAttribute("LengthMin", "27"),
                        new XAttribute("LengthMax", "37"),
                        new XAttribute("BL", "0.18"),
                        new XAttribute("DB", "0.3"),
                        new XAttribute("Crew", "0.2"),
                        new XAttribute("Cargo", "1"),
                        new XAttribute("BenchMod", "-17"),
                        new XAttribute("BenchMulti", "1.8"),
                        new XAttribute("OarsMulti", "4"),
                        new XAttribute("RowerMulti", "4"),
                        new XAttribute("IMGSource", "21.jpg"),
                        new XAttribute("Seaworthiness", "3T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Vågridare, tremastad"),
                        new XAttribute("Masts", "3"),
                        new XAttribute("LengthMin", "14"),
                        new XAttribute("LengthMax", "21"),
                        new XAttribute("BL", "0.25"),
                        new XAttribute("DB", "0.35"),
                        new XAttribute("Crew", "0.28"),
                        new XAttribute("Cargo", "1.5"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "22.jpg"),
                        new XAttribute("Seaworthiness", "2T6")
                    ),

                    new XElement("BoatType",
                        new XAttribute("BoatType", "Vågridare, tvåmastad"),
                        new XAttribute("Masts", "2"),
                        new XAttribute("LengthMin", "11"),
                        new XAttribute("LengthMax", "16"),
                        new XAttribute("BL", "0.24"),
                        new XAttribute("DB", "0.32"),
                        new XAttribute("Crew", "0.35"),
                        new XAttribute("Cargo", "1.4"),
                        new XAttribute("BenchMod", "0"),
                        new XAttribute("BenchMulti", "0"),
                        new XAttribute("OarsMulti", "0"),
                        new XAttribute("RowerMulti", "0"),
                        new XAttribute("IMGSource", "23.jpg"),
                        new XAttribute("Seaworthiness", "3T6")
                    )
                )
            );

            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/BoatbuildingSettings.xml";
            xmlDoc.Save(@filePath);
            BoatbuildingSettingsModel = LoadBoatbuildingSettingsFromXml();
        }

        #endregion

        #region Methods : ShipyardType Settings

        public List<ShipyardTypeSettingsModel> LoadShipyardTypeSettingsFromXml()
        {
            bool foundError = false;
            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/ShipyardTypeSettings.xml";
            XmlDocument doc = new XmlDocument();
            List<ShipyardTypeSettingsModel> tempList = new List<ShipyardTypeSettingsModel>();
            if (File.Exists(filePath))
            {
                doc.Load(filePath);
                XmlNodeList elemList = doc.GetElementsByTagName("Docks");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                        {
                            tempList.Add(new ShipyardTypeSettingsModel()
                            {
                                DockType = xmlAttributeCollection["DockType"].Value,
                                DockSize = Convert.ToInt32(xmlAttributeCollection["DockSize"].Value),
                                OperationBaseCostModifier = Convert.ToDecimal(xmlAttributeCollection["OperationBaseCostModifier"].Value.Replace(".", ",")),
                                OperationBaseIncomeModifier = Convert.ToDecimal(xmlAttributeCollection["OperationBaseIncomeModifier"].Value.Replace(".", ",")),
                                DockSmall = Convert.ToInt32(xmlAttributeCollection["DockSmall"].Value),
                                DockMedium = Convert.ToInt32(xmlAttributeCollection["DockMedium"].Value),
                                DockLarge = Convert.ToInt32(xmlAttributeCollection["DockLarge"].Value),
                                CrimeRate = Convert.ToInt32(xmlAttributeCollection["CrimeRate"].Value),
                                GuardBase = Convert.ToDecimal(xmlAttributeCollection["GuardBase"].Value.Replace(".", ",")),
                                MarketSilverMod = Convert.ToDecimal(xmlAttributeCollection["MarketSilverMod"].Value.Replace(".", ",")),
                                MarketBaseMod = Convert.ToDecimal(xmlAttributeCollection["MarketBaseMod"].Value.Replace(".", ",")),
                                MarketLuxuryMod = Convert.ToDecimal(xmlAttributeCollection["MarketLuxuryMod"].Value.Replace(".", ",")),
                                MarketWoodMod = Convert.ToDecimal(xmlAttributeCollection["MarketWoodMod"].Value.Replace(".", ",")),
                                MarketStoneMod = Convert.ToDecimal(xmlAttributeCollection["MarketStoneMod"].Value.Replace(".", ",")),
                                MarketIronMod = Convert.ToDecimal(xmlAttributeCollection["MarketIronMod"].Value.Replace(".", ",")),
                                BuildCostSilver = Convert.ToInt32(xmlAttributeCollection["BuildCostSilver"].Value),
                                BuildCostBase = Convert.ToInt32(xmlAttributeCollection["BuildCostBase"].Value),
                                BuildCostWood = Convert.ToInt32(xmlAttributeCollection["BuildCostWood"].Value),
                                BuildCostStone = Convert.ToInt32(xmlAttributeCollection["BuildCostStone"].Value),
                                BuildCostIron = Convert.ToInt32(xmlAttributeCollection["BuildCostIron"].Value),
                                DaysWork = Convert.ToInt32(xmlAttributeCollection["DaysWork"].Value),
                                TaxMod = Convert.ToDecimal(xmlAttributeCollection["TaxMod"].Value.Replace(".", ",")),
                                Workers = Convert.ToString(xmlAttributeCollection["Workers"].Value)
                            });
                        }
                        else
                        {
                            tempList.Add(new ShipyardTypeSettingsModel()
                            {
                                DockType = xmlAttributeCollection["DockType"].Value,
                                DockSize = Convert.ToInt32(xmlAttributeCollection["DockSize"].Value),
                                OperationBaseCostModifier = Convert.ToDecimal(xmlAttributeCollection["OperationBaseCostModifier"].Value),
                                OperationBaseIncomeModifier = Convert.ToDecimal(xmlAttributeCollection["OperationBaseIncomeModifier"].Value),
                                DockSmall = Convert.ToInt32(xmlAttributeCollection["DockSmall"].Value),
                                DockMedium = Convert.ToInt32(xmlAttributeCollection["DockMedium"].Value),
                                DockLarge = Convert.ToInt32(xmlAttributeCollection["DockLarge"].Value),
                                CrimeRate = Convert.ToInt32(xmlAttributeCollection["CrimeRate"].Value),
                                GuardBase = Convert.ToDecimal(xmlAttributeCollection["GuardBase"].Value),
                                MarketSilverMod = Convert.ToDecimal(xmlAttributeCollection["MarketSilverMod"].Value),
                                MarketBaseMod = Convert.ToDecimal(xmlAttributeCollection["MarketBaseMod"].Value),
                                MarketLuxuryMod = Convert.ToDecimal(xmlAttributeCollection["MarketLuxuryMod"].Value),
                                MarketWoodMod = Convert.ToDecimal(xmlAttributeCollection["MarketWoodMod"].Value),
                                MarketStoneMod = Convert.ToDecimal(xmlAttributeCollection["MarketStoneMod"].Value),
                                MarketIronMod = Convert.ToDecimal(xmlAttributeCollection["MarketIronMod"].Value),
                                BuildCostSilver = Convert.ToInt32(xmlAttributeCollection["BuildCostSilver"].Value),
                                BuildCostBase = Convert.ToInt32(xmlAttributeCollection["BuildCostBase"].Value),
                                BuildCostWood = Convert.ToInt32(xmlAttributeCollection["BuildCostWood"].Value),
                                BuildCostStone = Convert.ToInt32(xmlAttributeCollection["BuildCostStone"].Value),
                                BuildCostIron = Convert.ToInt32(xmlAttributeCollection["BuildCostIron"].Value),
                                DaysWork = Convert.ToInt32(xmlAttributeCollection["DaysWork"].Value),
                                TaxMod = Convert.ToDecimal(xmlAttributeCollection["TaxMod"].Value),
                                Workers = Convert.ToString(xmlAttributeCollection["Workers"].Value)
                            });
                        }
                    }
                    else
                    {
                        foundError = true;
                    }
                }
            }
            else
            {
                foundError = true;
            }

            if (foundError)
            {
                CreateDefaultShipyardTypeSettingsXmlFile();
                return null;
            }
            else
            {
                return tempList;
            }
        }

        public void CreateDefaultShipyardTypeSettingsXmlFile()
        {
            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", string.Empty),
                new XElement("Settings",
                    new XElement("Docks",
                        new XAttribute("DockType", "Byhamn"),
                        new XAttribute("DockSize", "0"),
                        new XAttribute("OperationBaseCostModifier", "8"),
                        new XAttribute("OperationBaseIncomeModifier", "13.36"),
                        new XAttribute("DockSmall", "2"),
                        new XAttribute("DockMedium", "0"),
                        new XAttribute("DockLarge", "0"),
                        new XAttribute("CrimeRate", "0"),
                        new XAttribute("GuardBase", "0"),
                        new XAttribute("MarketSilverMod", "1.67"), // ^ 1.67 per DockSize
                        new XAttribute("MarketBaseMod", "1.25"), // ^ 1.56
                        new XAttribute("MarketLuxuryMod", "1.23"), // ^ 1.49
                        new XAttribute("MarketWoodMod", "1.21"), // ^ 1.43
                        new XAttribute("MarketStoneMod", "1.21"), // ^ 1.37
                        new XAttribute("MarketIronMod", "1.21"), // ^ 1.29
                        new XAttribute("BuildCostSilver", "0"),
                        new XAttribute("BuildCostBase", "25"),
                        new XAttribute("BuildCostWood", "30"),
                        new XAttribute("BuildCostStone", "0"),
                        new XAttribute("BuildCostIron", "0"),
                        new XAttribute("DaysWork", "2500"),
                        new XAttribute("TaxMod", "1.15"),
                        new XAttribute("Workers", "3T6")
                    ),
                    new XElement("Docks",
                        new XAttribute("DockType", "Fiskehamn"),
                        new XAttribute("DockSize", "1"),
                        new XAttribute("OperationBaseCostModifier", "10"),
                        new XAttribute("OperationBaseIncomeModifier", "33.4"),
                        new XAttribute("DockSmall", "4"),
                        new XAttribute("DockMedium", "1"),
                        new XAttribute("DockLarge", "0"),
                        new XAttribute("CrimeRate", "1"),
                        new XAttribute("GuardBase", "1"),
                        new XAttribute("MarketSilverMod", "2.355"),
                        new XAttribute("MarketBaseMod", "1.416"),
                        new XAttribute("MarketLuxuryMod", "1.361"),
                        new XAttribute("MarketWoodMod", "1.313"),
                        new XAttribute("MarketStoneMod", "1.298"),
                        new XAttribute("MarketIronMod", "1.279"),
                        new XAttribute("BuildCostSilver", "0"),
                        new XAttribute("BuildCostBase", "75"),
                        new XAttribute("BuildCostWood", "60"),
                        new XAttribute("BuildCostStone", "20"),
                        new XAttribute("BuildCostIron", "0"),
                        new XAttribute("DaysWork", "5000"),
                        new XAttribute("TaxMod", "1.3"),
                        new XAttribute("Workers", "9T6")
                    ),
                    new XElement("Docks",
                        new XAttribute("DockType", "Liten hamn"),
                        new XAttribute("DockSize", "2"),
                        new XAttribute("OperationBaseCostModifier", "12.5"),
                        new XAttribute("OperationBaseIncomeModifier", "62.625"),
                        new XAttribute("DockSmall", "8"),
                        new XAttribute("DockMedium", "2"),
                        new XAttribute("DockLarge", "0"),
                        new XAttribute("CrimeRate", "3"),
                        new XAttribute("GuardBase", "10"),
                        new XAttribute("MarketSilverMod", "4.18"),
                        new XAttribute("MarketBaseMod", "1.721"),
                        new XAttribute("MarketLuxuryMod", "1.583"),
                        new XAttribute("MarketWoodMod", "1.477"),
                        new XAttribute("MarketStoneMod", "1.43"),
                        new XAttribute("MarketIronMod", "1.373"),
                        new XAttribute("BuildCostSilver", "0"),
                        new XAttribute("BuildCostBase", "225"),
                        new XAttribute("BuildCostWood", "300"),
                        new XAttribute("BuildCostStone", "1000"),
                        new XAttribute("BuildCostIron", "25"),
                        new XAttribute("DaysWork", "10000"),
                        new XAttribute("TaxMod", "1.45"),
                        new XAttribute("Workers", "27T6")
                    ),

                    new XElement("Docks",
                        new XAttribute("DockType", "Hamn"),
                        new XAttribute("DockSize", "3"),
                        new XAttribute("OperationBaseCostModifier", "18.75"),
                        new XAttribute("OperationBaseIncomeModifier", "125.25"),
                        new XAttribute("DockSmall", "16"),
                        new XAttribute("DockMedium", "4"),
                        new XAttribute("DockLarge", "1"),
                        new XAttribute("CrimeRate", "9"),
                        new XAttribute("GuardBase", "25"),
                        new XAttribute("MarketSilverMod", "10.897"),
                        new XAttribute("MarketBaseMod", "2.333"),
                        new XAttribute("MarketLuxuryMod", "1.361"),
                        new XAttribute("MarketWoodMod", "1.746"),
                        new XAttribute("MarketStoneMod", "1.633"),
                        new XAttribute("MarketIronMod", "1.506"),
                        new XAttribute("BuildCostSilver", "0"),
                        new XAttribute("BuildCostBase", "675"),
                        new XAttribute("BuildCostWood", "600"),
                        new XAttribute("BuildCostStone", "5000"),
                        new XAttribute("BuildCostIron", "150"),
                        new XAttribute("DaysWork", "20000"),
                        new XAttribute("TaxMod", "1.6"),
                        new XAttribute("Workers", "81T6")
                    ),

                    new XElement("Docks",
                        new XAttribute("DockType", "Handelshamn"),
                        new XAttribute("DockSize", "4"),
                        new XAttribute("OperationBaseCostModifier", "28.125"),
                        new XAttribute("OperationBaseIncomeModifier", "261.459"),
                        new XAttribute("DockSmall", "32"),
                        new XAttribute("DockMedium", "8"),
                        new XAttribute("DockLarge", "2"),
                        new XAttribute("CrimeRate", "27"),
                        new XAttribute("GuardBase", "62.5"),
                        new XAttribute("MarketSilverMod", "53.986"),
                        new XAttribute("MarketBaseMod", "3.749"),
                        new XAttribute("MarketLuxuryMod", "2.774"),
                        new XAttribute("MarketWoodMod", "2.219"),
                        new XAttribute("MarketStoneMod", "1.957"),
                        new XAttribute("MarketIronMod", "1.695"),
                        new XAttribute("BuildCostSilver", "0"),
                        new XAttribute("BuildCostBase", "2025"),
                        new XAttribute("BuildCostWood", "3000"),
                        new XAttribute("BuildCostStone", "12500"),
                        new XAttribute("BuildCostIron", "450"),
                        new XAttribute("DaysWork", "40000"),
                        new XAttribute("TaxMod", "1.75"),
                        new XAttribute("Workers", "243T6")
                    ),

                    new XElement("Docks",
                        new XAttribute("DockType", "Stor handelshamn"),
                        new XAttribute("DockSize", "5"),
                        new XAttribute("OperationBaseCostModifier", "42.188"),
                        new XAttribute("OperationBaseIncomeModifier", "523.965"),
                        new XAttribute("DockSmall", "64"),
                        new XAttribute("DockMedium", "16"),
                        new XAttribute("DockLarge", "4"),
                        new XAttribute("CrimeRate", "81"),
                        new XAttribute("GuardBase", "156.25"),
                        new XAttribute("MarketSilverMod", "781.463"),
                        new XAttribute("MarketBaseMod", "7.859"),
                        new XAttribute("MarketLuxuryMod", "4.574"),
                        new XAttribute("MarketWoodMod", "3.126"),
                        new XAttribute("MarketStoneMod", "2.509"),
                        new XAttribute("MarketIronMod", "1.976"),
                        new XAttribute("BuildCostSilver", "0"),
                        new XAttribute("BuildCostBase", "6075"),
                        new XAttribute("BuildCostWood", "6000"),
                        new XAttribute("BuildCostStone", "25000"),
                        new XAttribute("BuildCostIron", "1350"),
                        new XAttribute("DaysWork", "80000"),
                        new XAttribute("TaxMod", "1.9"),
                        new XAttribute("Workers", "729T6")
                    ),

                    new XElement("Docks",
                        new XAttribute("DockType", "Enorm handelshamn"),
                        new XAttribute("DockSize", "6"),
                        new XAttribute("OperationBaseCostModifier", "63.281"),
                        new XAttribute("OperationBaseIncomeModifier", "1020.858"),
                        new XAttribute("DockSmall", "128"),
                        new XAttribute("DockMedium", "32"),
                        new XAttribute("DockLarge", "8"),
                        new XAttribute("CrimeRate", "243"),
                        new XAttribute("GuardBase", "390.625"),
                        new XAttribute("MarketSilverMod", "67788.674"),
                        new XAttribute("MarketBaseMod", "24.931"),
                        new XAttribute("MarketLuxuryMod", "9.634"),
                        new XAttribute("MarketWoodMod", "5.104"),
                        new XAttribute("MarketStoneMod", "3.527"),
                        new XAttribute("MarketIronMod", "2.407"),
                        new XAttribute("BuildCostSilver", "0"),
                        new XAttribute("BuildCostBase", "18225"),
                        new XAttribute("BuildCostWood", "10000"),
                        new XAttribute("BuildCostStone", "50000"),
                        new XAttribute("BuildCostIron", "4250"),
                        new XAttribute("DaysWork", "160000"),
                        new XAttribute("TaxMod", "2.05"),
                        new XAttribute("Workers", "2187T6")
                    )
                )
            );

            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/ShipyardTypeSettings.xml";
            xmlDoc.Save(@filePath);
            ShipyardTypeSettingsList = LoadShipyardTypeSettingsFromXml();
        }

        #endregion

        #region Methods : Livingconditions Settings

        public LivingconditionsSettingsModel LoadLivingconditionsSettingsFromXml()
        {
            bool foundError = false;
            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/LivingconditionsSettings.xml";
            XmlDocument doc = new XmlDocument();
            List<LivingconditionModel> tempList = new List<LivingconditionModel>();
            LivingconditionsSettingsModel livingconditionsSettingsModel = new LivingconditionsSettingsModel();
            if (File.Exists(filePath))
            {
                doc.Load(filePath);
                XmlNodeList elemList = doc.GetElementsByTagName("Livingcondition");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        tempList.Add(
                            new LivingconditionModel()
                            {
                                Livingcondition = xmlAttributeCollection["Livingcondition"].Value,
                                BaseCost = Convert.ToInt32(xmlAttributeCollection["Base"].Value),
                                LuxuryCost = Convert.ToInt32(xmlAttributeCollection["Luxury"].Value),
                                Focus = Convert.ToInt32(xmlAttributeCollection["Focus"].Value),
                                Wellbeing = Convert.ToInt32(xmlAttributeCollection["Wellbeing"].Value)
                            }
                        );
                    }
                    else
                    {
                        foundError = true;
                    }
                }
            }
            else
            {
                foundError = true;
            }

            if (foundError)
            {
                CreateDefaultLivingconditionsSettingsXmlFile();
                return null;
            }
            else
            {
                livingconditionsSettingsModel.LivingconditionsList = tempList;
                return livingconditionsSettingsModel;
            }
        }

        public void CreateDefaultLivingconditionsSettingsXmlFile()
        {
            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", string.Empty),
                new XElement("Settings",
                    new XElement("Livingcondition",
                        new XAttribute("Livingcondition", "Nödtorftig"),
                        new XAttribute("Base", "1"),
                        new XAttribute("Luxury", "0"),
                        new XAttribute("Focus", "-4"),
                        new XAttribute("Wellbeing", "-2")
                    ),
                    new XElement("Livingcondition",
                        new XAttribute("Livingcondition", "Gemen"),
                        new XAttribute("Base", "2"),
                        new XAttribute("Luxury", "0"),
                        new XAttribute("Focus", "-2"),
                        new XAttribute("Wellbeing", "-1")
                    ),
                    new XElement("Livingcondition",
                        new XAttribute("Livingcondition", "God"),
                        new XAttribute("Base", "2"),
                        new XAttribute("Luxury", "2"),
                        new XAttribute("Focus", "2"),
                        new XAttribute("Wellbeing", "1")
                    ),
                    new XElement("Livingcondition",
                        new XAttribute("Livingcondition", "Mycket god"),
                        new XAttribute("Base", "3"),
                        new XAttribute("Luxury", "4"),
                        new XAttribute("Focus", "4"),
                        new XAttribute("Wellbeing", "2")
                    ),
                    new XElement("Livingcondition",
                        new XAttribute("Livingcondition", "Lyxliv"),
                        new XAttribute("Base", "6"),
                        new XAttribute("Luxury", "8"),
                        new XAttribute("Focus", "6"),
                        new XAttribute("Wellbeing", "3")
                    )
                )
            );

            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/LivingconditionsSettings.xml";
            xmlDoc.Save(@filePath);
            LivingconditionsSettingsModel = LoadLivingconditionsSettingsFromXml();
        }

        #endregion

        #region Methods : Stable Settings

        public StableSettingsModel LoadStableSettingsFromXml()
        {
            bool foundError = false;
            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/StableSettings.xml";
            XmlDocument doc = new XmlDocument();
            List<AnimalModel> tempList = new List<AnimalModel>();
            StableSettingsModel stableSettingsModel = new StableSettingsModel();
            if (File.Exists(filePath))
            {
                doc.Load(filePath);
                XmlNodeList elemList = doc.GetElementsByTagName("Stable");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        tempList.Add(
                            new AnimalModel()
                            {
                                Animal = xmlAttributeCollection["Animal"].Value,
                                BaseCost = Convert.ToInt32(xmlAttributeCollection["Base"].Value)
                            }
                        );
                    }
                    else
                    {
                        foundError = true;
                    }
                }
            }
            else
            {
                foundError = true;
            }

            if (foundError)
            {
                CreateDefaultStableSettingsXmlFile();
                return null;
            }
            else
            {
                stableSettingsModel.StableList = tempList;
                return stableSettingsModel;
            }
        }

        public void CreateDefaultStableSettingsXmlFile()
        {
            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", string.Empty),
                new XElement("Settings",
                    new XElement("Stable",
                        new XAttribute("Animal", "Ridhäst"),
                        new XAttribute("BaseCost", "2")
                    ),
                    new XElement("Stable",
                        new XAttribute("Animal", "Stridshäst"),
                        new XAttribute("BaseCost", "4")
                    )
                )
            );

            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/StableSettings.xml";
            xmlDoc.Save(@filePath);
            StableSettingsModel = LoadStableSettingsFromXml();
        }

        #endregion

        #region Methods : Expenses Settings

        public ExpensesSettingsModel LoadExpensesSettingsFromXml()
        {
            bool foundError = false;
            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/ExpensesSettings.xml";
            XmlDocument doc = new XmlDocument();
            List<RoadModel> roadList = new List<RoadModel>();
            List<EventModel> eventList = new List<EventModel>();
            ExpensesSettingsModel expensesSettingsModel = new ExpensesSettingsModel();
            if (File.Exists(filePath))
            {
                doc.Load(filePath);
                XmlNodeList elemList = doc.GetElementsByTagName("FeedingPoorFactor");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                        {
                            expensesSettingsModel.FeedingPoorFactor = Convert.ToDecimal(xmlAttributeCollection["Factor"].Value.Replace(".", ","));
                        }
                        else
                        {
                            expensesSettingsModel.FeedingPoorFactor = Convert.ToDecimal(xmlAttributeCollection["Factor"].Value);
                        }
                    }
                    else
                    {
                        foundError = true;
                    }
                }

                elemList = doc.GetElementsByTagName("FeedingDaysWorkFactor");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                        {
                            expensesSettingsModel.FeedingDaysWorkFactor = Convert.ToDecimal(xmlAttributeCollection["Factor"].Value.Replace(".", ","));
                        }
                        else
                        {
                            expensesSettingsModel.FeedingDaysWorkFactor = Convert.ToDecimal(xmlAttributeCollection["Factor"].Value);
                        }
                    }
                    else
                    {
                        foundError = true;
                    }
                }

                elemList = doc.GetElementsByTagName("ManorMaintenanceFactor");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                        {
                            expensesSettingsModel.ManorMaintenanceFactor = Convert.ToDecimal(xmlAttributeCollection["Factor"].Value.Replace(".", ","));
                        }
                        else
                        {
                            expensesSettingsModel.ManorMaintenanceFactor = Convert.ToDecimal(xmlAttributeCollection["Factor"].Value);
                        }
                    }
                    else
                    {
                        foundError = true;
                    }
                }
                roadList = InformationSettingsModel.RoadTypesList;

                elemList = doc.GetElementsByTagName("DayWorkersBaseCost");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        expensesSettingsModel.DayWorkersBaseCost = Convert.ToInt32(xmlAttributeCollection["DayWorkersBaseCost"].Value);
                    }
                    else
                    {
                        foundError = true;
                    }
                }

                elemList = doc.GetElementsByTagName("SlavesBaseCost");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        expensesSettingsModel.SlavesBaseCost = Convert.ToInt32(xmlAttributeCollection["SlavesBaseCost"].Value);
                    }
                    else
                    {
                        foundError = true;
                    }
                }

                elemList = doc.GetElementsByTagName("Event");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        eventList.Add(new EventModel()
                        {
                            EventName = xmlAttributeCollection["Event"].Value,
                            SilverCost = Convert.ToInt32(xmlAttributeCollection["SilverCost"].Value),
                            BaseCost = Convert.ToInt32(xmlAttributeCollection["BaseCost"].Value),
                            LuxuryCost = Convert.ToInt32(xmlAttributeCollection["LuxuryCost"].Value)
                        });
                    }
                    else
                    {
                        foundError = true;
                        break;
                    }
                }
            }
            else
            {
                foundError = true;
            }

            expensesSettingsModel.RoadsList = roadList;
            expensesSettingsModel.EventList = eventList;

            if (!foundError)
            {
                return expensesSettingsModel;
            }
            else
            {
                CreateDefaultExpensesSettingsXmlFile();
                return null;
            }
        }

        public void CreateDefaultExpensesSettingsXmlFile()
        {
            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", string.Empty),
                new XElement("Settings",
                    new XElement("FeedingPoorFactor",
                        new XAttribute("Factor", "0.05")
                    ),
                    new XElement("FeedingDaysWorkFactor",
                        new XAttribute("Factor", "0.0025")
                    ),
                    new XElement("ManorMaintenanceFactor",
                        new XAttribute("Factor", "0.025")
                    ),
                    new XElement("DayWorkersBaseCost",
                        new XAttribute("DayWorkersBaseCost", "2")
                    ),
                    new XElement("SlavesBaseCost",
                        new XAttribute("SlavesBaseCost", "1")
                    ),
                    new XElement("Event",
                        new XAttribute("Event", "Feast"),
                        new XAttribute("SilverCost", "0"),
                        new XAttribute("BaseCost", "8"),
                        new XAttribute("LuxuryCost", "6")
                    ),
                    new XElement("Event",
                        new XAttribute("Event", "People"),
                        new XAttribute("SilverCost", "0"),
                        new XAttribute("BaseCost", "16"),
                        new XAttribute("LuxuryCost", "2")
                    ),
                    new XElement("Event",
                        new XAttribute("Event", "Religious"),
                        new XAttribute("SilverCost", "1000"),
                        new XAttribute("BaseCost", "12"),
                        new XAttribute("LuxuryCost", "6")
                    ),
                    new XElement("Event",
                        new XAttribute("Event", "Tourney"),
                        new XAttribute("SilverCost", "10000"),
                        new XAttribute("BaseCost", "24"),
                        new XAttribute("LuxuryCost", "10")
                    )
                )
            );

            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/ExpensesSettings.xml";
            xmlDoc.Save(@filePath);
            ExpensesSettingsModel = LoadExpensesSettingsFromXml();
        }

        #endregion

        #region Methods : Subsidiary Settings

        public List<SubsidiarySettingsModel> LoadSubsidiarySettingsFromXml()
        {
            bool foundError = false;
            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/SubsidiarySettings.xml";
            XmlDocument doc = new XmlDocument();
            List<SubsidiarySettingsModel> settingsList = new List<SubsidiarySettingsModel>();
            if (File.Exists(filePath))
            {
                doc.Load(filePath);
                XmlNodeList elemList = doc.GetElementsByTagName("Subsidiary");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                        {
                            settingsList.Add(new SubsidiarySettingsModel()
                            {
                                Subsidiary = xmlAttributeCollection["Subsidiary"].Value,
                                IncomeFactor = Convert.ToDecimal(xmlAttributeCollection["IncomeFactor"].Value.Replace(".", ",")),
                                IncomeBase = Convert.ToDecimal(xmlAttributeCollection["IncomeBase"].Value.Replace(".", ",")),
                                IncomeLuxury = Convert.ToDecimal(xmlAttributeCollection["IncomeLuxury"].Value.Replace(".", ",")),
                                IncomeSilver = Convert.ToDecimal(xmlAttributeCollection["IncomeSilver"].Value.Replace(".", ",")),
                                DaysWorkUpkeep = Convert.ToInt32(xmlAttributeCollection["DaysWorkUpkeep"].Value),
                                DaysWorkBuild = Convert.ToInt32(xmlAttributeCollection["DaysWorkBuild"].Value),
                                Spring = Convert.ToDecimal(xmlAttributeCollection["Spring"].Value.Replace(".", ",")),
                                Summer = Convert.ToDecimal(xmlAttributeCollection["Summer"].Value.Replace(".", ",")),
                                Fall = Convert.ToDecimal(xmlAttributeCollection["Fall"].Value.Replace(".", ",")),
                                Winter = Convert.ToDecimal(xmlAttributeCollection["Winter"].Value.Replace(".", ","))
                            });
                        }
                        else
                        {
                            settingsList.Add(new SubsidiarySettingsModel()
                            {
                                Subsidiary = xmlAttributeCollection["Subsidiary"].Value,
                                IncomeFactor = Convert.ToDecimal(xmlAttributeCollection["IncomeFactor"].Value),
                                IncomeBase = Convert.ToDecimal(xmlAttributeCollection["IncomeBase"].Value),
                                IncomeLuxury = Convert.ToDecimal(xmlAttributeCollection["IncomeLuxury"].Value),
                                IncomeSilver = Convert.ToDecimal(xmlAttributeCollection["IncomeSilver"].Value),
                                DaysWorkUpkeep = Convert.ToInt32(xmlAttributeCollection["DaysWorkUpkeep"].Value),
                                DaysWorkBuild = Convert.ToInt32(xmlAttributeCollection["DaysWorkBuild"].Value),
                                Spring = Convert.ToDecimal(xmlAttributeCollection["Spring"].Value),
                                Summer = Convert.ToDecimal(xmlAttributeCollection["Summer"].Value),
                                Fall = Convert.ToDecimal(xmlAttributeCollection["Fall"].Value),
                                Winter = Convert.ToDecimal(xmlAttributeCollection["Winter"].Value)
                            });
                        }
                    }
                    else
                    {
                        foundError = true;
                    }
                }
            }
            else
            {
                foundError = true;
            }

            if (!foundError)
            {
                return settingsList;
            }
            else
            {
                CreateDefaultSubsidiarySettingsXmlFile();
                return null;
            }
        }

        public void CreateDefaultSubsidiarySettingsXmlFile()
        {
            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", string.Empty),
                new XElement("Settings",
                    new XElement("Subsidiary",
                        new XAttribute("Subsidiary", "Biodling"),
                        new XAttribute("IncomeFactor", "15"),
                        new XAttribute("IncomeBase", "0.65"),
                        new XAttribute("IncomeLuxury", "0.25"),
                        new XAttribute("IncomeSilver", "1"),
                        new XAttribute("DaysWorkUpkeep", "365"),
                        new XAttribute("DaysWorkBuild", "730"),
                        new XAttribute("Spring", "0.35"),
                        new XAttribute("Summer", "0.5"),
                        new XAttribute("Fall", "0.1"),
                        new XAttribute("Winter", "0.05")
                    ),
                    new XElement("Subsidiary",
                        new XAttribute("Subsidiary", "Fiskdammar"),
                        new XAttribute("IncomeFactor", "0.2"),
                        new XAttribute("IncomeBase", "1.0"),
                        new XAttribute("IncomeLuxury", "0"),
                        new XAttribute("IncomeSilver", "0"),
                        new XAttribute("DaysWorkUpkeep", "365"),
                        new XAttribute("DaysWorkBuild", "1095"),
                        new XAttribute("Spring", "0.35"),
                        new XAttribute("Summer", "0.35"),
                        new XAttribute("Fall", "0.25"),
                        new XAttribute("Winter", "0.05")
                    ),
                    new XElement("Subsidiary",
                        new XAttribute("Subsidiary", "Glasbruk"),
                        new XAttribute("IncomeFactor", "0.2"),
                        new XAttribute("IncomeBase", "0"),
                        new XAttribute("IncomeLuxury", "0.75"),
                        new XAttribute("IncomeSilver", "0.25"),
                        new XAttribute("DaysWorkUpkeep", "1095"),
                        new XAttribute("DaysWorkBuild", "4225"),
                        new XAttribute("Spring", "0"),
                        new XAttribute("Summer", "0"),
                        new XAttribute("Fall", "0"),
                        new XAttribute("Winter", "0")
                    ),
                    new XElement("Subsidiary",
                        new XAttribute("Subsidiary", "Olivlundar"),
                        new XAttribute("IncomeFactor", "20"),
                        new XAttribute("IncomeBase", "0.7"),
                        new XAttribute("IncomeLuxury", "0.2"),
                        new XAttribute("IncomeSilver", "0.1"),
                        new XAttribute("DaysWorkUpkeep", "1460"),
                        new XAttribute("DaysWorkBuild", "5320"),
                        new XAttribute("Spring", "0.25"),
                        new XAttribute("Summer", "0.35"),
                        new XAttribute("Fall", "0.25"),
                        new XAttribute("Winter", "0.15")
                    ),
                    new XElement("Subsidiary",
                        new XAttribute("Subsidiary", "Vindistrikt"),
                        new XAttribute("IncomeFactor", "20"),
                        new XAttribute("IncomeBase", "0.3"),
                        new XAttribute("IncomeLuxury", "0.4"),
                        new XAttribute("IncomeSilver", "0.3"),
                        new XAttribute("DaysWorkUpkeep", "2920"),
                        new XAttribute("DaysWorkBuild", "5320"),
                        new XAttribute("Spring", "0.25"),
                        new XAttribute("Summer", "0.3"),
                        new XAttribute("Fall", "0.25"),
                        new XAttribute("Winter", "0.2")
                    ),
                    new XElement("Subsidiary",
                        new XAttribute("Subsidiary", "Äppellundar"),
                        new XAttribute("IncomeFactor", "20"),
                        new XAttribute("IncomeBase", "1.0"),
                        new XAttribute("IncomeLuxury", "0"),
                        new XAttribute("IncomeSilver", "0"),
                        new XAttribute("DaysWorkUpkeep", "730"),
                        new XAttribute("DaysWorkBuild", "3285"),
                        new XAttribute("Spring", "0.3"),
                        new XAttribute("Summer", "0.3"),
                        new XAttribute("Fall", "0.3"),
                        new XAttribute("Winter", "0.1")
                    )
                )
            );

            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/SubsidiarySettings.xml";
            xmlDoc.Save(@filePath);
            SubsidiarySettingsList = LoadSubsidiarySettingsFromXml();

        }

        #endregion

        #region Methods : Building Settings

        public List<BuildingsSettingsModel> LoadBuildingsSettingsFromXml()
        {
            bool foundError = false;
            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/BuildingsSettings.xml";
            XmlDocument doc = new XmlDocument();
            List<BuildingsSettingsModel> settingsList = new List<BuildingsSettingsModel>();
            if (File.Exists(filePath))
            {
                doc.Load(filePath);
                XmlNodeList elemList = doc.GetElementsByTagName("Building");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlAttributeCollection xmlAttributeCollection = elemList[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                        {
                            settingsList.Add(new BuildingsSettingsModel()
                            {
                                Building = xmlAttributeCollection["Building"].Value,
                                Woodwork = Convert.ToInt32(xmlAttributeCollection["Woodwork"].Value),
                                Stonework = Convert.ToInt32(xmlAttributeCollection["Stonework"].Value),
                                Smithwork = Convert.ToInt32(xmlAttributeCollection["Smithswork"].Value),
                                Wood = Convert.ToInt32(xmlAttributeCollection["Wood"].Value),
                                Stone = Convert.ToInt32(xmlAttributeCollection["Stone"].Value),
                                Iron = Convert.ToInt32(xmlAttributeCollection["Iron"].Value),
                                Upkeep = Convert.ToDecimal(xmlAttributeCollection["Upkeep"].Value.Replace(".", ","))
                            });
                        }
                        else
                        {
                            settingsList.Add(new BuildingsSettingsModel()
                            {
                                Building = xmlAttributeCollection["Building"].Value,
                                Woodwork = Convert.ToInt32(xmlAttributeCollection["Woodwork"].Value),
                                Stonework = Convert.ToInt32(xmlAttributeCollection["Stonework"].Value),
                                Smithwork = Convert.ToInt32(xmlAttributeCollection["Smithswork"].Value),
                                Wood = Convert.ToInt32(xmlAttributeCollection["Wood"].Value),
                                Stone = Convert.ToInt32(xmlAttributeCollection["Stone"].Value),
                                Iron = Convert.ToInt32(xmlAttributeCollection["Iron"].Value),
                                Upkeep = Convert.ToDecimal(xmlAttributeCollection["Upkeep"].Value)
                            });
                        }
                    }
                    else
                    {
                        foundError = true;
                    }
                }
            }
            else
            {
                foundError = true;
            }

            if (!foundError)
            {
                return settingsList;
            }
            else
            {
                CreateDefaultBuildingsSettingsXmlFile();
                return null;
            }
        }

        public void CreateDefaultBuildingsSettingsXmlFile()
        {
            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", string.Empty),
                new XElement("Settings",
                    new XElement("Building",
                        new XAttribute("Building", "Vall"),
                        new XAttribute("Woodwork", "0"),
                        new XAttribute("Stonework", "40"),
                        new XAttribute("Smithswork", "0"),
                        new XAttribute("Wood", "0"),
                        new XAttribute("Stone", "0"),
                        new XAttribute("Iron", "0"),
                        new XAttribute("Upkeep", "0.1")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Vall och grav"),
                        new XAttribute("Woodwork", "0"),
                        new XAttribute("Stonework", "60"),
                        new XAttribute("Smithswork", "0"),
                        new XAttribute("Wood", "0"),
                        new XAttribute("Stone", "0"),
                        new XAttribute("Iron", "0"),
                        new XAttribute("Upkeep", "0.2")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Palissad"),
                        new XAttribute("Woodwork", "20"),
                        new XAttribute("Stonework", "0"),
                        new XAttribute("Smithswork", "0"),
                        new XAttribute("Wood", "5"),
                        new XAttribute("Stone", "0"),
                        new XAttribute("Iron", "0"),
                        new XAttribute("Upkeep", "0.1")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Dubbel palissad"),
                        new XAttribute("Woodwork", "60"),
                        new XAttribute("Stonework", "0"),
                        new XAttribute("Smithswork", "0"),
                        new XAttribute("Wood", "15"),
                        new XAttribute("Stone", "0"),
                        new XAttribute("Iron", "0"),
                        new XAttribute("Upkeep", "0.2")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Mur"),
                        new XAttribute("Woodwork", "53"),
                        new XAttribute("Stonework", "1100"),
                        new XAttribute("Smithswork", "0"),
                        new XAttribute("Wood", "0"),
                        new XAttribute("Stone", "53"),
                        new XAttribute("Iron", "0"),
                        new XAttribute("Upkeep", "0.4")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Dubbel mur"),
                        new XAttribute("Woodwork", "150"),
                        new XAttribute("Stonework", "3000"),
                        new XAttribute("Smithswork", "0"),
                        new XAttribute("Wood", "0"),
                        new XAttribute("Stone", "150"),
                        new XAttribute("Iron", "0"),
                        new XAttribute("Upkeep", "0.6")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Tjock mur"),
                        new XAttribute("Woodwork", "750"),
                        new XAttribute("Stonework", "5000"),
                        new XAttribute("Smithswork", "0"),
                        new XAttribute("Wood", "0"),
                        new XAttribute("Stone", "750"),
                        new XAttribute("Iron", "0"),
                        new XAttribute("Upkeep", "0.8")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Massiv mur"),
                        new XAttribute("Woodwork", "4000"),
                        new XAttribute("Stonework", "35000"),
                        new XAttribute("Smithswork", "0"),
                        new XAttribute("Wood", "0"),
                        new XAttribute("Stone", "4000"),
                        new XAttribute("Iron", "0"),
                        new XAttribute("Upkeep", "1")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Mur med stävpelare"),
                        new XAttribute("Woodwork", "58"),
                        new XAttribute("Stonework", "1200"),
                        new XAttribute("Smithswork", "0"),
                        new XAttribute("Wood", "0"),
                        new XAttribute("Stone", "58"),
                        new XAttribute("Iron", "0"),
                        new XAttribute("Upkeep", "0.45")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Litet porttorn"),
                        new XAttribute("Woodwork", "92"),
                        new XAttribute("Stonework", "1400"),
                        new XAttribute("Smithswork", "12"),
                        new XAttribute("Wood", "6"),
                        new XAttribute("Stone", "68"),
                        new XAttribute("Iron", "12"),
                        new XAttribute("Upkeep", "1")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Porttorn"),
                        new XAttribute("Woodwork", "270"),
                        new XAttribute("Stonework", "4400"),
                        new XAttribute("Smithswork", "24"),
                        new XAttribute("Wood", "12"),
                        new XAttribute("Stone", "220"),
                        new XAttribute("Iron", "24"),
                        new XAttribute("Upkeep", "2")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Stort porttorn"),
                        new XAttribute("Woodwork", "1500"),
                        new XAttribute("Stonework", "28000"),
                        new XAttribute("Smithswork", "42"),
                        new XAttribute("Wood", "21"),
                        new XAttribute("Stone", "1400"),
                        new XAttribute("Iron", "42"),
                        new XAttribute("Upkeep", "4")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Trätorn"),
                        new XAttribute("Woodwork", "88"),
                        new XAttribute("Stonework", "0"),
                        new XAttribute("Smithswork", "44"),
                        new XAttribute("Wood", "22"),
                        new XAttribute("Stone", "1400"),
                        new XAttribute("Iron", "44"),
                        new XAttribute("Upkeep", "1")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Litet stentorn, fyrkantigt"),
                        new XAttribute("Woodwork", "110"),
                        new XAttribute("Stonework", "1700"),
                        new XAttribute("Smithswork", "12"),
                        new XAttribute("Wood", "6"),
                        new XAttribute("Stone", "87"),
                        new XAttribute("Iron", "12"),
                        new XAttribute("Upkeep", "1")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Stentorn, fyrkantigt"),
                        new XAttribute("Woodwork", "370"),
                        new XAttribute("Stonework", "5400"),
                        new XAttribute("Smithswork", "52"),
                        new XAttribute("Wood", "26"),
                        new XAttribute("Stone", "270"),
                        new XAttribute("Iron", "52"),
                        new XAttribute("Upkeep", "2")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Stort stentorn, fyrkantigt"),
                        new XAttribute("Woodwork", "1600"),
                        new XAttribute("Stonework", "30000"),
                        new XAttribute("Smithswork", "80"),
                        new XAttribute("Wood", "40"),
                        new XAttribute("Stone", "1500"),
                        new XAttribute("Iron", "80"),
                        new XAttribute("Upkeep", "4")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Litet stentorn, runt"),
                        new XAttribute("Woodwork", "80"),
                        new XAttribute("Stonework", "1300"),
                        new XAttribute("Smithswork", "8"),
                        new XAttribute("Wood", "4"),
                        new XAttribute("Stone", "63"),
                        new XAttribute("Iron", "8"),
                        new XAttribute("Upkeep", "4")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Stentorn, runt"),
                        new XAttribute("Woodwork", "290"),
                        new XAttribute("Stonework", "4200"),
                        new XAttribute("Smithswork", "42"),
                        new XAttribute("Wood", "21"),
                        new XAttribute("Stone", "210"),
                        new XAttribute("Iron", "42"),
                        new XAttribute("Upkeep", "2")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Stort stentorn, runt"),
                        new XAttribute("Woodwork", "1250"),
                        new XAttribute("Stonework", "23500"),
                        new XAttribute("Smithswork", "60"),
                        new XAttribute("Wood", "31"),
                        new XAttribute("Stone", "1175"),
                        new XAttribute("Iron", "60"),
                        new XAttribute("Upkeep", "4")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Borgkärna, fyrkantig"),
                        new XAttribute("Woodwork", "2500"),
                        new XAttribute("Stonework", "44000"),
                        new XAttribute("Smithswork", "160"),
                        new XAttribute("Wood", "74"),
                        new XAttribute("Stone", "2200"),
                        new XAttribute("Iron", "160"),
                        new XAttribute("Upkeep", "8")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Stor borgkärna, fyrkantig"),
                        new XAttribute("Woodwork", "4000"),
                        new XAttribute("Stonework", "68000"),
                        new XAttribute("Smithswork", "280"),
                        new XAttribute("Wood", "140"),
                        new XAttribute("Stone", "3400"),
                        new XAttribute("Iron", "280"),
                        new XAttribute("Upkeep", "16")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Borgkärna, rund"),
                        new XAttribute("Woodwork", "1900"),
                        new XAttribute("Stonework", "34000"),
                        new XAttribute("Smithswork", "120"),
                        new XAttribute("Wood", "58"),
                        new XAttribute("Stone", "1700"),
                        new XAttribute("Iron", "120"),
                        new XAttribute("Upkeep", "8")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Sammansatt borgkärna"),
                        new XAttribute("Woodwork", "5700"),
                        new XAttribute("Stonework", "102000"),
                        new XAttribute("Smithswork", "340"),
                        new XAttribute("Wood", "170"),
                        new XAttribute("Stone", "5100"),
                        new XAttribute("Iron", "340"),
                        new XAttribute("Upkeep", "16")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Trähus"),
                        new XAttribute("Woodwork", "24"),
                        new XAttribute("Stonework", "0"),
                        new XAttribute("Smithswork", "0"),
                        new XAttribute("Wood", "6"),
                        new XAttribute("Stone", "0"),
                        new XAttribute("Iron", "0"),
                        new XAttribute("Upkeep", "0.025")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Stenhus"),
                        new XAttribute("Woodwork", "16"),
                        new XAttribute("Stonework", "440"),
                        new XAttribute("Smithswork", "8"),
                        new XAttribute("Wood", "4"),
                        new XAttribute("Stone", "22"),
                        new XAttribute("Iron", "8"),
                        new XAttribute("Upkeep", "0.025")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Tvåvånings trähus"),
                        new XAttribute("Woodwork", "48"),
                        new XAttribute("Stonework", "0"),
                        new XAttribute("Smithswork", "8"),
                        new XAttribute("Wood", "12"),
                        new XAttribute("Stone", "0"),
                        new XAttribute("Iron", "8"),
                        new XAttribute("Upkeep", "0.05")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Tvåvånings stenhus"),
                        new XAttribute("Woodwork", "60"),
                        new XAttribute("Stonework", "880"),
                        new XAttribute("Smithswork", "8"),
                        new XAttribute("Wood", "4"),
                        new XAttribute("Stone", "44"),
                        new XAttribute("Iron", "8"),
                        new XAttribute("Upkeep", "0.05")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Tempel/Kyrka"),
                        new XAttribute("Woodwork", "1700"),
                        new XAttribute("Stonework", "47000"),
                        new XAttribute("Smithswork", "130"),
                        new XAttribute("Wood", "66"),
                        new XAttribute("Stone", "1200"),
                        new XAttribute("Iron", "130"),
                        new XAttribute("Upkeep", "0")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Stort tempel/kyrka"),
                        new XAttribute("Woodwork", "5100"),
                        new XAttribute("Stonework", "140000"),
                        new XAttribute("Smithswork", "400"),
                        new XAttribute("Wood", "200"),
                        new XAttribute("Stone", "3500"),
                        new XAttribute("Iron", "400"),
                        new XAttribute("Upkeep", "0")
                    ),
                    new XElement("Building",
                        new XAttribute("Building", "Katedral"),
                        new XAttribute("Woodwork", "26000"),
                        new XAttribute("Stonework", "700000"),
                        new XAttribute("Smithswork", "2000"),
                        new XAttribute("Wood", "1000"),
                        new XAttribute("Stone", "18000"),
                        new XAttribute("Iron", "2000"),
                        new XAttribute("Upkeep", "0")
                    )
                )
            );

            string filePath = "../../../FiefApp.Common.Infrastructure/Settings/BuildingsSettings.xml";
            xmlDoc.Save(@filePath);
            BuildingsSettingsList = LoadBuildingsSettingsFromXml();

        }

        #endregion
    }
}