using CommonServiceLocator;
using FiefApp.Common.Infrastructure.Services;
using Prism.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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
                "Subsidiary",
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

        public int MerchantId
        {
            get => (int)GetValue(MerchantIdProperty);
            set => SetValue(MerchantIdProperty, value);
        }

        public static readonly DependencyProperty MerchantIdProperty =
            DependencyProperty.Register(
                "MerchantId",
                typeof(int),
                typeof(EndOfYearTradeUI),
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

        #region Methods

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

                    resultFactor = i + j + k / 100;
                }
                else if (control == 3)
                {
                    resultFactor = 1M;
                }
                else
                {
                    resultFactor = (control - 3) * 0.25M;
                }
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
            }

            if (calculateFinalResult)
            {
                int nrResourcesBack = 0;

                if (SilverBack)
                {
                    nrResourcesBack++;
                }

                if (BaseBack)
                {
                    nrResourcesBack++;
                }

                if (LuxuryBack)
                {
                    nrResourcesBack++;
                }

                if (StoneBack)
                {
                    nrResourcesBack++;
                }

                if (WoodBack)
                {
                    nrResourcesBack++;
                }

                if (IronBack)
                {
                    nrResourcesBack++;
                }

                bool silverBackDone = false;
                bool baseBackDone = false;
                bool luxuryBackDone = false;
                bool stoneBackDone = false;
                bool woodBackDone = false;
                bool ironBackDone = false;
                int factor;
                decimal total = 100;
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

                for (int x = 0; x < nrResourcesBack; x++)
                {
                    bool skipRest = false;

                    if (!baseBackDone)
                    {
                        if (BaseBack)
                        {
                            factor = _baseService.RollDie(1, 101);
                            resultBase = Convert.ToInt32(Math.Floor(factor / (decimal)100 * silver / BASE_COST));
                            total -= (decimal)resultBase * BASE_COST / silver;
                            silver -= resultBase * BASE_COST;
                            skipRest = true;
                        }
                        baseBackDone = true;
                    }
                    else if (!luxuryBackDone && !skipRest)
                    {
                        if (LuxuryBack)
                        {
                            factor = _baseService.RollDie(1, Convert.ToInt32(Math.Floor(total)) + 1);
                            resultLuxury = Convert.ToInt32(Math.Floor(factor / (decimal)100 * silver / LUXURY_COST));
                            total -= (decimal)resultLuxury * LUXURY_COST / silver;
                            silver -= resultLuxury * LUXURY_COST;
                            skipRest = true;
                        }
                        luxuryBackDone = true;
                    }
                    else if (!stoneBackDone && !skipRest)
                    {
                        if (StoneBack)
                        {
                            factor = _baseService.RollDie(1, Convert.ToInt32(Math.Floor(total)) + 1);
                            resultStone = Convert.ToInt32(Math.Floor(factor / (decimal)100 * silver / STONE_COST));
                            total -= (decimal)resultStone * STONE_COST / silver;
                            silver -= resultStone * STONE_COST;
                            skipRest = true;
                        }
                        stoneBackDone = true;
                    }
                    else if (!woodBackDone && !skipRest)
                    {
                        if (WoodBack)
                        {
                            factor = _baseService.RollDie(1, Convert.ToInt32(Math.Floor(total)) + 1);
                            resultWood = Convert.ToInt32(Math.Floor(factor / (decimal)100 * silver / WOOD_COST));
                            total -= (decimal)resultWood * WOOD_COST / silver;
                            silver -= resultWood * WOOD_COST;
                            skipRest = true;
                        }
                        woodBackDone = true;
                    }
                    else if (!ironBackDone && !skipRest)
                    {
                        if (IronBack)
                        {
                            factor = _baseService.RollDie(1, Convert.ToInt32(Math.Floor(total)) + 1);
                            resultIron = Convert.ToInt32(Math.Floor(factor / (decimal)100 * silver / IRON_COST));
                            total -= (decimal)resultIron * IRON_COST / silver;
                            silver -= resultIron * IRON_COST;
                            skipRest = true;
                        }
                        ironBackDone = true;
                    }
                    else if (!silverBackDone && !skipRest)
                    {
                        resultSilver = silver;
                        skipRest = true;
                        silverBackDone = true;
                    }
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

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
