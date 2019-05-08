using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using CommonServiceLocator;
using FiefApp.Common.Infrastructure.Services;
using Prism.Commands;

namespace FiefApp.Module.EndOfYear.UIElements.EndOfYearMineUI
{
    /// <summary>
    /// Interaction logic for EndOfYearMineUI.xaml
    /// </summary>
    public partial class EndOfYearMineUI : INotifyPropertyChanged
    {
        private readonly IBaseService _baseService;
        public EndOfYearMineUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            Roll = new DelegateCommand<object>(ExecuteRoll);

            _baseService = ServiceLocator.Current.GetInstance<IBaseService>();
        }

        #region DelegateCommand : Roll

        public DelegateCommand<object> Roll { get; set; }
        private void ExecuteRoll(object obj)
        {
            int i = Convert.ToInt32(obj);

            switch (i)
            {
                case 1:
                    Roll1 = _baseService.RollObDice(SkillNumeric);
                    break;

                case 2:
                    Roll2 = _baseService.RollObDice(SkillNumeric);
                    break;

                case 3:
                    Roll3 = _baseService.RollObDice(SkillNumeric);
                    break;
            }
        }

        #endregion

        #region Dependency Properties

        public string Type
        {
            get => (string)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register(
                "Type",
                typeof(string),
                typeof(EndOfYearMineUI),
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
                typeof(EndOfYearMineUI),
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
                typeof(EndOfYearMineUI),
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
                typeof(EndOfYearMineUI),
                new PropertyMetadata("0", RaiseSkillChanged)
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
                typeof(EndOfYearMineUI),
                new PropertyMetadata(0)
            );

        public int Crime
        {
            get => (int)GetValue(CrimeProperty);
            set => SetValue(CrimeProperty, value);
        }

        public static readonly DependencyProperty CrimeProperty =
            DependencyProperty.Register(
                "Crime",
                typeof(int),
                typeof(EndOfYearMineUI),
                new PropertyMetadata(0)
            );

        public int Guards
        {
            get => (int)GetValue(GuardsProperty);
            set => SetValue(GuardsProperty, value);
        }

        public static readonly DependencyProperty GuardsProperty =
            DependencyProperty.Register(
                "Guards",
                typeof(int),
                typeof(EndOfYearMineUI),
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
                typeof(EndOfYearMineUI),
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

        #endregion

        #region Methods

        private static void RaiseSkillChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is EndOfYearMineUI c)
                c.ConvertToNumeric();
        }

        private void CalculateResult()
        {
            if (Roll1 > 0 && Roll2 > 0 && Roll3 > 0)
            {
                int control = 0;

                if (Roll1 - Difficulty >= 0)
                {
                    control += Convert.ToInt32(Math.Floor(((decimal)Roll1 - Difficulty) / 5) + 1);
                }

                if (Roll2 - Difficulty >= 0)
                {
                    control += Convert.ToInt32(Math.Floor(((decimal)Roll2 - Difficulty) / 5) + 1);
                }

                if (Roll3 - Difficulty >= 0)
                {
                    control += Convert.ToInt32(Math.Floor(((decimal)Roll3 - Difficulty) / 5) + 1);
                }

                if (control == 0)
                {
                    ResultSilver = "0";
                }
                else if (control < 3)
                {
                    int i = Roll1 - Difficulty;
                    if (i < 0)
                    {
                        i *= 15;
                    }

                    int j = Roll2 - Difficulty;
                    if (j < 0)
                    {
                        j *= 15;
                    }

                    int k = Roll3 - Difficulty;
                    if (k < 0)
                    {
                        k *= 15;
                    }
                    int r = Convert.ToInt32(Math.Floor(BaseIncomeSilver + BaseIncomeSilver * (decimal)(i + j + k) / 100));
                    if (r <= 0)
                    {
                        ResultSilver = "0";
                    }
                    else
                    {
                        ResultSilver = Convert.ToString(r);
                    }
                }
                else if (control == 3)
                {
                    ResultSilver = Convert.ToInt32(BaseIncomeSilver).ToString();
                }
                else
                {
                    ResultSilver = Convert.ToString(Convert.ToInt32(Math.Floor(BaseIncomeSilver * ((control - 3) * 0.25M + 1))));
                }
            }
            else
            {
                ResultSilver = "-";
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
