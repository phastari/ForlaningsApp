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

        public int GetMinesDifficulty(int index)
        {
            int difficulty = 8;

            decimal spring = 0.15M * _fiefService.WeatherList[index].SpringRollMod;
            decimal summer = 0.15M * _fiefService.WeatherList[index].SummerRollMod;
            decimal fall = 0.15M * _fiefService.WeatherList[index].FallRollMod;
            decimal winter = 0.15M * _fiefService.WeatherList[index].WinterRollMod;

            int result = Convert.ToInt32(Math.Ceiling(difficulty + spring + summer + fall + winter));

            if (result > 4)
            {
                return result;
            }
            return 4;
        }

        public int GetQuarriesDifficulty(int index)
        {
            int difficulty = 8;

            decimal spring = 0.25M * _fiefService.WeatherList[index].SpringRollMod;
            decimal summer = 0.25M * _fiefService.WeatherList[index].SummerRollMod;
            decimal fall = 0.15M * _fiefService.WeatherList[index].FallRollMod;

            int result = Convert.ToInt32(Math.Ceiling(difficulty + spring + summer + fall));

            if (result > 6)
            {
                return result;
            }
            return 6;
        }

        public List<StewardModel> GetStewardsCollection()
        {
            return new List<StewardModel>(_fiefService.StewardsCollection);
        }

        public List<MineModel> GetMinesCollection(int index)
        {
            return new List<MineModel>(_fiefService.MinesList[index].MinesCollection);
        }

        public int GetAvailableGuards(int index)
        {
            return _fiefService.ArmyList[index].AvailableGuards;
        }

        public bool SetUsedGuards(int index, int amount)
        {
            if (amount < 0)
            {
                if (0 <= _fiefService.ArmyList[index].UsedGuards + amount)
                {
                    _fiefService.ArmyList[index].UsedGuards += amount;
                    return true;
                }
                return false;
            }
            else if (amount > 0)
            {
                if (_fiefService.ArmyList[index].ArmyGuards >= _fiefService.ArmyList[index].UsedGuards + amount)
                {
                    _fiefService.ArmyList[index].UsedGuards += amount;
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
