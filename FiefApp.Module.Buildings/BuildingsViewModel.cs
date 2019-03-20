using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Services;

namespace FiefApp.Module.Buildings
{
    public class BuildingsViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IBuildingsService _buildingsService;

        public BuildingsViewModel(
            IBaseService baseService,
            IBuildingsService buildingsService
            ) : base(baseService)
        {
            _baseService = baseService;
            _buildingsService = buildingsService;

            TabName = "Byggnadsverk";
        }

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
    }
}
