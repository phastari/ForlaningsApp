using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Documents;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Mines.RoutedEvents;
using Prism.Commands;
using Prism.Events;

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

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
        }

        #region DelegateCommand : AddQuarryCommand

        public DelegateCommand AddQuarryCommand { get; set; }
        private void ExecuteAddQuarryCommand()
        {

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
                e.Model.Id = _minesService.GetNewIdForQuarry();

                DataModel.QuarriesCollection.Add(e.Model);
                DataModel.UpdateTotals();

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
                e.Model.Id = _minesService.GetNewIdForMine();
                e.Model.StewardId = -1;
                e.Model.Steward = "";
                e.Model.StewardsCollection = DataModel.StewardsCollection;
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

            if (e.Action == "Changed")
            {
                ChangeStewardInMinesCollection(e.StewardId, e.Steward, e.MineId, e.Skill);
                _minesService.ChangeSteward(e.StewardId, e.MineId, e.Steward);
                List<MineModel> tempList = new List<MineModel>(DataModel.MinesCollection);
                DataModel.MinesCollection.Clear();
                DataModel.MinesCollection = new ObservableCollection<MineModel>(tempList);
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

            UpdateFiefCollection();
        }

        private void GetInformationSetDataModel()
        {
            UpdateDifficultyInMines();
            UpdateStewardsCollectionInMines();
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

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            LoadData();
        }

        private void ChangeStewardInMinesCollection(int stewardId, string steward, int mineId, string skill)
        {
            for (int x = 0; x < DataModel.MinesCollection.Count; x++)
            {
                if (stewardId == DataModel.MinesCollection[x].StewardId)
                {
                    DataModel.MinesCollection[x].StewardId = -1;
                    DataModel.MinesCollection[x].Steward = "";
                }
            }

            for (int y = 0; y < DataModel.MinesCollection.Count; y++)
            {
                if (mineId == DataModel.MinesCollection[y].Id)
                {
                    DataModel.MinesCollection[y].StewardId = stewardId;
                    DataModel.MinesCollection[y].Steward = steward;
                    DataModel.MinesCollection[y].Skill = skill;
                }
            }
        }

        #endregion
    }
}
