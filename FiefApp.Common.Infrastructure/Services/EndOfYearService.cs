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

        public List<EndOfYearIncomeFiefModel> InitializeIncomes()
        {
            List<EndOfYearIncomeFiefModel> tempList = new List<EndOfYearIncomeFiefModel>();
            List<EndOfYearIncomeModel> incomeList = new List<EndOfYearIncomeModel>();

            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {
                for (int y = 0; y < _fiefService.IncomeList[x].IncomesCollection.Count; y++)
                {
                    if (_fiefService.IncomeList[x].IncomesCollection[y].IsStewardNeeded)
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

                tempList.Add(new EndOfYearIncomeFiefModel()
                {
                    FiefName = _fiefService.InformationList[x].FiefName,
                    IncomeCollection = new ObservableCollection<EndOfYearIncomeModel>(incomeList)
                });
            }

            return tempList;
        }

        public List<EndOfYearSubsidiaryModel> InitializeSubsidiaries(int index, string fief)
        {
            List<EndOfYearSubsidiaryModel> tempList = new List<EndOfYearSubsidiaryModel>();

            for (int x = 0; x < _fiefService.SubsidiaryList[index].SubsidiaryCollection.Count; x++)
            {
                tempList.Add(new EndOfYearSubsidiaryModel()
                {
                    Subsidiary = _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].Subsidiary,
                    Steward = _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].Steward,
                    StewardId = _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].StewardId,
                    Skill = _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].Skill,
                    Crewed = (decimal)_fiefService.SubsidiaryList[index].SubsidiaryCollection[x].DaysWorkThisYear / _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].DaysWorkUpkeep,
                    Difficulty = _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].Difficulty,
                    BaseIncomeSilver = _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].Silver,
                    BaseIncomeBase = _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].Base,
                    BaseIncomeLuxury = _fiefService.SubsidiaryList[index].SubsidiaryCollection[x].Luxury
                });
            }

            return tempList;
        }
    }
}
