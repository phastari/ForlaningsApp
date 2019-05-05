using System.Windows;

namespace FiefApp.Module.EndOfYear
{
    /// <summary>
    /// Interaction logic for EndOfYearView.xaml
    /// </summary>
    public partial class EndOfYearView
    {
        public EndOfYearView(EndOfYearViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
