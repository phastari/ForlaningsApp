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

                e.Model.Id = tempList.Count > 0 ? tempList.Max() : 0;
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

        #region DelegateCommand : AddBuilderCommand

        public DelegateCommand AddBuilderCommand { get; set; }
        private void ExecuteAddBuilderCommand()
        {
            int id = _buildingsService.GetNewIdForBuilder();
            DataModel.BuildersCollection.Add(new BuilderModel()
            {
                Id = id
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
            DataModel = Index
                        == 0 ? _buildingsService.GetAllBuildingsDataModel()
                : _baseService.GetDataModel<BuildingsDataModel>(Index);

            GetInformationSetDataModel();

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

            for (int x = 0; x < DataModel.BuildingsCollection.Count; x++)
            {
                DataModel.BuildingsCollection[x].BuildersCollection = DataModel.BuildersCollection;
            }
        }
    }
}
