using System;
using System.Collections.Generic;
using System.Linq;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Buildings.RoutedEvents;
using Prism.Events;

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
                List<int> tempList = new List<int>();
                for (int x = 0; x < DataModel.BuildsCollection.Count; x++)
                {
                    tempList.Add(DataModel.BuildsCollection[x].Id);
                }
                if (tempList.Count > 0)
                {
                    e.Model.Id = tempList.Max();
                }
                else
                {
                    e.Model.Id = 0;
                }

                DataModel.BuildsCollection.Add(e.Model);
                SaveData();
            }
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

            UpdateFiefCollection();
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            LoadData();
        }
    }
}
