using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace FiefApp.Module.EndOfYear.UIElements.EndOfYearSubsidiaryUI
{
    /// <summary>
    /// Interaction logic for EndOfYearSubsidiaryUI.xaml
    /// </summary>
    public partial class EndOfYearSubsidiaryUI : INotifyPropertyChanged
    {
        public EndOfYearSubsidiaryUI()
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
                typeof(EndOfYearSubsidiaryUI),
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
                typeof(EndOfYearSubsidiaryUI),
                new PropertyMetadata("")
            );

        public string StewardName
        {
            get => (string)GetValue(StewardNameProperty);
            set => SetValue(StewardNameProperty, value);
        }

        public static readonly DependencyProperty StewardNameProperty =
            DependencyProperty.Register(
                "StewardName",
                typeof(string),
                typeof(EndOfYearSubsidiaryUI),
                new PropertyMetadata("")
            );

        public int StewardId
        {
            get => (int)GetValue(StewardIdProperty);
            set => SetValue(StewardIdProperty, value);
        }

        public static readonly DependencyProperty StewardIdProperty =
            DependencyProperty.Register(
                "StewardId",
                typeof(int),
                typeof(EndOfYearSubsidiaryUI),
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
                typeof(EndOfYearSubsidiaryUI),
                new PropertyMetadata("0", RaiseSkillChanged)
            );

        public decimal Crewed
        {
            get => (decimal)GetValue(CrewedProperty);
            set => SetValue(CrewedProperty, value);
        }

        public static readonly DependencyProperty CrewedProperty =
            DependencyProperty.Register(
                "Crewed",
                typeof(decimal),
                typeof(EndOfYearSubsidiaryUI),
                new PropertyMetadata(0.0M)
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
                typeof(EndOfYearSubsidiaryUI),
                new PropertyMetadata(0)
            );

        public int BaseIncomeSilver
        {
            get => (int)GetValue(BaseIncomeSilverProperty);
            set => SetValue(BaseIncomeSilverProperty, value);
        }

        public static readonly DependencyProperty BaseIncomeSilverProperty =
            DependencyProperty.Register(
                "BaseIncomeSilver",
                typeof(int),
                typeof(EndOfYearSubsidiaryUI),
                new PropertyMetadata(0)
            );

        public int BaseIncomeBase
        {
            get => (int)GetValue(BaseIncomeBaseProperty);
            set => SetValue(BaseIncomeBaseProperty, value);
        }

        public static readonly DependencyProperty BaseIncomeBaseProperty =
            DependencyProperty.Register(
                "BaseIncomeBase",
                typeof(int),
                typeof(EndOfYearSubsidiaryUI),
                new PropertyMetadata(0)
            );

        public int BaseIncomeLuxury
        {
            get => (int)GetValue(BaseIncomeLuxuryProperty);
            set => SetValue(BaseIncomeLuxuryProperty, value);
        }

        public static readonly DependencyProperty BaseIncomeLuxuryProperty =
            DependencyProperty.Register(
                "BaseIncomeLuxury",
                typeof(int),
                typeof(EndOfYearSubsidiaryUI),
                new PropertyMetadata(0)
            );

        #endregion

        #region UI Properties

        private int _skillNumeric;
        public int SkillNumeric
        {
            get => _skillNumeric;
            set
            {
                _skillNumeric = value;
                NotifyPropertyChanged();
            }
        }

        private int _roll1;
        public int Roll1
        {
            get => _roll1;
            set
            {
                _roll1 = value;
                CalculateResult();
                NotifyPropertyChanged();
            }
        }

        private int _roll2;
        public int Roll2
        {
            get => _roll2;
            set
            {
                _roll2 = value;
                CalculateResult();
                NotifyPropertyChanged();
            }
        }

        private int _roll3;
        public int Roll3
        {
            get => _roll3;
            set
            {
                _roll3 = value;
                CalculateResult();
                NotifyPropertyChanged();
            }
        }

        private string _resultSilver = "-";
        public string ResultSilver
        {
            get => _resultSilver;
            set
            {
                _resultSilver = value;
                NotifyPropertyChanged();
            }
        }

        private string _resultBase = "-";
        public string ResultBase
        {
            get => _resultBase;
            set
            {
                _resultBase = value;
                NotifyPropertyChanged();
            }
        }

        private string _resultLuxury = "-";
        public string ResultLuxury
        {
            get => _resultLuxury;
            set
            {
                _resultLuxury = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Methods

        private static void RaiseSkillChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is EndOfYearSubsidiaryUI c)
                c.ConvertToNumeric();
        }

        private void CalculateResult()
        {
            if (Roll1 > 0 && Roll2 > 0 && Roll3 > 0)
            {
                
            }
        }

        private void ConvertToNumeric()
        {
            if (string.IsNullOrEmpty(Skill))
            {
                SkillNumeric = 0;
            }
            else
            {
                if (Skill.IndexOf('T') != -1 || Skill.Length < 3)
                {
                    bool isNegative;
                    string temp;

                    if (Skill.IndexOf('-') == -1)
                    {
                        temp = (string)GetValue(SkillProperty);
                        isNegative = false;
                    }
                    else
                    {
                        temp = Skill;
                        temp = temp.Substring(1);
                        isNegative = true;
                    }
                    if (temp.IndexOf('+') == 0)
                    {
                        temp = temp.Substring(1);
                    }


                    string temp2;
                    var value = 0;
                    var loop = true;
                    var holder = "";

                    if (temp.Length < 3)
                    {
                        value = Convert.ToInt32(temp);
                    }
                    else
                    {
                        int x;
                        for (x = 0; x < temp.Length && loop; x++)
                        {
                            if (Char.IsDigit(temp[x]))
                            {
                                temp2 = temp;
                                holder = holder + temp2[x];
                            }
                            else
                            {
                                value = Convert.ToInt32(holder);
                                value = value * 4;
                                loop = false;
                            }
                        }
                    }
                    if (temp.IndexOf('+') != -1)
                    {
                        var pos = temp.IndexOf('+');
                        pos = pos + 1;
                        var y = temp.Substring(pos, 1);
                        value = value + Convert.ToInt32(y);
                    }
                    if (isNegative)
                    {
                        SkillNumeric = -value;
                    }
                    else
                    {
                        SkillNumeric = value;
                    }
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
