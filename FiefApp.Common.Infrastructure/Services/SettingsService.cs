using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;
using System;
using System.IO;
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
        }

        public ArmySettingsModel ArmySettingsModel { get; private set; }
        public EmployeeSettingsModel EmployeeSettingsModel { get; private set; }
        public InformationSettingsModel InformationSettingsModel { get; private set; }
        public ManorSettingsModel ManorSettingsModel { get; private set; }

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
                        switch (xmlAttributeCollection["Name"].Value)
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
                            switch (xmlAttributeCollection["Name"].Value)
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
                            switch (xmlAttributeCollection["Name"].Value)
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
                        new XAttribute("Name", "ArmyCrossbowmen"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyBowmen"),
                        new XAttribute("Silver", 160),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyMedics"),
                        new XAttribute("Silver", 2920),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyMedicsSkilled"),
                        new XAttribute("Silver", 4480),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyInfantry"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyInfantryMedium"),
                        new XAttribute("Silver", 480),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyInfantryHeavy"),
                        new XAttribute("Silver", 800),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyInfantryElite"),
                        new XAttribute("Silver", 1360),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyLongbowmen"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyMercenary"),
                        new XAttribute("Silver", 560),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyMercenaryElite"),
                        new XAttribute("Silver", 1840),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyMercenaryBowmen"),
                        new XAttribute("Silver", 480),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyEngineers"),
                        new XAttribute("Silver", 1360),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmySpearmen"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyScouts"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyScoutsSkilled"),
                        new XAttribute("Silver", 1620),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyKnightTemplars"),
                        new XAttribute("Silver", 2360),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", true)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyGuards"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Army",
                        new XAttribute("Name", "ArmyWeaponmasters"),
                        new XAttribute("Silver", 2400),
                        new XAttribute("Base", 2),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Cavalry",
                        new XAttribute("Name", "CavalryBowmen"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 6),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Cavalry",
                        new XAttribute("Name", "CavalryCourier"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 6),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Cavalry",
                        new XAttribute("Name", "CavalryLight"),
                        new XAttribute("Silver", 320),
                        new XAttribute("Base", 6),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Cavalry",
                        new XAttribute("Name", "CavalryKnights"),
                        new XAttribute("Silver", 3260),
                        new XAttribute("Base", 4),
                        new XAttribute("Accommodation", true)
                    ),
                    new XElement("Cavalry",
                        new XAttribute("Name", "CavalryScouts"),
                        new XAttribute("Silver", 540),
                        new XAttribute("Base", 6),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Cavalry",
                        new XAttribute("Name", "CavalryKnightTemplars"),
                        new XAttribute("Silver", 2360),
                        new XAttribute("Base", 4),
                        new XAttribute("Accommodation", true)
                    ),
                    new XElement("Cavalry",
                        new XAttribute("Name", "CavalryHeavy"),
                        new XAttribute("Silver", 1660),
                        new XAttribute("Base", 6),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Cavalry",
                        new XAttribute("Name", "CavalryElite"),
                        new XAttribute("Silver", 2040),
                        new XAttribute("Base", 6),
                        new XAttribute("Accommodation", false)
                    ),
                    new XElement("Officers",
                        new XAttribute("Name", "OfficersCorporal"),
                        new XAttribute("Silver", 2340),
                        new XAttribute("Base", 0),
                        new XAttribute("Accommodation", true)
                    ),
                    new XElement("Officers",
                        new XAttribute("Name", "OfficersSergeant"),
                        new XAttribute("Silver", 3120),
                        new XAttribute("Base", 0),
                        new XAttribute("Accommodation", true)
                    ),
                    new XElement("Officers",
                        new XAttribute("Name", "OfficersCaptain"),
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
                        switch (xmlAttributeCollection["Name"].Value)
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
                                tempModel.ProspectorLuxury = xmlAttributeCollection["Luxury"].Value;
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
                        new XAttribute("Name", "Falconer"),
                        new XAttribute("Base", 2),
                        new XAttribute("Luxury", 1)
                    ),
                    new XElement("Employee",
                        new XAttribute("Name", "Bailiff"),
                        new XAttribute("Base", 3),
                        new XAttribute("Luxury", 1)
                    ),
                    new XElement("Employee",
                        new XAttribute("Name", "Herald"),
                        new XAttribute("Base", 4),
                        new XAttribute("Luxury", 1)
                    ),
                    new XElement("Employee",
                        new XAttribute("Name", "Hunter"),
                        new XAttribute("Base", 2),
                        new XAttribute("Luxury", 0)
                    ),
                    new XElement("Employee",
                        new XAttribute("Name", "Physician"),
                        new XAttribute("Base", 3),
                        new XAttribute("Luxury", 3)
                    ),
                    new XElement("Employee",
                        new XAttribute("Name", "Scholar"),
                        new XAttribute("Base", 3),
                        new XAttribute("Luxury", 1)
                    ),
                    new XElement("Employee",
                        new XAttribute("Name", "Cupbearer"),
                        new XAttribute("Base", 2),
                        new XAttribute("Luxury", 0)
                    ),
                    new XElement("Employee",
                        new XAttribute("Name", "Prospector"),
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
                        tempModel.RoadTypesList.Add(xmlAttributeCollection["Road"].Value);
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
                        new XAttribute("InformationText", "Vasallen får länet mot att han utför vapentjänst och svär trohet till länsherren. När vasallen dör så ärver hans arvinge länet. Har vasallen inte har någon arvinge så går länet tillbaka till länsherren.")
                    ),
                    new XElement("Information",
                        new XAttribute("TypText", "Icke ärftligt län"),
                        new XAttribute("InformationText", "Vasallen får länet mot att han utför vapentjänst och svär trohet till länsherren. När vasallen dör så går länet tillbaka till länsherren.")
                    ),
                    new XElement("Information",
                        new XAttribute("TypText", "Pantlän"),
                        new XAttribute("InformationText", "Ibland händer det att en länsherre måste låna pengar, till exempel i tider av ofred. Länsherren kan då skänka långivaren ett län att disponera fritt tills dess att lånet är återbetalat.")
                    ),
                    new XElement("Information",
                        new XAttribute("TypText", "Län på avgift"),
                        new XAttribute("InformationText", "Vasallen betalar en fast avgift till sin länsherre mot att han fritt får disponera länets inkomster.")
                    ),
                    new XElement("RoadNetwork",
                        new XAttribute("Road", "Stigar")
                    ),
                    new XElement("RoadNetwork",
                        new XAttribute("Road", "Väg")
                    ),
                    new XElement("RoadNetwork",
                        new XAttribute("Road", "Stenlagdväg")
                    ),
                    new XElement("RoadNetwork",
                        new XAttribute("Road", "Kungliglandsväg")
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

    }
}
