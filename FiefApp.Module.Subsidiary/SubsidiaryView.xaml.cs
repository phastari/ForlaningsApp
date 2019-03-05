namespace FiefApp.Module.Subsidiary
{
    /// <summary>
    /// Interaction logic for SubsidiaryView.xaml
    /// </summary>
    public partial class SubsidiaryView
    {
        public SubsidiaryView(SubsidiaryViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
