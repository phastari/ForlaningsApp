using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Stewards.RoutedEvents;
using Prism.Commands;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FiefApp.Module.Stewards.UIElements.StewardUI;

namespace FiefApp.Module.Stewards
{
    public class StewardsViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IStewardsService _stewardsService;

        public StewardsViewModel(
            IBaseService baseService,
            IStewardsService stewardsService
            ) : base(baseService)
        {
            _baseService = baseService;
            _stewardsService = stewardsService;

            TabName = "Förvaltare";

            StewardUIEventHandler = new CustomDelegateCommand(ExecuteStewardUIEventHandler, o => true);
            AddStewardCommand = new DelegateCommand(ExecuteAddStewardCommand);
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

            if (e.Action == "Save")
            {
                for (int x = 0; x < DataModel.StewardsCollection.Count; x++)
                {
                    if (e.Id == DataModel.StewardsCollection[x].Id)
                    {
                        DataModel.StewardsCollection[x].Age = e.StewardModel.Age;
                        DataModel.StewardsCollection[x].Bonus = e.StewardModel.Bonus;
                        DataModel.StewardsCollection[x].Family = e.StewardModel.Family;
                        DataModel.StewardsCollection[x].Loyalty = e.StewardModel.Loyalty;
                        DataModel.StewardsCollection[x].PersonName = e.StewardModel.PersonName;
                        DataModel.StewardsCollection[x].Resources = e.StewardModel.Resources;
                        DataModel.StewardsCollection[x].Skill = e.StewardModel.Skill;
                        DataModel.StewardsCollection[x].Speciality = e.StewardModel.Speciality;

                        if (DataModel.StewardsCollection[x].IndustryId != e.StewardModel.IndustryId)
                        {
                            for (int i = 0; i < DataModel.IndustryCollection.Count; i++)
                            {
                                if (DataModel.IndustryCollection[x].Id == e.StewardModel.IndustryId)
                                {
                                    DataModel.IndustryCollection[x].Steward = "";
                                    DataModel.IndustryCollection[x].StewardId = -1;
                                }
                            }
                        }

                        DataModel.StewardsCollection[x].IndustryId = e.StewardModel.IndustryId;
                    }
                }
            }
            else if (e.Action == "Expanded")
            {
                for (int x = 0; x < DataModel.StewardsCollection.Count; x++)
                {
                    if (e.Id != DataModel.StewardsCollection[x].Id)
                    {
                        DataModel.StewardsCollection[x].TreeViewIsExpanded = false;
                    }
                }
            }
        }

        #endregion

        #region DelegateCommand : AddStewardCommand

        public DelegateCommand AddStewardCommand { get; set; }
        private void ExecuteAddStewardCommand()
        {
            DataModel.StewardsCollection.Add(
                new StewardModel()
                {
                    Id = _stewardsService.GetNextStewardId()
                });
        }

        #endregion

        #region DataModels

        private StewardsDataModel _dataModel = new StewardsDataModel();
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
            DataModel = Index
                        == 0 ? _stewardsService.GetAllStewardsDataModel()
                : _baseService.GetDataModel<StewardsDataModel>(Index);

            UpdateFiefCollection();
        }

        #endregion
    }
}
