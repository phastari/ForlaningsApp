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
                        BaseIncomeSilver = _fiefService.MinesList[x].MinesCollection[k].BaseIncomeSilver
                    });
                }

                tempList.Add(new EndOfYearIncomeFiefModel()
                {
                    FiefName = _fiefService.InformationList[x].FiefName,
                    IncomeCollection = new ObservableCollection<EndOfYearIncomeModel>(incomeList),
                    SubsidiariesCollection = new ObservableCollection<EndOfYearSubsidiaryModel>(subsidiaryList),
                    MinesCollection = new ObservableCollection<MineModel>(minesList)
                });
            }

            return tempList;
        }
    }
}
