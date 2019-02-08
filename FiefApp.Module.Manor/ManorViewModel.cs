using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Services;
using Prism.Events;

namespace FiefApp.Module.Manor
{
    public class ManorViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IManorService _manorService;

        public ManorViewModel(
            IBaseService baseService,
            IManorService manorService
            ) : base(baseService)
        {
            _baseService = baseService;
            _manorService = manorService;

            TabName = "Gods/Byar";

            ResidentUIEventHandler = new CustomDelegateCommand(ExecuteResidentUIEventHandler, o => true);
        }

        #region CustomDelegateCommand : ResidentUIEventHandler

        public CustomDelegateCommand ResidentUIEventHandler { get; set; }
        private void ExecuteResidentUIEventHandler(object obj)
        {

        }

        #endregion

        #region View Data Model Properties

        private ManorDataModel _dataModel;
        public ManorDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        #region Methods 

        protected override void SaveData(int index = -1)
        {
            _manorService.SetManorDataModel(DataModel);
        }

        protected override void LoadData()
        {
            if (Index == 0)
            {
                //DataModel = _manorService.GetAllManorDataModel();
            }
            else
            {
                DataModel = _baseService.GetDataModel<ManorDataModel>(Index);
            }
            UpdateFiefCollection();
        }

        #endregion
    }
}
