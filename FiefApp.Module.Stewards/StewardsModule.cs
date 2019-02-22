using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FiefApp.Module.Stewards
{
    public class StewardsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("TabRegion", typeof(StewardsView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}