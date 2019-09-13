using CommonServiceLocator;
using FiefApp.Common.Infrastructure.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FiefApp.Module.EndOfYear.RoutedEvents;

namespace FiefApp.Module.EndOfYear.UIElements.EndOfYearTradeUI
{
    /// <summary>
    /// Interaction logic for EndOfYearTradeUI.xaml
    /// </summary>
    public partial class EndOfYearTradeUI : INotifyPropertyChanged
    {
        private const int BASE_COST = 360;
        private const int LUXURY_COST = 720;
        private const int STONE_COST = 18;
        private const int WOOD_COST = 7;
        private const int IRON_COST = 22;

        private readonly IBaseService _baseService;

        public EndOfYearTradeUI()
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
                typeof(EndOfYearTradeUI),
                new PropertyMetadata(-1)
            );

        public string Type
        {
            get => (string)GetValue(SubsidiaryProperty);
            set => SetValue(SubsidiaryProperty, value);
        }

        public static readonly DependencyProperty SubsidiaryProperty =
            DependencyProperty.Register(
                "Type",
                typeof(string),
                typeof(EndOfYearTradeUI),
                new PropertyMetadata("")
            );

        public string Merchant
        {
            get => (string)GetValue(MerchantProperty);
            set => SetValue(MerchantProperty, value);
        }

        public static readonly DependencyProperty MerchantProperty =
            DependencyProperty.Register(
                "Merchant",
                typeof(string),
                typeof(EndOfYearTradeUI),
                new PropertyMetadata("")
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
                typeof(EndOfYearTradeUI),
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
                typeof(EndOfYearTradeUI),
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
                typeof(EndOfYearTradeUI),
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
                typeof(EndOfYearTradeUI),
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
                typeof(EndOfYearTradeUI),
                new PropertyMetadata(0)
            );

        public int BaseIncomeStone
        {
            get => (int)GetValue(BaseIncomeStoneProperty);
            set => SetValue(BaseIncomeStoneProperty, value);
        }

        public static readonly DependencyProperty BaseIncomeStoneProperty =
            DependencyProperty.Register(
                "BaseIncomeStone",
                typeof(int),
                typeof(EndOfYearTradeUI),
                new PropertyMetadata(0)
            );

        public int BaseIncomeWood
        {
            get => (int)GetValue(BaseIncomeWoodProperty);
            set => SetValue(BaseIncomeWoodProperty, value);
        }

        public static readonly DependencyProperty BaseIncomeWoodProperty =
            DependencyProperty.Register(
                "BaseIncomeWood",
                typeof(int),
                typeof(EndOfYearTradeUI),
                new PropertyMetadata(0)
            );

        public int BaseIncomeIron
        {
            get => (int)GetValue(BaseIncomeIronProperty);
            set => SetValue(BaseIncomeIronProperty, value);
        }

        public static readonly DependencyProperty BaseIncomeIronProperty =
            DependencyProperty.Register(
                "BaseIncomeIron",
                typeof(int),
                typeof(EndOfYearTradeUI),
                new PropertyMetadata(0)
            );

        public bool SilverBack
        {
            get => (bool)GetValue(SilverBackProperty);
            set => SetValue(SilverBackProperty, value);
        }

        public static readonly DependencyProperty SilverBackProperty =
            DependencyProperty.Register(
                "SilverBack",
                typeof(bool),
                typeof(EndOfYearTradeUI),
                new PropertyMetadata(false)
            );

        public bool BaseBack
        {
            get => (bool)GetValue(BaseBackProperty);
            set => SetValue(BaseBackProperty, value);
        }

        public static readonly DependencyProperty BaseBackProperty =
            DependencyProperty.Register(
                "BaseBack",
                typeof(bool),
                typeof(EndOfYearTradeUI),
                new PropertyMetadata(false)
            );

        public bool LuxuryBack
        {
            get => (bool)GetValue(LuxuryBackProperty);
            set => SetValue(LuxuryBackProperty, value);
        }

        public static readonly DependencyProperty LuxuryBackProperty =
            DependencyProperty.Register(
                "LuxuryBack",
                typeof(bool),
                typeof(EndOfYearTradeUI),
                new PropertyMetadata(false)
            );

        public bool StoneBack
        {
            get => (bool)GetValue(StoneBackProperty);
            set => SetValue(StoneBackProperty, value);
        }

        public static readonly DependencyProperty StoneBackProperty =
            DependencyProperty.Register(
                "StoneBack",
                typeof(bool),
                typeof(EndOfYearTradeUI),
                new PropertyMetadata(false)
            );

        public bool WoodBack
        {
            get => (bool)GetValue(WoodBackProperty);
            set => SetValue(WoodBackProperty, value);
        }

        public static readonly DependencyProperty WoodBackProperty =
            DependencyProperty.Register(
                "WoodBack",
                typeof(bool),
                typeof(EndOfYearTradeUI),
                new PropertyMetadata(false)
            );

        public bool IronBack
        {
            get => (bool)GetValue(IronBackProperty);
            set => SetValue(IronBackProperty, value);
        }

