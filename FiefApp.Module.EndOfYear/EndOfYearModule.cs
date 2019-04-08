using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FiefApp.Module.EndOfYear
{
    public class EndOfYearModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("TabRegion", typeof(EndOfYearView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}