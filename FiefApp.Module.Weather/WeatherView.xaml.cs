namespace FiefApp.Module.Weather
{
    /// <summary>
    /// Interaction logic for WeatherView.xaml
    /// </summary>
    public partial class WeatherView
    {
        public WeatherView(WeatherViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
