using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Stewards.RoutedEvents;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FiefApp.Module.Stewards
{
    public class StewardsViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IStewardsService _stewardsService;

        public StewardsViewModel(
            IBaseService baseService,
            IStewardsService stewardsService,
            IEventAggregator eventAggregator
            ) : base(baseService)
        {
            _baseService = baseService;
            _stewardsService = stewardsService;

            TabName = "Förvaltare";

            StewardUIEventHandler = new CustomDelegateCommand(ExecuteStewardUIEventHandler, o => true);
            IndustryUIEventHandler = new CustomDelegateCommand(ExecuteIndustryUIEventHandler, o => true);
            AddStewardCommand = new DelegateCommand(ExecuteAddStewardCommand);

            eventAggregator.GetEvent<NewFiefLoadedEvent>().Subscribe(ExecuteNewFiefLoadedEvent);
        }

        #region CustomDelegateCommand : StewardUIEventHandler

        public CustomDelegateCommand StewardUIEventHandler { get; set; }
        private void ExecuteStewardUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is StewardUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Save":
                    {
                        for (int x = 0; x < DataModel.StewardsCollection.Count; x++)
                        {
                            if (e.Id == DataModel.StewardsCollection[x].Id)
                            {
                                DataModel.StewardsCollection[x].Age = e.StewardModel.Age;
                                DataModel.StewardsCollection[x].Loyalty = e.StewardModel.Loyalty;
                                DataModel.StewardsCollection[x].PersonName = e.StewardModel.PersonName;
                                DataModel.StewardsCollection[x].Resources = e.StewardModel.Resources;
                                DataModel.StewardsCollection[x].Skill = e.StewardModel.Skill;
                            }
                        }
                        SetStewardsAndIndustriesCount();
                        break;
                    }

                case "Delete":
                    {
                        for (int x = 0; x < DataModel.StewardsCollection.Count; x++)
                        {
                            if (e.Id == DataModel.StewardsCollection[x].Id)
                            {
                                DataModel.StewardsCollection.RemoveAt(x);
                                _baseService.RemoveSteward(e.Id);
                                break;
                            }
                        }
                        SetStewardsAndIndustriesCount();
                        break;
                    }

                case "Change":
                    {
                        SaveData();
                        _baseService.ChangeSteward(e.Id, e.StewardModel.IndustryId, e.StewardModel.IndustryType);

                        for (int x = 0; x < DataModel.StewardsCollection.Count; x++)
                        {
                            if (e.Id == DataModel.StewardsCollection[x].Id)
                            {
                                DataModel.StewardsCollection[x].IndustryId = e.StewardModel.IndustryId;
                                DataModel.StewardsCollection[x].Industry = e.StewardModel.Industry;
                                DataModel.StewardsCollection[x].IndustryType = e.StewardModel.IndustryType;
                                DataModel.StewardsCollection[x].ManorId = e.StewardModel.ManorId;
                            }
                        }

                        List<StewardModel> tempList = new List<StewardModel>(DataModel.StewardsCollection);

                        DataModel.StewardsCollection.Clear();
                        DataModel.IndustriesCollection.Clear();

                        DataModel.IndustriesCollection = new ObservableCollection<StewardIndustryModel>(_stewardsService.GetIndustries());

                        for (int x = 0; x < tempList.Count; x++)
                        {
                            tempList[x].IndustriesCollection = DataModel.IndustriesCollection;
                        }

                        DataModel.StewardsCollection = new ObservableCollection<StewardModel>(tempList);
                        SetStewardsAndIndustriesCount();
                        break;
                    }
            }
        }

        #endregion
        #region CustomDelegateCommand : IndustryUIEventHandler

        public CustomDelegateCommand IndustryUIEventHandler { get; set; }
        private void ExecuteIndustryUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is IndustryUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            switch (e.Action)
            {
                case "Changed":
                    {
                        for (int x = 0; x < DataModel.IndustriesCollection.Count; x++)
                        {
                            if (DataModel.IndustriesCollection[x].IndustryId == e.Id)
                            {
                                DataModel.IndustriesCollection[x].BeingDeveloped = e.BeingDeveloped;
                                break;
                            }
                        }

                        _baseService.SaveStewardsCollection(DataModel.StewardsCollection);
                        _stewardsService.SetBeingDevelopedInIndustries(e.Id, e.BeingDeveloped);
                        GetInformationSetDataModel();

                        break;
                    }
            }
        }

        #endregion
        #region DelegateCommand : AddStewardCommand

        public DelegateCommand AddStewardCommand { get; set; }
        private void ExecuteAddStewardCommand()
        {
            SaveData();
            DataModel.StewardsCollection.Add(
                new StewardModel()
                {
                    Id = _stewardsService.GetNextStewardId(),
                    PersonName = _baseService.GetNobleName(),
                    Age = _baseService.RollDie(14, 60),
                    Skill = "0",
                    Resources = "0",
                    Loyalty = "0",
                    IndustriesCollection = DataModel.IndustriesCollection
                });

            SetStewardsAndIndustriesCount();
            SaveData();
        }

        #endregion

        #region DataModels

        private StewardsDataModel _dataModel;
        public StewardsDataModel DataModel
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
            DataModel = _baseService.GetDataModel<StewardsDataModel>(Index);

            GetInformationSetDataModel();
            UpdateFiefCollection();

            if (DataModel.IndustriesBeingDevelopedCollection.Count == 0)
            {
                int id = _baseService.GetNewIndustryId();

                DataModel.IndustriesBeingDevelopedCollection = _baseService.CreateNewBeingDevelopedIndustries();
                SaveData();
            }
            else if (DataModel.IndustriesBeingDevelopedCollection.Count / 3 != FiefCollection.Count - 1)
            {
                DataModel.IndustriesBeingDevelopedCollection = _baseService.UpdateIndustriesBeingDevelopedCollection(DataModel.IndustriesBeingDevelopedCollection);
                SaveData();
            }
        }

        private void GetInformationSetDataModel()
        {
            GetIndustries();
            GetStewardsCollection();
            SetAllStewardsIndustiresCollections();
            SetStewardsAndIndustriesCount();
        }

        private void GetStewardsCollection()
        {
            DataModel.StewardsCollection = _baseService.GetStewardsCollection();
        }
        private void GetIndustries()
        {
            DataModel.IndustriesCollection = new ObservableCollection<StewardIndustryModel>(_stewardsService.GetIndustries());
            SetStewardsAndIndustriesCount();
        }

        private void SetStewardsAndIndustriesCount()
        {
            DataModel.NumberOfStewards = DataModel.StewardsCollection.Count - 1;
            DataModel.NumberOfIndustires = DataModel.IndustriesCollection.Count - 1 - DataModel.IndustriesBeingDevelopedCollection.Count;
        }

        private void SetAllStewardsIndustiresCollections()
        {
            for (int x = 0; x < DataModel.StewardsCollection.Count; x++)
            {
                DataModel.StewardsCollection[x].IndustriesCollection = DataModel.IndustriesCollection;
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
