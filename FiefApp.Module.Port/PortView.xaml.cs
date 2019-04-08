namespace FiefApp.Module.Port
{
    /// <summary>
    /// Interaction logic for PortView.xaml
    /// </summary>
    public partial class PortView
    {
        public PortView(PortViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
