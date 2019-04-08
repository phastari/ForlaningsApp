namespace FiefApp.Module.Mines
{
    /// <summary>
    /// Interaction logic for MinesView.xaml
    /// </summary>
    public partial class MinesView
    {
        public MinesView(MinesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
