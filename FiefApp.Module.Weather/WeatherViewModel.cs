using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;

namespace FiefApp.Module.Weather
{
    public class WeatherViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IWeatherService _weatherService;
        private readonly IEventAggregator _eventAggregator;
        private readonly Timer _timer;
        private bool _executing = false;
        private List<UpdateAllEventParameters> _awaitAllModulesList = new List<UpdateAllEventParameters>()
        {
            new UpdateAllEventParameters()
            {
                ModuleName = "Army",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Boatbuilding",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Buildings",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Employees",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Expenses",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Income",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Information",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Manor",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Mines",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Port",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Stewards",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Subsidiary",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Trade",
                Completed = false
            }
        };

        public WeatherViewModel(
            IBaseService baseService,
            IWeatherService weatherService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            _baseService = baseService;
            _weatherService = weatherService;
            _eventAggregator = eventAggregator;

            _timer = new Timer();

            TabName = "Väder/Dagsverk";

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
            _eventAggregator.GetEvent<UpdateAllResponseEvent>().Subscribe(HandleUpdateAllEventResponses);
            _eventAggregator.GetEvent<UpdateAllEvent>().Subscribe(UpdateAllAndRespond);

            EndOfYearCommand = new DelegateCommand(ExecuteEndOfYearCommand);
        }

        #region EventAggregator Events

        private void HandleUpdateAllEventResponses(UpdateAllEventParameters param)
        {
            if (param.Publisher == "Weather"
                && _awaitAllModulesList != null)
            {
                for (int x = 0; x < _awaitAllModulesList.Count; x++)
                {
                    if (_awaitAllModulesList[x].ModuleName == param.ModuleName)
                    {
                        _awaitAllModulesList[x].Completed = param.Completed;
                    }
                }

                if (_awaitAllModulesList.All(o => o.Completed))
                {
                    CompleteLoadData();
                }
            }
        }

        private void UpdateAllAndRespond(string str)
        {
            SaveData(Index);

            UpdateFiefCollection();
            for (int x = 1; x < FiefCollection.Count; x++)
            {
                DataModel = _baseService.GetDataModel<WeatherDataModel>(x);
                GetInformationSetDataModel(x);
                SaveData(x);
            }

            _eventAggregator.GetEvent<UpdateAllResponseEvent>().Publish(new UpdateAllEventParameters()
            {
                ModuleName = "Weather",
                Completed = true,
                Publisher = str
            });
        }

        #endregion

        private void CompleteLoadData()
        {
            DataModel = Index
                        == 0 ? _weatherService.GetAllWeatherDataModels()
                : _baseService.GetDataModel<WeatherDataModel>(Index);

            if (DataModel != null)
            {
                DataModel.PropertyChanged += DataModelPropertyChange;
            }

            GetInformationSetDataModel();
            UpdateFiefCollection();
        }

        #region DelegateCommand : EndOfYearCommand

        public DelegateCommand EndOfYearCommand { get; set; }
        private void ExecuteEndOfYearCommand()
        {
            if (!_executing)
            {
                if (_weatherService.CheckAllWeather())
                {
                    _executing = true;
                    SetAwaitAllModules();
                    _eventAggregator.GetEvent<UpdateAllEvent>().Publish();
                }
                else
                {
                    _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    _timer.Interval = 2750;
                    _timer.Start();
                    DataModel.EndOfYearError = "Du har inte fyllt i årets väder!";
                    _executing = false;
                }
            }
        }

        #endregion

        #region DataModel

        private WeatherDataModel _dataModel;
        public WeatherDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        protected override void SaveData(int index = -1)
        {
            _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
        }

        protected override void LoadData()
        {
            CompleteLoadData();
        }

        private void OnTimedEvent(
            object sender,
            ElapsedEventArgs e)
        {
            DataModel.EndOfYearError = "";
            _timer.Stop();
        }

