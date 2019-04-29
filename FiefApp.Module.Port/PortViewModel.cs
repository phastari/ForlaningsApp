using System;
using System.Windows;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Port.RoutedEvents;
using Prism.Commands;
using Prism.Events;

namespace FiefApp.Module.Port
{
    public class PortViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IPortService _portService;
        private readonly ISettingsService _settingsService;
        private readonly IEventAggregator _eventAggregator;

        public PortViewModel(
            IBaseService baseService,
            IPortService portService,
            ISettingsService settingsService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            TabName = "Hamn";

            _baseService = baseService;
            _portService = portService;
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;

            ConstructShipyardCommand = new DelegateCommand(ExecuteConstructShipyardCommand);

            // Test Delegate Commands
            SetGotShipyard = new DelegateCommand(ExecuteSetGotShipyard);
            SetBuildingShipyard = new DelegateCommand(ExecuteSetBuildingShipyard);
            SetUpgradingShipyard = new DelegateCommand(ExecuteSetUpgradingShipyard);
            SetCanBuildShipyard = new DelegateCommand(ExecuteSetCanBuildShipyard);

            AddCaptain = new DelegateCommand(ExecuteAddCaptain);
            CaptainUIEventHandler = new CustomDelegateCommand(ExecuteCaptainUIEventHandler, o => true);
            BoatUIEventHandler = new CustomDelegateCommand(ExecuteBoatUIEventHandler, o => true);
            CrewBoatUIEventHandler = new CustomDelegateCommand(ExecuteCrewBoatUIEventHandler, o => true);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
        }

        #region Test DelegateCommands

        public DelegateCommand SetGotShipyard { get; set; }
        private void ExecuteSetGotShipyard()
        {
            DataModel.UpgradingShipyard = false;
            DataModel.BuildingShipyard = false;
            DataModel.CanBuildShipyard = false;
            DataModel.GotShipyard = true;
        }

        public DelegateCommand SetBuildingShipyard { get; set; }
        private void ExecuteSetBuildingShipyard()
        {
            DataModel.UpgradingShipyard = false;
            DataModel.BuildingShipyard = true;
            DataModel.CanBuildShipyard = false;
            DataModel.GotShipyard = false;
        }

        public DelegateCommand SetUpgradingShipyard { get; set; }
        private void ExecuteSetUpgradingShipyard()
        {
            DataModel.UpgradingShipyard = true;
            DataModel.BuildingShipyard = false;
            DataModel.CanBuildShipyard = false;
            DataModel.GotShipyard = false;
        }

        public DelegateCommand SetCanBuildShipyard { get; set; }
        private void ExecuteSetCanBuildShipyard()
        {
            DataModel.UpgradingShipyard = false;
            DataModel.BuildingShipyard = false;
            DataModel.CanBuildShipyard = true;
            DataModel.GotShipyard = false;
        }

        #endregion

        #region DelegateCommand : ConstructShipyardCommand

        public DelegateCommand ConstructShipyardCommand { get; set; }
        private void ExecuteConstructShipyardCommand()
        {
            // TA BETALT FÖR BYGGET!

            DataModel.BuildingShipyard = true;
            DataModel.CanBuildShipyard = false;
            DataModel.Shipyard = new ShipyardModel()
            {
                Shipyard = "",
                Size = _settingsService.ShipyardTypeSettingsList[0].DockType,
                UN = "1",
                OperationBaseCost = _settingsService.ShipyardTypeSettingsList[0].OperationBaseCostModifier,
                OperationBaseIncome = _settingsService.ShipyardTypeSettingsList[0].OperationBaseIncomeModifier,
                IsBeingUpgraded = false,
                DockSmall = _settingsService.ShipyardTypeSettingsList[0].DockSmall,
                DockSmallFree = _settingsService.ShipyardTypeSettingsList[0].DockSmall,
                DockMedium = _settingsService.ShipyardTypeSettingsList[0].DockMedium,
                DockMediumFree = _settingsService.ShipyardTypeSettingsList[0].DockMedium,
                DockLarge = _settingsService.ShipyardTypeSettingsList[0].DockLarge,
                DockLargeFree = _settingsService.ShipyardTypeSettingsList[0].DockLarge,
                Taxes = "20"
            };

            SaveData();
        }

        #endregion
        #region DelegateCommand : AddCaptain

        public DelegateCommand AddCaptain { get; set; }
        private void ExecuteAddCaptain()
        {
            DataModel.CaptainsCollection.Add(new CaptainModel()
            {
                Id = _portService.GetNewCaptainId(Index)
            });
        }

        #endregion
        #region CustomDelegateCommand : CaptainUIEventHandler

        public CustomDelegateCommand CaptainUIEventHandler { get; set; }
        private void ExecuteCaptainUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is CaptainUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Delete")
            {
                for (int x = 0; x < DataModel.CaptainsCollection.Count; x++)
                {
                    if (e.Id == DataModel.CaptainsCollection[x].Id)
                    {
                        DataModel.CaptainsCollection.RemoveAt(x);
                        break;
                    }
                }
            }

