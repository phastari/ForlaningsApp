using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FiefApp.Module.Boatbuilding
{
    public class BoatbuildingModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("TabRegion", typeof(BoatbuildingView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
