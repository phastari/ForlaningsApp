namespace FiefApp.Module.Buildings
{
    /// <summary>
    /// Interaction logic for BuildingsView.xaml
    /// </summary>
    public partial class BuildingsView
    {
        public BuildingsView(BuildingsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
