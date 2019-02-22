using System.Windows;

namespace FiefApp.Module.Boatbuilding
{
    /// <summary>
    /// Interaction logic for BoatbuildingView.xaml
    /// </summary>
    public partial class BoatbuildingView
    {
        public BoatbuildingView(BoatbuildingViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
