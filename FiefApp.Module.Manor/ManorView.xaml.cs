namespace FiefApp.Module.Manor
{
    /// <summary>
    /// Interaction logic for ManorView.xaml
    /// </summary>
    public partial class ManorView
    {
        public ManorView(ManorViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
