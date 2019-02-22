using System.Windows;

namespace FiefApp.Module.Boatbuilding.UIElements.BuildingShipyardUI
{
    /// <summary>
    /// Interaction logic for BuildingShipyardUI.xaml
    /// </summary>
    public partial class BuildingShipyardUI
    {
        public BuildingShipyardUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;
        }

        public bool CantEdit
        {
            get => (bool)GetValue(CantEditProperty);
            set => SetValue(CantEditProperty, value);
        }

        public static readonly DependencyProperty CantEditProperty =
            DependencyProperty.Register(
                "CantEdit",
                typeof(bool),
                typeof(BuildingShipyardUI),
                new PropertyMetadata(
                    true)
            );

        public int DaysWorkNeeded
        {
            get => (int)GetValue(DaysWorkNeededProperty);
            set => SetValue(DaysWorkNeededProperty, value);
        }

        public static readonly DependencyProperty DaysWorkNeededProperty =
            DependencyProperty.Register(
                "DaysWorkNeeded",
                typeof(int),
                typeof(BuildingShipyardUI),
                new PropertyMetadata(0)
            );

        public int DaysWorkThisYear
        {
            get => (int)GetValue(DaysWorkThisYearProperty);
            set => SetValue(DaysWorkThisYearProperty, value);
        }

        public static readonly DependencyProperty DaysWorkThisYearProperty =
            DependencyProperty.Register(
                "DaysWorkThisYear",
                typeof(int),
                typeof(BuildingShipyardUI),
                new PropertyMetadata(0)
            );
    }
}
