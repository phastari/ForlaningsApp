using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using Prism.Events;

namespace FiefApp.Common.Infrastructure.Services
{
    public class EndOfYearService : IEndOfYearService
    {
        private readonly IFiefService _fiefService;
        private readonly ISupplyService _supplyService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IBaseService _baseService;

        public EndOfYearService(
            IFiefService fiefService,
            ISupplyService supplyService,
            IEventAggregator eventAggregator,
            IBaseService baseService
            )
        {
            _fiefService = fiefService;
            _supplyService = supplyService;
            _eventAggregator = eventAggregator;
            _baseService = baseService;
        }

        public List<EndOfYearIncomeFiefModel> Initialize()
        {
            List<EndOfYearIncomeFiefModel> tempList = new List<EndOfYearIncomeFiefModel>();
            List<EndOfYearIncomeModel> incomeList = new List<EndOfYearIncomeModel>();
            List<EndOfYearSubsidiaryModel> subsidiaryList = new List<EndOfYearSubsidiaryModel>();
            List<SubsidiaryModel> constructingList = new List<SubsidiaryModel>();
            List<MineModel> minesList = new List<MineModel>();
            List<QuarryModel> quarriesList = new List<QuarryModel>();
            List<IndustryBeingDevelopedModel> developmentList = new List<IndustryBeingDevelopedModel>();
            EndOfYearFellingModel fellingModel = new EndOfYearFellingModel();
            EndOfYearTaxesModel taxesModel = new EndOfYearTaxesModel();
            EndOfYearPopulationModel populationModel = new EndOfYearPopulationModel();
            ShipyardModel shipyard = new ShipyardModel();
            Dictionary<int, bool> dictionary = new Dictionary<int, bool>();

            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {
                for (int y = 0; y < _fiefService.IncomeList[x].IncomesCollection.Count; y++)
                {
                    if (_fiefService.IncomeList[x].IncomesCollection[y].IsStewardNeeded
                        && _fiefService.IncomeList[x].IncomesCollection[y]?.StewardId > 0)
                    {
                        if (_fiefService.IncomeList[x].IncomesCollection[y].Income != "Skogsavverkning")
                        {
                            string steward = "";
                            string skill = "0";

                            steward = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.IncomeList[x].IncomesCollection[y].StewardId).PersonName;
                            skill = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.IncomeList[x].IncomesCollection[y].StewardId).Skill;

                            incomeList.Add(new EndOfYearIncomeModel()
                            {
                                Id = _fiefService.IncomeList[x].IncomesCollection[y].Id,
                                Income = _fiefService.IncomeList[x].IncomesCollection[y].Income,
                                Crewed = 1M,
                                StewardId = _fiefService.IncomeList[x].IncomesCollection[y].StewardId,
                                StewardName = steward,
                                Skill = skill,
                                Difficulty = _fiefService.IncomeList[x].IncomesCollection[y].Difficulty,
                                BaseIncome = _fiefService.IncomeList[x].IncomesCollection[y].Base
                            });
                            dictionary.Add(_fiefService.IncomeList[x].IncomesCollection[y].Id, false);
                        }
                    }
                }

