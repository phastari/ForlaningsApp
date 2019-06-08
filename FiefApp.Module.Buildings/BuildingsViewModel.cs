using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Buildings.RoutedEvents;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace FiefApp.Module.Buildings
{
    public class BuildingsViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IBuildingsService _buildingsService;
        private readonly IEventAggregator _eventAggregator;

        public BuildingsViewModel(
            IBaseService baseService,
            IBuildingsService buildingsService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            _baseService = baseService;
            _buildingsService = buildingsService;
            _eventAggregator = eventAggregator;

            TabName = "Byggnadsverk";

            AddBuildingUIEventHandler = new CustomDelegateCommand(ExecuteAddBuildingUIEventHandler, o => true);
            BuilderUIEventHandler = new CustomDelegateCommand(ExecuteBuilderUIEventHandler, o => true);
            BuildingBuildingUIEventHandler = new CustomDelegateCommand(ExecuteBuildingBuildingUIEventHandler, o => true);
            AddBuilderCommand = new DelegateCommand(ExecuteAddBuilderCommand);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
        }

        #region CustomDelegateCommand : AddBuildingUIEvent

        public CustomDelegateCommand AddBuildingUIEventHandler { get; set; }
        private void ExecuteAddBuildingUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is AddBuildingUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Save")
            {
                List<int> tempList = DataModel.BuildsCollection.Select(t => t.Id).ToList();

                e.Model.Id = tempList.Count > 0 ? tempList.Max() + 1 : 0;
                e.Model.BuildersCollection = DataModel.BuildersCollection;

                DataModel.BuildsCollection.Add(e.Model);
                SaveData();
            }
        }

        #endregion
        #region CustomDelegateCommand : BuilderUIEvent

        public CustomDelegateCommand BuilderUIEventHandler { get; set; }
        private void ExecuteBuilderUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is BuilderUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Delete":
                    {
                        for (int x = 0; x < DataModel.BuildersCollection.Count; x++)
                        {
                            if (DataModel.BuildersCollection[x].Id == e.Model.Id)
                            {
                                DataModel.BuildersCollection.RemoveAt(x);
                                break;
                            }
                        }
                        List<BuilderModel> tempList = new List<BuilderModel>(DataModel.BuildersCollection);
                        DataModel.BuildersCollection.Clear();
                        DataModel.BuildersCollection = new ObservableCollection<BuilderModel>(tempList);
                        break;
                    }

                case "Save":
                    for (int x = 0; x < DataModel.BuildersCollection.Count; x++)
                    {
                        if (DataModel.BuildersCollection[x].Id == e.Model.Id)
                        {
                            DataModel.BuildersCollection[x].PersonName = e.Model.PersonName;
                            DataModel.BuildersCollection[x].Age = e.Model.Age;
                            DataModel.BuildersCollection[x].Skill = e.Model.Skill;
                            DataModel.BuildersCollection[x].Resources = e.Model.Resources;
                            DataModel.BuildersCollection[x].Loyalty = e.Model.Loyalty;
                            break;
                        }
                    }
                    break;
            }
        }

        #endregion
        #region CustomDelegateCommand : BuildingBuildingUIEventHandler

        public CustomDelegateCommand BuildingBuildingUIEventHandler { get; set; }
        private void ExecuteBuildingBuildingUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is BuildingBuildingUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Expanded":
                    for (int x = 0; x < DataModel.BuildsCollection.Count; x++)
                    {
                        if (e.Id != DataModel.BuildsCollection[x].Id)
                        {
                            DataModel.BuildsCollection[x].IsExpanded = false;
                        }
                    }
                    break;

                case "Updated":
                    for (int x = 0; x < DataModel.BuildsCollection.Count; x++)
                    {
                        if (e.Id == DataModel.BuildsCollection[x].Id)
                        {
                            DataModel.BuildsCollection[x].SmithsworkThisYear = e.Model.SmithsworkThisYear;
                            DataModel.BuildsCollection[x].IronThisYear = e.Model.IronThisYear;
                            DataModel.BuildsCollection[x].WoodworkThisYear = e.Model.WoodworkThisYear;
                            DataModel.BuildsCollection[x].WoodThisYear = e.Model.WoodThisYear;
                            DataModel.BuildsCollection[x].StoneworkThisYear = e.Model.StoneworkThisYear;
                            DataModel.BuildsCollection[x].StoneThisYear = e.Model.StoneThisYear;
                            DataModel.BuildsCollection[x].BuilderId = e.Model.BuilderId;
                            DataModel.BuildsCollection[x].BuildingTime = e.Model.BuildingTime;
                        }
                        else
                        {
                            if (e.Model.BuilderId == DataModel.BuildsCollection[x].BuilderId)
                            {
                                DataModel.BuildsCollection[x].BuilderId = -1;
                            }
                        }
                    }

                    for (int x = 0; x < DataModel.BuildersCollection.Count; x++)
                    {
                        if (DataModel.BuildersCollection[x].Id == e.Model.BuilderId)
                        {
                            DataModel.BuildersCollection[x].BuildingId = e.Id;
                        }
                        else
                        {
                            if (DataModel.BuildersCollection[x].BuildingId == e.Id)
                            {
                                DataModel.BuildersCollection[x].BuildingId = -1;
                            }
                        }
                    }

                    for (int x = 0; x < DataModel.BuildsCollection.Count; x++)
                    {
                        DataModel.BuildsCollection[x].BuildersCollection = DataModel.BuildersCollection;
                    }

                    List<BuildingModel> tempList = new List<BuildingModel>(DataModel.BuildsCollection);
                    DataModel.BuildsCollection.Clear();
                    DataModel.BuildsCollection = new ObservableCollection<BuildingModel>(tempList);

                    DataModel.SetDaysWorkLeft();
                    break;
            }
        }

        #endregion

        #region DelegateCommand : AddBuilderCommand

        public DelegateCommand AddBuilderCommand { get; set; }
        private void ExecuteAddBuilderCommand()
        {
            int id = _buildingsService.GetNewIdForBuilder();
            DataModel.BuildersCollection.Add(new BuilderModel()
            {
                Id = id,
                PersonName = _baseService.GetCommonerName(),
                Age = _baseService.RollDie(14, 60)
            });
            SaveData();
        }

        #endregion

        #region View Data Model Properties

        private BuildingsDataModel _dataModel;
        public BuildingsDataModel DataModel
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
            _buildingsService.SetAllBuildsCollectionIsAll(Index);
            DataModel = Index
                        == 0 ? _buildingsService.GetAllBuildingsDataModel()
                : _baseService.GetDataModel<BuildingsDataModel>(Index);

            if (Index == 0)
            {
                DataModel.IsAll = true;
                SetBuildersInBuildsCollection();
            }
            else
            {
                DataModel.IsAll = false;
                GetInformationSetDataModel();
            }
            DataModel.BuildersCollection.CollectionChanged += UpdateBuildersCollectionInBuildingsCollection;

            UpdateFiefCollection();
        }

        private void UpdateBuildersCollectionInBuildingsCollection(
            object sender,
            NotifyCollectionChangedEventArgs e)
        {
            for (int x = 0; x < DataModel.BuildingsCollection.Count; x++)
            {
                DataModel.BuildingsCollection[x].BuildersCollection = DataModel.BuildersCollection;
            }
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            LoadData();
        }

        private void GetInformationSetDataModel()
        {
            DataModel.AvailableBuildings = new ObservableCollection<BuildingModel>(_buildingsService.GetAvailableBuildings());
            SetBuildersInBuildsCollection();
        }

        public void SetBuildersInBuildsCollection()
        {
            for (int x = 0; x < DataModel.BuildsCollection.Count; x++)
            {
                DataModel.BuildsCollection[x].BuildersCollection = DataModel.BuildersCollection;
            }
        }
    }
}
