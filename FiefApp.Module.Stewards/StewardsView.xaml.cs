namespace FiefApp.Module.Stewards
{
    /// <summary>
    /// Interaction logic for StewardsView.xaml
    /// </summary>
    public partial class StewardsView
    {
        public StewardsView(StewardsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
