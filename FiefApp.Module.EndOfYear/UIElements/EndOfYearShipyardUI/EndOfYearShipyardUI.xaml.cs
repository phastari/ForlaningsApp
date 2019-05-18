using System.Windows.Controls;

namespace FiefApp.Module.EndOfYear.UIElements.EndOfYearShipyardUI
{
    /// <summary>
    /// Interaction logic for EndOfYearShipyardUI.xaml
    /// </summary>
    public partial class EndOfYearShipyardUI
    {
        public EndOfYearShipyardUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;
        }
    }
}
