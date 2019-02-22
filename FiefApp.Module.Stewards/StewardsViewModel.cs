using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.Services;

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
        }

        protected override void SaveData(int index = -1)
        {
            
        }

        protected override void LoadData()
        {
            
        }
    }
}
