using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Subsidiary.RoutedEvents;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using Prism.Events;

namespace FiefApp.Module.Subsidiary
{
    public class SubsidiaryViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly ISubsidiaryService _subsidiaryService;
        private readonly IEventAggregator _eventAggregator;

        public SubsidiaryViewModel(
            IBaseService baseService,
            ISubsidiaryService subsidiaryService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            _baseService = baseService;
            _subsidiaryService = subsidiaryService;
            _eventAggregator = eventAggregator;

            TabName = "Binäringar";

            ConstructSubsidiaryCommand = new CustomDelegateCommand(ExecuteConstructSubsidiaryCommand, o => true);
            AddSubsidiaryUIEventHandler = new CustomDelegateCommand(ExecuteAddSubsidiaryUIEventHandler, o => true);
            ConstructingUIEventHandler = new CustomDelegateCommand(ExecuteConstructingUIEventHandler, o => true);
            SubsidiaryUIEventHandler = new CustomDelegateCommand(ExecuteSubsidiaryUIEventHandler, o => true);
            AddSubsidiaryCommand = new DelegateCommand(ExecuteAddSubsidiaryCommand);
            EditSubsidiaryUIEventHandler = new CustomDelegateCommand(ExecuteEditSubsidiaryUIEventHandler, o => true);

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
        }

        #region CustomDelegateCommand : ConstructSubsidiaryCommand

        public CustomDelegateCommand ConstructSubsidiaryCommand { get; set; }
        private void ExecuteConstructSubsidiaryCommand(object obj)
        {
            DataModel.SubsidiaryTypesCollection = _subsidiaryService.GetSubsidiaryTypesCollection();
        }

        #endregion

        #region CustomDelegateCommand : AddSubsidiaryUIEventHandler

