using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Mines.RoutedEvents;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FiefApp.Module.Mines
{
    public class MinesViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IMinesService _minesService;
        private readonly IEventAggregator _eventAggregator;

        public MinesViewModel(
            IBaseService baseService,
            IMinesService minesService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            TabName = "Stenbrott/Gruvor";

            _baseService = baseService;
            _minesService = minesService;
            _eventAggregator = eventAggregator;

            AddQuarryCommand = new DelegateCommand(ExecuteAddQuarryCommand);
            ConstructQuarryUIEventHandler = new CustomDelegateCommand(ExecuteConstructQuarryUIEventHandler, o => true);
            AddMineUIEventHandler = new CustomDelegateCommand(ExecuteAddMineUIEventHandler, o => true);
            MineUIEventHandler = new CustomDelegateCommand(ExecuteMineUIEventHandler, o => true);
            QuarryUIEventHandler = new CustomDelegateCommand(ExecuteQuarryUIEventHandler, o => true);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
        }

        #region DelegateCommand : AddQuarryCommand

        public DelegateCommand AddQuarryCommand { get; set; }
        private void ExecuteAddQuarryCommand()
        {
            DataModel.UpdateTotals();
        }

        #endregion

        #region CustomDelegateCommand : ConstructQuarryUIEventHandler

        public CustomDelegateCommand ConstructQuarryUIEventHandler { get; set; }
        private void ExecuteConstructQuarryUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is ConstructQuarryUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Save")
            {
                e.Model.Id = _baseService.GetNewIndustryId();
                e.Model.IsFirstYear = true;

                DataModel.QuarriesCollection.Add(e.Model);
                DataModel.UpdateTotals();

                GetInformationSetDataModel();
                SaveData();
            }
        }

        #endregion

        #region CustomDelegateCommand : AddMineUIEventHandler

        public CustomDelegateCommand AddMineUIEventHandler { get; set; }
        private void ExecuteAddMineUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is AddMineUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Save")
            {
                e.Model.Id = _baseService.GetNewIndustryId();
                e.Model.StewardId = -1;
                e.Model.Steward = "";
                e.Model.StewardsCollection = DataModel.StewardsCollection;
                e.Model.BaseIncome = e.Model.Income;
                e.Model.Income = 0;
                DataModel.MinesCollection.Add(e.Model);
                DataModel.UpdateTotals();

                GetInformationSetDataModel();
                SaveData();
            }
        }

        #endregion

        #region CustomDelegateCommand : MineUIEventHandler

        public CustomDelegateCommand MineUIEventHandler { get; set; }
        private void ExecuteMineUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is MineUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Changed":
                {
                    SaveData();
                    _baseService.ChangeSteward(e.StewardId, e.MineId);
                    List<MineModel> tempList = new List<MineModel>(DataModel.MinesCollection);
                    List<QuarryModel> quarryList = new List<QuarryModel>(DataModel.QuarriesCollection);
                    DataModel.MinesCollection.Clear();
                    DataModel.QuarriesCollection.Clear();
                    DataModel.MinesCollection = new ObservableCollection<MineModel>(tempList);
                    DataModel.QuarriesCollection = new ObservableCollection<QuarryModel>(quarryList);
                    DataModel.UpdateTotals();
                    break;
                }

                case "Guards" when _minesService.SetUsedGuards(Index, e.Guards):
                {
                    UpdateAvailableGuardsInMines();

                    for (int x = 0; x < DataModel.MinesCollection.Count; x++)
                    {
                        if (e.MineId == DataModel.MinesCollection[x].Id)
                        {
                            DataModel.MinesCollection[x].Guards += e.Guards;
                            DataModel.UpdateTotals();
                            break;
                        }
                    }
                    break;
                }

                case "Guards":
                {
                    for (int x = 0; x < DataModel.MinesCollection.Count; x++)
                    {
                        if (DataModel.MinesCollection[x].Id == e.MineId)
                        {
                            DataModel.MinesCollection[x].Guards = e.OldAmountGuards;
                        }
                    }

                    List<MineModel> tempList = new List<MineModel>(DataModel.MinesCollection);
                    DataModel.MinesCollection.Clear();
                    DataModel.MinesCollection = new ObservableCollection<MineModel>(tempList);
                    DataModel.UpdateTotals();
                    break;
                }

                case "Delete":
                {
                    for (int x = 0; x < DataModel.MinesCollection.Count; x++)
                    {
                        if (DataModel.MinesCollection[x].Id == e.MineId)
                        {
                            DataModel.MinesCollection.RemoveAt(x);
                            DataModel.UpdateTotals();
                            break;
                        }
                    }
                    break;
                }

                case "IncomeUpdated":
                {
                    for (int x = 0; x < DataModel.MinesCollection.Count; x++)
                    {
                        if (DataModel.MinesCollection[x].Id == e.MineId)
                        {
                            DataModel.MinesCollection[x].Income = e.Income;
                            DataModel.UpdateTotals();
                            break;
                        }
                    }
                    break;
                }
            }
        }

        #endregion

        #region CustomDelegateCommand : QuarryUIEventHandler

        public CustomDelegateCommand QuarryUIEventHandler { get; set; }
        private void ExecuteQuarryUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is QuarryUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Delete":
                {
                    for (int x = 0; x < DataModel.QuarriesCollection.Count; x++)
                    {
                        if (DataModel.QuarriesCollection[x].Id == e.QuarryId)
                        {
                            DataModel.QuarriesCollection.RemoveAt(x);
                            DataModel.UpdateTotals();
                            break;
                        }
                    }
                    break;
                }

                case "DaysWork":
                {
                    for (int x = 0; x < DataModel.QuarriesCollection.Count; x++)
                    {
                        if (DataModel.QuarriesCollection[x].Id == e.QuarryId)
                        {
                            DataModel.QuarriesCollection[x].DaysWorkThisYear = e.DaysWork;
                            DataModel.UpdateTotals();
                            break;
                        }
                    }
                    break;
                }

                case "Changed":
                {
                    SaveData();
                    _baseService.ChangeSteward(e.StewardId, e.QuarryId);
                    List<MineModel> tempList = new List<MineModel>(DataModel.MinesCollection);
                    List<QuarryModel> quarryList = new List<QuarryModel>(DataModel.QuarriesCollection);
                    DataModel.MinesCollection.Clear();
                    DataModel.QuarriesCollection.Clear();
                    DataModel.MinesCollection = new ObservableCollection<MineModel>(tempList);
                    DataModel.QuarriesCollection = new ObservableCollection<QuarryModel>(quarryList);
                    DataModel.UpdateTotals();
                    break;
                }

                case "IncomeUpdated":
                {
                    for (int x = 0; x < DataModel.QuarriesCollection.Count; x++)
                    {
                        if (DataModel.QuarriesCollection[x].Id == e.QuarryId)
                        {
                            DataModel.QuarriesCollection[x].ApproximateIncome = e.Income;
                            DataModel.UpdateTotals();
                            break;
                        }
                    }
                    break;
                }
            }
        }

        #endregion

        #region DataModel

        private MinesDataModel _dataModel = new MinesDataModel();
        public MinesDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        #region Methods

        protected override void SaveData(int index = -1)
        {
            _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
        }

        protected override void LoadData()
        {
            DataModel = Index
                        == 0 ? _minesService.GetAllMinesDataModel()
                : _baseService.GetDataModel<MinesDataModel>(Index);

            GetInformationSetDataModel();
            DataModel.UpdateTotals();

            UpdateFiefCollection();
        }

        private void GetInformationSetDataModel()
        {
            UpdateDifficultyInMines();
            UpdateStewardsCollectionInMines();
            UpdateAvailableGuardsInMines();
            UpdateDifficultyInQuarries();
            UpdateStewardsCollectionInQuarries();
        }

        private void UpdateDifficultyInMines()
        {
            if (DataModel.MinesCollection.Count > 0)
            {
                for (int x = 0; x < DataModel.MinesCollection.Count; x++)
                {
                    DataModel.MinesCollection[x].Difficulty = _minesService.GetMinesDifficulty(Index);
                }
            }
        }

        private void UpdateDifficultyInQuarries()
        {
            if (DataModel.QuarriesCollection.Count > 0)
            {
                for (int x = 0; x < DataModel.QuarriesCollection.Count; x++)
                {
                    DataModel.QuarriesCollection[x].Difficulty = _minesService.GetQuarriesDifficulty(Index);
                }
            }
        }

        private void UpdateStewardsCollectionInMines()
        {
            if (DataModel.MinesCollection.Count > 0)
            {
                for (int x = 0; x < DataModel.MinesCollection.Count; x++)
                {
                    DataModel.MinesCollection[x].StewardsCollection = new ObservableCollection<StewardModel>(_minesService.GetStewardsCollection());
                }
            }
        }

        private void UpdateStewardsCollectionInQuarries()
        {
            if (DataModel.QuarriesCollection.Count > 0)
            {
                for (int x = 0; x < DataModel.QuarriesCollection.Count; x++)
                {
                    DataModel.QuarriesCollection[x].StewardsCollection = new ObservableCollection<StewardModel>(_minesService.GetStewardsCollection());
                }
            }
        }

        private void UpdateAvailableGuardsInMines()
        {
            int guards = _minesService.GetAvailableGuards(Index);

            if (DataModel.MinesCollection.Count > 0)
            {
                for (int x = 0; x < DataModel.MinesCollection.Count; x++)
                {
                    DataModel.MinesCollection[x].AvailableGuards = guards;
                }
            }
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            LoadData();
        }

        #endregion
    }
}
