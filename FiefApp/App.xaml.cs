using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using FiefApp.Common.Infrastructure.Services;
using Prism.Events;

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
            containerRegistry.RegisterSingleton<ISupplyService, SupplyService>();
            containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();

            containerRegistry.Register<IBaseService, BaseService>();
            containerRegistry.Register<IInformationService, InformationService>();
            containerRegistry.Register<IArmyService, ArmyService>();
            containerRegistry.Register<IEmployeeService, EmployeeService>();
            containerRegistry.Register<IManorService, ManorService>();
            containerRegistry.Register<IBoatbuildingService, BoatbuildingService>();
            containerRegistry.Register<IExpensesService, ExpensesService>();
            containerRegistry.Register<IStewardsService, StewardsService>();
            containerRegistry.Register<ISubsidiaryService, SubsidiaryService>();
            containerRegistry.Register<IIncomeService, IncomeService>();
            containerRegistry.Register<ICalculations, Calculations>();
            containerRegistry.Register<IBuildingsService, BuildingsService>();
            containerRegistry.Register<IWeatherService, WeatherService>();
            containerRegistry.Register<ITradeService, TradeService>();
            containerRegistry.Register<IPortService, PortService>();
            containerRegistry.Register<IMinesService, MinesService>();
            containerRegistry.Register<IEndOfYearService, EndOfYearService>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @".\" };
        }
    }
}
