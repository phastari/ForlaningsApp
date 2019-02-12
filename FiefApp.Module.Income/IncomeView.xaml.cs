namespace FiefApp.Module.Income
{
    /// <summary>
    /// Interaction logic for IncomeView.xaml
    /// </summary>
    public partial class IncomeView
    {
        public IncomeView(IncomeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
