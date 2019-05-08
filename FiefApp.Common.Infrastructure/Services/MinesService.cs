using System;
using System.Collections.Generic;
using System.Linq;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public class MinesService : IMinesService
    {
        private readonly IFiefService _fiefService;

        public MinesService(
            IFiefService fiefService
            )
        {
            _fiefService = fiefService;
        }

        public MinesDataModel GetAllMinesDataModel()
        {
            return null;
        }

        public int GetNewIdForQuarry()
        {
            List<int> tempList = new List<int>();

            for (int x = 1; x < _fiefService.MinesList.Count; x++)
            {
                for (int y = 0; y < _fiefService.MinesList[x].QuarriesCollection.Count; y++)
                {
                    tempList.Add(_fiefService.MinesList[x].QuarriesCollection[y].Id);
                }
            }

            if (tempList.Count > 0)
            {
                return tempList.Max() + 1;
            }
            return 0;
        }

        public int GetNewIdForMine()
        {
            List<int> tempList = new List<int>();

            for (int x = 1; x < _fiefService.MinesList.Count; x++)
            {
                for (int y = 0; y < _fiefService.MinesList[x].MinesCollection.Count; y++)
                {
                    tempList.Add(_fiefService.MinesList[x].MinesCollection[y].Id);
                }
            }

            if (tempList.Count > 0)
            {
                return tempList.Max() + 1;
            }
            return 0;
        }

        public int GetMinesDifficulty(int index)
        {
            int difficulty = 8;

            decimal spring = 0.15M * _fiefService.WeatherList[index].SpringRollMod;
            decimal summer = 0.15M * _fiefService.WeatherList[index].SummerRollMod;
            decimal fall = 0.15M * _fiefService.WeatherList[index].FallRollMod;
            decimal winter = 0.15M * _fiefService.WeatherList[index].WinterRollMod;

            int result = Convert.ToInt32(Math.Ceiling(difficulty + spring + summer + fall + winter));

            if (result > 6)
            {
                return result;
            }
            return 6;
        }

        public List<StewardModel> GetStewardsCollection()
        {
            return new List<StewardModel>(_fiefService.StewardsList[1].StewardsCollection);
        }

        public void ChangeSteward(int stewardId, int mineId, string steward)
        {
            for (int x = 1; x < _fiefService.PortsList.Count; x++)
            {
                if (_fiefService.PortsList[x].Shipyard.StewardId == stewardId)
                {
                    _fiefService.PortsList[x].Shipyard.StewardId = -1;
                    _fiefService.PortsList[x].Shipyard.Steward = "";
                }
            }

            for (int x = 1; x < _fiefService.SubsidiaryList.Count; x++)
            {
                for (int y = 0; y < _fiefService.SubsidiaryList[x].SubsidiaryCollection.Count; y++)
                {
                    if (_fiefService.SubsidiaryList[x].SubsidiaryCollection[y].StewardId == stewardId)
                    {
                        _fiefService.SubsidiaryList[x].SubsidiaryCollection[y].StewardId = -1;
                        _fiefService.SubsidiaryList[x].SubsidiaryCollection[y].Steward = "";
                    }
                }
            }

            for (int x = 1; x < _fiefService.IncomeList.Count; x++)
            {
                for (int y = 1; y < _fiefService.IncomeList[x].IncomesCollection.Count; y++)
                {
                    if (_fiefService.IncomeList[x].IncomesCollection[y].StewardId == stewardId)
                    {
                        _fiefService.IncomeList[x].IncomesCollection[y].StewardId = -1;
                        _fiefService.IncomeList[x].IncomesCollection[y].Steward = "";
                    }
                }
            }

            for (int x = 1; x < _fiefService.MinesList.Count; x++)
            {
                for (int y = 0; y < _fiefService.MinesList[x].MinesCollection.Count; y++)
                {
                    if (_fiefService.MinesList[x].MinesCollection[y].StewardId == stewardId)
                    {
                        _fiefService.MinesList[x].MinesCollection[y].StewardId = -1;
                        _fiefService.MinesList[x].MinesCollection[y].Steward = "";
                    }
                    if (_fiefService.MinesList[x].MinesCollection[y].Id == mineId)
                    {
                        _fiefService.MinesList[x].MinesCollection[y].StewardId = stewardId;
                        _fiefService.MinesList[x].MinesCollection[y].Steward = steward;
                    }
                }
            }
        }

        public List<MineModel> GetMinesCollection(int index)
        {
            return new List<MineModel>(_fiefService.MinesList[index].MinesCollection);
        }
    }
}
