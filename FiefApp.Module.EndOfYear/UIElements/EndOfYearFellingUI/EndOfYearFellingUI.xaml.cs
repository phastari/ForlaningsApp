﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using CommonServiceLocator;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.EndOfYear.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.EndOfYear.UIElements.EndOfYearFellingUI
{
    /// <summary>
    /// Interaction logic for EndOfYearFelling.xaml
    /// </summary>
    public partial class EndOfYearFellingUI : INotifyPropertyChanged
    {
        private readonly IBaseService _baseService;
        public EndOfYearFellingUI()
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
                typeof(EndOfYearFellingUI),
                new PropertyMetadata(-1)
            );

        public int Felling
        {
            get => (int)GetValue(FellingProperty);
            set => SetValue(FellingProperty, value);
        }

        public static readonly DependencyProperty FellingProperty =
            DependencyProperty.Register(
                "Felling",
                typeof(int),
                typeof(EndOfYearFellingUI),
                new PropertyMetadata(0)
            );

        public int LandClearing
        {
            get => (int)GetValue(LandClearingProperty);
            set => SetValue(LandClearingProperty, value);
        }

        public static readonly DependencyProperty LandClearingProperty =
            DependencyProperty.Register(
                "LandClearing",
                typeof(int),
                typeof(EndOfYearFellingUI),
                new PropertyMetadata(0)
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
                typeof(EndOfYearFellingUI),
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
                typeof(EndOfYearFellingUI),
                new PropertyMetadata(0)
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
                typeof(EndOfYearFellingUI),
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
                typeof(EndOfYearFellingUI),
                new PropertyMetadata(0)
            );

        #endregion

        #region UI Properties

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

        private int _prediction;
        public int Prediction
        {
            get => _prediction;
            set
            {
                _prediction = value;
                NotifyPropertyChanged();
            }
        }

        private int _fellingWood;
        public int FellingWood
        {
            get => _fellingWood;
            set
            {
                _fellingWood = value;
                NotifyPropertyChanged();
            }
        }

        private int _landClearingWood;
        public int LandClearingWood
        {
            get => _landClearingWood;
            set
            {
                _landClearingWood = value;
                NotifyPropertyChanged();
            }
        }

        private int _baseIncome;
        public int BaseIncome
        {
            get => _baseIncome;
            set
            {
                _baseIncome = value;
                NotifyPropertyChanged();
            }
        }

        private string _result;
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                NotifyPropertyChanged();
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

        #endregion

        #region Methods

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
                    
                    int r = Convert.ToInt32(Math.Floor((decimal)FellingWood + LandClearingWood + Convert.ToInt32((FellingWood + LandClearingWood) * ((decimal)(i + j + k) / 100))));
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
                    Result = Convert.ToInt32(FellingWood + LandClearingWood).ToString();
                }
                else
                {
                    Result = Convert.ToString(Convert.ToInt32(Math.Floor((FellingWood + LandClearingWood) * ((control - 3) * 0.25M + 1))));
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
                    "Felling",
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
            if (d is EndOfYearFellingUI c)
                c.ConvertToNumeric();
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
                typeof(EndOfYearFellingUI)
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

        private void EndOfYearFellingUI_OnLoaded(
            object sender, 
            RoutedEventArgs e)
        {
            if (Felling > 0 || LandClearing > 0)
            {
                FellingWood = _baseService.RollObDice(Felling * 4) + Felling * 6;
                LandClearingWood = _baseService.RollObDice(LandClearing * 4) + LandClearing * 6;
                Prediction = Felling * 9 + LandClearing * 9;
            }
            else
            {
                Self.Visibility = Visibility.Collapsed;
                SendOk(true);
            }
        }
    }
}
