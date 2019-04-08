using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.Services;

namespace FiefApp.Module.EndOfYear
{
    public class EndOfYearViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;

        public EndOfYearViewModel(
            IBaseService baseService
            ) : base(baseService)
        {
            _baseService = baseService;

            TabName = "Bokslut";
        }

        protected override void SaveData(int index = -1)
        {

        }

        protected override void LoadData()
        {

        }
    }
}