            if (e.Action == "Save")
            {
                for (int x = 0; x < DataModel.CaptainsCollection.Count; x++)
                {
                    if (e.Id == DataModel.CaptainsCollection[x].Id)
                    {
                        DataModel.CaptainsCollection[x].PersonName = e.Model.PersonName;
                        DataModel.CaptainsCollection[x].Age = e.Model.Age;
                        DataModel.CaptainsCollection[x].Skill = e.Model.Skill;
                        DataModel.CaptainsCollection[x].Resources = e.Model.Resources;
                        DataModel.CaptainsCollection[x].Loyalty = e.Model.Loyalty;
                        break;
                    }
                }
            }

            if (e.Action == "Change")
            {
                for (int x = 0; x < DataModel.CaptainsCollection.Count; x++)
                {
                    if (DataModel.CaptainsCollection[x].CaptainOfId == e.BoatId)
                    {
                        if (DataModel.CaptainsCollection[x].Id != e.Id)
                        {
                            DataModel.CaptainsCollection[x].CaptainOfId = -1;
                            DataModel.CaptainsCollection[x].CaptainOf = "";
                        }
                        else
                        {
                            DataModel.CaptainsCollection[x].CaptainOfId = e.BoatId;
                            DataModel.CaptainsCollection[x].CaptainOf = e.BoatName;
                        }
                    }
                }
            }
        }

        #endregion
        #region CustomDelegateCommand : CrewBoatUIEventHandler

        public CustomDelegateCommand CrewBoatUIEventHandler { get; set; }
        private void ExecuteCrewBoatUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is CrewBoatUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Cancel":
                    CrewBoatVisibility = Visibility.Collapsed;
                    break;

                case "Save":
                    CrewBoatVisibility = Visibility.Collapsed;

                    for (int x = 0; x < DataModel.BoatsCollection.Count; x++)
                    {
                        if (e.Model.Id == DataModel.BoatsCollection[x].Id)
                        {
                            DataModel.BoatsCollection[x].CrewNeeded = e.Model.CrewNeeded;
                            DataModel.BoatsCollection[x].RowersNeeded = e.Model.RowersNeeded;
                            DataModel.BoatsCollection[x].CrewedSailors = e.Model.CrewedSailors;
                            DataModel.BoatsCollection[x].CrewedMariners = e.Model.CrewedMariners;
                            DataModel.BoatsCollection[x].CrewedRowers = e.Model.CrewedRowers;
                            DataModel.BoatsCollection[x].CrewedSeamens = e.Model.CrewedSeamens;
                            DataModel.BoatsCollection[x].AmountOfficers = e.Model.AmountOfficers;
                            DataModel.BoatsCollection[x].AmountNavigators = e.Model.AmountNavigators;
                            DataModel.BoatsCollection[x].AmountGuards = e.Model.AmountGuards;
                            DataModel.BoatsCollection[x].Seaworthiness = e.Model.Seaworthiness;
                            DataModel.BoatsCollection[x].Defense = e.Model.Defense;
                            DataModel.BoatsCollection[x].CostSilver = e.Model.CostSilver;
                        }
                    }
                    break;
            }
        }

        #endregion
        #region CustomDelegateCommand : BoatUIEventHandler

        public CustomDelegateCommand BoatUIEventHandler { get; set; }
        private void ExecuteBoatUIEventHandler(object obj)

        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is BoatUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Crew":
                {
                    int id = -1;

                    for (int x = 0; x < DataModel.BoatsCollection.Count; x++)
                    {
                        if (DataModel.BoatsCollection[x].Id == e.Id)
                        {
                            id = x;
                            break;
                        }
                    }

                    if (id != -1)
                    {
                        DataModel.CrewBoat = DataModel.BoatsCollection[id];
                        DataModel.CrewBoat.AmountSailors = DataModel.Sailors;
                        DataModel.CrewBoat.AmountSeamens = DataModel.Seaman;
                        DataModel.CrewBoat.AmountRowers = DataModel.Rowers;
                        DataModel.CrewBoat.AmountMariners = DataModel.Mariner;

                        CrewBoatVisibility = Visibility.Visible;
                    }
                    break;
                }

                case "Changed":
                {
                    break;
                }
            }
        }

        #endregion

        #region DataModel

        private PortDataModel _dataModel = new PortDataModel();
        public PortDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        #region UI Properties

        private Visibility _crewBoatVisibility = Visibility.Collapsed;
        public Visibility CrewBoatVisibility
        {
            get => _crewBoatVisibility;
            set => SetProperty(ref _crewBoatVisibility, value);
        }

        #endregion

        protected override void SaveData(int index = -1)
        {
            _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
        }

        protected override void LoadData()
        {
            DataModel = Index
                        == 0 ? _portService.GetAllPortDataModel()
                : _baseService.GetDataModel<PortDataModel>(Index);

            if (!DataModel.BuildingShipyard && !DataModel.GotShipyard)
            {
                DataModel.CanBuildShipyard = _portService.CheckShipyardPossibility(Index);
            }

            UpdateFiefCollection();
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            LoadData();
        }
    }
}
