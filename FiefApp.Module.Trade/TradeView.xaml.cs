namespace FiefApp.Module.Trade
{
    /// <summary>
    /// Interaction logic for TradeView.xaml
    /// </summary>
    public partial class TradeView
    {
        public TradeView(TradeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
