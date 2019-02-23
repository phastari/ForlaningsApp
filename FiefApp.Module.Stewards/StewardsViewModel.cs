using System;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Stewards.RoutedEvents;

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
                        DataModel.StewardsCollection[x].Name = e.StewardModel.Name;
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

        }

        protected override void LoadData()
        {
            CreateFakeData();
        }

        private void CreateFakeData()
        {
            DataModel.StewardsCollection.Add(
                new StewardModel()
                {
                    Id = 0,
                    Age = 32,
                    Bonus = 0,
                    Speciality = "",
                    Family = "Vitfjäder",
                    Name = "Kalle",
                    Loyalty = "3T6",
                    Resources = "2T6+3",
                    Skill = "4T6"
                }
            );

            DataModel.StewardsCollection.Add(
                new StewardModel()
                {
                    Id = 1,
                    Age = 54,
                    Bonus = 1,
                    Speciality = "Jordbruk",
                    Family = "Svensson",
                    Name = "Gunilla",
                    Loyalty = "2T6",
                    Resources = "4T6+3",
                    Skill = "3T6"
                }
            );
        }

        #endregion
    }
}
