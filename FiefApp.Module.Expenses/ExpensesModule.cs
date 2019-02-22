using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FiefApp.Module.Expenses
{
    public class ExpensesModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("TabRegion", typeof(ExpensesView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}