                for (int j = 0; j < _fiefService.SubsidiaryList[x].SubsidiaryCollection.Count; j++)
                {
                    if (_fiefService.SubsidiaryList[x].SubsidiaryCollection[j].StewardId > 0)
                    {
                        string steward = "";
                        string skill = "0";

                        steward = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].StewardId).PersonName;
                        skill = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].StewardId).Skill;

                        subsidiaryList.Add(new EndOfYearSubsidiaryModel()
                        {
                            Id = _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].Id,
                            Subsidiary = _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].Subsidiary,
                            Steward = steward,
                            StewardId = _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].StewardId,
                            Skill = skill,
                            Crewed = (decimal)_fiefService.SubsidiaryList[x].SubsidiaryCollection[j].DaysWorkThisYear / _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].DaysWorkUpkeep,
                            Difficulty = _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].Difficulty,
                            BaseIncomeSilver = _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].Silver,
                            BaseIncomeBase = _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].Base,
                            BaseIncomeLuxury = _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].Luxury
                        });
                        dictionary.Add(_fiefService.SubsidiaryList[x].SubsidiaryCollection[j].Id, false);
                    }
                }

                for (int j = 0; j < _fiefService.SubsidiaryList[x].ConstructingCollection.Count; j++)
                {
                    if (_fiefService.SubsidiaryList[x].ConstructingCollection[j].StewardId > 0)
                    {
                        string steward = "";
                        string skill = "0";

                        steward = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].StewardId).PersonName;
                        skill = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].StewardId).Skill;

                        constructingList.Add(_fiefService.SubsidiaryList[x].ConstructingCollection[j]);
                        dictionary.Add(_fiefService.SubsidiaryList[x].ConstructingCollection[j].Id, false);
                    }
                }

                for (int k = 0; k < _fiefService.MinesList[x].MinesCollection.Count; k++)
                {
                    if (_fiefService.MinesList[x].MinesCollection[k].StewardId > 0)
                    {
                        string steward = "";
                        string skill = "0";

                        steward = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.MinesList[x].MinesCollection[k].StewardId).PersonName;
                        skill = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.MinesList[x].MinesCollection[k].StewardId).Skill;

                        minesList.Add(new MineModel()
                        {
                            Id = _fiefService.MinesList[x].MinesCollection[k].Id,
                            MineType = _fiefService.MinesList[x].MinesCollection[k].MineType,
                            Guards = _fiefService.MinesList[x].MinesCollection[k].Guards,
                            Steward = steward,
                            StewardId = _fiefService.MinesList[x].MinesCollection[k].StewardId,
                            Skill = skill,
                            Difficulty = _fiefService.MinesList[x].MinesCollection[k].Difficulty,
                            Income = _fiefService.MinesList[x].MinesCollection[k].Income
                        });
                        dictionary.Add(_fiefService.MinesList[x].MinesCollection[k].Id, false);
                    }
                }

                for (int l = 0; l < _fiefService.MinesList[x].QuarriesCollection.Count; l++)
                {
                    if (_fiefService.MinesList[x].QuarriesCollection[l].StewardId > 0)
                    {
                        string steward = "";
                        string skill = "0";

                        steward = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.MinesList[x].QuarriesCollection[l].StewardId).PersonName;
                        skill = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.MinesList[x].QuarriesCollection[l].StewardId).Skill;

                        quarriesList.Add(new QuarryModel()
                        {
                            Id = _fiefService.MinesList[x].QuarriesCollection[l].Id,
                            QuarryType = _fiefService.MinesList[x].QuarriesCollection[l].QuarryType,
                            Steward = steward,
                            StewardId = _fiefService.MinesList[x].QuarriesCollection[l].StewardId,
                            Skill = skill,
                            Difficulty = _fiefService.MinesList[x].QuarriesCollection[l].Difficulty,
                            Income = _fiefService.MinesList[x].QuarriesCollection[l].Income,
                            Modifier = _fiefService.MinesList[x].QuarriesCollection[l].Modifier,
                            Upkeep = _fiefService.MinesList[x].QuarriesCollection[l].Upkeep,
                            CombinationRoll = _fiefService.MinesList[x].QuarriesCollection[l].CombinationRoll,
                            DaysWorkThisYear = _fiefService.MinesList[x].QuarriesCollection[l].DaysWorkThisYear,
                            DaysWorkNeeded = _fiefService.MinesList[x].QuarriesCollection[l].DaysWorkNeeded,
                            IsFirstYear = _fiefService.MinesList[x].QuarriesCollection[l].IsFirstYear
                        });
                        dictionary.Add(_fiefService.MinesList[x].QuarriesCollection[l].Id, false);
                    }
                }

                fellingModel.Felling = _fiefService.WeatherList[x].Felling;
                fellingModel.LandClearing = _fiefService.WeatherList[x].LandClearing;


                for (int m = 0; m < _fiefService.IncomeList[x].IncomesCollection.Count; m++)
                {
                    if (_fiefService.IncomeList[x].IncomesCollection[m].Income == "Skogsavverkning")
                    {
                        string steward = "";
                        string skill = "0";

                        steward = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.IncomeList[x].IncomesCollection[m].StewardId)?.PersonName;
                        skill = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.IncomeList[x].IncomesCollection[m].StewardId)?.Skill;

                        fellingModel.Id = _fiefService.IncomeList[x].IncomesCollection[m].Id;
                        fellingModel.Steward = steward;
                        fellingModel.StewardId = _fiefService.IncomeList[x].IncomesCollection[m].StewardId;
                        fellingModel.Skill = skill;

                        dictionary.Add(_fiefService.IncomeList[x].IncomesCollection[m].Id, false);
                    }
                }

                int difficulty = Convert.ToInt32(Math.Ceiling(0.1 * _fiefService.WeatherList[x].SpringRollMod
                                                              + 0.1 * _fiefService.WeatherList[x].SummerRollMod
                                                              + 0.1 * _fiefService.WeatherList[x].FallRollMod
                                                              + 8));

                if (difficulty < 5)
                {
                    fellingModel.Difficulty = 4;
                }
                else
                {
                    fellingModel.Difficulty = difficulty;
                }

                taxesModel.Loyalty = _fiefService.InformationList[x].Loyalty;

                int happiness = _baseService.ConvertToNumeric(_fiefService.WeatherList[x].HappinessTotal);
                if (happiness / 2 < 5)
                {
                    taxesModel.Difficulty = 0;
                }
                else
                {
                    taxesModel.Difficulty = happiness / 2;
                }

                if (8 + happiness / 4 < 5)
                {
                    populationModel.Difficulty = 4;
                }
                else
                {
                    populationModel.Difficulty = 7 + happiness / 4;
                }

                populationModel.TotalPopulation = _fiefService.ManorList[x].ManorPopulation;
                populationModel.ModificationPopulation = Convert.ToInt32(_fiefService.MinesList[x].MinesCollection.Sum(t => t.Population))
                                                         + Convert.ToInt32(_fiefService.MinesList[x].QuarriesCollection.Sum(t => t.Population))
                                                         + _fiefService.MinesList[x].PopulationChange;

                populationModel.Amor = _fiefService.InformationList[x].Amor;

                for (int y = 0; y < _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection.Count; y++)
                {
                    if (_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[y].BeingDeveloped
                        && _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[y].StewardId > 0)
                    {
                        developmentList.Add(_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[y]);
                    }
                }


                if (_fiefService.PortsList[x].GotShipyard && _fiefService.PortsList[x].Shipyard.StewardId > 0)
                {
                    PortDataModel temp = (PortDataModel)_fiefService.PortsList[x].Clone();
                    shipyard = temp.Shipyard;

                    string steward = "";
                    string skill = "0";

                    steward = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.PortsList[x].Shipyard.StewardId)?.PersonName;
                    skill = _fiefService.StewardsDataModel.StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.PortsList[x].Shipyard.StewardId)?.Skill;

                    shipyard.Steward = steward;
                    shipyard.Skill = skill;
                    shipyard.Id = _fiefService.PortsList[x].Shipyard.Id;

                    dictionary.Add(_fiefService.PortsList[x].Shipyard.Id, false);

                    difficulty = Convert.ToInt32(Math.Ceiling(0.2 * _fiefService.WeatherList[x].SpringRollMod
                                                                  + 0.2 * _fiefService.WeatherList[x].SummerRollMod
                                                                  + 0.2 * _fiefService.WeatherList[x].FallRollMod
                                                                  + 0.2 * _fiefService.WeatherList[x].WinterRollMod
                                                                  + 8));

                    if (_fiefService.PortsList[x].Shipyard.Upgrading)
                    {
                        difficulty += 4;
                    }

                    if (_fiefService.PortsList[x].Shipyard.CrimeRate < _fiefService.PortsList[x].Shipyard.Guards)
                    {
                        difficulty -= 8;
                    }
                    else if (_fiefService.PortsList[x].Shipyard.CrimeRate < _fiefService.PortsList[x].Shipyard.Guards * 1.37M)
                    {
                        difficulty -= 4;
                    }
                    else if (_fiefService.PortsList[x].Shipyard.CrimeRate < _fiefService.PortsList[x].Shipyard.Guards * 2.74M)
                    {
                        difficulty += 0;
                    }
                    else if (_fiefService.PortsList[x].Shipyard.CrimeRate < _fiefService.PortsList[x].Shipyard.Guards * 5.48M)
                    {
                        difficulty += 4;
                    }
                    else if (_fiefService.PortsList[x].Shipyard.CrimeRate < _fiefService.PortsList[x].Shipyard.Guards * 11.96M)
                    {
                        difficulty += 8;
                    }
                    else
                    {
                        difficulty += 12;
                    }

                    if (difficulty < 5)
                    {
                        shipyard.Difficulty = 4;
                    }
                    else
                    {
                        shipyard.Difficulty = difficulty;
                    }
                }

                tempList.Add(new EndOfYearIncomeFiefModel()
                {
                    Id = x,
                    FiefName = _fiefService.InformationList[x].FiefName,
                    IncomeCollection = new ObservableCollection<EndOfYearIncomeModel>(incomeList),
                    SubsidiariesCollection = new ObservableCollection<EndOfYearSubsidiaryModel>(subsidiaryList),
                    ConstructingCollection = new ObservableCollection<SubsidiaryModel>(constructingList),
                    MinesCollection = new ObservableCollection<MineModel>(minesList),
                    QuarriesCollection = new ObservableCollection<QuarryModel>(quarriesList),
                    DevelopmentCollection = new ObservableCollection<IndustryBeingDevelopedModel>(developmentList),
                    FellingModel = fellingModel,
                    Shipyard = shipyard,
                    TaxesModel = taxesModel,
                    PopulationModel = populationModel,
                    EndOfYearOkDictionary = dictionary,
                    ShowShipyard = _fiefService.PortsList[x].GotShipyard
                });
            }

            return tempList;
        }
    }
}