        public static readonly DependencyProperty IronBackProperty =
            DependencyProperty.Register(
                "IronBack",
                typeof(bool),
                typeof(EndOfYearTradeUI),
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

        private string _tradeType;
        public string TradeType
        {
            get => _tradeType;
            set
            {
                _tradeType = value;
                NotifyPropertyChanged();
            }
        }

        private string _resultSilver;
        public string ResultSilver
        {
            get => _resultSilver;
            set
            {
                _resultSilver = value;
                NotifyPropertyChanged();
            }
        }

        private string _resultBase;
        public string ResultBase
        {
            get => _resultBase;
            set
            {
                _resultBase = value;
                NotifyPropertyChanged();
            }
        }

        private string _resultLuxury;
        public string ResultLuxury
        {
            get => _resultLuxury;
            set
            {
                _resultLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private string _resultStone;
        public string ResultStone
        {
            get => _resultStone;
            set
            {
                _resultStone = value;
                NotifyPropertyChanged();
            }
        }

        private string _resultWood;
        public string ResultWood
        {
            get => _resultWood;
            set
            {
                _resultWood = value;
                NotifyPropertyChanged();
            }
        }

        private string _resultIron;
        public string ResultIron
        {
            get => _resultIron;
            set
            {
                _resultIron = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        private void SendOk(bool ok)
        {
            EndOfYearEventArgs newEventArgs =
                new EndOfYearEventArgs(
                    EndOfYearOkRoutedEvent,
                    "Trade",
                    Id,
                    ok,
                    "",
                    0,
                    ResultSilver,
                    ResultBase,
                    ResultLuxury,
                    true,
                    ResultIron,
                    ResultStone,
                    ResultWood
                );

            RaiseEvent(newEventArgs);
        }

        #region Methods

        #region RoutedEvents

        public static readonly RoutedEvent EndOfYearOkRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "EndOfYearOkEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(EndOfYearTradeUI)
            );

        public event RoutedEventHandler EndOfYearOkEvent
        {
            add => AddHandler(EndOfYearOkRoutedEvent, value);
            remove => RemoveHandler(EndOfYearOkRoutedEvent, value);
        }

        #endregion

        private static void RaiseSkillChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is EndOfYearTradeUI c)
                c.ConvertToNumeric();
        }

        private void CalculateResult()
        {
            decimal resultFactor = 0M;
            bool calculateFinalResult = true;

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
                    resultFactor = 0M;
                }
                else if (control < 3)
                {
                    int i = Roll1 - Difficulty;
                    if (i < 0)
                    {
                        i *= 15;
                    }
                    else
                    {
                        i = 0;
                    }

                    int j = Roll2 - Difficulty;
                    if (j < 0)
                    {
                        j *= 15;
                    }
                    else
                    {
                        j = 0;
                    }

                    int k = Roll3 - Difficulty;
                    if (k < 0)
                    {
                        k *= 15;
                    }
                    else
                    {
                        k = 0;
                    }

                    resultFactor = 1 + (i + j + k) / 100M;
                }
                else if (control == 3)
                {
                    resultFactor = 1M;
                }
                else
                {
                    resultFactor = (control - 3) * 0.25M + 1;
                }
                Console.WriteLine($"control: {control}");
                Console.WriteLine($"resultFactor: {resultFactor}");
            }
            else
            {
                ResultSilver = "-";
                ResultBase = "-";
                ResultLuxury = "-";
                ResultStone = "-";
                ResultIron = "-";
                ResultWood = "-";
                calculateFinalResult = false;
                SendOk(false);
            }

            if (calculateFinalResult)
            {
                List<string> resourcesBack = new List<string>();
                if (SilverBack)
                {
                    resourcesBack.Add("silver");
                }

                if (BaseBack)
                {
                    resourcesBack.Add("base");
                }

                if (LuxuryBack)
                {
                    resourcesBack.Add("luxury");
                }

                if (StoneBack)
                {
                    resourcesBack.Add("stone");
                }

                if (WoodBack)
                {
                    resourcesBack.Add("wood");
                }

                if (IronBack)
                {
                    resourcesBack.Add("iron");
                }

                int resultSilver = 0;
                int resultBase = 0;
                int resultLuxury = 0;
                int resultStone = 0;
                int resultWood = 0;
                int resultIron = 0;

                int silver = Convert.ToInt32(
                    Math.Floor(
                        (BaseIncomeSilver
                         + BaseIncomeBase * BASE_COST
                         + BaseIncomeLuxury * LUXURY_COST
                         + BaseIncomeIron * IRON_COST
                         + BaseIncomeWood * WOOD_COST
                         + BaseIncomeStone * STONE_COST)
                        * resultFactor
                    ));

                while (silver > 0)
                {
                    switch (resourcesBack[_baseService.RollDie(0, resourcesBack.Count)])
                    {
                        case "luxury":
                            if (silver >= LUXURY_COST)
                            {
                                resultLuxury++;
                                silver -= LUXURY_COST;
                            }
                            break;

                        case "base":
                            if (silver >= BASE_COST)
                            {
                                resultBase++;
                                silver -= BASE_COST;
                            }
                            break;

                        case "iron":
                            if (silver >= IRON_COST)
                            {
                                resultIron++;
                                silver -= IRON_COST;
                            }
                            break;

                        case "stone":
                            if (silver >= STONE_COST)
                            {
                                resultStone++;
                                silver -= STONE_COST;
                            }
                            break;

                        case "wood":
                            if (silver >= WOOD_COST)
                            {
                                resultWood++;
                                silver -= WOOD_COST;
                            }
                            break;

                        case "silver":
                            int temp = (int)Math.Floor(silver / 25D);
                            resultSilver += temp;
                            silver -= temp;
                            break;
                    }
                    if (silver < LUXURY_COST)
                    {
                        resultSilver += silver;
                        silver = 0;
                    }
                }
                ResultSilver = resultSilver.ToString();
                ResultBase = resultBase.ToString();
                ResultLuxury = resultLuxury.ToString();
                ResultIron = resultIron.ToString();
                ResultStone = resultStone.ToString();
                ResultWood = resultWood.ToString();
                SendOk(true);
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
