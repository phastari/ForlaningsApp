using System;
using System.ComponentModel;
using System.Timers;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Services;
using Prism.Commands;
using Prism.Events;

namespace FiefApp.Module.Weather
{
    public class WeatherViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IWeatherService _weatherService;
        private readonly IEventAggregator _eventAggregator;
        private readonly Timer _timer;

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

            EndOfYearCommand = new DelegateCommand(ExecuteEndOfYearCommand);
        }

        #region DelegateCommand : EndOfYearCommand

        public DelegateCommand EndOfYearCommand { get; set; }
        private void ExecuteEndOfYearCommand()
        {
            if (DataModel.SpringRoll > 0
                && DataModel.SummerRoll > 0
                && DataModel.FallRoll > 0
                && DataModel.WinterRoll > 0)
            {
                _eventAggregator.GetEvent<EndOfYearEvent>().Publish();
            }
            else
            {
                _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                _timer.Interval = 2750;
                _timer.Start();
                DataModel.EndOfYearError = "Du har inte fyllt i årets väder!";
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
                    break;
                }

                case "TaxSerfs":
                {
                    UpdateForecast();
                    break;
                }

                case "TaxFarmers":
                {
                    UpdateForecast();
                    break;
                }

                case "TaxFreemen":
                {
                    UpdateForecast();
                    break;
                }
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

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            LoadData();
        }

        private void GetInformationSetDataModel()
        {
            GetTotalAmountOfSerfs();
            GetTotalAmountOfSlaves();
            GetNumberOfFishingboats();
            GetSubsidiaryData();
            GetForecasts();
            GetManorAcresSetManorDaysWork();
            GetMaxFellingLandClearing();
        }

        #region Methods : GetInformationSetDataModel

        private void GetMaxFellingLandClearing()
        {
            DataModel.LandClearingMax = _weatherService.GetMaxLandClearing(Index);
            DataModel.LandClearingOfFellingMax = _weatherService.GetMaxLandClearFelling(Index);
            DataModel.FellingMax = _weatherService.GetMaxFelling(Index);
            DataModel.ClearUselessMax = _weatherService.GetMaxUseless(Index);
        }

        private void GetTotalAmountOfSerfs()
        {
            DataModel.Serfs = _weatherService.GetTotalAmountOfSerfs(Index);
        }

        private void GetTotalAmountOfSlaves()
        {
            DataModel.Slaves = _weatherService.GetTotalAmountOfSlaves(Index);
        }

        private void GetSubsidiaryData()
        {
            DataModel.NumberOfSubsidiaries = _weatherService.GetTotalNumberOfSubsidaries(Index);
            DataModel.SubsidiariesDayswork = _weatherService.GetTotalAmountOfDaysworkFromSubsidiaries(Index);
        }

        private void GetForecasts()
        {
            DataModel.ThisYearSilver = _weatherService.GetForecastForSilver(Index);
            DataModel.ThisYearBase = _weatherService.GetForecastForBase(Index);
            DataModel.ThisYearLuxury = _weatherService.GetForecastForLuxury(Index);
            DataModel.ThisYearIron = _weatherService.GetForecastForIron(Index);
            DataModel.ThisYearStone = _weatherService.GetForecastForStone(Index);
            DataModel.ThisYearWood = _weatherService.GetForecastForWood(Index);
        }

        private void GetManorAcresSetManorDaysWork()
        {
            int acres = _weatherService.GetManorAcres(Index);

            DataModel.ManorDaysWork = Convert.ToInt32(Math.Ceiling((decimal)acres / 6 * 80));
        }

        private void GetNumberOfFishingboats()
        {
            DataModel.NumberOfFishingBoats = _weatherService.GetNumberOfFishingboats(Index);
        }

        #endregion
    }
}
