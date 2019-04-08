using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FiefApp.Module.Supply
{
    public class SupplyModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("SupplyRegion", typeof(SupplyView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}