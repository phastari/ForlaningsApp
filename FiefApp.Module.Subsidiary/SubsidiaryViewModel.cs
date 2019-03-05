using System;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Subsidiary.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Subsidiary
{
    public class SubsidiaryViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly ISubsidiaryService _subsidiaryService;

        public SubsidiaryViewModel(
            IBaseService baseService,
            ISubsidiaryService subsidiaryService
            ) : base(baseService)
        {
            _baseService = baseService;
            _subsidiaryService = subsidiaryService;

            TabName = "Binäringar";

            ConstructSubsidiaryCommand = new CustomDelegateCommand(ExecuteConstructSubsidiaryCommand, o => true);
            AddSubsidiaryUIEventHandler = new CustomDelegateCommand(ExecuteAddSubsidiaryUIEventHandler, o => true);
            ConstructingUIEventHandler = new CustomDelegateCommand(ExecuteConstructingUIEventHandler, o => true);
            SubsidiaryUIEventHandler = new CustomDelegateCommand(ExecuteSubsidiaryUIEventHandler, o => true);
            AddSubsidiaryCommand = new DelegateCommand(ExecuteAddSubsidiaryCommand);
        }

        #region CustomDelegateCommand : ConstructSubsidiaryCommand

        public CustomDelegateCommand ConstructSubsidiaryCommand { get; set; }
        private void ExecuteConstructSubsidiaryCommand(object obj)
        {
            Console.WriteLine($"ConstructSubsidiaryCommand!");
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

            if (e.Action == "Save")
            {

            }
            else if (e.Action == "Cancel")
            {
                Console.WriteLine($"AddSubsidiaryUIEventHandler!");
            }
        }

        #endregion

        #region CustomDelegateCommand : ConstructingUIEventHandler

        public CustomDelegateCommand ConstructingUIEventHandler { get; set; }
        private void ExecuteConstructingUIEventHandler(object obj)
        {
            Console.WriteLine($"ConstructingUIEventHandler!");
        }

        #endregion

        #region CustomDelegateCommand : SubsidiaryUIEventHandler

        public CustomDelegateCommand SubsidiaryUIEventHandler { get; set; }
        private void ExecuteSubsidiaryUIEventHandler(object obj)
        {

        }

        #endregion

        #region DelegateCommand : AddSubsidiaryCommand

        public DelegateCommand AddSubsidiaryCommand { get; set; }
        private void ExecuteAddSubsidiaryCommand()
        {

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

            CreateFakeData();
            UpdateFiefCollection();
        }

        private void CreateFakeData()
        {
            DataModel.ConstructingCollection.Add(
                new SubsidiaryModel()
                {
                    Id = 0,
                    Subsidiary = "Vingård",
                    DaysWorkBuild = 5000,
                    DaysWorkThisYear = 0
                });

            DataModel.ConstructingCollection.Add(
                new SubsidiaryModel()
                {
                    Id = 1,
                    Subsidiary = "Tegelbruk",
                    DaysWorkBuild = 4000,
                    DaysWorkThisYear = 0
                });

            DataModel.SubsidiaryCollection.Add(
                new SubsidiaryModel()
                {
                    Id = 0,
                    Subsidiary = "Olivlund",
                    DaysWorkUpkeep = 500
                });

            DataModel.SubsidiaryCollection.Add(
                new SubsidiaryModel()
                {
                    Id = 0,
                    Subsidiary = "Fiskdamm",
                    DaysWorkUpkeep = 100
                });

            DataModel.SubsidiaryCollection.Add(
                new SubsidiaryModel()
                {
                    Id = 0,
                    Subsidiary = "Vingård",
                    DaysWorkUpkeep = 1000
                });
        }

        #endregion
    }
}
