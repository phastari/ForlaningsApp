using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.Services;

namespace FiefApp.Module.Income
{
    public class IncomeViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly ISettingsService _settingsService;

        public IncomeViewModel(
            IBaseService baseService, 
            ISettingsService settingsService
            ) : base(baseService)
        {
            _baseService = baseService;
            _settingsService = settingsService;

            TabName = "Inkomst";
        }

        protected override void LoadData()
        {
            
        }

        protected override void SaveData(int index = -1)
        {
            
        }
    }
}
