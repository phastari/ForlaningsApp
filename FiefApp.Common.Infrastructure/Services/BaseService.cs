using FiefApp.Common.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure.HelpClasses.NameGenerator;
using FiefApp.Common.Infrastructure.Models;
using System.Linq;

namespace FiefApp.Common.Infrastructure.Services
{
    public class BaseService : IBaseService
    {
        private readonly IFiefService _fiefService;
        private readonly ISettingsService _settingsService;
        private readonly NameGenerator _nameGenerator;

        public BaseService(IFiefService fiefService, ISettingsService settingsService)
        {
            _fiefService = fiefService;
            _settingsService = settingsService;

            _nameGenerator = new NameGenerator(this);
        }

        public int GetIndex()
        {
            return _fiefService.Index;
        }

        public void SetIndex(int index)
        {
            _fiefService.Index = index;
        }

        public T GetDataModel<T>(int index)
        {
            if (_fiefService.InformationList.Count != 0)
            {
                if (typeof(T) == typeof(InformationDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.InformationList[index].Clone(), typeof(InformationDataModel));
                }
                else if (typeof(T) == typeof(ArmyDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.ArmyList[index].Clone(), typeof(ArmyDataModel));
                }
                else if (typeof(T) == typeof(EmployeesDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.EmployeesList[index].Clone(), typeof(EmployeesDataModel));
                }
                else if (typeof(T) == typeof(ManorDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.ManorList[index].Clone(), typeof(ManorDataModel));
                }
                else if (typeof(T) == typeof(BoatbuildingDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.BoatbuildingList[index].Clone(), typeof(BoatbuildingDataModel));
                }
                else if (typeof(T) == typeof(ExpensesDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.ExpensesList[index].Clone(), typeof(ExpensesDataModel));
                }
                else if (typeof(T) == typeof(SubsidiaryDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.SubsidiaryList[index].Clone(), typeof(SubsidiaryDataModel));
                }
                else if (typeof(T) == typeof(IncomeDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.IncomeList[index].Clone(), typeof(IncomeDataModel));
                }
                else if (typeof(T) == typeof(StewardsDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.StewardsDataModel.Clone(), typeof(StewardsDataModel));
                }
                else if (typeof(T) == typeof(BuildingsDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.BuildingsList[index].Clone(), typeof(BuildingsDataModel));
                }
                else if (typeof(T) == typeof(WeatherDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.WeatherList[index].Clone(), typeof(WeatherDataModel));
                }
                else if (typeof(T) == typeof(PortDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.PortsList[index].Clone(), typeof(PortDataModel));
                }
                else if (typeof(T) == typeof(MinesDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.MinesList[index].Clone(), typeof(MinesDataModel));
                }
                else if (typeof(T) == typeof(TradeDataModel))
                {
                    return (T)Convert.ChangeType(_fiefService.TradeList[index].Clone(), typeof(TradeDataModel));
                }
                else
                {
                    return default(T);
                }
            }
            else
            {
                return default(T);
            }
        }

