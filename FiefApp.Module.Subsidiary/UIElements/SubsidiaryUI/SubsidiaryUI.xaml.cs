using System.Collections.ObjectModel;
using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Subsidiary.UIElements.SubsidiaryUI
{
    /// <summary>
    /// Interaction logic for SubsidiaryUI.xaml
    /// </summary>
    public partial class SubsidiaryUI
    {
        public SubsidiaryUI()
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
                typeof(SubsidiaryUI),
                new PropertyMetadata(-1)
            );

        public string Subsidiary
        {
            get => (string)GetValue(SubsidiaryProperty);
            set => SetValue(SubsidiaryProperty, value);
        }

        public static readonly DependencyProperty SubsidiaryProperty =
            DependencyProperty.Register(
                "Subsidiary",
                typeof(string),
                typeof(SubsidiaryUI),
                new PropertyMetadata("")
            );

        public int Quality
        {
            get => (int)GetValue(QualityProperty);
            set => SetValue(QualityProperty, value);
        }

        public static readonly DependencyProperty QualityProperty =
            DependencyProperty.Register(
                "Quality",
                typeof(int),
                typeof(SubsidiaryUI),
                new PropertyMetadata(-1)
            );

        public int DevelopmentLevel
        {
            get => (int)GetValue(DevelopmentLevelProperty);
            set => SetValue(DevelopmentLevelProperty, value);
        }

        public static readonly DependencyProperty DevelopmentLevelProperty =
            DependencyProperty.Register(
                "DevelopmentLevel",
                typeof(int),
                typeof(SubsidiaryUI),
                new PropertyMetadata(-1)
            );

        public int Silver
        {
            get => (int)GetValue(SilverProperty);
            set => SetValue(SilverProperty, value);
        }

        public static readonly DependencyProperty SilverProperty =
            DependencyProperty.Register(
                "Silver",
                typeof(int),
                typeof(SubsidiaryUI),
                new PropertyMetadata(-1)
            );

        public int Base
        {
            get => (int)GetValue(BaseProperty);
            set => SetValue(BaseProperty, value);
        }

        public static readonly DependencyProperty BaseProperty =
            DependencyProperty.Register(
                "Base",
                typeof(int),
                typeof(SubsidiaryUI),
                new PropertyMetadata(-1)
            );

        public int Luxury
        {
            get => (int)GetValue(LuxuryProperty);
            set => SetValue(LuxuryProperty, value);
        }

        public static readonly DependencyProperty LuxuryProperty =
            DependencyProperty.Register(
                "Luxury",
                typeof(int),
                typeof(SubsidiaryUI),
                new PropertyMetadata(-1)
            );

        public int DaysWorkLeft
        {
            get => (int)GetValue(DaysWorkLeftProperty);
            set => SetValue(DaysWorkLeftProperty, value);
        }

        public static readonly DependencyProperty DaysWorkLeftProperty =
            DependencyProperty.Register(
                "DaysWorkLeft",
                typeof(int),
                typeof(SubsidiaryUI),
                new PropertyMetadata(-1)
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
                typeof(SubsidiaryUI),
                new PropertyMetadata(-1)
            );

        public string Steward
        {
            get => (string)GetValue(StewardProperty);
            set => SetValue(StewardProperty, value);
        }

        public static readonly DependencyProperty StewardProperty =
            DependencyProperty.Register(
                "Steward",
                typeof(string),
                typeof(SubsidiaryUI),
                new PropertyMetadata("")
            );

        public ObservableCollection<StewardModel> StewardsCollection
        {
            get => (ObservableCollection<StewardModel>)GetValue(StewardsCollectionProperty);
            set => SetValue(StewardsCollectionProperty, value);
        }

        public static readonly DependencyProperty StewardsCollectionProperty =
            DependencyProperty.Register(
                "StewardsCollection",
                typeof(ObservableCollection<StewardModel>),
                typeof(SubsidiaryUI),
                new PropertyMetadata(new ObservableCollection<StewardModel>())
            );

        public int Difficulty
        {
            get => (int)GetValue(DifficultyProperty);
            set => SetValue(DifficultyProperty, value);
        }

        public static readonly DependencyProperty DifficultyProperty =
            DependencyProperty.Register(
                "Difficulty",
                typeof(int),
                typeof(SubsidiaryUI),
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
                typeof(SubsidiaryUI),
                new PropertyMetadata("")
            );
    }
}
