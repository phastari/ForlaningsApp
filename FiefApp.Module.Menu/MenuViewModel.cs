using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.Services;

namespace FiefApp.Module.Menu
{
    public class MenuViewModel : ViewModelBaseClass
    {
        public MenuViewModel(
            IBaseService baseService
            ) : base(baseService)
        {

        }

        protected override void SaveData(int index = -1)
        {
            
        }

        protected override void LoadData()
        {
            
        }
    }
}
