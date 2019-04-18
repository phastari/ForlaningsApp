using System;
using System.ComponentModel;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Mines.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Mines
{
    public class MinesViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IMinesService _minesService;

        public MinesViewModel(
            IBaseService baseService,
            IMinesService minesService
            ) : base(baseService)
        {
            TabName = "Stenbrott/Gruvor";

            _baseService = baseService;
            _minesService = minesService;

            AddQuarryCommand = new DelegateCommand(ExecuteAddQuarryCommand);
            ConstructQuarryUIEventHandler = new CustomDelegateCommand(ExecuteConstructQuarryUIEventHandler, o => true);
        }

        #region DelegateCommand : AddQuarryCommand

        public DelegateCommand AddQuarryCommand { get; set; }
        private void ExecuteAddQuarryCommand()
        {

        }

        #endregion

        #region CustomDelegateCommand : ConstructQuarryUIEventHandler

        public CustomDelegateCommand ConstructQuarryUIEventHandler { get; set; }
        private void ExecuteConstructQuarryUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is ConstructQuarryUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Save")
            {
                e.Model.Id = _minesService.GetNewIdForQuarry(Index);

                DataModel.QuarriesCollection.Add(e.Model);
                DataModel.UpdateTotals();

                SaveData();
            }
        }

        #endregion

        #region DataModel

        private MinesDataModel _dataModel = new MinesDataModel();
        public MinesDataModel DataModel
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
                        == 0 ? _minesService.GetAllMinesDataModel()
                : _baseService.GetDataModel<MinesDataModel>(Index);

            UpdateFiefCollection();
        }

        #endregion
    }
}