        public void SetDataModel(IDataModelBase dataModel, int index)
        {
            if (dataModel != null)
            {
                if (dataModel.GetType() == typeof(InformationDataModel))
                {
                    InformationDataModel tempDataModel = (InformationDataModel)dataModel;
                    _fiefService.InformationList[index] = (InformationDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(ArmyDataModel))
                {
                    ArmyDataModel tempDataModel = (ArmyDataModel)dataModel;
                    _fiefService.ArmyList[index] = (ArmyDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(EmployeesDataModel))
                {
                    EmployeesDataModel tempDataModel = (EmployeesDataModel)dataModel;
                    _fiefService.EmployeesList[index] = (EmployeesDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(ManorDataModel))
                {
                    ManorDataModel tempDataModel = (ManorDataModel)dataModel;
                    tempDataModel.ResidentsCollection.Clear();
                    _fiefService.ManorList[index] = (ManorDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(BoatbuildingDataModel))
                {
                    BoatbuildingDataModel tempDataModel = (BoatbuildingDataModel)dataModel;
                    _fiefService.BoatbuildingList[index] = (BoatbuildingDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(ExpensesDataModel))
                {
                    ExpensesDataModel tempDataModel = (ExpensesDataModel)dataModel;
                    _fiefService.ExpensesList[index] = (ExpensesDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(SubsidiaryDataModel))
                {
                    SubsidiaryDataModel tempDataModel = (SubsidiaryDataModel)dataModel;
                    _fiefService.SubsidiaryList[index] = (SubsidiaryDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(StewardsDataModel))
                {
                    StewardsDataModel tempDataModel = (StewardsDataModel)dataModel;
                    _fiefService.StewardsDataModel = (StewardsDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(IncomeDataModel))
                {
                    IncomeDataModel tempDataModel = (IncomeDataModel)dataModel;
                    _fiefService.IncomeList[index] = (IncomeDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(BuildingsDataModel))
                {
                    BuildingsDataModel tempDataModel = (BuildingsDataModel)dataModel;
                    _fiefService.BuildingsList[index] = (BuildingsDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(WeatherDataModel))
                {
                    WeatherDataModel tempDataModel = (WeatherDataModel)dataModel;
                    _fiefService.WeatherList[index] = (WeatherDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(MinesDataModel))
                {
                    MinesDataModel tempDataModel = (MinesDataModel)dataModel;
                    _fiefService.MinesList[index] = (MinesDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(PortDataModel))
                {
                    PortDataModel tempDataModel = (PortDataModel)dataModel;
                    _fiefService.PortsList[index] = (PortDataModel)tempDataModel.Clone();
                }
                else if (dataModel.GetType() == typeof(TradeDataModel))
                {
                    TradeDataModel tempDataModel = (TradeDataModel)dataModel;
                    _fiefService.TradeList[index] = (TradeDataModel)tempDataModel.Clone();
                }
                else
                {
                    Console.WriteLine("ERROR!");
                }
            }
        }

        public int RemoveFief(int index)
        {
            bool removedLast = index == _fiefService.InformationList.Count - 1;

            _fiefService.InformationList.RemoveAt(index);
            _fiefService.ArmyList.RemoveAt(index);
            _fiefService.EmployeesList.RemoveAt(index);
            _fiefService.ManorList.RemoveAt(index);
            _fiefService.BoatbuildingList.RemoveAt(index);
            _fiefService.ExpensesList.RemoveAt(index);
            _fiefService.SubsidiaryList.RemoveAt(index);
            _fiefService.CustomSubsidiaryList.RemoveAt(index);
            _fiefService.IncomeList.RemoveAt(index);
            _fiefService.BuildingsList.RemoveAt(index);
            _fiefService.WeatherList.RemoveAt(index);
            _fiefService.MinesList.RemoveAt(index);
            _fiefService.PortsList.RemoveAt(index);
            _fiefService.TradeList.RemoveAt(index);

            return removedLast ? index - 1 : index;
        }

        public int CreateNewFief()
        {
            _fiefService.InformationList.Add(new InformationDataModel());
            _fiefService.ArmyList.Add(new ArmyDataModel());
            _fiefService.EmployeesList.Add(new EmployeesDataModel());
            _fiefService.ManorList.Add(new ManorDataModel());
            _fiefService.BoatbuildingList.Add(new BoatbuildingDataModel());
            _fiefService.ExpensesList.Add(new ExpensesDataModel());
            _fiefService.SubsidiaryList.Add(new SubsidiaryDataModel());
            _fiefService.CustomSubsidiaryList.Add(new SubsidiaryModel());
            _fiefService.IncomeList.Add(new IncomeDataModel());
            _fiefService.BuildingsList.Add(new BuildingsDataModel());
            _fiefService.WeatherList.Add(new WeatherDataModel());
            _fiefService.MinesList.Add(new MinesDataModel());
            _fiefService.PortsList.Add(new PortDataModel());
            _fiefService.TradeList.Add(new TradeDataModel());

            int id = GetNewIndustryId();

            _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection.Add(new IndustryBeingDevelopedModel()
            {
                Id = id,
                Industry = "Utveckla Medicin" + " (" + _fiefService.InformationList[_fiefService.InformationList.Count - 1].FiefName + ")",
                FiefId = _fiefService.InformationList.Count - 1,
                IndustryId = id,
                StewardId = -1
            });
            id++;

            _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection.Add(new IndustryBeingDevelopedModel()
            {
                Id = id,
                Industry = "Utveckla Militär" + " (" + _fiefService.InformationList[_fiefService.InformationList.Count - 1].FiefName + ")",
                FiefId = _fiefService.InformationList.Count - 1,
                IndustryId = id,
                StewardId = -1
            });
            id++;

            _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection.Add(new IndustryBeingDevelopedModel()
            {
                Id = id,
                Industry = "Utveckla Utbildning" + " (" + _fiefService.InformationList[_fiefService.InformationList.Count - 1].FiefName + ")",
                FiefId = _fiefService.InformationList.Count - 1,
                IndustryId = id,
                StewardId = -1
            });

            return _fiefService.InformationList.Count - 1;
        }

        public ObservableCollection<IndustryBeingDevelopedModel> CreateNewBeingDevelopedIndustries()
        {
            ObservableCollection<IndustryBeingDevelopedModel> collection = new ObservableCollection<IndustryBeingDevelopedModel>();
            int id = GetNewIndustryId();

            collection.Add(new IndustryBeingDevelopedModel()
            {
                Id = id,
                Industry = "Utveckla Medicin" + " (" + _fiefService.InformationList[1].FiefName + ")",
                FiefId = 1,
                IndustryId = id,
                StewardId = -1
            });
            id++;

            collection.Add(new IndustryBeingDevelopedModel()
            {
                Id = id,
                Industry = "Utveckla Militär" + " (" + _fiefService.InformationList[1].FiefName + ")",
                FiefId = 1,
                IndustryId = id,
                StewardId = -1
            });
            id++;

            collection.Add(new IndustryBeingDevelopedModel()
            {
                Id = id,
                Industry = "Utveckla Utbildning" + " (" + _fiefService.InformationList[1].FiefName + ")",
                FiefId = 1,
                IndustryId = id,
                StewardId = -1
            });

            return collection;
        }

        public ObservableCollection<IndustryBeingDevelopedModel> UpdateIndustriesBeingDevelopedCollection(ObservableCollection<IndustryBeingDevelopedModel> collection)
        {
            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {
                string fief = _fiefService.InformationList[x].FiefName;
                bool fiefFound = false;

                for (int y = 0; y < collection.Count; y++)
                {
                    if (collection[y].Industry.Contains(fief))
                    {
                        fiefFound = true;
                        break;
                    }
                }

                if (!fiefFound)
                {
                    int id = GetNewIndustryId();

                    collection.Add(new IndustryBeingDevelopedModel()
                    {
                        Id = id,
                        Industry = "Utveckla Medicin" + " (" + fief + ")",
                        FiefId = x,
                        IndustryId = id,
                        StewardId = -1
                    });
                    id++;

                    collection.Add(new IndustryBeingDevelopedModel()
                    {
                        Id = id,
                        Industry = "Utveckla Militär" + " (" + fief + ")",
                        FiefId = x,
                        IndustryId = id,
                        StewardId = -1
                    });
                    id++;

                    collection.Add(new IndustryBeingDevelopedModel()
                    {
                        Id = id,
                        Industry = "Utveckla Utbildning" + " (" + fief + ")",
                        FiefId = x,
                        IndustryId = id,
                        StewardId = -1
                    });
                }
            }

            return collection;
        }

        public ObservableCollection<string> GetFiefCollection()
        {
            ObservableCollection<string> tempCollection = new ObservableCollection<string>();

            for (int x = 0; x < _fiefService.InformationList.Count; x++)
            {
                tempCollection.Add(_fiefService.InformationList[x].FiefName);
            }

            return tempCollection;
        }

        public int RollObDice(int skill)
        {
            int total = 0;
            int nrDice = Convert.ToInt32(Math.Floor((decimal)skill / 4));
            int temp = nrDice;
            int mod = skill - nrDice * 4;
            List<int> tempList = new List<int>();
            string str = "";

            while (nrDice > 0)
            {
                int roll = _fiefService.GetRandom(1, 7);

                if (roll == 6)
                {
                    tempList.Add(6);
                    nrDice++;
                }
                else
                {
                    total += roll;
                    tempList.Add(roll);
                    nrDice--;
                }
            }

            total += mod;

            if (mod > 0)
            {
                str += $"Tärningsslag: { temp }T6+{ mod } = ";
            }
            else
            {
                str += $"Tärningsslag: { temp }T6 = ";
            }
            
            for (int i = 0; i < tempList.Count; i++)
            {
                if (i == 0)
                {
                    str += $"{ tempList[i] } ";
                }
                else
                {
                    str +=$",{ tempList[i] } ";
                }
            }

            str += $"Totalt={ total }";
            Console.WriteLine(str);
            return total;
        }

        public int RollDie(int x, int y)
        {
            return _fiefService.GetRandom(x, y);
        }

        public int ConvertToNumeric(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            else
            {
                if (str.IndexOf('T') != -1 || str.Length < 3)
                {
                    bool isNegative;
                    string temp;

                    if (str.IndexOf('-') == -1)
                    {
                        temp = str;
                        isNegative = false;
                    }
                    else
                    {
                        temp = str;
                        temp = temp.Substring(1);
                        isNegative = true;
                    }
                    if (temp.IndexOf('+') == 0)
                    {
                        temp = temp.Substring(1);
                    }


                    string temp2;
                    var value = 0;
                    var loop = true;
                    var holder = "";

                    if (temp.Length < 3)
                    {
                        value = Convert.ToInt32(temp);
                    }
                    else
                    {
                        int x;
                        for (x = 0; x < temp.Length && loop; x++)
                        {
                            if (Char.IsDigit(temp[x]))
                            {
                                temp2 = temp;
                                holder = holder + temp2[x];
                            }
                            else
                            {
                                value = Convert.ToInt32(holder);
                                value = value * 4;
                                loop = false;
                            }
                        }
                    }
                    if (temp.IndexOf('+') != -1)
                    {
                        var pos = temp.IndexOf('+');
                        pos = pos + 1;
                        var y = temp.Substring(pos, 1);
                        value = value + Convert.ToInt32(y);
                    }
                    if (isNegative)
                    {
                        return -value;
                    }
                    else
                    {
                        return value;
                    }
                }
            }
            return 0;
        }

        public string ConvertToT6(int num)
        {
            string value;
            bool isNegative;
            string temp = num.ToString();

            if (temp.IndexOf('-') != -1)
            {
                temp = temp.Substring(1);
                isNegative = true;
            }
            else
            {
                isNegative = false;
            }


            var y = Convert.ToInt32(temp);
            var i = y / 4;
            var x = y - i * 4;

            if (!isNegative)
            {
                if (x > 0)
                {
                    if (i != 0)
                        value = i + "T6+" + x;
                    else
                        value = "+" + x;
                }
                else
                {
                    if (i != 0)
                        value = i + "T6";
                    else
                        value = "0";
                }
            }
            else
            {
                if (x > 0)
                {
                    if (i != 0)
                        value = "-" + i + "T6+" + x;
                    else
                        value = "-" + x;
                }
                else
                {
                    if (i != 0)
                        value = "-" + i + "T6";
                    else
                        value = "0";
                }
            }
            return value;
        }

        public string GetCommonerName()
        {
            return _nameGenerator.GetRandomCommonerName();
        }

        public string GetNobleName()
        {
            return _nameGenerator.GetRandomNoble();
        }
        public ObservableCollection<StewardModel> GetStewards()
        {
            return _fiefService.StewardsDataModel.StewardsCollection;
        }

        public int GetNewIndustryId()
        {
            List<int> tempList = new List<int>();

            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {
                if (_fiefService.IncomeList[x].IncomesCollection.Count > 0)
                {
                    tempList.Add(_fiefService.IncomeList[x].IncomesCollection.Max(o => o.Id));
                }
                if (_fiefService.MinesList[x].MinesCollection.Count > 0)
                {
                    tempList.Add(_fiefService.MinesList[x].MinesCollection.Max(o => o.Id));
                }
                if (_fiefService.MinesList[x].QuarriesCollection.Count > 0)
                {
                    tempList.Add(_fiefService.MinesList[x].QuarriesCollection.Max(o => o.Id));
                }
                if (_fiefService.PortsList[x].Shipyard.Id != -1)
                {
                    tempList.Add(_fiefService.PortsList[x].Shipyard.Id);
                }
                if (_fiefService.SubsidiaryList[x].SubsidiaryCollection.Count > 0)
                {
                    tempList.Add(_fiefService.SubsidiaryList[x].SubsidiaryCollection.Max(o => o.Id));
                }
                if (_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection.Count > 0)
                {
                    tempList.Add(_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection.Max(o => o.IndustryId));
                }
                if (_fiefService.SubsidiaryList[x].ConstructingCollection.Count > 0)
                {
                    tempList.Add(_fiefService.SubsidiaryList[x].ConstructingCollection.Max(o => o.Id));
                }
            }

            if (tempList.Count > 0)
            {
                return tempList.Max() + 1;
            }
            return 0;
        }

        public void RemoveSteward(int stewardId)
        {
            for (int x = 0; x < _fiefService.InformationList.Count; x++)
            {
                for (int y = 0; y < _fiefService.IncomeList[x].IncomesCollection.Count; y++)
                {
                    if (_fiefService.IncomeList[x].IncomesCollection[y].StewardId == stewardId)
                    {
                        _fiefService.IncomeList[x].IncomesCollection[y].StewardId = -1;
                    }
                }

                for (int y = 0; y < _fiefService.MinesList[x].MinesCollection.Count; y++)
                {
                    if (_fiefService.MinesList[x].MinesCollection[y].StewardId == stewardId)
                    {
                        _fiefService.MinesList[x].MinesCollection[y].StewardId = -1;
                    }
                }

                for (int y = 0; y < _fiefService.MinesList[x].QuarriesCollection.Count; y++)
                {
                    if (_fiefService.MinesList[x].QuarriesCollection[y].StewardId == stewardId)
                    {
                        _fiefService.MinesList[x].QuarriesCollection[y].StewardId = -1;
                    }
                }

                if (_fiefService.PortsList[x].Shipyard.StewardId == stewardId)
                {
                    _fiefService.PortsList[x].Shipyard.StewardId = -1;
                }

                for (int y = 0; y < _fiefService.SubsidiaryList[x].SubsidiaryCollection.Count; y++)
                {
                    if (_fiefService.SubsidiaryList[x].SubsidiaryCollection[y].StewardId == stewardId)
                    {
                        _fiefService.SubsidiaryList[x].SubsidiaryCollection[y].StewardId = -1;
                    }
                }

                for (int y = 0; y < _fiefService.SubsidiaryList[x].ConstructingCollection.Count; y++)
                {
                    if (_fiefService.SubsidiaryList[x].ConstructingCollection[y].StewardId == stewardId)
                    {
                        _fiefService.SubsidiaryList[x].ConstructingCollection[y].StewardId = -1;
                    }
                }
            }

            for (int x = 0; x < _fiefService.StewardsDataModel.StewardsCollection.Count; x++)
            {
                if (_fiefService.StewardsDataModel.StewardsCollection[x].Id == stewardId)
                {
                    _fiefService.StewardsDataModel.StewardsCollection.RemoveAt(x);
                }
            }
        }

        public void RemoveIndustry(int industryId)
        {
            bool foundIndustry = false;

            for (int x = 0; x < _fiefService.StewardsDataModel.StewardsCollection.Count; x++)
            {
                if (_fiefService.StewardsDataModel.StewardsCollection[x].IndustryId == industryId)
                {
                    _fiefService.StewardsDataModel.StewardsCollection[x].IndustryId = -1;
                    break;
                }
            }

            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {
                for (int y = 0; y < _fiefService.IncomeList[x].IncomesCollection.Count; y++)
                {
                    if (_fiefService.IncomeList[x].IncomesCollection[y].Id == industryId)
                    {
                        foundIndustry = true;
                        _fiefService.IncomeList[x].IncomesCollection.RemoveAt(y);
                        break;
                    }
                }

                if (foundIndustry)
                {
                    break;
                }
                else
                {
                    for (int y = 0; y < _fiefService.MinesList[x].MinesCollection.Count; y++)
                    {
                        if (_fiefService.MinesList[x].MinesCollection[y].Id == industryId)
                        {
                            foundIndustry = true;
                            _fiefService.MinesList[x].MinesCollection.RemoveAt(y);
                            break;
                        }
                    }
                }

                if (foundIndustry)
                {
                    break;
                }
                else
                {
                    for (int y = 0; y < _fiefService.MinesList[x].QuarriesCollection.Count; y++)
                    {
                        if (_fiefService.MinesList[x].QuarriesCollection[y].Id == industryId)
                        {
                            foundIndustry = true;
                            _fiefService.MinesList[x].QuarriesCollection.RemoveAt(y);
                            break;
                        }
                    }
                }

                if (foundIndustry)
                {
                    break;
                }
                else
                {
                    if (_fiefService.PortsList[x].Shipyard.Id == industryId)
                    {
                        foundIndustry = true;
                        _fiefService.PortsList[x].Shipyard.Id = -1;
                        break;
                    }
                }

                if (foundIndustry)
                {
                    break;
                }
                else
                {
                    for (int y = 0; y < _fiefService.SubsidiaryList[x].SubsidiaryCollection.Count; y++)
                    {
                        if (_fiefService.SubsidiaryList[x].SubsidiaryCollection[y].Id == industryId)
                        {
                            foundIndustry = true;
                            _fiefService.SubsidiaryList[x].SubsidiaryCollection.RemoveAt(y);
                            break;
                        }
                    }
                }

                if (foundIndustry)
                {
                    break;
                }
                else
                {
                    for (int y = 0; y < _fiefService.SubsidiaryList[x].ConstructingCollection.Count; y++)
                    {
                        if (_fiefService.SubsidiaryList[x].ConstructingCollection[y].Id == industryId)
                        {
                            foundIndustry = true;
                            _fiefService.SubsidiaryList[x].ConstructingCollection.RemoveAt(y);
                            break;
                        }
                    }
                }
            }
        }

        public void ChangeSteward(int stewardId, int industryId, string industryType = "")
        {
            SetIndustryForStewardInStewardsCollection(stewardId, industryId, industryType);
            SetStewardInIndustriesCollections(stewardId, industryId);
        }

        private void SetIndustryForStewardInStewardsCollection(int stewardId, int industryId, string industryType)
        {
            for (int x = 1; x < _fiefService.StewardsDataModel.StewardsCollection.Count; x++)
            {
                if (_fiefService.StewardsDataModel.StewardsCollection[x].IndustryId == industryId
                    && _fiefService.StewardsDataModel.StewardsCollection[x].Id != stewardId)
                {
                    _fiefService.StewardsDataModel.StewardsCollection[x].IndustryId = -1;
                    _fiefService.StewardsDataModel.StewardsCollection[x].IndustryType = "";
                }
                else if (_fiefService.StewardsDataModel.StewardsCollection[x].Id == stewardId)
                {
                    _fiefService.StewardsDataModel.StewardsCollection[x].IndustryId = industryId;
                    _fiefService.StewardsDataModel.StewardsCollection[x].IndustryType = industryType;
                }
            }
        }

        private void SetStewardInIndustriesCollections(int stewardId, int industryId)
        {
            bool check = true;
            for (int x = 1; x < _fiefService.InformationList.Count; x++)
            {
                for (int y = 0; y < _fiefService.IncomeList[x].IncomesCollection.Count; y++)
                {
                    if (_fiefService.IncomeList[x].IncomesCollection[y].StewardId == stewardId)
                    {
                        if (_fiefService.IncomeList[x].IncomesCollection[y].Id != industryId)
                        {
                            _fiefService.IncomeList[x].IncomesCollection[y].StewardId = -1;
                        }
                    }
                    else
                    {
                        if (_fiefService.IncomeList[x].IncomesCollection[y].Id == industryId)
                        {
                            _fiefService.IncomeList[x].IncomesCollection[y].StewardId = stewardId;
                        }
                    }
                }

                for (int y = 0; y < _fiefService.MinesList[x].MinesCollection.Count; y++)
                {
                    if (_fiefService.MinesList[x].MinesCollection[y].StewardId == stewardId)
                    {
                        if (_fiefService.MinesList[x].MinesCollection[y].Id != industryId)
                        {
                            _fiefService.MinesList[x].MinesCollection[y].StewardId = -1;
                        }
                    }
                    else
                    {
                        if (_fiefService.MinesList[x].MinesCollection[y].Id == industryId)
                        {
                            _fiefService.MinesList[x].MinesCollection[y].StewardId = stewardId;
                        }
                    }
                }

                for (int y = 0; y < _fiefService.MinesList[x].QuarriesCollection.Count; y++)
                {
                    if (_fiefService.MinesList[x].QuarriesCollection[y].StewardId == stewardId)
                    {
                        if (_fiefService.MinesList[x].QuarriesCollection[y].Id != industryId)
                        {
                            _fiefService.MinesList[x].QuarriesCollection[y].StewardId = -1;
                        }
                    }
                    else
                    {
                        if (_fiefService.MinesList[x].QuarriesCollection[y].Id == industryId)
                        {
                            _fiefService.MinesList[x].QuarriesCollection[y].StewardId = stewardId;
                        }
                    }
                }

                if (_fiefService.PortsList[x].Shipyard.StewardId == stewardId)
                {
                    if (_fiefService.PortsList[x].Shipyard.Id != industryId)
                    {
                        _fiefService.PortsList[x].Shipyard.StewardId = -1;
                    }
                }
                else
                {
                    if (_fiefService.PortsList[x].Shipyard.Id == industryId)
                    {
                        _fiefService.PortsList[x].Shipyard.StewardId = stewardId;
                    }
                }

                for (int y = 0; y < _fiefService.SubsidiaryList[x].SubsidiaryCollection.Count; y++)
                {
                    if (_fiefService.SubsidiaryList[x].SubsidiaryCollection[y].StewardId == stewardId)
                    {
                        if (_fiefService.SubsidiaryList[x].SubsidiaryCollection[y].Id != industryId)
                        {
                            _fiefService.SubsidiaryList[x].SubsidiaryCollection[y].StewardId = -1;
                        }
                    }
                    else
                    {
                        if (_fiefService.SubsidiaryList[x].SubsidiaryCollection[y].Id == industryId)
                        {
                            _fiefService.SubsidiaryList[x].SubsidiaryCollection[y].StewardId = stewardId;
                        }
                    }
                }

                for (int y = 0; y < _fiefService.SubsidiaryList[x].ConstructingCollection.Count; y++)
                {
                    if (_fiefService.SubsidiaryList[x].ConstructingCollection[y].StewardId == stewardId)
                    {
                        if (_fiefService.SubsidiaryList[x].ConstructingCollection[y].Id != industryId)
                        {
                            _fiefService.SubsidiaryList[x].ConstructingCollection[y].StewardId = -1;
                        }
                    }
                    else
                    {
                        if (_fiefService.SubsidiaryList[x].ConstructingCollection[y].Id == industryId)
                        {
                            _fiefService.SubsidiaryList[x].ConstructingCollection[y].StewardId = stewardId;
                        }
                    }
                }

                if (check)
                {
                    check = false;
                    for (int y = 0; y < _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection.Count; y++)
                    {
                        if (_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[y].StewardId == stewardId)
                        {
                            if (_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[y].Id != industryId)
                            {
                                _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[y].StewardId = -1;
                            }
                        }
                        else
                        {
                            if (_fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[y].Id == industryId)
                            {
                                _fiefService.StewardsDataModel.IndustriesBeingDevelopedCollection[y].StewardId = stewardId;
                            }
                        }
                    }
                }
            }
        }

        public ObservableCollection<StewardModel> GetStewardsCollection()
        {
            List<StewardModel> tempList = new List<StewardModel>(_fiefService.StewardsDataModel.StewardsCollection);
            return new ObservableCollection<StewardModel>(tempList);
        }

        public void SaveStewardsCollection(ObservableCollection<StewardModel> collection)
        {
            List<StewardModel> tempList = new List<StewardModel>(collection);
            _fiefService.StewardsDataModel.StewardsCollection = new ObservableCollection<StewardModel>(tempList);
        }
    }
}
