using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FiefApp.Module.Subsidiary
{
    public class SubsidiaryModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("TabRegion", typeof(SubsidiaryView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}