using CommonServiceLocator;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.EndOfYear.RoutedEvents;
using Prism.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace FiefApp.Module.EndOfYear.UIElements.EndOfYearIncomeUI
{
    /// <summary>
    /// Interaction logic for EndOfYearIncomeUI.xaml
    /// </summary>
    public partial class EndOfYearIncomeUI : INotifyPropertyChanged
    {
        private readonly IBaseService _baseService;

        public EndOfYearIncomeUI()
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

        public int Id
        {
            get => (int)GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register(
                "Id",
                typeof(int),
                typeof(EndOfYearIncomeUI),
                new PropertyMetadata(-1)
            );

        public string Income
        {
            get => (string)GetValue(IncomeProperty);
            set => SetValue(IncomeProperty, value);
        }

        public static readonly DependencyProperty IncomeProperty =
            DependencyProperty.Register(
                "Income",
                typeof(string),
                typeof(EndOfYearIncomeUI),
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
                typeof(EndOfYearIncomeUI),
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
                typeof(EndOfYearIncomeUI),
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
                typeof(EndOfYearIncomeUI),
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
                typeof(EndOfYearIncomeUI),
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
                typeof(EndOfYearIncomeUI),
                new PropertyMetadata(0)
            );

        public int BaseIncome
        {
            get => (int)GetValue(BaseIncomeProperty);
            set => SetValue(BaseIncomeProperty, value);
        }

        public static readonly DependencyProperty BaseIncomeProperty =
            DependencyProperty.Register(
                "BaseIncome",
                typeof(int),
                typeof(EndOfYearIncomeUI),
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

        private string _result = "-";
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Methods

        private void SendOk(bool ok)
        {
            EndOfYearEventArgs newEventArgs =
                new EndOfYearEventArgs(
                    EndOfYearOkRoutedEvent,
                    "Industry",
                    Id,
                    ok,
                    Result
                );

            RaiseEvent(newEventArgs);
        }

        private static void RaiseSkillChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is EndOfYearIncomeUI c)
                c.ConvertToNumeric();
        }

        private void CalculateResult()
        {
            if (Roll1 > 0 && Roll2 > 0 && Roll3 > 0)
            {
                int control = 0;

                if (Roll1 > 0 && Roll2 > 0 && Roll3 > 0)
                {
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
                        Result = "0";
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
                        int r = BaseIncome + Convert.ToInt32(Math.Floor(BaseIncome * ((decimal)(i + j + k) / 100)));
                        if (r <= 0)
                        {
                            Result = "0";
                        }
                        else
                        {
                            Result = Convert.ToString(r);
                        }
                    }
                    else if (control == 3)
                    {
                        Result = BaseIncome.ToString();
                    }
                    else
                    {
                        Result = Convert.ToString(Convert.ToInt32(Math.Floor(BaseIncome * ((control - 3) * 0.25M + 1))));
                    }
                    SendOk(true);
                }
                else
                {
                    Result = "-";
                    SendOk(false);
                }
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

        #region RoutedEvents

        public static readonly RoutedEvent EndOfYearOkRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "EndOfYearOkEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(EndOfYearIncomeUI)
            );

        public event RoutedEventHandler EndOfYearOkEvent
        {
            add => AddHandler(EndOfYearOkRoutedEvent, value);
            remove => RemoveHandler(EndOfYearOkRoutedEvent, value);
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
