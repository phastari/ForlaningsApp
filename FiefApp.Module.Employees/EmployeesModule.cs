using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FiefApp.Module.Employees
{
    public class EmployeesModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("TabRegion", typeof(EmployeesView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}