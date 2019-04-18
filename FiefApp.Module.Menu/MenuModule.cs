using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FiefApp.Module.Menu
{
    public class MenuModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MenuRegion", typeof(MenuView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
