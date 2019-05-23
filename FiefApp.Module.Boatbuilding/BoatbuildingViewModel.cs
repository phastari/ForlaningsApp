using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Boatbuilding.RoutedEvents;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using Prism.Events;

namespace FiefApp.Module.Boatbuilding
{
    public class BoatbuildingViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IBoatbuildingService _boatbuildingService;
        private readonly ISettingsService _settingsService;
        private readonly IEventAggregator _eventAggregator;

        public BoatbuildingViewModel(
            IBaseService baseService,
            IBoatbuildingService boatbuildingService,
            ISettingsService settingsService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            _baseService = baseService;
            _boatbuildingService = boatbuildingService;
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;

            TabName = "Skeppsbygge";

            BuildingBoatUIEventHandler = new CustomDelegateCommand(ExecuteBuildingBoatUIEventHandler, o => true);
            BoatBuilderUIEventHandler = new CustomDelegateCommand(ExecuteBoatBuilderUIEventHandler, o => true);
            ConstructingBoatUIEventHandler = new CustomDelegateCommand(ExecuteConstructingBoatUIEventHandler, o => true);

            AddBoatbuilderCommand = new DelegateCommand(ExecuteAddBoatbuilderCommand);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
        }

        #region CustomDelegateCommand : BuildingBoatUIEventHandler

