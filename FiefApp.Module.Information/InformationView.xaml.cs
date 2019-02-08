namespace FiefApp.Module.Information
{
    /// <summary>
    /// Interaction logic for InformationView.xaml
    /// </summary>
    public partial class InformationView
    {
        public InformationView(InformationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