        public CustomDelegateCommand AddSubsidiaryUIEventHandler { get; set; }
        private void ExecuteAddSubsidiaryUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is AddSubsidiaryUIEventArgs e))
            {
                return;
            }

            e.Handled = true;


            if (DataModel.AddSubsidiaryTag == "ConstructSubsidiary")
            {
                if (e.Action == "Save")
                {
                    e.SubsidiaryModel.Id = _subsidiaryService.GetNewIdForSubsidiary();
                    _subsidiaryService.AddCustomSubsidiary(e.SubsidiaryModel);
                    e.SubsidiaryModel.StewardsCollection = DataModel.StewardsCollection;
                    e.SubsidiaryModel.StewardId = -1;
                    e.SubsidiaryModel.Steward = "";
                    DataModel.ConstructingCollection.Add(e.SubsidiaryModel);

                    DataModel.AddSubsidiaryTag = "";
                }
            }
            else if (DataModel.AddSubsidiaryTag == "AddSubsidiary")
            {
                if (e.Action == "Save")
                {
                    e.SubsidiaryModel.Id = _subsidiaryService.GetNewIdForSubsidiary();
                    _subsidiaryService.AddCustomSubsidiary(e.SubsidiaryModel);
                    e.SubsidiaryModel.StewardsCollection = DataModel.StewardsCollection;
                    e.SubsidiaryModel.StewardId = -1;
                    e.SubsidiaryModel.Steward = "";
                    DataModel.SubsidiaryCollection.Add(e.SubsidiaryModel);

                    DataModel.AddSubsidiaryTag = "";
                }
            }
        }

        #endregion

        #region CustomDelegateCommand : ConstructingUIEventHandler

        public CustomDelegateCommand ConstructingUIEventHandler { get; set; }
        private void ExecuteConstructingUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is ConstructingUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Changed")
            {
                ChangeStewardInConstructingCollection(e.SubsidiaryId, e.StewardId, e.Steward);
                _subsidiaryService.ChangeSteward(e.StewardId, e.SubsidiaryId, e.Subsidiary);

                List<SubsidiaryModel> tempConstructingList = new List<SubsidiaryModel>(DataModel.ConstructingCollection);
                List<SubsidiaryModel> tempSubsidiaryList = new List<SubsidiaryModel>(DataModel.SubsidiaryCollection);

                DataModel.ConstructingCollection.Clear();
                DataModel.ConstructingCollection = new ObservableCollection<SubsidiaryModel>(tempConstructingList);

                DataModel.SubsidiaryCollection.Clear();
                DataModel.SubsidiaryCollection = new ObservableCollection<SubsidiaryModel>(tempSubsidiaryList);
            }

            if (e.Action == "DaysWorkThisYearChanged")
            {
                for (int x = 0; x < DataModel.ConstructingCollection.Count; x++)
                {
                    if (DataModel.ConstructingCollection[x].Id == e.SubsidiaryId)
                    {
                        DataModel.ConstructingCollection[x].DaysWorkThisYear = e.DaysWorkThisYear;
                    }
                }
            }
        }

        #endregion

        #region CustomDelegateCommand : SubsidiaryUIEventHandler

        public CustomDelegateCommand SubsidiaryUIEventHandler { get; set; }
        private void ExecuteSubsidiaryUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is SubsidiaryUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Changed")
            {
                ChangeStewardInSubsidiaryCollections(e.StewardId, e.Steward, e.SubsidiaryId);
                _subsidiaryService.ChangeSteward(e.StewardId, e.SubsidiaryId, e.Subsidiary);

                List<SubsidiaryModel> tempConstructingList = new List<SubsidiaryModel>(DataModel.ConstructingCollection);
                List<SubsidiaryModel> tempSubsidiaryList = new List<SubsidiaryModel>(DataModel.SubsidiaryCollection);

                DataModel.ConstructingCollection.Clear();
                DataModel.ConstructingCollection = new ObservableCollection<SubsidiaryModel>(tempConstructingList);

                DataModel.SubsidiaryCollection.Clear();
                DataModel.SubsidiaryCollection = new ObservableCollection<SubsidiaryModel>(tempSubsidiaryList);
            }
            else if (e.Action == "Delete")
            {
                for (int x = 0; x < DataModel.SubsidiaryCollection.Count; x++)
                {
                    if (e.SubsidiaryId == DataModel.SubsidiaryCollection[x].Id)
                    {
                        DataModel.SubsidiaryCollection.RemoveAt(x);
                    }
                }
            }
            else if (e.Action == "Edit")
            {
                for (int x = 0; x < DataModel.SubsidiaryCollection.Count; x++)
                {
                    if (DataModel.SubsidiaryCollection[x].Id == e.SubsidiaryId)
                    {
                        DataModel.EditModel.IncomeFactor = DataModel.SubsidiaryCollection[x].IncomeFactor;
                        DataModel.EditModel.IncomeBase = DataModel.SubsidiaryCollection[x].IncomeBase;
                        DataModel.EditModel.IncomeLuxury = DataModel.SubsidiaryCollection[x].IncomeLuxury;
                        DataModel.EditModel.IncomeSilver = DataModel.SubsidiaryCollection[x].IncomeSilver;
                        DataModel.EditModel.DaysWorkUpkeep = DataModel.SubsidiaryCollection[x].DaysWorkUpkeep;
                        DataModel.EditModel.Spring = DataModel.SubsidiaryCollection[x].Spring;
                        DataModel.EditModel.Summer = DataModel.SubsidiaryCollection[x].Summer;
                        DataModel.EditModel.Fall = DataModel.SubsidiaryCollection[x].Fall;
                        DataModel.EditModel.Winter = DataModel.SubsidiaryCollection[x].Winter;
                        DataModel.EditModel.Quality = DataModel.SubsidiaryCollection[x].Quality;
                        DataModel.EditModel.DevelopmentLevel = DataModel.SubsidiaryCollection[x].DevelopmentLevel;
                        break;
                    }
                }
            }
        }

        #endregion

        #region DelegateCommand : AddSubsidiaryCommand

        public DelegateCommand AddSubsidiaryCommand { get; set; }
        private void ExecuteAddSubsidiaryCommand()
        {
            DataModel.SubsidiaryTypesCollection = _subsidiaryService.GetSubsidiaryTypesCollection();
        }

        #endregion

        #region CustomDelegateCommand : EditSubsidiaryUIEventHandler

        public CustomDelegateCommand EditSubsidiaryUIEventHandler { get; set; }

        private void ExecuteEditSubsidiaryUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is EditSubsidiaryUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Cancel")
            {
                DataModel.EditModel.Id = -1;
                DataModel.EditModel.IncomeFactor = 0M;
                DataModel.EditModel.IncomeBase = 0M;
                DataModel.EditModel.IncomeLuxury = 0M;
                DataModel.EditModel.IncomeSilver = 0M;
                DataModel.EditModel.DaysWorkUpkeep = 0;
                DataModel.EditModel.Spring = 0M;
                DataModel.EditModel.Summer = 0M;
                DataModel.EditModel.Fall = 0M;
                DataModel.EditModel.Winter = 0M;
                DataModel.EditModel.Quality = 0;
                DataModel.EditModel.DevelopmentLevel = 0;
            }
            else if (e.Action == "Save")
            {
                _subsidiaryService.SetSubsidiary(Index, e.SubsidiaryModel.Id, e.SubsidiaryModel);

                DataModel.EditModel.Id = -1;
                DataModel.EditModel.IncomeFactor = 0M;
                DataModel.EditModel.IncomeBase = 0M;
                DataModel.EditModel.IncomeLuxury = 0M;
                DataModel.EditModel.IncomeSilver = 0M;
                DataModel.EditModel.DaysWorkUpkeep = 0;
                DataModel.EditModel.Spring = 0M;
                DataModel.EditModel.Summer = 0M;
                DataModel.EditModel.Fall = 0M;
                DataModel.EditModel.Winter = 0M;
                DataModel.EditModel.Quality = 0;
                DataModel.EditModel.DevelopmentLevel = 0;
            }
        }

        #endregion

        #region DataModel

        private SubsidiaryDataModel _dataModel;
        public SubsidiaryDataModel DataModel
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
                        == 0 ? _subsidiaryService.GetAllSubsidiaryDataModel()
                : _baseService.GetDataModel<SubsidiaryDataModel>(Index);

            DataModel.StewardsCollection = _subsidiaryService.GetAllStewards();
            for (int x = 0; x < DataModel.ConstructingCollection.Count; x++)
            {
                DataModel.ConstructingCollection[x].StewardsCollection = DataModel.StewardsCollection;
            }

            for (int x = 0; x < DataModel.SubsidiaryCollection.Count; x++)
            {
                DataModel.SubsidiaryCollection[x].StewardsCollection = DataModel.StewardsCollection;
            }

            UpdateFiefCollection();
        }

        private void ExecuteNewFiefLoadedEvent()
        {
            Index = 1;
            LoadData();
        }

        #endregion

        #region Methods

        private void ChangeStewardInSubsidiaryCollections(int stewardId, string steward, int subsidiaryId)
        {
            for (int x = 0; x < DataModel.ConstructingCollection.Count; x++)
            {
                if (stewardId == DataModel.ConstructingCollection[x].StewardId)
                {
                    DataModel.ConstructingCollection[x].StewardId = -1;
                    DataModel.ConstructingCollection[x].Steward = "";
                }
            }

            for (int x = 0; x < DataModel.SubsidiaryCollection.Count; x++)
            {
                if (stewardId == DataModel.SubsidiaryCollection[x].StewardId)
                {
                    DataModel.SubsidiaryCollection[x].StewardId = -1;
                    DataModel.SubsidiaryCollection[x].Steward = "";
                }
            }

            for (int x = 0; x < DataModel.SubsidiaryCollection.Count; x++)
            {
                if (subsidiaryId == DataModel.SubsidiaryCollection[x].Id)
                {
                    DataModel.SubsidiaryCollection[x].StewardId = stewardId;
                    DataModel.SubsidiaryCollection[x].Steward = steward;
                }
            }
        }

        private void ChangeStewardInConstructingCollection(int subsidiaryId, int stewardId, string steward)
        {
            for (int x = 0; x < DataModel.ConstructingCollection.Count; x++)
            {
                if (stewardId == DataModel.ConstructingCollection[x].StewardId)
                {
                    DataModel.ConstructingCollection[x].StewardId = -1;
                    DataModel.ConstructingCollection[x].Steward = "";
                }
            }

            for (int x = 0; x < DataModel.SubsidiaryCollection.Count; x++)
            {
                if (stewardId == DataModel.SubsidiaryCollection[x].StewardId)
                {
                    DataModel.SubsidiaryCollection[x].StewardId = -1;
                    DataModel.SubsidiaryCollection[x].Steward = "";
                }
            }

            for (int x = 0; x < DataModel.ConstructingCollection.Count; x++)
            {
                if (subsidiaryId == DataModel.ConstructingCollection[x].Id)
                {
                    DataModel.ConstructingCollection[x].StewardId = stewardId;
                    DataModel.ConstructingCollection[x].Steward = steward;
                }
            }
        }

        #endregion
    }
}