        public CustomDelegateCommand BuildingBoatUIEventHandler { get; set; }
        private void ExecuteBuildingBoatUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is BuildingBoatUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Save")
            {
                if (e.BoatModel.AddAsBuilt)
                {
                    if (e.BoatModel.BoatType == "Fiskebåt")
                    {
                        _boatbuildingService.AddFishingBoat(Index, e.BoatModel.Amount);
                    }
                    else
                    {
                        e.BoatModel.Id = _boatbuildingService.GetNewBuildingBoatId(Index);
                        _boatbuildingService.AddBoatToCompletedBoats(Index, e.BoatModel);
                    }
                }
                else
                {
                    e.BoatModel.Id = _boatbuildingService.GetNewBuildingBoatId(Index);
                    e.BoatModel.BoatBuildersCollection = DataModel.BoatBuildersCollection;
                    DataModel.BoatsBuildingCollection.Add(e.BoatModel);
                }
                SaveData();
            }
        }

        #endregion
        #region CustomDelegateCommand : BoatBuilderUIEventHandler

        public CustomDelegateCommand BoatBuilderUIEventHandler { get; set; }
        private void ExecuteBoatBuilderUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is BoatBuilderUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Delete")
            {
                for (int x = 0; x < DataModel.BoatBuildersCollection.Count; x++)
                {
                    if (e.BoatbuilderModel.Id == DataModel.BoatBuildersCollection[x].Id)
                    {
                        DataModel.BoatBuildersCollection.RemoveAt(x);
                        break;
                    }
                }
            }

            if (e.Action == "Save")
            {
                for (int x = 0; x < DataModel.BoatBuildersCollection.Count; x++)
                {
                    if (e.BoatbuilderModel.Id == DataModel.BoatBuildersCollection[x].Id)
                    {
                        DataModel.BoatBuildersCollection[x].PersonName = e.BoatbuilderModel.PersonName;
                        DataModel.BoatBuildersCollection[x].Loyalty = e.BoatbuilderModel.Loyalty;
                        DataModel.BoatBuildersCollection[x].Skill = e.BoatbuilderModel.Skill;
                        DataModel.BoatBuildersCollection[x].Resources = e.BoatbuilderModel.Resources;
                        DataModel.BoatBuildersCollection[x].Age = e.BoatbuilderModel.Age;
                    }
                }

                for (int x = 0; x < DataModel.BoatsBuildingCollection.Count; x++)
                {
                    DataModel.BoatsBuildingCollection[x].BoatBuildersCollection = DataModel.BoatBuildersCollection;
                }

                SaveData();
            }
        }

        #endregion
        #region CustomDelegateCommand : ConstructingBoatUIEventHandler

        public CustomDelegateCommand ConstructingBoatUIEventHandler { get; set; }
        private void ExecuteConstructingBoatUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is ConstructingBoatUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Update")
            {
                int index = -1;

                for (int x = 0; x < DataModel.BoatsBuildingCollection.Count; x++)
                {
                    if (DataModel.BoatsBuildingCollection[x].Id == e.ConstructingBoatId)
                    {
                        DataModel.BoatsBuildingCollection[x].BoatbuilderId = e.BoatbuilderId;
                        index = x;
                    }
                    else if (DataModel.BoatsBuildingCollection[x].BoatbuilderId == e.BoatbuilderId)
                    {
                        DataModel.BoatsBuildingCollection[x].BoatbuilderId = -1;
                        DataModel.BoatsBuildingCollection[x].SelectedIndex = -1;
                    }
                }

                for (int x = 0; x < DataModel.BoatBuildersCollection.Count; x++)
                {
                    if (DataModel.BoatBuildersCollection[x].Id == e.BoatbuilderId)
                    {
                        DataModel.BoatBuildersCollection[x].BuildingBoatId = e.ConstructingBoatId;
                        DataModel.BoatBuildersCollection[x].Assignment = DataModel.BoatsBuildingCollection[index].BoatType;
                    }
                    else
                    {
                        if (DataModel.BoatBuildersCollection[x].BuildingBoatId == e.ConstructingBoatId)
                        {
                            DataModel.BoatBuildersCollection[x].BuildingBoatId = -1;
                            DataModel.BoatBuildersCollection[x].Assignment = "";
                        }
                    }
                }
            }

            if (e.Action == "Delete")
            {
                for (int x = 0; x < DataModel.BoatsBuildingCollection.Count; x++)
                {
                    if (DataModel.BoatsBuildingCollection[x].Id == e.ConstructingBoatId)
                    {
                        DataModel.BoatsBuildingCollection.RemoveAt(x);
                        break;
                    }
                }

                for (int x = 0; x < DataModel.BoatBuildersCollection.Count; x++)
                {
                    if (DataModel.BoatBuildersCollection[x].BuildingBoatId == e.ConstructingBoatId)
                    {
                        DataModel.BoatBuildersCollection[x].BuildingBoatId = -1;
                        DataModel.BoatBuildersCollection[x].Assignment = "";
                    }
                }
            }

            SaveData();
        }

        #endregion
        
        #region DelegateCommand : AddBoatbuilderCommand

        public DelegateCommand AddBoatbuilderCommand { get; set; }
        private void ExecuteAddBoatbuilderCommand()
        {
            SaveData();

            DataModel.BoatBuildersCollection.Add(
                new BoatbuilderModel()
                {
                    Id = _boatbuildingService.GetNewBoatbuilderId(),
                    PersonName = _baseService.GetCommonerName(),
                    Age = _baseService.RollDie(14, 60)
                });
        }

        #endregion

        #region DataModel

        private BoatbuildingDataModel _dataModel;
        public BoatbuildingDataModel DataModel
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
                        == 0 ? _boatbuildingService.GetAllBoatbuildingDataModel()
                : _baseService.GetDataModel<BoatbuildingDataModel>(Index);

            DataModel.BoatTypeCollection = new ObservableCollection<BoatModel>(_settingsService.BoatbuildingSettingsModel.BoatSettingsList);
            SetDataModelInformation();

            UpdateFiefCollection();
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            LoadData();
        }

        private void SetDataModelInformation()
        {
            DataModel.VillageBoatBuilders = _boatbuildingService.GetNrVillageBoatbuilders(Index);
            DataModel.DocksVillage = _boatbuildingService.GetNrVillageBoatbuilders(Index);
            DataModel.GotShipyard = _boatbuildingService.GetGotShipyard(Index);
            DataModel.UpgradingShipyard = _boatbuildingService.GetUpgradingShipyard(Index);
            DataModel.BoatBuildersCollection.CollectionChanged += UpdateTotalBoatBuilders;
            DataModel.VillageBoatBuilders = _boatbuildingService.GetVillageBoatBuilders(Index);

            if (DataModel.VillageBoatBuilders > 0)
            {
                DataModel.GotVillageBoatbuilders = true;
            }
        }

        private void UpdateTotalBoatBuilders(object sender, NotifyCollectionChangedEventArgs e)
        {
            DataModel.UpdateTotalBoatBuilders();
        }
    }
}
