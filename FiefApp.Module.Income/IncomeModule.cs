using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FiefApp.Module.Income
{
    public class IncomeModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("TabRegion", typeof(IncomeView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
