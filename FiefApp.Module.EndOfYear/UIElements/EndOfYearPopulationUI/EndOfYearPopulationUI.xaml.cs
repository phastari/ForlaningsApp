using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using CommonServiceLocator;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.EndOfYear.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.EndOfYear.UIElements.EndOfYearPopulationUI
{
    /// <summary>
    /// Interaction logic for EndOfYearPopulationUI.xaml
    /// </summary>
    public partial class EndOfYearPopulationUI : INotifyPropertyChanged
    {
        private readonly IBaseService _baseService;
        public EndOfYearPopulationUI()
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
                    Roll1 = _baseService.RollObDice(AmorNumeric);
                    break;

                case 2:
                    Roll2 = _baseService.RollObDice(AmorNumeric);
                    break;

                case 3:
                    Roll3 = _baseService.RollObDice(AmorNumeric);
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
                typeof(EndOfYearPopulationUI),
                new PropertyMetadata(-1)
            );

        public string Amor
        {
            get => (string)GetValue(AmorProperty);
            set => SetValue(AmorProperty, value);
        }

        public static readonly DependencyProperty AmorProperty =
            DependencyProperty.Register(
                "Amor",
                typeof(string),
                typeof(EndOfYearPopulationUI),
                new PropertyMetadata("0", RaiseAmorChanged)
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
                typeof(EndOfYearPopulationUI),
                new PropertyMetadata(0)
            );

        public int TotalPopulation
        {
            get => (int)GetValue(TotalPopulationProperty);
            set => SetValue(TotalPopulationProperty, value);
        }

        public static readonly DependencyProperty TotalPopulationProperty =
            DependencyProperty.Register(
                "TotalPopulation",
                typeof(int),
                typeof(EndOfYearPopulationUI),
                new PropertyMetadata(0)
            );

        public int ModificationPopulation
        {
            get => (int)GetValue(ModificationPopulationProperty);
            set => SetValue(ModificationPopulationProperty, value);
        }

        public static readonly DependencyProperty ModificationPopulationProperty =
            DependencyProperty.Register(
                "ModificationPopulation",
                typeof(int),
                typeof(EndOfYearPopulationUI),
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

        private string _outcome;
        public string Outcome
        {
            get => _outcome;
            set
            {
                _outcome = value;
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

        private int _amorNumeric;
        public int AmorNumeric
        {
            get => _amorNumeric;
            set
            {
                _amorNumeric = value;
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
                    "Population",
                    Id,
                    ok,
                    Result,
                    AmorNumeric
                );

            RaiseEvent(newEventArgs);
        }

        private void CalculateResult()
        {
            if (Roll1 > 0 && Roll2 > 0 && Roll3 > 0)
            {
                int control = 0;
                int population;
                int change;

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
                    change = _baseService.RollDie(1, 10);
                    population = -(TotalPopulation / 100 * change);
                    population += ModificationPopulation;

                    if (population > 0)
                    {
                        Outcome = $"Din befolkning ökar med {population}!";
                    }
                    else if (population == 0)
                    {
                        Outcome = $"Ingen förändring i befolkning!";
                    }
                    else
                    {
                        Outcome = $"Din befolkning minskar med {population}!";
                    }
                    AmorNumeric += 2;
                }
                else if (control < 3)
                {
                    change = _baseService.RollDie(1, 6);
                    population = -(TotalPopulation / 100 * change);
                    population += ModificationPopulation;

                    if (population > 0)
                    {
                        Outcome = $"Din befolkning ökar med {population}.";
                    }
                    else if (population == 0)
                    {
                        Outcome = $"Ingen förändring i befolkning.";
                    }
                    else
                    {
                        Outcome = $"Din befolkning minskar med {population}.";
                    }
                    AmorNumeric += 1;
                }
                else if (control == 3)
                {
                    change = _baseService.RollDie(3, 9);
                    population = TotalPopulation / 100 * change;
                    population += ModificationPopulation;

                    if (population > 0)
                    {
                        Outcome = $"Din befolkning ökar med {population}.";
                    }
                    else if (population == 0)
                    {
                        Outcome = $"Ingen förändring i befolkning.";
                    }
                    else
                    {
                        Outcome = $"Din befolkning minskar med {population}.";
                    }
                }
                else
                {
                    change = _baseService.RollDie(3, 9);
                    change += _baseService.RollObDice(control - 3);
                    population = TotalPopulation / 100 * change;
                    population += ModificationPopulation;

                    if (population > 0)
                    {
                        Outcome = $"Din befolkning ökar med {population}!";
                    }
                    else if (population == 0)
                    {
                        Outcome = $"Ingen förändring i befolkning!";
                    }
                    else
                    {
                        Outcome = $"Din befolkning minskar med {population}!";
                    }
                    AmorNumeric -= 1;
                }
                Result = population.ToString();
                SendOk(true);
            }
            else
            {
                Result = "-";
                SendOk(false);
            }
        }

        private static void RaiseAmorChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is EndOfYearPopulationUI c)
                c.ConvertToNumeric();
        }

        private void ConvertToNumeric()
        {
            if (string.IsNullOrEmpty(Amor))
            {
                AmorNumeric = 0;
            }
            else
            {
                if (Amor.IndexOf('T') != -1 || Amor.Length < 3)
                {
                    bool isNegative;
                    string temp;

                    if (Amor.IndexOf('-') == -1)
                    {
                        temp = (string)GetValue(AmorProperty);
                        isNegative = false;
                    }
                    else
                    {
                        temp = Amor;
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
                        AmorNumeric = -value;
                    }
                    else
                    {
                        AmorNumeric = value;
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
                typeof(EndOfYearPopulationUI)
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
