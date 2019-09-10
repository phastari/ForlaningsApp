using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Markup;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Services
{
    public class TradeService : ITradeService
    {
        private readonly IFiefService _fiefService;

        public TradeService(
            IFiefService fiefService
            )
        {
            _fiefService = fiefService;
        }

        public TradeDataModel GetAllTradeDataModel()
        {
            TradeDataModel model = new TradeDataModel();

            for (int x = 1; x < _fiefService.TradeList.Count; x++)
            {
                model.MarketAvailableBase += _fiefService.TradeList[x].MarketAvailableBase;
                model.MarketAvailableIron += _fiefService.TradeList[x].MarketAvailableIron;
                model.MarketAvailableLuxury += _fiefService.TradeList[x].MarketAvailableLuxury;
                model.MarketAvailableStone += _fiefService.TradeList[x].MarketAvailableStone;
                model.MarketAvailableWood += _fiefService.TradeList[x].MarketAvailableWood;

                model.MerchantsCollection.AddRange(_fiefService.TradeList[x].MerchantsCollection);
                model.ShipsCollection.AddRange(_fiefService.TradeList[x].ShipsCollection);
            }

            return model;
        }

        public int GetNewMerchantId()
        {
            List<int> tempList = new List<int>();

            for (int x = 1; x < _fiefService.TradeList.Count; x++)
            {
                tempList.Add(_fiefService.TradeList[x].MerchantsCollection.Max(t => t.Id));
            }

            if (tempList.Count > 0)
            {
                return tempList.Max() + 1;
            }
            return 0;
        }

        public MarketModel GetMarket(int index)
        {
            var model = new MarketModel();
            decimal bas;
            decimal lyx;
            decimal iron;
            decimal wood;
            decimal stone;

            bas = _fiefService.ManorList[index].VillagesCollection.Sum(o => o.Population);
            bas = bas * bas * bas;
            bas = (decimal)Math.Sqrt((double)bas);
            bas /= 100;
            bas += _fiefService.WeatherList[index].Dayworkers / 6.43M;
            bas += _fiefService.ArmyList[index].TotalSilver / 1440M;
            bas += _fiefService.EmployeesList[index].TotalLuxury / 2.8M;

            lyx = Math.Floor(bas * 0.239M);
            iron = Math.Floor(lyx * 2.375M);
            wood = Math.Floor(lyx * 73.375M);
            stone = Math.Floor(lyx * 11.875M);
            lyx /= 3;

            if (_fiefService.PortsList[index].GotShipyard)
            {
                if (int.TryParse(_fiefService.PortsList[index].Shipyard.Size, out int size))
                {
                    bas *= (size + 1.5M);
                    lyx *= (size + 1.25M);
                    iron *= (size + 1.25M);
                    wood *= (size + 3.25M);
                    stone *= (size + 2.25M);
                }
            }

            if (_fiefService.InformationList[index].Roads == "Stigar")
            {
                bas *= 0.6M;
                lyx *= 0.1M;
                iron *= 0.2M;
                wood *= 0.1M;
                stone *= 0.1M;
            }
            else if (_fiefService.InformationList[index].Roads == "Stenlagd väg")
            {
                bas *= 1.4M;
                lyx *= 2.1M;
                iron *= 2.4M;
                wood *= 4.7M;
                stone *= 2.5M;
            }
            else if (_fiefService.InformationList[index].Roads == "Kunglig landsväg")
            {
                bas *= 3;
                lyx *= 4.5M;
                iron *= 3.2M;
                wood *= 7.05M;
                stone *= 3.76M;
            }

            if (_fiefService.MinesList[index].MinesCollection.Count > 0)
            {
                bas += _fiefService.MinesList[index].MinesCollection.Sum(o => o.BaseIncome) / 10000M;
                lyx += _fiefService.MinesList[index].MinesCollection.Sum(o => o.BaseIncome) / 20000M;
            }

            if (_fiefService.MinesList[index].QuarriesCollection.Count > 0)
            {
                bas += _fiefService.MinesList[index].QuarriesCollection.Sum(o => o.Approximate) / 100M;
                lyx += _fiefService.MinesList[index].QuarriesCollection.Sum(o => o.Approximate) / 300M;
            }

            model.MarketBase = (int)Math.Floor(bas);
            model.MarketLuxury = (int)Math.Floor(lyx);
            model.MarketIron = (int)Math.Floor(iron);
            model.MarketStone = (int)Math.Floor(stone);
            model.MarketWood = (int)Math.Floor(wood);

            return model;
        }

        public List<BoatModel> GetBoatsFromPortLists()
        {
            List<BoatModel> tempList = new List<BoatModel>()
            {
                new BoatModel()
                {
                    Id = 0
                }
            };

            for (int x = 1; x < _fiefService.PortsList.Count; x++)
            {
                if (_fiefService.PortsList[x].BoatsCollection.Count > 0)
                {
                    tempList.AddRange(_fiefService.PortsList[x].BoatsCollection);
                }
            }

            return tempList;
        }

        public void RemoveShipInPortListBoatsCollection(int id)
        {
            bool found = false;
            for (int i = 1; i < _fiefService.PortsList.Count; i++)
            {
                for (int j = 0; j < _fiefService.PortsList[i].BoatsCollection.Count; j++)
                {
                    if (_fiefService.PortsList[i].BoatsCollection[j].Id == id)
                    {
                        _fiefService.PortsList[i].BoatsCollection.RemoveAt(j);
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }
        }
    }
}
