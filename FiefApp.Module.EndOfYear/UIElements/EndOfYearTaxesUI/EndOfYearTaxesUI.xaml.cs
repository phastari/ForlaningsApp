using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using CommonServiceLocator;
using FiefApp.Common.Infrastructure.Services;
using Prism.Commands;

namespace FiefApp.Module.EndOfYear.UIElements.EndOfYearTaxesUI
{
    /// <summary>
    /// Interaction logic for EndOfYearTaxesUI.xaml
    /// </summary>
    public partial class EndOfYearTaxesUI : INotifyPropertyChanged
    {
        private readonly IBaseService _baseService;
        public EndOfYearTaxesUI()
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
                    Roll1 = _baseService.RollObDice(LoyaltyNumeric);
                    break;

                case 2:
                    Roll2 = _baseService.RollObDice(LoyaltyNumeric);
                    break;

                case 3:
                    Roll3 = _baseService.RollObDice(LoyaltyNumeric);
                    break;
            }
        }

        #endregion

        #region Dependency Properties

        public string Loyalty
        {
            get => (string)GetValue(LoyaltyProperty);
            set => SetValue(LoyaltyProperty, value);
        }

        public static readonly DependencyProperty LoyaltyProperty =
            DependencyProperty.Register(
                "Loyalty",
                typeof(string),
                typeof(EndOfYearTaxesUI),
                new PropertyMetadata("0", RaiseLoyaltyChanged)
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
                typeof(EndOfYearTaxesUI),
                new PropertyMetadata(0)
            );

        public int TaxFactor
        {
            get => (int)GetValue(TaxFactorProperty);
            set => SetValue(TaxFactorProperty, value);
        }

        public static readonly DependencyProperty TaxFactorProperty =
            DependencyProperty.Register(
                "TaxFactor",
                typeof(int),
                typeof(EndOfYearTaxesUI),
                new PropertyMetadata(100)
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

        private int _loyaltyNumeric;
        public int LoyaltyNumeric
        {
            get => _loyaltyNumeric;
            set
            {
                _loyaltyNumeric = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Methods

        private void CalculateResult()
        {
            if (Difficulty == 0)
            {
                Outcome = "Dina undersåtar betalar sina skatter.";
                LoyaltyNumeric += 1;
            }
            else
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
                        Outcome = "Dina undersåtar vägrar betala skatt!";
                        TaxFactor = 0;
                        LoyaltyNumeric -= 4;
                    }
                    else if (control < 3)
                    {
                        int i = Roll1 - Difficulty;
                        if (i < 0)
                        {
                            TaxFactor -= 16;
                        }

                        int j = Roll2 - Difficulty;
                        if (j < 0)
                        {
                            TaxFactor -= 16;
                        }

                        int k = Roll3 - Difficulty;
                        if (k < 0)
                        {
                            TaxFactor -= 16;
                        }
                        Outcome = "Dina undersåtar betalar knappt hälften!";
                        LoyaltyNumeric -= 2;
                    }
                    else if (control == 3)
                    {
                        TaxFactor = 100;
                        Outcome = "Skatten kommer in utan problem!";
                    }
                    else
                    {
                        TaxFactor = 100;
                        Outcome = "Dina undersåtar betalar glatt sina skatter!";
                        LoyaltyNumeric += control - 3;
                    }
                }
            }
        }

        private static void RaiseLoyaltyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is EndOfYearTaxesUI c)
                c.ConvertToNumeric();
        }

        private void ConvertToNumeric()
        {
            if (string.IsNullOrEmpty(Loyalty))
            {
                LoyaltyNumeric = 0;
            }
            else
            {
                if (Loyalty.IndexOf('T') != -1 || Loyalty.Length < 3)
                {
                    bool isNegative;
                    string temp;

                    if (Loyalty.IndexOf('-') == -1)
                    {
                        temp = (string)GetValue(LoyaltyProperty);
                        isNegative = false;
                    }
                    else
                    {
                        temp = Loyalty;
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
                        LoyaltyNumeric = -value;
                    }
                    else
                    {
                        LoyaltyNumeric = value;
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
