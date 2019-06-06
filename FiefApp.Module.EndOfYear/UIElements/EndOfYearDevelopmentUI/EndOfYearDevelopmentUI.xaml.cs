using CommonServiceLocator;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.EndOfYear.RoutedEvents;
using Prism.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace FiefApp.Module.EndOfYear.UIElements.EndOfYearDevelopmentUI
{
    /// <summary>
    /// Interaction logic for EndOfYearDevelopmentUI.xaml
    /// </summary>
    public partial class EndOfYearDevelopmentUI : INotifyPropertyChanged
    {
        private readonly IBaseService _baseService;
        public EndOfYearDevelopmentUI()
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
                typeof(EndOfYearDevelopmentUI),
                new PropertyMetadata(-1)
            );

        public string Industry
        {
            get => (string)GetValue(IndustryProperty);
            set => SetValue(IndustryProperty, value);
        }

        public static readonly DependencyProperty IndustryProperty =
            DependencyProperty.Register(
                "Industry",
                typeof(string),
                typeof(EndOfYearDevelopmentUI),
                new PropertyMetadata("")
            );

        public int IndustryId
        {
            get => (int)GetValue(IndustryIdProperty);
            set => SetValue(IndustryIdProperty, value);
        }

        public static readonly DependencyProperty IndustryIdProperty =
            DependencyProperty.Register(
                "IndustryId",
                typeof(int),
                typeof(EndOfYearDevelopmentUI),
                new PropertyMetadata(-1)
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
                typeof(EndOfYearDevelopmentUI),
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
                typeof(EndOfYearDevelopmentUI),
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
                typeof(EndOfYearDevelopmentUI),
                new PropertyMetadata("0", RaiseSkillChanged)
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
                typeof(EndOfYearDevelopmentUI),
                new PropertyMetadata(0)
            );

        public bool BeingDeveloped
        {
            get => (bool)GetValue(BeingDevelopedProperty);
            set => SetValue(BeingDevelopedProperty, value);
        }

        public static readonly DependencyProperty BeingDevelopedProperty =
            DependencyProperty.Register(
                "BeingDeveloped",
                typeof(bool),
                typeof(EndOfYearDevelopmentUI),
                new PropertyMetadata(false)
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

        private static void RaiseSkillChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is EndOfYearDevelopmentUI c)
                c.ConvertToNumeric();
        }

        private void CalculateResult()
        {
            if (Roll1 > 0 && Roll2 > 0 && Roll3 > 0)
            {
                int control = 0;

                if (Roll1 - (7 + DevelopmentLevel * 2) >= 0)
                {
                    control += Convert.ToInt32(Math.Floor(((decimal)Roll1 - (7 + DevelopmentLevel * 2)) / 5) + 1);
                }

                if (Roll2 - (7 + DevelopmentLevel * 2) >= 0)
                {
                    control += Convert.ToInt32(Math.Floor(((decimal)Roll2 - (7 + DevelopmentLevel * 2)) / 5) + 1);
                }

                if (Roll3 - (7 + DevelopmentLevel * 2) >= 0)
                {
                    control += Convert.ToInt32(Math.Floor(((decimal)Roll3 - (7 + DevelopmentLevel * 2)) / 5) + 1);
                }

                if (control < 3)
                {
                    Result = "Misslyckat!";
                }
                else if (control > 8)
                {
                    Result = "Legendariskt";
                }
                else
                {
                    Result = "Lyckat";
                }
                SendOk(true);
            }
            else
            {
                Result = "-";
                SendOk(false);
            }
        }

        private void SendOk(bool ok)
        {
            EndOfYearEventArgs newEventArgs =
                new EndOfYearEventArgs(
                    EndOfYearOkRoutedEvent,
                    "Industry",
                    Id,
                    ok
                );

            RaiseEvent(newEventArgs);
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
                typeof(EndOfYearDevelopmentUI)
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

        private void Self_Loaded(
            object sender, 
            RoutedEventArgs e)
        {
            if (!BeingDeveloped || StewardId < 1)
            {
                Self.Visibility = Visibility.Collapsed;
            }
        }
    }
}
