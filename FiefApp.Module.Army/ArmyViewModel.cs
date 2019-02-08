using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Services;

namespace FiefApp.Module.Army
{
    public class ArmyViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IArmyService _armyService;

        public ArmyViewModel(
            IBaseService baseService,
            IArmyService armyService
            ) : base(baseService)
        {
            _baseService = baseService;
            _armyService = armyService;

            TabName = "Arme";
        }

        #region View Data Model Properties

        private ArmyDataModel _dataModel;
        public ArmyDataModel DataModel
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
                        == 0 ? _armyService.GetAllArmyDataModel() 
                : _baseService.GetDataModel<ArmyDataModel>(Index);

            UpdateFiefCollection();
        }

        #endregion
    }
}
