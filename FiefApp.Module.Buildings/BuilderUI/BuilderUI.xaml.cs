using System.Windows;

namespace FiefApp.Module.Buildings.BuilderUI
{
    /// <summary>
    /// Interaction logic for BuilderUI.xaml
    /// </summary>
    public partial class BuilderUI
    {
        public BuilderUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;
        }

        #region Dependency Properties

        public int Id
        {
            get => (int)GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register(
                "Id",
                typeof(int),
                typeof(BuilderUI),
                new PropertyMetadata(-1)
            );

        public string Builder
        {
            get => (string)GetValue(BuilderProperty);
            set => SetValue(BuilderProperty, value);
        }

        public static readonly DependencyProperty BuilderProperty =
            DependencyProperty.Register(
                "Builder",
                typeof(string),
                typeof(BuilderUI),
                new PropertyMetadata("")
            );

        public int Age
        {
            get => (int)GetValue(AgeProperty);
            set => SetValue(AgeProperty, value);
        }

        public static readonly DependencyProperty AgeProperty =
            DependencyProperty.Register(
                "Age",
                typeof(int),
                typeof(BuilderUI),
                new PropertyMetadata(-1)
            );

        public string Skill
        {
            get => (string)GetValue(SkillProperty);
            set => SetValue(SkillProperty, value);
        }

        public static readonly DependencyProperty SkillProperty =
            DependencyProperty.Register(
                "Skill",
                typeof(string),
                typeof(BuilderUI),
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
                typeof(BuilderUI),
                new PropertyMetadata("")
            );

        public string BBResources
        {
            get => (string)GetValue(BBResourcesProperty);
            set => SetValue(BBResourcesProperty, value);
        }

        public static readonly DependencyProperty BBResourcesProperty =
            DependencyProperty.Register(
                "BBResources",
                typeof(string),
                typeof(BuilderUI),
                new PropertyMetadata("")
            );

        public string Assignment
        {
            get => (string)GetValue(AssignmentProperty);
            set => SetValue(AssignmentProperty, value);
        }

        public static readonly DependencyProperty AssignmentProperty =
            DependencyProperty.Register(
                "Assignment",
                typeof(string),
                typeof(BuilderUI),
                new PropertyMetadata("")
            );

        #endregion
    }
}
