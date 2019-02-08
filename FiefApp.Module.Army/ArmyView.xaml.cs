namespace FiefApp.Module.Army
{
    /// <summary>
    /// Interaction logic for ArmyView.xaml
    /// </summary>
    public partial class ArmyView
    {
        public ArmyView(ArmyViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}