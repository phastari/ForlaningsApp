using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FiefApp.Common.Infrastructure.Services
{
    public class InformationService : IInformationService
    {
        private readonly IFiefService _fiefService;

        public InformationService(IFiefService fiefService)
        {
            _fiefService = fiefService;
        }

        public InformationDataModel GetInformationDataModel(int index)
        {
            return _fiefService.InformationList[index];
        }

        public void SetInformationDataModel(InformationDataModel dataModel, int index)
        {
            _fiefService.InformationList[index] = dataModel;
        }

        public InformationDataModel GetAllInformationDataModel()
        {
            InformationDataModel tempDataModel = _fiefService.InformationList[0];

            string mountain = "Nej";
            string mountainRange = "Nej";
            string river = "Nej";
            string coast = "Nej";
            string lake = "Nej";
            string plain = "Nej";
            string desert = "Nej";
            string swamp = "Nej";
            string jungle = "Nej";
            List<int> animalHusbandryQuality = new List<int>();
            List<int> fishingQuality = new List<int>();
            List<int> huntingQuality = new List<int>();
            List<int> agricultureQuality = new List<int>();
            List<int> oreQuality = new List<int>();
            List<int> animalHusbandryDevelopmentLevel = new List<int>();
            List<int> fishingDevelopmentLevel = new List<int>();
            List<int> huntingDevelopmentLevel = new List<int>();
            List<int> agricultureDevelopmentLevel = new List<int>();
            List<int> oreDevelopmentLevel = new List<int>();
            List<int> healthcareDevelopmentLevel = new List<int>();
            List<int> militaryDevelopmentLevel = new List<int>();
            List<int> shippingDevelopmentLevel = new List<int>();
            List<int> woodlandDevelopmentLevel = new List<int>();
            List<int> educationDevelopmentLevel = new List<int>();

            List<ReligionModel> religionModelList = new List<ReligionModel>()
            {
                new ReligionModel()
                {
                    Religion = "Daakkyrkan",
                    Followers = 0,
                    Resources = "0",
                    Loyalty = "0"
                },
                new ReligionModel()
                {
                    Religion = "Jordesoldatens vittnen",
                    Followers = 0,
                    Resources = "0",
                    Loyalty = "0"
                },
                new ReligionModel()
                {
                    Religion = "Vindtron",
                    Followers = 0,
                    Resources = "0",
                    Loyalty = "0"
                },
                new ReligionModel()
                {
                    Religion = "Hedendomen",
                    Followers = 0,
                    Resources = "0",
                    Loyalty = "0"
                },
                new ReligionModel()
                {
                    Religion = "Samoriska läran",
                    Followers = 0,
                    Resources = "0",
                    Loyalty = "0"
                },
                new ReligionModel()
                {
                    Religion = "Kristallorden",
                    Followers = 0,
                    Resources = "0",
                    Loyalty = "0"
                },
                new ReligionModel()
                {
                    Religion = "Xinukulten",
                    Followers = 0,
                    Resources = "0",
                    Loyalty = "0"
                },
                new ReligionModel()
                {
                    Religion = "Commersium lamia",
                    Followers = 0,
                    Resources = "0",
                    Loyalty = "0"
                },
                new ReligionModel()
                {
                    Religion = "Övriga",
                    Followers = 0,
                    Resources = "0",
                    Loyalty = "0"
                }
            };

            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {

                if (_fiefService.InformationList[x].Mountain == "Ja")
                {
                    mountain = "Ja";
                }

                if (_fiefService.InformationList[x].MountainRange == "Ja")
                {
                    mountainRange = "Ja";
                }

                if (_fiefService.InformationList[x].River == "Ja")
                {
                    river = "Ja";
                }

                if (_fiefService.InformationList[x].Coast == "Ja")
                {
                    coast = "Ja";
                }

                if (_fiefService.InformationList[x].Lake == "Ja")
                {
                    lake = "Ja";
                }

                if (_fiefService.InformationList[x].Plain == "Ja")
                {
                    plain = "Ja";
                }

                if (_fiefService.InformationList[x].Swamp == "Ja")
                {
                    swamp = "Ja";
                }

                if (_fiefService.InformationList[x].Jungle == "Ja")
                {
                    jungle = "Ja";
                }

                if (_fiefService.InformationList[x].Desert == "Ja")
                {
                    desert = "Ja";
                }

                animalHusbandryQuality.Add(Convert.ToInt16(_fiefService.InformationList[x].AnimalHusbandryQuality));
                fishingQuality.Add(Convert.ToInt16(_fiefService.InformationList[x].FishingQuality));
                huntingQuality.Add(Convert.ToInt16(_fiefService.InformationList[x].HuntingQuality));
                agricultureQuality.Add(Convert.ToInt16(_fiefService.InformationList[x].AgricultureQuality));
                oreQuality.Add(Convert.ToInt16(_fiefService.InformationList[x].OreQuality));

                animalHusbandryDevelopmentLevel.Add(Convert.ToInt16(_fiefService.InformationList[x].AnimalHusbandryDevelopmentLevel));
                fishingDevelopmentLevel.Add(Convert.ToInt16(_fiefService.InformationList[x].FishingDevelopmentLevel));
                huntingDevelopmentLevel.Add(Convert.ToInt16(_fiefService.InformationList[x].HuntingDevelopmentLevel));
                agricultureDevelopmentLevel.Add(Convert.ToInt16(_fiefService.InformationList[x].AgricultureDevelopmentLevel));
                oreDevelopmentLevel.Add(Convert.ToInt16(_fiefService.InformationList[x].OreDevelopmentLevel));
                healthcareDevelopmentLevel.Add(Convert.ToInt16(_fiefService.InformationList[x].HealthcareDevelopmentLevel));
                militaryDevelopmentLevel.Add(Convert.ToInt16(_fiefService.InformationList[x].MilitaryDevelopmentLevel));
                shippingDevelopmentLevel.Add(Convert.ToInt16(_fiefService.InformationList[x].ShippingDevelopmentLevel));
                woodlandDevelopmentLevel.Add(Convert.ToInt16(_fiefService.InformationList[x].WoodlandDevelopmentLevel));
                educationDevelopmentLevel.Add(Convert.ToInt16(_fiefService.InformationList[x].EducationDevelopmentLevel));

                for (int i = 1; i < _fiefService.InformationList[x].ReligionsList.Count; i++)
                {
                    religionModelList[i].Followers += _fiefService.InformationList[x].ReligionsList[i].Followers;
                }
            }

            tempDataModel.Mountain = mountain;
            tempDataModel.MountainRange = mountainRange;
            tempDataModel.River = river;
            tempDataModel.Coast = coast;
            tempDataModel.Lake = lake;
            tempDataModel.Plain = plain;
            tempDataModel.Desert = desert;
            tempDataModel.Jungle = jungle;
            tempDataModel.Swamp = swamp;

            tempDataModel.AnimalHusbandryQuality = $"{animalHusbandryQuality.Min()} - {animalHusbandryQuality.Max()}";
            tempDataModel.FishingQuality = $"{fishingQuality.Min()} - {fishingQuality.Max()}";
            tempDataModel.HuntingQuality = $"{huntingQuality.Min()} - {huntingQuality.Max()}";
            tempDataModel.AgricultureQuality = $"{agricultureQuality.Min()} - {agricultureQuality.Max()}";
            tempDataModel.OreQuality = $"{oreQuality.Min()} - {oreQuality.Max()}";

            tempDataModel.AnimalHusbandryDevelopmentLevel = $"{animalHusbandryDevelopmentLevel.Min()} - {animalHusbandryDevelopmentLevel.Max()}";
            tempDataModel.FishingDevelopmentLevel = $"{fishingDevelopmentLevel.Min()} - {fishingDevelopmentLevel.Max()}";
            tempDataModel.HuntingDevelopmentLevel = $"{huntingDevelopmentLevel.Min()} - {huntingDevelopmentLevel.Max()}";
            tempDataModel.AgricultureDevelopmentLevel = $"{agricultureDevelopmentLevel.Min()} - {agricultureDevelopmentLevel.Max()}";
            tempDataModel.OreDevelopmentLevel = $"{oreDevelopmentLevel.Min()} - {oreDevelopmentLevel.Max()}";
            tempDataModel.HealthcareDevelopmentLevel = $"{healthcareDevelopmentLevel.Min()} - {healthcareDevelopmentLevel.Max()}";
            tempDataModel.MilitaryDevelopmentLevel = $"{militaryDevelopmentLevel.Min()} - {militaryDevelopmentLevel.Max()}";
            tempDataModel.ShippingDevelopmentLevel = $"{shippingDevelopmentLevel.Min()} - {shippingDevelopmentLevel.Max()}";
            tempDataModel.WoodlandDevelopmentLevel = $"{woodlandDevelopmentLevel.Min()} - {woodlandDevelopmentLevel.Max()}";
            tempDataModel.EducationDevelopmentLevel = $"{educationDevelopmentLevel.Min()} - {educationDevelopmentLevel.Max()}";

            tempDataModel.ReligionsList = religionModelList;
            tempDataModel.SortReligionsListIntoReligionsShowCollection();

            string s = "";
            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {
                s += $"{_fiefService.InformationList[x].FiefName} : ";
                s += $"{_fiefService.InformationList[x].FiefType}, ";
                s += $"{_fiefService.InformationList[x].Roads}{Environment.NewLine}";
            }

            tempDataModel.SelectedAllInformationText = s;

            return tempDataModel;
        }

        public void SetupPopulationReligion(int index)
        {
            int population;
            int gotReligion;
            bool loop = true;

            while (loop)
            {
                population = 0;
                gotReligion = 0;

                for (int x = 0; x < _fiefService.ManorList[index].VillagesCollection.Count; x++)
                {
                    population += _fiefService.ManorList[index].VillagesCollection[x].Population;
                }

                for (int y = 0; y < _fiefService.InformationList[index].ReligionsList.Count; y++)
                {
                    if (_fiefService.InformationList[index].ReligionsList[y].Religion != "" || _fiefService.InformationList[index].ReligionsList[y].Religion != null)
                    {
                        gotReligion += _fiefService.InformationList[index].ReligionsList[y].Followers;
                    }
                }

                if (population >= gotReligion)
                {
                    for (int i = 0; i < population - gotReligion; i++)
                    {
                        int z = _fiefService.GetRandom(1, 100);

                        if (z < 10)
                        {
                            for (int b = 0; b < _fiefService.InformationList[index].ReligionsList.Count; b++)
                            {
                                if (_fiefService.InformationList[index].ReligionsList[b].Religion.Equals("Jordesoldatens vittnen"))
                                {
                                    _fiefService.InformationList[index].ReligionsList[b].Followers++;
                                    break;
                                }
                            }
                        }
                        else if (z > 9 && z < 25)
                        {
                            for (int b = 0; b < _fiefService.InformationList[index].ReligionsList.Count; b++)
                            {
                                if (_fiefService.InformationList[index].ReligionsList[b].Religion.Equals("Daakkyrkan"))
                                {
                                    _fiefService.InformationList[index].ReligionsList[b].Followers++;
                                    break;
                                }
                            }
                        }
                        else if (z > 24 && z < 47)
                        {
                            for (int b = 0; b < _fiefService.InformationList[index].ReligionsList.Count; b++)
                            {
                                if (_fiefService.InformationList[index].ReligionsList[b].Religion.Equals("Vindtron"))
                                {
                                    _fiefService.InformationList[index].ReligionsList[b].Followers++;
                                    break;
                                }
                            }
                        }
                        else if (z > 46 && z < 77)
                        {
                            for (int b = 0; b < _fiefService.InformationList[index].ReligionsList.Count; b++)
                            {
                                if (_fiefService.InformationList[index].ReligionsList[b].Religion.Equals("Hedendomen"))
                                {
                                    _fiefService.InformationList[index].ReligionsList[b].Followers++;
                                    break;
                                }
                            }
                        }
                        else if (z > 76 && z < 84)
                        {
                            for (int b = 0; b < _fiefService.InformationList[index].ReligionsList.Count; b++)
                            {
                                if (_fiefService.InformationList[index].ReligionsList[b].Religion.Equals("Samoriska läran"))
                                {
                                    _fiefService.InformationList[index].ReligionsList[b].Followers++;
                                    break;
                                }
                            }
                        }
                        else if (z > 83 && z < 93)
                        {
                            for (int b = 0; b < _fiefService.InformationList[index].ReligionsList.Count; b++)
                            {
                                if (_fiefService.InformationList[index].ReligionsList[b].Religion.Equals("Kristallorden"))
                                {
                                    _fiefService.InformationList[index].ReligionsList[b].Followers++;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            int k = _fiefService.GetRandom(1, 7);

                            if (k < 4)
                            {
                                for (int b = 0; b < _fiefService.InformationList[index].ReligionsList.Count; b++)
                                {
                                    if (_fiefService.InformationList[index].ReligionsList[b].Religion.Equals("Xinukulten"))
                                    {
                                        _fiefService.InformationList[index].ReligionsList[b].Followers++;
                                        break;
                                    }
                                }
                            }
                            else if (k > 3 && k < 7)
                            {
                                for (int b = 0; b < _fiefService.InformationList[index].ReligionsList.Count; b++)
                                {
                                    if (_fiefService.InformationList[index].ReligionsList[b].Religion.Equals("Commersium lamia"))
                                    {
                                        _fiefService.InformationList[index].ReligionsList[b].Followers++;
                                        break;
                                    }
                                }
                            }
                            else if (k > 6 && k < 8)
                            {
                                for (int b = 0; b < _fiefService.InformationList[index].ReligionsList.Count; b++)
                                {
                                    if (_fiefService.InformationList[index].ReligionsList[b].Religion.Equals("Övriga"))
                                    {
                                        _fiefService.InformationList[index].ReligionsList[b].Followers++;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    for (int x = 0; x < _fiefService.InformationList[index].ReligionsList.Count; x++)
                    {
                        if (_fiefService.InformationList[index].ReligionsList[x].Followers == 0)
                        {
                            _fiefService.InformationList[index].ReligionsList[x].PercentOfPopulation = 0;
                        }
                        else
                        {
                            _fiefService.InformationList[index].ReligionsList[x].PercentOfPopulation = Convert.ToInt32(Math.Round(100 / (decimal)population * _fiefService.InformationList[index].ReligionsList[x].Followers));
                        }
                    }

                    loop = false;
                }
                else
                {
                    for (int x = 0; x < _fiefService.InformationList[index].ReligionsList.Count; x++)
                    {
                        _fiefService.InformationList[index].ReligionsList[x].Followers = 0;
                    }
                }
            }
        }
    }
}
