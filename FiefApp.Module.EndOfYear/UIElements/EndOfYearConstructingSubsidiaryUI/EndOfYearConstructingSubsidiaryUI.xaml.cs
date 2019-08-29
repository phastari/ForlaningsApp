using CommonServiceLocator;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.EndOfYear.RoutedEvents;
using Prism.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace FiefApp.Module.EndOfYear.UIElements.EndOfYearConstructingSubsidiaryUI
{
    /// <summary>
    /// Interaction logic for EndOfYearConstructingSubsidiaryUI.xaml
    /// </summary>
    public partial class EndOfYearConstructingSubsidiaryUI : UserControl
    {
        private readonly IBaseService _baseService;

        public EndOfYearConstructingSubsidiaryUI()
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
                typeof(EndOfYearConstructingSubsidiaryUI),
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
                typeof(EndOfYearConstructingSubsidiaryUI),
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
                typeof(EndOfYearConstructingSubsidiaryUI),
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
                typeof(EndOfYearConstructingSubsidiaryUI),
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
                typeof(EndOfYearConstructingSubsidiaryUI),
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
                typeof(EndOfYearConstructingSubsidiaryUI),
                new PropertyMetadata(0)
            );

        public int DaysWorkBuild
        {
            get => (int)GetValue(DaysWorkBuildProperty);
            set => SetValue(DaysWorkBuildProperty, value);
        }

        public static readonly DependencyProperty DaysWorkBuildProperty =
            DependencyProperty.Register(
                "DaysWorkBuild",
                typeof(int),
                typeof(EndOfYearConstructingSubsidiaryUI),
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
                typeof(EndOfYearConstructingSubsidiaryUI),
                new PropertyMetadata(0)
            );

        #endregion

        #region UI Properties

        private bool _succeeded;
        public bool Succeeded
        {
            get => _succeeded;
            set
            {
                _succeeded = value;
                if (value)
                {
                    _succeededText = "Lyckat";
                }
                else
                {
                    _succeededText = "Misslyckat";
                }
            }
        }

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

        private int _daysWorkMod;
        public int DaysWorkMod
        {
            get => _daysWorkMod;
            set
            {
                _daysWorkMod = value;
                NotifyPropertyChanged();
            }
        }

        private int _quality;
        public int Quality
        {
            get => _quality;
            set
            {
                _quality = value;
                NotifyPropertyChanged();
            }
        }

        private int _developmentLevel;
        public int DevelopmentLevel
        {
            get => _developmentLevel;
            set
            {
                _developmentLevel = value;
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

        private string _succeededText;
        public string SucceededText
        {
            get => _succeededText;
            set
            {
                _succeededText = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Methods

        private void SendOk(bool ok)
        {
            EndOfYearConstructingSubsidiaryEventArgs newEventArgs =
                new EndOfYearConstructingSubsidiaryEventArgs(
                    EndOfYearConstructingSubsidiaryRoutedEvent,
                    Id,
                    Succeeded,
                    DaysWorkMod,
                    Quality,
                    DevelopmentLevel,
                    ok
                );

            RaiseEvent(newEventArgs);
        }

        private static void RaiseSkillChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is EndOfYearConstructingSubsidiaryUI c)
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
                    Succeeded = false;
                    DaysWorkMod = 2500;
                    Quality = 0;
                    DevelopmentLevel = 0;
                }
                else if (control < 3)
                {
                    Succeeded = false;
                    DaysWorkMod = 0;
                    Quality = 0;
                    DevelopmentLevel = 0;
                }
                else if (control == 3)
                {
                    Succeeded = true;
                    DaysWorkMod = 0;
                    Quality = 3;
                    DevelopmentLevel = 0;
                }
                else if (control >= 9)
                {
                    Succeeded = true;
                    DaysWorkMod = 0;
                    Quality = 5;
                    DevelopmentLevel = control;
                }
                else
                {
                    Succeeded = true;
                    DaysWorkMod = 0;
                    Quality = 4;
                    DevelopmentLevel = control - 3;
                }
                SendOk(true);
            }
            else
            {
                SucceededText = "-";
                SendOk(false);
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

        public static readonly RoutedEvent EndOfYearConstructingSubsidiaryRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "EndOfYearConstructingSubsidiaryEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(EndOfYearConstructingSubsidiaryUI)
            );

        public event RoutedEventHandler EndOfYearConstructingSubsidiaryEvent
        {
            add => AddHandler(EndOfYearConstructingSubsidiaryRoutedEvent, value);
            remove => RemoveHandler(EndOfYearConstructingSubsidiaryRoutedEvent, value);
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
