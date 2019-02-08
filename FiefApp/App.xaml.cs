using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using FiefApp.Common.Infrastructure.Services;

namespace FiefApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IFiefService, FiefService>();
            containerRegistry.RegisterSingleton<ISettingsService, SettingsService>();

            containerRegistry.Register<IBaseService, BaseService>();
            containerRegistry.Register<IInformationService, InformationService>();
            containerRegistry.Register<IArmyService, ArmyService>();
            containerRegistry.Register<IEmployeeService, EmployeeService>();
            containerRegistry.Register<IManorService, ManorService>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @".\" };
        }
    }
}
