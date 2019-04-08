using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Services;

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
        }

        #region DataModel

        private MinesDataModel _dataModel = new MinesDataModel();
        public MinesDataModel DataModel
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
                        == 0 ? _minesService.GetAllMinesDataModel()
                : _baseService.GetDataModel<MinesDataModel>(Index);

            UpdateFiefCollection();
        }
    }
}