        private void DataModelPropertyChange(
            object sender,
            PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Tariffs":
                    {
                        UpdateForecast();
                        CalculateHappiness();
                        break;
                    }

                case "TaxSerfs":
                    {
                        UpdateForecast();
                        CalculateHappiness();
                        break;
                    }

                case "TaxFarmers":
                    {
                        UpdateForecast();
                        CalculateHappiness();
                        break;
                    }

                case "TaxBurgess":
                    {
                        UpdateForecast();
                        CalculateHappiness();
                        break;
                    }

                case "HappinessTotal":
                    UpdateForecast();
                    SaveData();
                    break;
            }
        }

        private void UpdateForecast()
        {
            DataModel.ThisYearSilver = _weatherService.GetForecastForSilver(Index);
            DataModel.ThisYearBase = _weatherService.GetForecastForBase(Index);
            DataModel.ThisYearLuxury = _weatherService.GetForecastForLuxury(Index);
            DataModel.ThisYearIron = _weatherService.GetForecastForIron(Index);
            DataModel.ThisYearStone = _weatherService.GetForecastForStone(Index);
            DataModel.ThisYearWood = _weatherService.GetForecastForWood(Index);
        }

        private void CalculateHappiness()
        {
            if (Index != 0)
            {
                int totalPopulation = _weatherService.GetTotalPopulation(Index);
                int serfs = _weatherService.GetTotalAmountOfSerfs(Index);
                int burgess = _weatherService.GetTotalAmountOfBurgess(Index);
                int farmers = totalPopulation - serfs - burgess;

                double pSerfs = (double)serfs / totalPopulation;
                double pBurgess = (double)burgess / totalPopulation;
                double pFarmers = 1 - pSerfs - pBurgess;

                int happinessSerfs = (int)Math.Floor((DataModel.TaxSerfs - 20) * pSerfs + (DataModel.TaxSerfs - DataModel.TaxSerfsLastYear) * pSerfs);
                int happinessBurgess = (int)Math.Floor(((DataModel.TaxBurgess - 20) * pBurgess + (DataModel.TaxBurgess - DataModel.TaxBurgessLastYear) * pBurgess) * 18);
                int happinessFarmers = (int)Math.Floor(((DataModel.TaxFarmers - 20) * pFarmers + (DataModel.TaxFarmers - DataModel.TaxFarmersLastYear) * pFarmers) * 6);

                if (happinessSerfs < 0)
                {
                    happinessSerfs /= 2;
                }
                if (happinessFarmers < 0)
                {
                    happinessFarmers /= 2;
                }
                if (happinessBurgess < 0)
                {
                    happinessBurgess /= 2;
                }
                DataModel.HappinessTaxes = (happinessSerfs + happinessBurgess + happinessFarmers).ToString();
            }
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            CompleteLoadData();
        }

        private void GetInformationSetDataModel(int index = -1)
        {
            if (index == -1)
            {
                GetTotalAmountOfSerfs();
                GetTotalAmountOfSlaves();
                GetNumberOfFishingboats();
                GetSubsidiaryData();
                GetMinesAndQuarriesData();
                GetForecasts();
                GetManorAcresSetManorDaysWork();
                GetMaxFellingLandClearing();
            }
            else
            {
                GetTotalAmountOfSerfs(index);
                GetTotalAmountOfSlaves(index);
                GetNumberOfFishingboats(index);
                GetSubsidiaryData(index);
                GetMinesAndQuarriesData(index);
                GetForecasts(index);
                GetManorAcresSetManorDaysWork(index);
                GetMaxFellingLandClearing(index);
            }
        }

        #region Methods : GetInformationSetDataModel

        private void GetMaxFellingLandClearing(int index = -1)
        {
            if (index == -1)
            {
                DataModel.LandClearingMax = _weatherService.GetMaxLandClearing(Index);
                DataModel.LandClearingOfFellingMax = _weatherService.GetMaxLandClearFelling(Index);
                DataModel.FellingMax = _weatherService.GetMaxFelling(Index);
                DataModel.ClearUselessMax = _weatherService.GetMaxUseless(Index);
            }
            else
            {
                DataModel.LandClearingMax = _weatherService.GetMaxLandClearing(index);
                DataModel.LandClearingOfFellingMax = _weatherService.GetMaxLandClearFelling(index);
                DataModel.FellingMax = _weatherService.GetMaxFelling(index);
                DataModel.ClearUselessMax = _weatherService.GetMaxUseless(index);
            }
        }

        private void GetTotalAmountOfSerfs(int index = -1)
        {
            if (index == -1)
            {
                DataModel.Serfs = _weatherService.GetTotalAmountOfSerfs(Index);
            }
            else
            {
                DataModel.Serfs = _weatherService.GetTotalAmountOfSerfs(index);
            }
        }

        private void GetTotalAmountOfSlaves(int index = -1)
        {
            if (index == -1)
            {
                DataModel.Slaves = _weatherService.GetTotalAmountOfSlaves(Index);
            }
            else
            {
                DataModel.Slaves = _weatherService.GetTotalAmountOfSlaves(index);
            }
        }

        private void GetSubsidiaryData(int index = -1)
        {
            if (index == -1)
            {
                DataModel.NumberOfSubsidiaries = _weatherService.GetTotalNumberOfSubsidaries(Index);
                DataModel.SubsidiariesDayswork = _weatherService.GetTotalAmountOfDaysworkFromSubsidiaries(Index);
            }
            else
            {
                DataModel.NumberOfSubsidiaries = _weatherService.GetTotalNumberOfSubsidaries(index);
                DataModel.SubsidiariesDayswork = _weatherService.GetTotalAmountOfDaysworkFromSubsidiaries(index);
            }
        }

        private void GetMinesAndQuarriesData(int index = -1)
        {
            if (index == -1)
            {
                DataModel.NumberOfMinesAndQuarries = _weatherService.GetNumberOfMinesAndQuarries(Index);
                DataModel.MinesAndQuarriesDaysWork = _weatherService.GetTotalAmountOfDaysWorkFromQuarries(Index);
            }
            else
            {
                DataModel.NumberOfMinesAndQuarries = _weatherService.GetNumberOfMinesAndQuarries(index);
                DataModel.MinesAndQuarriesDaysWork = _weatherService.GetTotalAmountOfDaysWorkFromQuarries(index);
            }
        }

        private void GetForecasts(int index = -1)
        {
            if (index == -1)
            {
                DataModel.ThisYearSilver = _weatherService.GetForecastForSilver(Index);
                DataModel.ThisYearBase = _weatherService.GetForecastForBase(Index);
                DataModel.ThisYearLuxury = _weatherService.GetForecastForLuxury(Index);
                DataModel.ThisYearIron = _weatherService.GetForecastForIron(Index);
                DataModel.ThisYearStone = _weatherService.GetForecastForStone(Index);
                DataModel.ThisYearWood = _weatherService.GetForecastForWood(Index);
            }
            else
            {
                DataModel.ThisYearSilver = _weatherService.GetForecastForSilver(index);
                DataModel.ThisYearBase = _weatherService.GetForecastForBase(index);
                DataModel.ThisYearLuxury = _weatherService.GetForecastForLuxury(index);
                DataModel.ThisYearIron = _weatherService.GetForecastForIron(index);
                DataModel.ThisYearStone = _weatherService.GetForecastForStone(index);
                DataModel.ThisYearWood = _weatherService.GetForecastForWood(index);
            }
        }

        private void GetManorAcresSetManorDaysWork(int index = -1)
        {
            if (index == -1)
            {
                int acres = _weatherService.GetManorAcres(Index);

                DataModel.ManorDaysWork = Convert.ToInt32(Math.Ceiling((decimal)acres / 6 * 80));
            }
            else
            {
                int acres = _weatherService.GetManorAcres(index);

                DataModel.ManorDaysWork = Convert.ToInt32(Math.Ceiling((decimal)acres / 6 * 80));
            }
        }

        private void GetNumberOfFishingboats(int index = -1)
        {
            if (index == -1)
            {
                DataModel.NumberOfFishingBoats = _weatherService.GetNumberOfFishingboats(Index);
            }
            else
            {
                DataModel.NumberOfFishingBoats = _weatherService.GetNumberOfFishingboats(index);
            }
        }

        #endregion

        private void SetAwaitAllModules()
        {
            for (int x = 0; x < _awaitAllModulesList.Count; x++)
            {
                _awaitAllModulesList[x].Completed = false;
            }
        }

        private void HandleResponse(UpdateAllEventParameters param)
        {
            if (_awaitAllModulesList != null)
            {
                for (int x = 0; x < _awaitAllModulesList.Count; x++)
                {
                    if (_awaitAllModulesList[x].ModuleName == param.ModuleName)
                    {
                        _awaitAllModulesList[x].Completed = param.Completed;
                    }
                }

                if (_awaitAllModulesList.All(o => o.Completed))
                {
                    for (int y = 0; y < _awaitAllModulesList.Count; y++)
                    {
                        _awaitAllModulesList[y].Completed = false;
                    }
                    _eventAggregator.GetEvent<EndOfYearEvent>().Publish();
                    _executing = false;
                }
            }
        }
    }
}
