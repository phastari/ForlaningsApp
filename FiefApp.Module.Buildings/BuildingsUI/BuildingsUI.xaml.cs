using System.Windows;

namespace FiefApp.Module.Buildings.BuildingsUI
{
    /// <summary>
    /// Interaction logic for BuildingsUI.xaml
    /// </summary>
    public partial class BuildingsUI
    {
        public BuildingsUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;
        }

        public int Id
        {
            get => (int)GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register(
                "Id",
                typeof(int),
                typeof(BuildingsUI),
                new PropertyMetadata(0)
            );

        public string Building
        {
            get => (string)GetValue(BuildingProperty);
            set => SetValue(BuildingProperty, value);
        }

        public static readonly DependencyProperty BuildingProperty =
            DependencyProperty.Register(
                "Building",
                typeof(string),
                typeof(BuildingsUI),
                new PropertyMetadata("")
            );

        public int Amount
        {
            get => (int)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }

        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register(
                "Amount",
                typeof(int),
                typeof(BuildingsUI),
                new PropertyMetadata(0)
            );

        public int Upkeep
        {
            get => (int)GetValue(UpkeepProperty);
            set => SetValue(UpkeepProperty, value);
        }

        public static readonly DependencyProperty UpkeepProperty =
            DependencyProperty.Register(
                "Upkeep",
                typeof(int),
                typeof(BuildingsUI),
                new PropertyMetadata(0)
            );
    }
}
