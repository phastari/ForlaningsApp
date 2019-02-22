namespace FiefApp.Module.Expenses
{
    /// <summary>
    /// Interaction logic for ExpensesView.xaml
    /// </summary>
    public partial class ExpensesView
    {
        public ExpensesView(ExpensesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
