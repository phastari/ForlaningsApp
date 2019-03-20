using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Services;

namespace FiefApp.Module.Income
{
    public class IncomeViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IIncomeService _incomeService;

        public IncomeViewModel(
            IBaseService baseService, 
            IIncomeService incomeService
            ) : base(baseService)
        {
            _baseService = baseService;
            _incomeService = incomeService;

            TabName = "Inkomst";
        }

        #region DataModel

        private IncomeDataModel _dataModel;
        public IncomeDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        protected override void LoadData()
        {
            DataModel = _baseService.GetDataModel<IncomeDataModel>(Index);
            DataModel.IncomesCollection = _incomeService.GetAllIncomes(Index);

            UpdateFiefCollection();
        }

        protected override void SaveData(int index = -1)
        {
            _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
        }
    }
}
