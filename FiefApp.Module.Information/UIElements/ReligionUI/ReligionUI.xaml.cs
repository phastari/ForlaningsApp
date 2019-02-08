using System.Windows;

namespace FiefApp.Module.Information.UIElements.ReligionUI
{
    /// <summary>
    /// Interaction logic for ReligionUI.xaml
    /// </summary>
    public partial class ReligionUI
    {
        public ReligionUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;
        }

        public string Religion
        {
            get => (string)GetValue(ReligionProperty);
            set => SetValue(ReligionProperty, value);
        }

        public static readonly DependencyProperty ReligionProperty =
            DependencyProperty.Register(
                "Religion",
                typeof(string),
                typeof(ReligionUI),
                new PropertyMetadata("")
            );

        public int Followers
        {
            get => (int)GetValue(FollowersProperty);
            set => SetValue(FollowersProperty, value);
        }

        public static readonly DependencyProperty FollowersProperty =
            DependencyProperty.Register(
                "Followers",
                typeof(int),
                typeof(ReligionUI),
                new PropertyMetadata(0)
            );

        public int PercentOfPopulation
        {
            get => (int)GetValue(PercentOfPopulationProperty);
            set => SetValue(PercentOfPopulationProperty, value);
        }

        public static readonly DependencyProperty PercentOfPopulationProperty =
            DependencyProperty.Register(
                "PercentOfPopulation",
                typeof(int),
                typeof(ReligionUI),
                new PropertyMetadata(0)
            );

        public string HeadOfReligion
        {
            get => (string)GetValue(HeadOfReligionProperty);
            set => SetValue(HeadOfReligionProperty, value);
        }

        public static readonly DependencyProperty HeadOfReligionProperty =
            DependencyProperty.Register(
                "HeadOfReligion",
                typeof(string),
                typeof(ReligionUI),
                new PropertyMetadata("")
            );

        public string RResources
        {
            get => (string)GetValue(ResourcesProperty);
            set => SetValue(ResourcesProperty, value);
        }

        public static readonly DependencyProperty ResourcesProperty =
            DependencyProperty.Register(
                "RResources",
                typeof(string),
                typeof(ReligionUI),
                new PropertyMetadata("")
            );

        public string Loyalty
        {
            get => (string)GetValue(LoyaltyProperty);
            set => SetValue(LoyaltyProperty, value);
        }

        public static readonly DependencyProperty LoyaltyProperty =
            DependencyProperty.Register(
                "Loyalty",
                typeof(string),
                typeof(ReligionUI),
                new PropertyMetadata("")
            );

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(
                "IsReadOnly",
                typeof(bool),
                typeof(ReligionUI),
                new PropertyMetadata(true)
            );
    }
}
