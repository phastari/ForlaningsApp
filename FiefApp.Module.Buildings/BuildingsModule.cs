using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FiefApp.Module.Buildings
{
    public class BuildingsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("TabRegion", typeof(BuildingsView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}