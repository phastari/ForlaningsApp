using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            List<MineModel> minesList = new List<MineModel>();
            List<QuarryModel> quarriesList = new List<QuarryModel>();
            EndOfYearFellingModel fellingModel = new EndOfYearFellingModel();
            EndOfYearTaxesModel taxesModel = new EndOfYearTaxesModel();
            EndOfYearPopulationModel populationModel = new EndOfYearPopulationModel();

            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {
                for (int y = 0; y < _fiefService.IncomeList[x].IncomesCollection.Count; y++)
                {
                    if (_fiefService.IncomeList[x].IncomesCollection[y].IsStewardNeeded)
                    {
                        if (_fiefService.IncomeList[x].IncomesCollection[y].Income != "Skogsavverkning")
                        {
                            string steward = "";
                            string skill = "0";

                            if (_fiefService.StewardsList[1].StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.IncomeList[x].IncomesCollection[y].StewardId) != null)
                            {
                                steward = _fiefService.StewardsList[1].StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.IncomeList[x].IncomesCollection[y].StewardId)?.PersonName;
                                skill = _fiefService.StewardsList[1].StewardsCollection.FirstOrDefault(o => o.Id == _fiefService.IncomeList[x].IncomesCollection[y].StewardId)?.Skill;
                            }

                            incomeList.Add(new EndOfYearIncomeModel()
                            {
                                Income = _fiefService.IncomeList[x].IncomesCollection[y].Income,
                                Crewed = 1M,
                                StewardId = _fiefService.IncomeList[x].IncomesCollection[y].StewardId,
                                StewardName = steward,
                                Skill = skill,
                                Difficulty = _fiefService.IncomeList[x].IncomesCollection[y].Difficulty,
                                BaseIncome = _fiefService.IncomeList[x].IncomesCollection[y].Base
                            });
                        }
                    }
                }

                for (int j = 0; j < _fiefService.SubsidiaryList[x].SubsidiaryCollection.Count; j++)
                {
                    subsidiaryList.Add(new EndOfYearSubsidiaryModel()
                    {
                        Subsidiary = _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].Subsidiary,
                        Steward = _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].Steward,
                        StewardId = _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].StewardId,
                        Skill = _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].Skill,
                        Crewed = (decimal)_fiefService.SubsidiaryList[x].SubsidiaryCollection[j].DaysWorkThisYear / _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].DaysWorkUpkeep,
                        Difficulty = _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].Difficulty,
                        BaseIncomeSilver = _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].Silver,
                        BaseIncomeBase = _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].Base,
                        BaseIncomeLuxury = _fiefService.SubsidiaryList[x].SubsidiaryCollection[j].Luxury
                    });
                }

                for (int k = 0; k < _fiefService.MinesList[x].MinesCollection.Count; k++)
                {
                    minesList.Add(new MineModel()
                    {
                        MineType = _fiefService.MinesList[x].MinesCollection[k].MineType,
                        Guards = _fiefService.MinesList[x].MinesCollection[k].Guards,
                        Steward = _fiefService.MinesList[x].MinesCollection[k].Steward,
                        StewardId = _fiefService.MinesList[x].MinesCollection[k].StewardId,
                        Skill = _fiefService.MinesList[x].MinesCollection[k].Skill,
                        Difficulty = _fiefService.MinesList[x].MinesCollection[k].Difficulty,
                        Income = _fiefService.MinesList[x].MinesCollection[k].Income
                    });
                }

                for (int l = 0; l < _fiefService.MinesList[x].QuarriesCollection.Count; l++)
                {
                    quarriesList.Add(new QuarryModel()
                    {
                        QuarryType = _fiefService.MinesList[x].QuarriesCollection[l].QuarryType,
                        Steward = _fiefService.MinesList[x].QuarriesCollection[l].Steward,
                        StewardId = _fiefService.MinesList[x].QuarriesCollection[l].StewardId,
                        Skill = _fiefService.MinesList[x].QuarriesCollection[l].Skill,
                        Difficulty = _fiefService.MinesList[x].QuarriesCollection[l].Difficulty,
                        Income = _fiefService.MinesList[x].QuarriesCollection[l].Income,
                        Modifier = _fiefService.MinesList[x].QuarriesCollection[l].Modifier,
                        Upkeep = _fiefService.MinesList[x].QuarriesCollection[l].Upkeep,
                        CombinationRoll = _fiefService.MinesList[x].QuarriesCollection[l].CombinationRoll,
                        DaysWorkThisYear = _fiefService.MinesList[x].QuarriesCollection[l].DaysWorkThisYear,
                        DaysWorkNeeded = _fiefService.MinesList[x].QuarriesCollection[l].DaysWorkNeeded,
                        IsFirstYear = _fiefService.MinesList[x].QuarriesCollection[l].IsFirstYear
                    });
                }

                fellingModel.Felling = _fiefService.WeatherList[x].Felling;
                fellingModel.LandClearing = _fiefService.WeatherList[x].LandClearing;

                for (int m = 0; m < _fiefService.IncomeList[x].IncomesCollection.Count; m++)
                {
                    if (_fiefService.IncomeList[x].IncomesCollection[m].ManorId == x
                        && _fiefService.IncomeList[x].IncomesCollection[m].Income == "Skogsavverkning")
                    {
                        fellingModel.Steward = _fiefService.IncomeList[x].IncomesCollection[m].Steward;
                        fellingModel.StewardId = _fiefService.IncomeList[x].IncomesCollection[m].StewardId;
                        fellingModel.Skill = _fiefService.IncomeList[x].IncomesCollection[m].Skill;
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


                tempList.Add(new EndOfYearIncomeFiefModel()
                {
                    FiefName = _fiefService.InformationList[x].FiefName,
                    IncomeCollection = new ObservableCollection<EndOfYearIncomeModel>(incomeList),
                    SubsidiariesCollection = new ObservableCollection<EndOfYearSubsidiaryModel>(subsidiaryList),
                    MinesCollection = new ObservableCollection<MineModel>(minesList),
                    QuarriesCollection = new ObservableCollection<QuarryModel>(quarriesList),
                    FellingModel = fellingModel,
                    TaxesModel = taxesModel,
                    PopulationModel = populationModel
                });
            }

            return tempList;
        }
    }
}
